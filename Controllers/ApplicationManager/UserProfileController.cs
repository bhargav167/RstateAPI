using System;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RstateAPI;
using RstateAPI.Data;

[ApiController]
[Route ("api/[controller]")]

public class UserProfileController : ControllerBase {
    private UserManager<ApplicationUser> _usermanager;
    private readonly StoreContext store;
    public UserProfileController (UserManager<ApplicationUser> userManager, StoreContext store) {
        _usermanager = userManager;
        this.store = store;
    }
    // Otp Verification
    // [HttpPost ("SendOtp/{mobNo}")]
    // public IActionResult SendOTP (string mobNo) {
    //     int otpValue = new Random ().Next (100000, 999999);
    //     String message = HttpUtility.UrlEncode (otpValue.ToString());
    //     using (var wb = new WebClient ()) {
    //         byte[] response = wb.UploadValues ("https://api.textlocal.in/send/",
    //             new NameValueCollection () { { "apikey", "Afz0lreYvic-NMsJivxwZUmT42MJr2Zsxgbsm8jHqa" }, { "numbers", mobNo }, { "message", message }, { "sender", "TXTLCL" }
    //             });
    //         string result = System.Text.Encoding.UTF8.GetString (response);
    //     return Ok (otpValue);
    // }
    // }
    // Otp Verification
    [HttpPost ("SendOtp/{mobNo}")]
    public IActionResult SendOTP (string mobNo) {
         int otpValue = new Random ().Next (100000, 999999);
           String message = HttpUtility.UrlEncode (otpValue.ToString ());
            using (var wb = new WebClient ()) {
                byte[] response = wb.UploadValues ("http://2factor.in/API/V1/2d768e3a-6d13-11eb-a9bc-0200cd936042/SMS/"+mobNo+"/"+otpValue.ToString(),
                    new NameValueCollection () { { "apikey", "2d768e3a-6d13-11eb-a9bc-0200cd936042" }, { "numbers", mobNo }, { "message", "Spacing login otp" }, { "sender", "TXTLCL" }
                    });
                string result = System.Text.Encoding.UTF8.GetString (response);
                return Ok (otpValue);
            }  
       // return Ok (otpValue);
    }

    [HttpPost ("Verify/{otp}/{otpVerified}")]
    public IActionResult VerifyOTP (int otp, int otpVerified) {
        bool result = false;
        if (otp == otpVerified) {
            result = true;
        }
        return Ok (result);
    }

    [HttpPost ("OTPlogin/{mobNo}/{roles}")]
    public async Task<IActionResult> Login (string mobNo, string roles) {
        var user = store.appuser.FirstOrDefault (c => c.PhoneNumber == mobNo);
        if (user == null) {
            var applicationUsers = new ApplicationUser {
            UserName = mobNo,
            FullName = mobNo,
            Email = mobNo,
            userId = mobNo,
            PhoneNumber = mobNo,
            Password = "@@3e45fd!",
            RoleId = roles,
            PhoneNumberConfirmed = true
            };
            try {
                await _usermanager.CreateAsync (applicationUsers, applicationUsers.Password);
                await _usermanager.AddToRoleAsync (applicationUsers, applicationUsers.RoleId);

                  var users = store.appuser.FirstOrDefault (c => c.PhoneNumber == mobNo);
                 IdentityOptions _opt = new IdentityOptions ();

            var tokenDecriptor = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity (new Claim[] {
                new Claim ("UserId", users.userId.ToString ()),
                new Claim (_opt.ClaimsIdentity.RoleClaimType, users.RoleId)
                }),
                Expires = DateTime.UtcNow.AddMinutes (50),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("super secret key")), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandeler = new JwtSecurityTokenHandler ();
            var securityToken = tokenHandeler.CreateToken (tokenDecriptor);
            var token = tokenHandeler.WriteToken (securityToken);
            return Ok (new { token, users.Id, users.UserName, users.imagUrl, users.userId });

            } catch (Exception ex) {
                throw ex;
            }
        }

        if (user != null) {
            // user.PhoneNumberConfirmed = true;
            // await _usermanager.UpdateAsync (user);

            // //Get Role Assign
             var roless = await _usermanager.GetRolesAsync (user);

            // // if (roles.FirstOrDefault () != selectedRole)
            // //     return Unauthorized (new { message = "Role Invalid" });

            IdentityOptions _opt = new IdentityOptions ();

            var tokenDecriptor = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity (new Claim[] {
                new Claim ("UserId", user.Id.ToString ()),
                new Claim (_opt.ClaimsIdentity.RoleClaimType, roless.FirstOrDefault ())
                }),
                Expires = DateTime.UtcNow.AddMinutes (50),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("super secret key")), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandeler = new JwtSecurityTokenHandler ();
            var securityToken = tokenHandeler.CreateToken (tokenDecriptor);
            var token = tokenHandeler.WriteToken (securityToken);
            return Ok (new { token, user.Id, user.UserName, user.imagUrl, user.userId });

        } else {
            return BadRequest (new { message = "UserName & Otp Incorrect." });
        }
    }

    [HttpGet]
    public async Task<object> UserProfiles () {
        string userId = User.Claims.First (x => x.Type == "UserId").Value;

        var user = await _usermanager.FindByIdAsync (userId);
        return new {
            user.FullName,
                user.Email,
                user.UserName,
                user.RoleId
        };
    }

    [HttpGet ("OwnerDetail/{userId}")]
    public ActionResult OwnerDetail (string userId) {
        var owner = store.appuser.Where (c => c.userId == userId).FirstOrDefault ();
        return Ok (owner);
    }

    [HttpPost ("ConfirmPhome/{phoneNo}")]
    public async Task<IActionResult> ConfirmPhoneNo (string phoneNo) {
        var user = store.appuser.FirstOrDefault (c => c.PhoneNumber == phoneNo);

        if (user == null) {
            return NoContent ();
        } else {
            user.PhoneNumberConfirmed = true;
            var result = await _usermanager.UpdateAsync (user);
            return Ok (phoneNo);
        }
    }

    [HttpPost ("ConfirmPhomeFromEdit/{phoneNo}/{phonetoUpdate}")]
    public async Task<IActionResult> ConfirmPhoneNoFromEdit (string phoneNo, string phonetoUpdate) {
        var user = store.appuser.FirstOrDefault (c => c.PhoneNumber == phoneNo);
        var isPhoneexist = store.appuser.FirstOrDefault (c => c.PhoneNumber == phonetoUpdate);
        if (isPhoneexist != null)
            return NotFound ();

        if (user == null) {
            return NoContent ();
        } else {
            user.PhoneNumberConfirmed = true;
            user.PhoneNumber = phonetoUpdate;
            var result = await _usermanager.UpdateAsync (user);
            return Ok (phonetoUpdate);
        }
    }

}