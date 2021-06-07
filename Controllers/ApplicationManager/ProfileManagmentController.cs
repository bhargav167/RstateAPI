using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RstateAPI.Data;
using RstateAPI.Entities;
using RstateAPI.Entities.Dtos.Profile;
using RstateAPI.Entities.Profile;
using RstateAPI.Helpers;

namespace RstateAPI.Controllers.ApplicationManager {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProfileManagmentController : ControllerBase {
        private UserManager<ApplicationUser> _usermanager;
        private readonly StoreContext store;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public ProfileManagmentController (UserManager<ApplicationUser> userManager,
            StoreContext store, IOptions<CloudinarySettings> cloudinarysetting, IMapper mapper) {
            _usermanager = userManager;
            this.store = store;
            _mapper = mapper;
            _cloudinaryConfig = cloudinarysetting;
            Account acc = new Account (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary (acc);
        }

        [HttpGet ("{userId}")]
        public async Task<IActionResult> EditUser (string userId) {
            var user = await _usermanager.FindByIdAsync (userId);

            if (user == null) {
                return NoContent ();
            }
            // GetClaimsAsync retunrs the list of user Claims
            var userClaims = await _usermanager.GetClaimsAsync (user);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await _usermanager.GetRolesAsync (user);

            var model = new ProfileUpdate {
                Id = user.Id,
                Email = user.Email,
                UserName = user.FullName,
                MobileNo = user.PhoneNumber,
                Claims = userClaims.Select (c => c.Value).ToList (),
                Roles = userRoles,
                ConfirmEmail = user.EmailConfirmed,
                confirmMobile = user.PhoneNumberConfirmed,
                BrokerFirm = user.BrokerFirm,
                FirmAddress = user.FirmAddress,
                FirmAddress1 = user.FirmAddress1,
                GstNo = user.GstNo,
                reraNo = user.reraNo,
                landlineNo = user.landlineNo,
                mobNo1 = user.mobNo1,
                mobNo2 = user.mobNo2,
                mobNo3 = user.mobNo3,
                mobNo4 = user.mobNo4,
                locations = user.locations
            };
            return Ok (model);

        }

        [HttpPut]
        public async Task<IActionResult> EditUser (ProfileUpdate model) {
            var user = await _usermanager.FindByIdAsync (model.Id);

            if (user == null) {
                return NoContent ();
            } else {
                user.Email = model.Email;
                user.FullName = model.UserName;
                user.UserName = model.Email;
                user.BrokerFirm = model.BrokerFirm;
                user.FirmAddress = model.FirmAddress;
                user.FirmAddress1 = model.FirmAddress1;
                user.GstNo = model.GstNo;
                user.reraNo = model.reraNo;
                user.landlineNo = model.landlineNo;
                user.mobNo1 = model.mobNo1;
                user.mobNo2 = model.mobNo2;
                user.mobNo3 = model.mobNo3;
                user.mobNo4 = model.mobNo4;
                user.locations = model.locations;
                var result = await _usermanager.UpdateAsync (user);

                if (result.Succeeded) {
                    return Ok (200);
                }

                foreach (var error in result.Errors) {
                    ModelState.AddModelError ("", error.Description);
                }

                return Ok (model);
            }
        }

        [HttpPost ("UpdateEmail/{userId}/{email}")]
        public async Task<IActionResult> UpdateEmail (string userId, string email) {
            var user = await _usermanager.FindByIdAsync (userId);
            var useremail = await _usermanager.FindByEmailAsync (email);

            if (useremail != null)
                return NotFound ();

            if (user == null) {
                return NoContent ();
            } else {
                user.Email = email;
                user.UserName = email;
                user.EmailConfirmed = true;
                var result = await _usermanager.UpdateAsync (user);

                if (result.Succeeded) {
                    return Ok (200);
                }

                foreach (var error in result.Errors) {
                    ModelState.AddModelError ("", error.Description);
                }

                return Ok (email);
            }
        }

        [HttpPost ("upload/{userId}")]
        public async Task<IActionResult> UploadImage (string userId, [FromForm] UploadPhoto Uploadmodel) {
            var user = await _usermanager.FindByIdAsync (userId);

            var file = Uploadmodel.File;
            var uploadResult = new ImageUploadResult ();

            if (file.Length > 0) {
                using (var stream = file.OpenReadStream ()) {
                    var uploadParms = new ImageUploadParams () {
                    File = new FileDescription (file.Name, stream)
                    };
                    uploadResult = _cloudinary.Upload (uploadParms);
                    if (uploadResult.Format.ToLower () == "pdf" || uploadResult.Format.ToLower () == "docx")
                        return BadRequest ();
                }
            }
            user.imagUrl = uploadResult.Uri.ToString ();
            // var result = await _usermanager.UpdateAsync (user); 

            if (user.imagUrl != null) {
                return Ok (user.imagUrl);
            }

            return Ok (user.imagUrl);
        }

        [HttpPost ("UpdateUserImageUrl/{userId}")]
        public async Task<IActionResult> UPdateUserImage1 (string userId, [FromBody]IUrl url) {
           var user = await _usermanager.FindByIdAsync(userId);

            if (user == null)
                return NoContent ();

            user.imagUrl = url.url;
            await _usermanager.UpdateAsync(user);

            return Ok (url);
        }

        [HttpGet ("UserImage/{userId}")]
        public async Task<IActionResult> UserImg (string userId) {
            var user = await _usermanager.FindByIdAsync (userId);

            if (user == null)
                return NoContent ();

            return Ok (user);
        }

    }
}