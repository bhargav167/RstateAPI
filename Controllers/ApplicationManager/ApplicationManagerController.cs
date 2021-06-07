using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RstateAPI;
using RstateAPI.Data;

[ApiController]
[Route ("api/[controller]")]
public class ApplicationManagerController : ControllerBase {
    private UserManager<ApplicationUser> _usermanager;
    private readonly ISpacingRepo _repo;
    private readonly StoreContext store;
    private readonly ApplicationSetting _appSetting;
    public ApplicationManagerController (UserManager<ApplicationUser> userManager,
        IOptions<ApplicationSetting> appSetting, StoreContext context, ISpacingRepo repo) {
        _usermanager = userManager;
        _repo = repo;
        store = context;
        _appSetting = appSetting.Value;
    }

    //Register Method api/ApplicationManager/register
    [HttpPost ("register/{roles}")]
    public async Task<IActionResult> Register (ApplicationUserModel appuser, string roles) {
        appuser.Role = roles;
        if (await _repo.AdminExist (appuser.PhoneNo, appuser.Email))
            return Ok ("Admin Already Exist with this name");

        if (appuser.Email == "") {
            var applicationUsers = new ApplicationUser {
            UserName = appuser.PhoneNo,
            FullName = appuser.FullName,
            Email = appuser.PhoneNo,
            userId = appuser.PhoneNo,
            PhoneNumber = appuser.PhoneNo,
            Password = appuser.password,
            RoleId = roles
            };
            try {
                var user = await _usermanager.CreateAsync (applicationUsers, appuser.password);
                await _usermanager.AddToRoleAsync (applicationUsers, appuser.Role);

                return Ok (user);
            } catch (Exception ex) {
                throw ex;
            }
        } else {
            var applicationUsers = new ApplicationUser {
                UserName = appuser.Email,
                FullName = appuser.FullName,
                Email = appuser.Email,
                userId = appuser.Email,
                PhoneNumber = appuser.PhoneNo,
                Password = appuser.password,
                RoleId = roles
            };
            try {
                var user = await _usermanager.CreateAsync (applicationUsers, appuser.password);
                await _usermanager.AddToRoleAsync (applicationUsers, appuser.Role);

                return Ok (user);
            } catch (Exception ex) {
                throw ex;
            }
        }
    }

