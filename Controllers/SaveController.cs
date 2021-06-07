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
    public class SaveController : ControllerBase {
        private readonly StoreContext store;
        private readonly ISpacingRepo _repo;
        public string fileName;
        public SaveController (StoreContext store, ISpacingRepo repo) {
            this.store = store;
            _repo = repo;
        }

        [HttpGet ("LastUniqueId")]
        public async Task<ActionResult> GetLastUnique () {
            try {
                var totalpostProperty = await store.PostPropertyModel.ToListAsync ();
                if (totalpostProperty.Count == 0)
                    return Ok (1);

                var lastId = store.PostPropertyModel.OrderByDescending (x => x.Id).FirstOrDefault ().uniqueID;
                lastId++;
                return Ok (lastId);
            } catch (Exception ex) {
                return Ok (ex);
            }
        }

        [Authorize (Roles = "Admin, SuperAdmin")]
        [HttpGet ("TotalProperty")]
        public async Task<ActionResult> GetTotalProperty () {
            var totalProperty = await store.PostPropertyModel.ToListAsync ();
            var totalPro = totalProperty.Count ();
            return Ok (totalPro);
        }

        [Authorize (Roles = "Admin, SuperAdmin")]
        [HttpGet ("TotalPendingProperty")]
        public async Task<ActionResult> GetTotalPendingProperty () {
            var totalProperty = await store.PostPropertyModel.Where (c => c.isConfirmed == false && c.isDecline == false).ToListAsync ();
            var totalPro = totalProperty.Count ();
            return Ok (totalPro);
        }

        [Authorize (Roles = "Admin, SuperAdmin")]
        [HttpGet ("TotalConfirmedProperty")]
        public async Task<ActionResult> GetTotalConfirmedProperty () {
            var totalProperty = await store.PostPropertyModel.Where (c => c.isConfirmed == true && c.isDecline == false).ToListAsync ();
            var totalPro = totalProperty.Count ();
            return Ok (totalPro);
        }

        [HttpGet ("TotalRejectProperty")]
        [Authorize (Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> GetTotalRejectProperty () {
            var totalProperty = await store.PostPropertyModel.Where (c => c.isConfirmed == false && c.isDecline == true).ToListAsync ();
            var totalPro = totalProperty.Count ();
            return Ok (totalPro);
        }

        [HttpGet ("city")]
        public async Task<ActionResult> GetCity () {
            var city = await store.city.OrderBy (b => b.CitynName).ToListAsync ();
            return Ok (city);
        }

        [HttpGet ("locality/{cityId}")]
        public async Task<ActionResult> GetLocality (int cityId) {
            var locality = await store.AddressLocality.FromSqlRaw ("select * from AddressLocality where cityId={0} ORDER BY LocalityName", cityId).ToListAsync ();
            return Ok (locality);
        }

        [HttpGet ("sector/{localityId}")]
        public async Task<ActionResult> GetSector (int localityId) {
            var sectors = await store.sector.FromSqlRaw ("select * from sector where localityId={0} ORDER BY SectorName", localityId).ToListAsync ();
            return Ok (sectors);
        } 

        [HttpGet]
        public async Task<ActionResult> GetOrder ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllPropertyList (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpGet ("View/{uniqueID}")]
        public async Task<ActionResult> GetDetailOrder (int uniqueID) {
            var imgs = await store.images.Where (c => c.uniqueID == uniqueID).ToListAsync ();
            var all = await store.PostPropertyModel.Include (c => c.basicDetailId).Include (c => c.locationId)
                .Where (c => c.uniqueID == uniqueID)
                .ToListAsync ();

            return Ok (new { all, imgs });
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder ([FromBody] SaveModel order) {
            try {
                order.basicDetailId.coverParking.ToString ();

                await _repo.FormSubmit (order);
                await _repo.SaveAll ();
            } catch (Exception e) {
                throw e;
            }

            return Ok (order);
        }

        [HttpPut ("{id}")]
        [Authorize (Roles = "Admin, SuperAdmin")]
        public ActionResult Confirm (int id) {

            var updatedetail = store.PostPropertyModel.Where (v => v.Id == id);
            foreach (var item in updatedetail) {
                item.isConfirmed = true;
                store.PostPropertyModel.Update (item);

            }
            store.SaveChanges ();

            return Ok (id);
        }

          [HttpPost ("MarkLead/{uniqueId}")]
        
        public ActionResult Lead (int uniqueId) {

            var updatedetail = store.PostPropertyModel.Where (v => v.uniqueID == uniqueId);
            foreach (var item in updatedetail) {
                item.IsLead = true;
                store.PostPropertyModel.Update (item);

            }
            store.SaveChanges ();

            return Ok (200);
        }

        [HttpGet ("home")]
        public async Task<ActionResult> GetHome ([FromQuery] PropertyParams proParams) {
            var all = await _repo.AllHomePropertyList (proParams);
            Response.AddPagination (all.CurrentPage, all.PageSize, all.TotalCount, all.TotalPages);
            List<Images1> imgs = new List<Images1> ();
            for (int i = 0; i < all.Count (); i++) {
                imgs.AddRange (await store.images.Where (c => c.uniqueID == all[i].uniqueID).ToListAsync ());
            }
            return Ok (new { all, imgs });
        }

        [HttpPut ("decline/{id}")]
        //[Authorize (Roles = "Admin, SuperAdmin")]
        public ActionResult Decline (int id) {
            var updatedetail = store.PostPropertyModel.Where (v => v.Id == id);
            foreach (var item in updatedetail) {
                item.isDecline = true;
                store.PostPropertyModel.Update (item);
            }
            store.SaveChanges ();

            return Ok (id);

        }

    }
}