using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RstateAPI.Data;

namespace RstateAPI.Controllers.ApplicationManager {
    [ApiController]
    [Route ("api/[controller]")]
    public class ForgetPasswordController : ControllerBase {
        private UserManager<ApplicationUser> _usermanager;
        private readonly StoreContext store;
        public ForgetPasswordController (UserManager<ApplicationUser> userManager,
            StoreContext store) {
            _usermanager = userManager;
            this.store = store;
        }

        [HttpPut ("{phoneNo}/{password}")]
        public async Task<IActionResult> UpdatePassword (string phoneNo, string password) {
            var user = store.appuser.Where (c => c.PhoneNumber == phoneNo).FirstOrDefault ();

            if (user == null) {
                return NoContent ();
            } else {
                // generate a 128-bit salt using a secure PRNG
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create ()) {
                    rng.GetBytes (salt);
                }
                string hashed = Convert.ToBase64String (KeyDerivation.Pbkdf2 (
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                await _usermanager.ChangePasswordAsync (user, user.Password, password);
                user.Password = password;
                await _usermanager.UpdateAsync (user);
                return Ok (200);
            }
        }

    }
}