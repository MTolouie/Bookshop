using Bookshop_Api.Helper;
using BookShop_Common;
using Bookshop_DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IUserRepository _userRepository;
    private readonly ApiSettings _apiSettings;
    private readonly IEmailSender _emailSender;


    public AccountController(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<IdentityUser> signInManager,
        IEmailSender emailSender,
        IOptions<ApiSettings> options,
        IUserRepository userRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _apiSettings = options.Value;
        _emailSender = emailSender;
        _userRepository = userRepository;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] UserRequestDTO userRequestDTO)
    {


        if (userRequestDTO == null || !ModelState.IsValid)
        {
            return BadRequest();
        }



        var user = new ApplicationUser
        {
            UserName = userRequestDTO.Email,
            Email = userRequestDTO.Email,
            FullName = userRequestDTO.FullName,
            PhoneNumber = userRequestDTO.PhoneNo,
            IsActive = true
        };
        var result = await _userManager.CreateAsync(user, userRequestDTO.Password);

        if (result.Succeeded)
        {

            var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_Customer);
            if (!roleResult.Succeeded)
            {
                var errors = roleResult.Errors.Select(e => e.Description);
                return BadRequest(new RegisterationResponseDTO
                { Errors = errors, IsRegisterationSuccessful = false, Message = "Something Went Wrong" });
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userId, code = code }, Request.Scheme);

            await _emailSender.SendEmailAsync(userRequestDTO.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                return StatusCode(StatusCodes.Status201Created, new RegisterationResponseDTO() { Message = "You Need To Confirm Your Email", IsRegisterationSuccessful = true });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new RegisterationResponseDTO() { Message = "Something Went Wrong", IsRegisterationSuccessful = false });
            }

        }
        else
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new RegisterationResponseDTO
            { Errors = errors, IsRegisterationSuccessful = false, Message = "Something Went Wrong" });

        }

        return StatusCode(201);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, string code)
    {
        if (userId == null || code == null)
        {
            return BadRequest();
        }

        var user = await _userManager.FindByIdAsync(userId);


        if (user.EmailConfirmed)
        {
            return BadRequest(new RegisterationResponseDTO() { Message = "Email Already Confirmed", IsRegisterationSuccessful = false }); ;
        }

        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new RegisterationResponseDTO
            { Errors = errors, IsRegisterationSuccessful = false, Message = "Error confirming your email." });
        }

        return Redirect("https://localhost:44377/Account/ConfirmEmail");
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO authenticationDTO)
    {
        if (authenticationDTO is null)
        {
            return Unauthorized(new AuthenticationResponseDTO()
            {
                ErrorMessage = "bad Data",
                IsAuthSuccessful = false,
                Token = null,
                userDTO = null
            });
        }

        var result = await _signInManager.PasswordSignInAsync(authenticationDTO.Email,
            authenticationDTO.Password, false, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(authenticationDTO.Email);
            if (user == null)
            {
                return Unauthorized(new AuthenticationResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }

            //everything is valid and we need to login the user
            var signinCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);

            var tokenOptions = new JwtSecurityToken(
                issuer: _apiSettings.ValidIssuer,
                audience: _apiSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signinCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            var applicationUser = await _userRepository.GetUserById(user.Id);

            return Ok(new AuthenticationResponseDTO
            {
                IsAuthSuccessful = true,
                Token = token,
                userDTO = new UserDTO
                {
                    FullName = applicationUser.FullName,
                    Name = user.UserName,
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNo = user.PhoneNumber
                }
            });
        }
        else
        {
            return Unauthorized(new AuthenticationResponseDTO
            {
                IsAuthSuccessful = false,
                ErrorMessage = "Invalid Authentication"
            });
        }
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPassword)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {

                return BadRequest();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //= Url.Page(
            //     "/Account/ResetPassword",
            //     pageHandler: null,
            //     values: new { area = "Identity", code },
            //     protocol: Request.Scheme);

            var callbackUrl = Url.Action("ResetPassword", "Account", new { code = code }, Request.Scheme);

            await _emailSender.SendEmailAsync(
                forgotPassword.Email,
                "Reset Password",
                $"Please reset your password by <a href='https://localhost:44377/Account/ResetPassword?Code={code}'>clicking here</a>.");

            //return Redirect("https://localhost:44377/Account/ForgotPasswordConfirmation");  {HtmlEncoder.Default.Encode(callbackUrl)}
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(changePassword.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return BadRequest();
            }

            var result = await _userManager.ChangePasswordAsync(user,changePassword.OldPassword,changePassword.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            
        }

        return BadRequest();
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPassword)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = await _userManager.FindByEmailAsync(resetPassword.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return BadRequest();
        }
        var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPassword.Code));
        var result = await _userManager.ResetPasswordAsync(user, code, resetPassword.Password);
        if (result.Succeeded)
        {
            return Ok();
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return BadRequest();
    }


    private SigningCredentials GetSigningCredentials()
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim("Id",user.Id),

        };
        var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }


}
