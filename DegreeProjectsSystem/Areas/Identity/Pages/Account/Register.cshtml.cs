﻿using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using DegreeProjectsSystem.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DegreeProjectsSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitWork _iUnitWork;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IUnitWork iUnitWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _iUnitWork =  iUnitWork;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(15, ErrorMessage = "El {0} debe tener mínimo {2} caracteres y máximo {1} caracteres.", MinimumLength =5)]
            [Display(Name = "Usuario")]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Correo")]
            public string Email { get; set; }

            [Required]
            [StringLength(15, ErrorMessage = "The {0} debe tener un mínimo de {2} caracteres y máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Clave")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirme clave")]
            [Compare("Password", ErrorMessage = "La clave y su coonfirmación no coinciden.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Nombres")]
            public string Names { get; set; }
            [Required]
            [Display(Name = "Apellidos")]
            public string Surnames { get; set; }
            [Required]
            [Display(Name = "Dependencia")]
            public string Dependence { get; set; }
        
            public string Role { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                {
                    UserName = Input.UserName, 
                    Email = Input.Email,
                    Names = Input.Names,
                    Surnames = Input.Surnames,
                    Dependence = Input.Dependence,
                    Role = Input.Role
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuario creado, nueva cuenta con clave.");

                    if (!await _roleManager.RoleExistsAsync(DS.Role_Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(DS.Role_Admin));
                    }

                    if (!await _roleManager.RoleExistsAsync(DS.Role_Assistant))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(DS.Role_Assistant));
                    }

                    if (!await _roleManager.RoleExistsAsync(DS.Role_Consult))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(DS.Role_Consult));
                    }

                    await _userManager.AddToRoleAsync(user, DS.Role_Admin);

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