    //Login Method api/ApplicationManager/login
    [HttpPost ("login")]
    public async Task<IActionResult> Login (LoginModel loginUser) {
        var user = await _usermanager.FindByNameAsync (loginUser.UserName);

        if (user != null && await _usermanager.CheckPasswordAsync (user, loginUser.password)) {
            //Get Role Assign
            var roles = await _usermanager.GetRolesAsync (user);
            // if (roles.FirstOrDefault () != selectedRole || roles.FirstOrDefault () != "SuperAdmin")
            //     return Unauthorized (new { message = "Role Invalid" });

            IdentityOptions _opt = new IdentityOptions ();

            var tokenDecriptor = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity (new Claim[] {
                new Claim ("UserId", user.Id.ToString ()),
                new Claim (_opt.ClaimsIdentity.RoleClaimType, roles.FirstOrDefault ())
                }),
                Expires = DateTime.UtcNow.AddMinutes (50),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("super secret key")), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandeler = new JwtSecurityTokenHandler ();
            var securityToken = tokenHandeler.CreateToken (tokenDecriptor);
            var token = tokenHandeler.WriteToken (securityToken);
            //Add latest token to database
            user.tokenid = token;
            user.Islogin = true;
            store.appuser.Update (user);
            store.SaveChanges ();
            return Ok (new { token, user.Id, user.UserName, user.imagUrl, user.userId, roles });

        } else {
            return BadRequest (new { message = "UserName & Password Incorrect." });
        }
    }
    //Match token
    [HttpPost ("tokenMatch/{token}")]
    public async Task<bool> Token (string token) {
        var user = await store.appuser.FirstOrDefaultAsync (c => c.tokenid == token);
        if (user == null)
            return false;

        return true;
    }

    [HttpPost ("loginPhone")]
    public async Task<IActionResult> LoginPhone (LoginModel loginUser) {
        var user = store.appuser.FirstOrDefault (c => c.PhoneNumber == loginUser.UserName);

        if (user != null && await _usermanager.CheckPasswordAsync (user, loginUser.password)) {
            //Get Role Assign
            var roles = await _usermanager.GetRolesAsync (user);
            // if (roles.FirstOrDefault () != selectedRole || roles.FirstOrDefault () != "SuperAdmin")
            //     return Unauthorized (new { message = "Role Invalid" });

            IdentityOptions _opt = new IdentityOptions ();

            var tokenDecriptor = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity (new Claim[] {
                new Claim ("UserId", user.Id.ToString ()),
                new Claim (_opt.ClaimsIdentity.RoleClaimType, roles.FirstOrDefault ())
                }),
                Expires = DateTime.UtcNow.AddMinutes (50),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("super secret key")), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandeler = new JwtSecurityTokenHandler ();
            var securityToken = tokenHandeler.CreateToken (tokenDecriptor);
            var token = tokenHandeler.WriteToken (securityToken);
            return Ok (new { token, user.Id, user.UserName, user.imagUrl, user.userId, roles });

        } else {
            return BadRequest (new { message = "UserName & Password Incorrect." });
        }
    }

    //Login Method api/ApplicationManager/Phonelogin
    [HttpPost ("Phonelogin/{phoneno}/{selectedRole}")]
    public async Task<IActionResult> LoginByPhone (string phoneno, string selectedRole) {
        var user = await _usermanager.FindByNameAsync (phoneno);

        if (user != null && await _usermanager.CheckPasswordAsync (user, "@@Testdemo123")) {
            //Get Role Assign
            var roles = await _usermanager.GetRolesAsync (user);
            if (roles.FirstOrDefault () != selectedRole)
                return Unauthorized (new { message = "Role Invalid" });

            IdentityOptions _opt = new IdentityOptions ();

            var tokenDecriptor = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity (new Claim[] {
                new Claim ("UserId", user.Id.ToString ()),
                new Claim (_opt.ClaimsIdentity.RoleClaimType, roles.FirstOrDefault ())
                }),
                Expires = DateTime.UtcNow.AddMinutes (50),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("super secret key")), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandeler = new JwtSecurityTokenHandler ();
            var securityToken = tokenHandeler.CreateToken (tokenDecriptor);
            var token = tokenHandeler.WriteToken (securityToken);
            return Ok (new { token, selectedRole, user.Id, user.UserName, user.imagUrl, user.userId });

        } else {
            return BadRequest (new { message = "UserName & Password Incorrect." });
        }
    }

    //Check Login
    //Login Method api/ApplicationManager/login
    [HttpPost ("Checklogin/{mail}")]
    public async Task<IActionResult> CheckMailExit (string mail) {
        var user = store.appuser.Where (v => v.PhoneNumber == mail).FirstOrDefault ();
        if (user != null) {
            //Get Role Assign
            var roles = await _usermanager.GetRolesAsync (user);
            // if (roles.FirstOrDefault () != role)
            //     return Unauthorized (new { message = "Role Invalid" });

            return Ok (200);
        } else {
            return Ok (404);
        }
    }
    //Check Login for Email
    //Login Method api/ApplicationManager/login
    [HttpPost ("Checkloginmail/{mail}")]
    public async Task<IActionResult> CheckEMailExit (string mail) {
        var user = store.appuser.FirstOrDefault (x => x.Email == mail);

        if (user != null) {
            //Get Role Assign
            var roles = await _usermanager.GetRolesAsync (user);
            // if (roles.FirstOrDefault () != role)
            //     return Unauthorized (new { message = "Role Invalid" });

            return Ok (200);
        } else {
            return BadRequest (new { message = "UserName & Password Incorrect." });
        }
    }

    //Admin Phone Login
    [HttpPost ("CheckAdminloginmail/{mail}")]
    public async Task<IActionResult> CheckAdminEMailExit (string mail) {
        var user = store.appuser.FirstOrDefault (x => x.Email == mail);

        if (user != null) {
            //Get Role Assign
            var roles = await _usermanager.GetRolesAsync (user);
            if (roles.FirstOrDefault () != "Admin" && roles.FirstOrDefault () != "SuperAdmin")
                return Unauthorized (new { message = "Role Invalid" });

            return Ok (200);

        } else {
            return BadRequest (new { message = "UserName & Password Incorrect." });
        }
    }

    [HttpPost ("CheckAdminlogin/{mail}")]
    public async Task<IActionResult> CheckAdminMailExit (string mail) {
        var user = store.appuser.FirstOrDefault (v => v.PhoneNumber == mail);

        if (user != null) {
            //Get Role Assign
            var roles = await _usermanager.GetRolesAsync (user);
            if (roles.FirstOrDefault () != "Admin" && roles.FirstOrDefault () != "SuperAdmin")
                return Unauthorized (new { message = "Role Invalid" });

            return Ok (200);

        } else {
            return BadRequest (new { message = "UserName & Password Incorrect." });
        }
    }

}