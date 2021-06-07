using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RstateAPI.Data;
using RstateAPI.Entities;
using RstateAPI.Entities.ContactDetails;
using RstateAPI.Helpers;

namespace RstateAPI.Controllers.Home {
    [ApiController]
    [Route ("api/[controller]")]
    public class HomeController : ControllerBase {
        private readonly StoreContext store;
        private readonly ISpacingRepo _repo;
        public HomeController (StoreContext store, ISpacingRepo repo) {
            this.store = store;
            _repo = repo;
        }

        [HttpGet ("ByAddress/{address}/{rentorsell}")]
        public async Task<ActionResult> GetPropertyByAddress ([FromQuery] PropertyParams proParams, string address, string rentorsell) {
            var all = await _repo.AllPropertyListByAddress (proParams, address, rentorsell);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("ByLocality/{locality}/{rentorsell}")]
        public async Task<ActionResult> GetPropertyByLocality ([FromQuery] PropertyParams proParams, string locality, string rentorsell) {
            var all = await _repo.AllPropertyListByLocality (proParams, locality, rentorsell);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }

            return Ok (new { all, imgs });
        }

        [HttpGet ("BySector/{sector}/{rentorsell}")]
        public async Task<ActionResult> GetPropertyBySector ([FromQuery] PropertyParams proParams, string sector, string rentorsell) {
            var all = await _repo.AllPropertyListBySector (proParams, sector, rentorsell);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }

            return Ok (new { all, imgs });
        }

        [HttpGet ("ByType/{type}/{sellorrent}/{address}")]
        public async Task<ActionResult> GetPropertyType ([FromQuery] PropertyParams proParams, string type, string sellorrent, string address) {
            var all = await _repo.AllPropertyListByType (proParams, type, sellorrent, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

       [HttpGet ("ByType2/{type}/{type2}/{sellorrent}/{address}")]
        public async Task<ActionResult> GetPropertyType2 ([FromQuery] PropertyParams proParams, string type,string type2, string sellorrent, string address) {
            var all = await _repo.AllPropertyListByType2 (proParams, type,type2, sellorrent, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

       [HttpGet ("ByType3/{type}/{type2}/{type3}/{sellorrent}/{address}")]
        public async Task<ActionResult> GetPropertyType3 ([FromQuery] PropertyParams proParams, string type,string type2,string type3, string sellorrent, string address) {
            var all = await _repo.AllPropertyListByType3 (proParams, type,type2,type3, sellorrent, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

       [HttpGet ("ByType4/{type}/{type2}/{type3}/{type4}/{sellorrent}/{address}")]
        public async Task<ActionResult> GetPropertyType4 ([FromQuery] PropertyParams proParams, string type,string type2,string type3,string type4, string sellorrent, string address) {
            var all = await _repo.AllPropertyListByType4 (proParams, type,type2,type3,type4,sellorrent, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

       [HttpGet ("ByType5/{type}/{type2}/{type3}/{type4}/{type5}/{sellorrent}/{address}")]
        public async Task<ActionResult> GetPropertyType5 ([FromQuery] PropertyParams proParams, string type,string type2,string type3,string type4,string type5, string sellorrent, string address) {
            var all = await _repo.AllPropertyListByType5 (proParams, type,type2,type3,type4,type5,sellorrent, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("ByBHK/{type}/{sellorrent}/{address}")]
        public async Task<ActionResult> GetPropertyTypeBHK ([FromQuery] PropertyParams proParams, string type, string sellorrent, string address) {
            var all = await _repo.AllPropertyListByBHK (proParams, type, sellorrent, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

       [HttpGet ("ByBHK2/{type}/{type2}/{sellorrent}/{address}")]
        public async Task<ActionResult> GetPropertyTypeBHK2 ([FromQuery] PropertyParams proParams, string type,string type2, string sellorrent, string address) {
            var all = await _repo.AllPropertyListByBHK2 (proParams, type,type2, sellorrent, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

       [HttpGet ("ByBHK3/{type}/{type2}/{type3}/{sellorrent}/{address}")]
        public async Task<ActionResult> GetPropertyTypeBHK3 ([FromQuery] PropertyParams proParams, string type,string type2,string type3, string sellorrent, string address) {
            var all = await _repo.AllPropertyListByBHK3 (proParams, type,type2,type3, sellorrent, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("ByBHK4/{type}/{type2}/{type3}/{type4}/{sellorrent}/{address}")]
        public async Task<ActionResult> GetPropertyTypeBHK4 ([FromQuery] PropertyParams proParams, string type,string type2,string type3,string type4, string sellorrent, string address) {
            var all = await _repo.AllPropertyListByBHK4 (proParams, type,type2,type3,type4, sellorrent, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("ByPrice/{min}/{max}/{sellorrent}/{address}")]
        public async Task<ActionResult> GetPropertyTypePrice ([FromQuery] PropertyParams proParams, int min,int max, string sellorrent, string address) {
            var all = await _repo.AllPropertyListByPrice(proParams, min,max, sellorrent, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("ByTypeAndAddress/{type}/{address}/{rentorsell}")]
        public async Task<ActionResult> GetPropertyTypeandAddress ([FromQuery] PropertyParams proParams, string type, string address, string rentorsell) {

            var all = await _repo.AllPropertyListByAddressAndType (proParams, address, type, rentorsell);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }

            return Ok (new { all, imgs });
        }

        [HttpGet ("ByPGAddress/{address}")]
        public async Task<ActionResult> GetPropertyByPGAddress ([FromQuery] PropertyParams proParams, string address) {

            var all = await _repo.AllPropertyListByBGAddress (proParams, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("ByPG")]
        public async Task<ActionResult> GetPropertyByPG ([FromQuery] PropertyParams proParams) {

            var all = await _repo.AllPropertyListByBG (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("ByPGFor/{pgFor}/{address}")]
        public async Task<ActionResult> GetPropertyByPGFor ([FromQuery] PropertyParams proParams, string pgFor, string address) {

            var all = await _repo.AllPropertyListByPGFor (proParams, pgFor, address);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("ByPGFor/{pgFor}")]
        public async Task<ActionResult> GetPropertyByPGForOnly ([FromQuery] PropertyParams proParams, string pgFor) {
            var all = await _repo.AllPropertyListByOnlyPGFor (proParams, pgFor);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        // User Profile API
        [HttpGet ("UserProperty/{userId}/{propType}")]
        public async Task<ActionResult> GetPropertysByUserId ([FromQuery] PropertyParams proParams, string userId, string propType) {
            var all = await _repo.UserData (proParams, userId, propType);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        //Post UserDetails
        [HttpPost]
        public async Task<ActionResult> PostUserDetails ([FromBody] UserContact userContact) {
            if (await _repo.UserContactExist (userContact.Email, userContact.PhoneNumber, userContact.UniqueID, userContact.OwnerId)) {
                return NoContent ();
            }

            try {
                await _repo.PostUserContact (userContact);
                await _repo.SaveAll ();
            } catch (Exception e) {
                throw e;
            }

            return Ok (userContact);
        }
    }
}