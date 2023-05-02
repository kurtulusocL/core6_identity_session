using Identity_Session.Core.CrossCuttingConcern.Dtos.AuthDtos;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Identity_Session.Core.CrossCuttingConcern.Extensions;
using Identity_Session.Core.CrossCuttingConcern.Role.Microsoft;
using Identity_Session.Core.CrossCuttingConcern.ViewModels;

namespace Identity_Session.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginDto()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("userId", user.Id);
				
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(model.ReturnUrl))
                        return RedirectToAction("Index", "Admin", new { id = user.Id });
                    return Redirect(model.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Username or password not found");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameSurname,
                RespondTitle = "Admin",
                Birthdate = model.Birthdate,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                IsConfirmed = true,
                CreatedDate = DateTime.Now.ToLocalTime()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(user, "Admin"); // instead of this I used role assing model.
                TempData["success"] = "Registered, go to Login";
                return RedirectToAction(nameof(Register));
            }
            TempData["error"] = "Mistake";
            return RedirectToAction(nameof(Register));
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Admin logged out.");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return RedirectToAction(nameof(ForgotPasswordConfirmationError));
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);

                SmtpClient client = new SmtpClient("smtp.yandex.com.tr", 587);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("your mail address", "domain");
                mail.Priority = MailPriority.High;
                mail.Subject = "Forgot to My Password";
                mail.To.Add(new MailAddress(model.Email, ""));
                mail.Body = "Reset Password" + " " + "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>";
                mail.IsBodyHtml = true;

                NetworkCredential enter = new NetworkCredential("your mail address", "mail password");
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = enter;
                client.Send(mail);
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmationError()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(AdminController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        public IActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            ResetPasswordDto model = new ResetPasswordDto
            {
                Code = code
            };
            return View("ResetPassword", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model, string code)
        {
            ApplicationUser user = new ApplicationUser();
            user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmationError));
            }
            else
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                TempData["error"] = "Error";
                return RedirectToAction(nameof(ResetPassword));
            }
        }
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        public IActionResult ResetPasswordConfirmationError()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(Login));
            }

            var model = new ChangePasswordDto { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                TempData["error"] = "Mistake";
                return View(model);
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        public async Task<ActionResult> UpdateProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            UpdateProfileDto model = new UpdateProfileDto();
            model.PhoneNumber = user.PhoneNumber;
            model.Email = user.Email;
            model.Username = user.UserName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProfile(UpdateProfileDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.UpdatedDate = DateTime.Now.ToLocalTime();

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["success"] = "Profile Updated!";
                    return RedirectToAction(nameof(UpdateProfile));
                }
            }
            TempData["error"] = "There was an error!";
            return RedirectToAction(nameof(UpdateProfile));
        }

        public async Task<IActionResult> RoleAssign(string id) //set role by user
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            List<ApplicationRole> allRoles = _roleManager.Roles.ToList();
            List<string>? userRoles = await _userManager.GetRolesAsync(user) as List<string>;
            List<RoleAssignVM> assignRoles = new List<RoleAssignVM>();
            allRoles.ForEach(role => assignRoles.Add(new RoleAssignVM
            {
                HasAssign = userRoles.Contains(role.Name),
                RoleId = role.Id,
                RoleName = role.Name
            }));

            return View(assignRoles);
        }

        [HttpPost]
        public async Task<ActionResult> RoleAssign(List<RoleAssignVM> modelList, string id)//set role by user
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            foreach (RoleAssignVM role in modelList)
            {
                if (role.HasAssign)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                else
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
            }
            return RedirectToAction("Index", "User");
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(AdminController.Index), "Admin");
            }
        }
        #endregion
    }
}
