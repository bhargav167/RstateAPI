using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RstateAPI.Data;
using RstateAPI.Entities;

namespace RstateAPI.Controllers.AdminList {
    [ApiController]
    [Route ("api/[controller]")]
    public class AdminFeatureController : ControllerBase {
        private readonly StoreContext store;
        private readonly ISpacingRepo _repo;
        public AdminFeatureController (StoreContext store, ISpacingRepo repo) {
            this.store = store;
            _repo = repo;
        }

        [HttpGet ("AllAdmin")]
        public ActionResult GetAllAdmin () {
            var user = store.appuser.Where (c => c.RoleId == "Admin" && c.IsActive == true).ToList ();
            return Ok (user);
        }

        // Owner Module

        [HttpGet ("AllOwnerList")]
        public ActionResult GetAllOwner () {
            var user = store.appuser.Where (c => c.RoleId == "Owner" && c.IsActive == true).ToList ();
            return Ok (user);
        }

        [HttpGet ("GetOwner/{id}")]
        public ActionResult GetOwner (string id) {
            var user = store.appuser.Where (c => c.Id == id && c.IsActive == true).FirstOrDefault ();
            return Ok (user);
        }

        [HttpPost ("DeleteOwner/{id}")]
        public async Task<ActionResult> DeleteOwner (string id) {
            var userTodelete = store.appuser.FirstOrDefault (c => c.Id == id);
            userTodelete.IsActive = false;

            store.appuser.Update (userTodelete);
            await store.SaveChangesAsync ();
            return Ok (200);
        }

        [HttpPost ("UpdateOwner/{id}")]
        public async Task<ActionResult> UPdateOwner (string id, [FromBody] EditOBB edit) {
            var userToupdate = store.appuser.FirstOrDefault (c => c.Id == id);
            userToupdate.FullName = edit.name;
            userToupdate.Email = edit.email;
            userToupdate.PhoneNumber = edit.phone;
            userToupdate.BrokerFirm=edit.firmno;
            userToupdate.reraNo=edit.rerano;
            userToupdate.GstNo=edit.gstno;
            userToupdate.landlineNo=edit.landlineno;
            userToupdate.locations=edit.location;
            store.appuser.Update (userToupdate);
            await store.SaveChangesAsync ();
            return Ok (200);
        }

        // Owner Module End

        //Broker Module

        [HttpGet ("AllBrokerList")]
        public ActionResult GetAllBroker () {
            var user = store.appuser.Where (c => c.RoleId == "Broker" && c.IsActive == true).ToList ();
            return Ok (user);
        }

        [HttpGet ("GetBroker/{id}")]
        public ActionResult GetBroker (string id) {
            var user = store.appuser.Where (c => c.Id == id && c.IsActive == true).FirstOrDefault ();
            return Ok (user);
        }

        [HttpPost ("DeleteBroker/{id}")]
        public async Task<ActionResult> DeleteBroker (string id) {
            var userTodelete = store.appuser.FirstOrDefault (c => c.Id == id);
            userTodelete.IsActive = false;

            store.appuser.Update (userTodelete);
            await store.SaveChangesAsync ();
            return Ok (200);
        }
        //Broker Module End

        [HttpGet ("AllBuilderList")]
        public ActionResult GetAllBuilder () {
            var user = store.appuser.Where (c => c.RoleId == "Builder" && c.IsActive == true).ToList ();
            return Ok (user);
        } 
    }
}