using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RstateAPI.Data;
using RstateAPI.Entities;
using RstateAPI.Entities.ContactDetails;
using RstateAPI.Helpers;

namespace RstateAPI.Controllers.Home {
    [ApiController]
    [Authorize]
    [Route ("api/[controller]")]
    public class LeadController : ControllerBase {
        private readonly StoreContext store;
        private readonly ISpacingRepo _repo;
        public LeadController (StoreContext store, ISpacingRepo repo) {
            this.store = store;
            _repo = repo;
        }

        [HttpGet ("lead/{userId}")]
        public async Task<ActionResult> GetLeadProperty (string userId) {
            var model = await _repo.AllLeadProperty (userId);
           
            List<SaveModel> all = new List<SaveModel> ();
            List<UserContact> models = new List<UserContact> ();
            for (int i = 0; i < model.Count (); i++) {
                all.AddRange (await store.PostPropertyModel.Include(c=>c.basicDetailId).Include(c=>c.locationId)
                .Where (c => c.uniqueID == model[i].UniqueID && c.basicDetailId.WantTo=="Sell").ToListAsync ());
            
            }  
            for (var j = 0; j < all.Count(); j++)
            {
                models.AddRange (await store.userContact.Where(c=>c.UniqueID==all[j].uniqueID).ToListAsync());
            } 
            return Ok (new { all, models });
        }

         [HttpGet ("Rentlead/{userId}")]
        public async Task<ActionResult> GetRentLeadProperty (string userId) {
            var model = await _repo.AllLeadProperty (userId);
           
             List<SaveModel> all = new List<SaveModel> ();
             List<UserContact> models = new List<UserContact> ();
            for (int i = 0; i < model.Count (); i++) {
                all.AddRange (await store.PostPropertyModel.Include(c=>c.basicDetailId).Include(c=>c.locationId)
                .Where (c => c.uniqueID == model[i].UniqueID && c.basicDetailId.WantTo=="Rent").ToListAsync ());
            }
             for (var j = 0; j < all.Count(); j++)
            {
                models.AddRange (await store.userContact.Where(c=>c.UniqueID==all[j].uniqueID).ToListAsync());
            } 
            return Ok (new { all, models });
        }
         [HttpGet ("Pglead/{userId}")]
        public async Task<ActionResult> GetPgLeadProperty (string userId) {
            var model = await _repo.AllLeadProperty (userId);
            List<UserContact> models = new List<UserContact> ();
            List<SaveModel> all = new List<SaveModel> ();
            for (int i = 0; i < model.Count (); i++) {
                all.AddRange (await store.PostPropertyModel.Include(c=>c.basicDetailId).Include(c=>c.locationId)
                .Where (c => c.uniqueID == model[i].UniqueID && c.basicDetailId.WantTo=="PG").ToListAsync ());
            }
             for (var j = 0; j < all.Count(); j++)
            {
                models.AddRange (await store.userContact.Where(c=>c.UniqueID==all[j].uniqueID).ToListAsync());
            } 
            return Ok (new { all, model });
        }
    }
}