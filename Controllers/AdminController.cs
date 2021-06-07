using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RstateAPI.Data;
using RstateAPI.Entities;
using RstateAPI.Helpers;

namespace RstateAPI.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class AdminController : ControllerBase {
        private readonly StoreContext store;
        private readonly ISpacingRepo _repo;
        public AdminController (StoreContext store, ISpacingRepo repo) {
            this.store = store;
            _repo = repo;
        }

        [HttpGet ("AllProperty")]
        public async Task<ActionResult> GetAllProperty ([FromQuery] PropertyParams proParams) {

            var all = await _repo.AllProperty (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllPendingProperty")]
        public async Task<ActionResult> GetAllPendingProperty ([FromQuery] PropertyParams proParams) {

            var all = await _repo.AllPendingProperty (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllConfirmProperty")]
        public async Task<ActionResult> GetAllConfirmProperty ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllConfirmProperty (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllRejectProperty")]
        public async Task<ActionResult> GetAllRejectProperty ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllRejectProperty (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllRentProperty")]
        public async Task<ActionResult> GetRentProperty ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllRentPropertyList (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllRentConfirmProperty")]
        public async Task<ActionResult> GetRentConfirmProperty ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllRentConfirmedPropertyList (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllRentPendingProperty")]
        public async Task<ActionResult> GetRentPendingProperty ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllRentPendingPropertyList (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllRentRejectProperty")]
        public async Task<ActionResult> GetRentRejectProperty ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllRentRejectedPropertyList (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        //Sell
        [HttpGet ("AllSellRejectProperty")]
        public async Task<ActionResult> GetSellRejectProperty ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllSellRejectedPropertyList (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllSellProperty")]
        public async Task<ActionResult> GetSellProperty ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllSellPropertyList (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllSellConfirmedProperty")]
        public async Task<ActionResult> GetSellConfirmedProperty ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllSellConfirmedPropertyList (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllSellPendingProperty")]
        public async Task<ActionResult> GetSellPendingProperty ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllSellPendingPropertyList (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        //PG
        [HttpGet ("AllPGRejectProperty")]
        public ActionResult GetPGRejectProperty () {
            var all = store.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "PG" && c.isConfirmed == false && c.isDecline == true)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId)
                .ToList ();
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (store.images.Where (c => c.uniqueID == all[i].uniqueID).ToList ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllPGProperty")]
        public ActionResult GetPGProperty () {
            var all = store.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "PG")
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId)
                .ToList ();
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (store.images.Where (c => c.uniqueID == all[i].uniqueID).ToList ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllPGConfirmedProperty")]
        public ActionResult GetPGConfirmedProperty () {
            var all = store.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "PG" && c.isConfirmed == true && c.isDecline == false)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId)
                .ToList ();
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (store.images.Where (c => c.uniqueID == all[i].uniqueID).ToList ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("AllPGPendingProperty")]
        public ActionResult GetPGPendingProperty () {
            var all = store.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "PG" && c.isConfirmed == false && c.isDecline == false)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId)
                .ToList ();
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (store.images.Where (c => c.uniqueID == all[i].uniqueID).ToList ());
            }
            return Ok (new { all, imgs });
        }
    }
}