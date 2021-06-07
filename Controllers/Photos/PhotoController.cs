using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RstateAPI.Data;
using RstateAPI.Entities;
using RstateAPI.Entities.Dtos;
using RstateAPI.Helpers;

namespace RstateAPI.Controllers.Photos {
    [ApiController]
    [Route ("api/[controller]")]
    public class PhotoController : ControllerBase {
        private readonly StoreContext store;
        private readonly ISpacingRepo _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotoController (StoreContext store, ISpacingRepo repo, IMapper mapper, IOptions<CloudinarySettings> cloudinarysetting) {
            this.store = store;
            _repo = repo;
            _mapper = mapper;
            _cloudinaryConfig = cloudinarysetting;
            Account acc = new Account (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary (acc);
        }

        [HttpPost ("{Username}/{uniqueID}")]
        public async Task<IActionResult> AddPhotoForUser (string Username, int uniqueID, [FromForm] PhotoDtos photoDto) {

            var file = photoDto.File;
            var uploadResult = new ImageUploadResult ();

            if (file.Length > 0) {
                using (var stream = file.OpenReadStream ()) {
                    var uploadParms = new ImageUploadParams () {
                    File = new FileDescription (file.Name, stream),
                    Transformation = new Transformation ()
                    };
                    uploadResult = _cloudinary.Upload (uploadParms);
                    if (uploadResult.Format.ToLower () == "pdf" || uploadResult.Format.ToLower () == "docx")
                        return BadRequest ();
                }
            }
            photoDto.url = uploadResult.Uri.ToString ();
            photoDto.PublicId = uploadResult.PublicId;
            photoDto.UserId = Username;
            photoDto.uniqueID = uniqueID;

            var photo = _mapper.Map<Images1> (photoDto);

            store.images.Add (photo);

            if (await _repo.SaveAll ()) {

                return CreatedAtRoute ("GetPhoto", new { id = photo.Id }, photo);
            }
            return BadRequest ("Not Uploaded");
        }

        [HttpPost ("DummyImg/{Username}/{uniqueID}")]
        public async Task<IActionResult> AddPhotoForUserPhoto (string Username, int uniqueID) {
            PhotoDtos pp = new PhotoDtos ();
            pp.cover = true;
            pp.url = "null";
            pp.PublicId = Username;
            pp.UserId = Username;
            pp.uniqueID = uniqueID;

            var photo = _mapper.Map<Images1> (pp);
            store.images.Add (photo);

            if (await _repo.SaveAll ()) {

                return CreatedAtRoute ("GetPhoto", new { id = photo.Id }, photo);
            }
            return BadRequest ("Not Uploaded");
        }

        [HttpPost ("DummyImgByAdmin")]
        public async Task<IActionResult> AddPhotoFromAdmin ([FromForm] PhotoDtos photoDto) {
            var file = photoDto.File;
            var uploadResult = new ImageUploadResult ();
            if (file.Length > 0) {
                using (var stream = file.OpenReadStream ()) {
                    var uploadParms = new ImageUploadParams () {
                    File = new FileDescription (file.Name, stream),
                    Transformation = new Transformation ()
                    };
                    uploadResult = _cloudinary.Upload (uploadParms);
                    if (uploadResult.Format.ToLower () == "pdf" || uploadResult.Format.ToLower () == "docx")
                        return BadRequest ();
                }
            }
            photoDto.url = uploadResult.Uri.ToString ();
            photoDto.PublicId = uploadResult.PublicId;
            photoDto.UserId = "Admin";
            photoDto.uniqueID = 0;
            photoDto.cover=true;
            var photo = _mapper.Map<Images1> (photoDto);
            store.images.Add (photo);
            if (await _repo.SaveAll ()) {

                return CreatedAtRoute ("GetPhoto", new { id = photo.Id }, photo);
            }
            return BadRequest ("Not Uploaded");
        }

        [HttpGet ("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto (int id) {
            var photosFromRepo = await _repo.getPhoto (id);
            return Ok (photosFromRepo);
        }

        [HttpPost ("{uniqueID}/photos/{Id}/setMain")]
        public async Task<IActionResult> SetCoverPhoto (int uniqueID, int Id) {

            var photoFromRepo = await _repo.getPhoto (Id);
            if (photoFromRepo == null)
                return NotFound ();

            if (photoFromRepo.cover) {
                photoFromRepo.cover = false;
                return BadRequest ("This is already a cover photo");
            }

            var currentMainPhoto = await _repo.GetMainPhotoForUser (uniqueID);
            if (currentMainPhoto != null)
                currentMainPhoto.cover = false;

            photoFromRepo.cover = true;

            if (await _repo.SaveAll ())
                return NoContent ();

            return BadRequest ("Could not set photo to Cover");
        }

        [HttpPost ("{userId}/photos/{Id}/setTag/{tag}")]
        public async Task<IActionResult> SetTagPhoto (string userId, int Id, string tag) {

            var photoFromRepo = await _repo.getPhoto (Id);
            if (photoFromRepo == null)
                return NotFound ();

            photoFromRepo.tag = tag;

            if (await _repo.SaveAll ())
                return NoContent ();

            return BadRequest ("Could not set tag to image");
        }

        [HttpDelete ("{userId}/photos/{Id}")]
        public async Task<IActionResult> DeletePhoto (string userId, int id) {

            var photoFromRepo = await _repo.getPhoto (id);
            if (photoFromRepo == null)
                return NotFound ();

            // if (photoFromRepo.cover)
            //     return BadRequest ("You cannot delete the cover photo");

            if (photoFromRepo.PublicId != null) {
                var deleteParams = new DeletionParams (photoFromRepo.PublicId);

                var result = _cloudinary.Destroy (deleteParams);

                if (result.Result == "ok")
                    _repo.Delete (photoFromRepo);
            }

            if (photoFromRepo.PublicId == null) {
                _repo.Delete (photoFromRepo);
            }

            if (await _repo.SaveAll ())
                return Ok ();

            return BadRequest ("Failed to delete the photo");
        }

        [HttpDelete ("DeleteAdminImg")]
        public async Task<IActionResult> DeleteAdminPropertyPhoto () {
            var photoFromRepo = store.images.Where (c => c.UserId == "Admin").FirstOrDefault ();
            if (photoFromRepo == null)
                return NotFound ();

            if (photoFromRepo.PublicId != null) {
                var deleteParams = new DeletionParams (photoFromRepo.PublicId);

                var result = _cloudinary.Destroy (deleteParams);

                if (result.Result == "ok")
                    _repo.Delete (photoFromRepo);
            }

            if (photoFromRepo.PublicId == null) {
                _repo.Delete (photoFromRepo);
            }

            if (await _repo.SaveAll ())
                return Ok ();

            return BadRequest ("Failed to delete the photo");
        }

        [HttpGet ("PropImg")]
        public IActionResult GetPropPhoto () {
            var photosFromRepo = store.images.FirstOrDefault(c=>c.UserId=="Admin");
            return Ok (photosFromRepo);
        }

    }
}