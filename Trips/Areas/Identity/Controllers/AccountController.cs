using Mapster;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Trips.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {

        private readonly IEmailSender emailSender;
        private readonly IUnitOfWork unitOfWork;

        public AccountController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? Url.Action("Index", "Home", new { area = "Customer" });
            return View(new RegisterRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest registerRequest, string? returnUrl = "/")
        {
            if (!ModelState.IsValid)
            {
                return View(registerRequest);
            }

            var user = registerRequest.Adapt<ApplicationUser>();

            var result = await unitOfWork.UserManager.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                user.RegistrationDate = DateTime.UtcNow;
                await unitOfWork.CommitAsync();
                await unitOfWork.UserManager.AddToRoleAsync(user, SD.Customer);

                // Send Confirmation Email
                var token = await unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var link = Url.Action("ConfirmEmail", "Account",
                    new { userId = user.Id, token = encodedToken, area = "Identity" },
                    Request.Scheme);

                await emailSender.SendEmailAsync(user!.Email ?? "", "Confirm Your Account's Email", @$"
            <div style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 30px; text-align: center;"">
                <div style=""max-width: 500px; margin: auto; background-color: #ffffff; padding: 40px; border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.1);"">
                    <h1 style=""color: #333333; margin-bottom: 20px;"">Confirm Your Email</h1>
                    <p style=""font-size: 16px; color: #666666; margin-bottom: 30px;"">
                        Please confirm your account by clicking the button below.
                    </p>
                    <a href=""{link}"" style=""display: inline-block; padding: 14px 28px; background-color: #4CAF50; color: #ffffff; text-decoration: none; border-radius: 6px; font-weight: bold; transition: opacity 0.3s ease;"" 
                        onmouseover=""this.style.opacity='0.8';"" onmouseout=""this.style.opacity='1';"">
                        Confirm Email
                    </a>
                    <p style=""font-size: 12px; color: #aaaaaa; margin-top: 40px;"">
                        If you did not request this email, you can safely ignore it.
                    </p>
                </div>
            </div>
        ");

                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(registerRequest);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? Url.Action("Index", "Home", new { area = "Customer" });
            return View(new LoginRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest loginRequest, string? returnUrl = "/")
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            var user = await unitOfWork.UserManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(loginRequest);
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Please confirm your email before logging in.");
                return View(loginRequest);
            }

            // Check if user is locked out
            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                ModelState.AddModelError("", $"You have been blocked until {user.LockoutEnd}");
                return View(loginRequest);
            }

            var result = await unitOfWork.SignInManager.PasswordSignInAsync(user, loginRequest.Password, loginRequest.RememberMe, true);

            if (result.Succeeded)
            {
                user.LastLogin = DateTime.UtcNow;
                await unitOfWork.CommitAsync();

                // Send back JSON for JS redirect
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

            // If failed login → return modal with error
            TempData["LoginError"] = result.IsLockedOut
                                        ? "Too many attempts. Try again later."
                                        : "Invalid email or password.";

            return View(loginRequest);
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(userId);
            if (user is null) return NotFound();

            if (string.IsNullOrWhiteSpace(token)) return BadRequest("Invalid token");

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var result = await unitOfWork.UserManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
            {
                TempData["Success"] = "Your email has been confirmed!";
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            else
            {
                TempData["Error"] = "Email confirmation failed.";
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
        }

        [HttpGet]
        public IActionResult ExternalLogin(string provider, string returnUrl = "/")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = unitOfWork.SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/", string remoteError = null!)
        {
            if (!string.IsNullOrEmpty(remoteError))
            {
                TempData["Error"] = $"Error from external provider: {remoteError}";
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

            var info = await unitOfWork.SignInManager.GetExternalLoginInfoAsync();
            if (info == null) return RedirectToAction("Index", "Home", new { area = "Customer" });

            var signInResult = await unitOfWork.SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl ?? "/");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            if (email == null)
            {
                TempData["Error"] = "Couldn't find user email.";
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

            var user = await unitOfWork.UserManager.FindByEmailAsync(email);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name) ?? email!;

            if (user == null)
            {
                user = new ApplicationUser
                {
                    FirstName = name,
                    LastName = string.Empty,
                    UserName = name.Replace(" ", "") + Guid.NewGuid().ToString("N"),
                    Email = email,
                    EmailConfirmed = true
                };
                var createUserResult = await unitOfWork.UserManager.CreateAsync(user);
                if (!createUserResult.Succeeded)
                {
                    TempData["Error"] = "Error creating user.";
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }

                var addLoginResult = await unitOfWork.UserManager.AddLoginAsync(user, info);
                if (!addLoginResult.Succeeded)
                {
                    TempData["Error"] = "Error linking external login.";
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }
            }
            else
            {
                // Link external login if not already linked
                var userLogins = await unitOfWork.UserManager.GetLoginsAsync(user);
                if (!userLogins.Any(l => l.LoginProvider == info.LoginProvider && l.ProviderKey == info.ProviderKey))
                {
                    var addLoginResult = await unitOfWork.UserManager.AddLoginAsync(user, info);
                    if (!addLoginResult.Succeeded)
                    {
                        TempData["Error"] = "Error linking external login.";
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                }
            }

            await unitOfWork.SignInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await unitOfWork.SignInManager.SignOutAsync();
            TempData["Success"] = "Logged out successfully.";
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        [HttpGet]
        public IActionResult ResendEmailConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendEmailConfirmation(ResendEmailConfirmationRequest resendEmailConfirmationRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(resendEmailConfirmationRequest);
            }

            var user = await unitOfWork.UserManager.FindByEmailAsync(resendEmailConfirmationRequest.Email);

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Email");
                return View(resendEmailConfirmationRequest);
            }

            if (user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Email is already confirmed.");
                return View(resendEmailConfirmationRequest);
            }
            var token = await unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token, area = "Identity" }, Request.Scheme);

            await emailSender.SendEmailAsync(user.Email ?? "", "Confirm Your Account's Email", @$"
                            <div style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 30px; text-align: center;"">
                                <div style=""max-width: 500px; margin: auto; background-color: #ffffff; padding: 40px; border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.1);"">
                                    <h1 style=""color: #333333; margin-bottom: 20px;"">Confirm Your Email</h1>
                                    <p style=""font-size: 16px; color: #666666; margin-bottom: 30px;"">
                                        Please confirm your account by clicking the button below.
                                    </p>
                                    <a href=""{link}"" style=""display: inline-block; padding: 14px 28px; background-color: #4CAF50; color: #ffffff; text-decoration: none; border-radius: 6px; font-weight: bold; transition: opacity 0.3s ease;"" 
                                        onmouseover=""this.style.opacity='0.8';"" onmouseout=""this.style.opacity='1';"">
                                        Confirm Email
                                    </a>
                                    <p style=""font-size: 12px; color: #aaaaaa; margin-top: 40px;"">
                                        If you did not request this email, you can safely ignore it.
                                    </p>
                                </div>
                            </div>
                      ");

            
            TempData["Success"] = "Confirmation email sent successfully. Please check your inbox.";
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest forgetPasswordRequest)
        {
            if (!ModelState.IsValid) return View(forgetPasswordRequest);

            var user = await unitOfWork.UserManager.FindByEmailAsync(forgetPasswordRequest.Email);

            if (user is null)
            {
                ModelState.AddModelError("", "Invalid email.");
                return View(forgetPasswordRequest);
            }

            var otpNumber = RandomNumberGenerator.GetInt32(0, 999999).ToString("D6");

            var totalNumberOfOTPs = await unitOfWork.ApplicationUserOTPRepository.GetAsync(e => e.ApplicationUserId == user.Id && DateTime.UtcNow.Day == e.SendDate.Day);

            if (totalNumberOfOTPs.Count() > 5)
            {
                ModelState.AddModelError("", "Too many OTP requests today.");
                return View(forgetPasswordRequest);
            }

            await unitOfWork.ApplicationUserOTPRepository.CreateAsync(new()
            {
                ApplicationUserId = user.Id,
                Code = otpNumber,
                Reason = "ForgetPassword",
                SendDate = DateTime.UtcNow,
                Status = false,
                ValidTo = DateTime.UtcNow.AddMinutes(30)
            });

            await emailSender.SendEmailAsync(user!.Email ?? "", "Reset Password OTP", @$"
                        <div style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 30px; text-align: center;"">
                            <div style=""max-width: 500px; margin: auto; background-color: #ffffff; padding: 40px; border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.1);"">
                                <h1 style=""color: #333333; margin-bottom: 20px;"">Reset Your Password</h1>
                                <p style=""font-size: 16px; color: #666666; margin-bottom: 30px;"">
                                    Use the OTP below to reset your password:
                                </p>
                                <div style=""display: inline-block; font-size: 32px; letter-spacing: 8px; background-color: #f9f9f9; color: #333333; padding: 12px 24px; border-radius: 8px; font-weight: bold; margin-bottom: 30px; box-shadow: inset 0 1px 3px rgba(0,0,0,0.1);"">
                                    {otpNumber}
                                </div>
                                <p style=""font-size: 14px; color: #aaaaaa; margin-top: 30px;"">
                                    This OTP is valid for a limited time. If you did not request this, please ignore this email.
                                </p>
                            </div>
                        </div>
                        ");

            TempData["Success"] = "OTP sent to your email.";
            return RedirectToAction(nameof(ResetPassword), new { userId = user.Id });
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId)
        {
            var model = new ResetPasswordRequest { UserId = userId };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            if (!ModelState.IsValid) return View(resetPasswordRequest);

            var user = await unitOfWork.UserManager.FindByIdAsync(resetPasswordRequest.UserId);

            if (user is null)
                return NotFound();

            var lastOTP = (await unitOfWork.ApplicationUserOTPRepository
                            .GetAsync(e => e.ApplicationUserId == resetPasswordRequest.UserId))
                            .OrderByDescending(e => e.Id)
                            .FirstOrDefault();

            if (lastOTP is not null)
            {
                if (lastOTP.Code == resetPasswordRequest.OTP && (lastOTP.ValidTo - DateTime.UtcNow).TotalMinutes > 0 && !lastOTP.Status)
                {
                    var token = await unitOfWork.UserManager.GeneratePasswordResetTokenAsync(user);
                    var result = await unitOfWork.UserManager.ResetPasswordAsync(user, token, resetPasswordRequest.Password);

                    if (result.Succeeded)
                    {
                        lastOTP.Status = true;
                        await unitOfWork.ApplicationUserOTPRepository.CommitAsync();
                        TempData["Success"] = "Password reset successfully.";
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }

                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            else { ModelState.AddModelError("", "Invalid or expired OTP."); }

            return View(resetPasswordRequest);
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
    }
}
