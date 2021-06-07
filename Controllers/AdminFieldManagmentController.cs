using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RstateAPI.Data;
using RstateAPI.Entities.PropertyConfiguration;

namespace RstateAPI.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    // [Authorize (Roles = "Admin")]
    public class AdminFieldManagmentController : ControllerBase {
        private readonly StoreContext store;
        private readonly ISpacingRepo _repo;
        public AdminFieldManagmentController (StoreContext store, ISpacingRepo repo) {
            this.store = store;
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetCurrentActiveProperty () {
            var currentActive = await _repo.GetpropertyConfiguration (1);
            return Ok (currentActive);
        }

        [HttpGet ("AdminActiveProp/{userId}")]
        public async Task<ActionResult> GetCurrentAdminActiveProperty (string userId) {
            var currentActive = await _repo.GetpropertyAdminConfiguration (userId);
            if (currentActive == null) {
                AdminAccessProp prop = new AdminAccessProp ();
                prop.IsManageAddress = false;
                prop.IsManageFeild = false;
                prop.IsSetPropImg = false;
                prop.IsManageCity = false;
                prop.IsManageLocality = false;
                prop.IsManagePocket = false;
                prop.IsManageSector = false;
                return Ok (prop);
            }

            return Ok (currentActive);
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> ManagePropertyFields (int id, [FromBody] PropertyConfig config) {
            var city = await store.PropertyConfig.FindAsync (id);
            if (city == null)
                return NotFound ($"Could not find Property with id of {id}");

            city.IsPropertyType = config.IsPropertyType;
            city.IsBhk = config.IsBhk;
            city.ageOfProperty = config.ageOfProperty;
            city.bathRoom = config.bathRoom;
            city.balcony = config.balcony;
            city.maintainCharge = config.maintainCharge;
            city.furnishType = config.furnishType;
            city.coverParking = config.coverParking;
            city.tenantType = config.tenantType;
            city.availableFrom = config.availableFrom;
            city.monthlyRent = config.monthlyRent;
            city.securityDeposite = config.securityDeposite;
            city.brokerage = config.brokerage;
            city.buildArea = config.buildArea;
            city.carpetArea = config.carpetArea;
            city.transactionType = config.transactionType;
            city.constructionStatus = config.constructionStatus;
            city.pGName = config.pGName;
            city.pgFor = config.pgFor;
            city.suitedFor = config.suitedFor;
            city.mealAvalable = config.mealAvalable;
            city.roomType = config.roomType;
            city.bedInRoom = config.bedInRoom;
            city.pgRent = config.pgRent;
            city.totalBed = config.totalBed;
            city.securityType = config.securityType;
            city.facilitiesOffered = config.facilitiesOffered;
            city.commonAreas = config.commonAreas;
            city.propertyManagedBy = config.propertyManagedBy;
            city.nonVegAllowed = config.nonVegAllowed;
            city.oppositeSexAllowed = config.oppositeSexAllowed;
            city.anyTimeAllowed = config.anyTimeAllowed;
            city.visitorsAllowed = config.visitorsAllowed;
            city.guardianAllowed = config.guardianAllowed;
            city.drinkingAllowed = config.drinkingAllowed;
            city.smokingAllowed = config.smokingAllowed;

            store.PropertyConfig.Update (city);
            store.SaveChanges ();
            return NoContent ();

            throw new System.Exception ($"Updating Property Status with id {id} failed");
        }

        [HttpPost ("AdminFieldConfig/{userId}")]
        public async Task<ActionResult> ManageAdminPropertyFields (string userId, [FromBody] AdminAccessProp config) {
            var configProp = store.adminconfigProp.FirstOrDefault (c => c.UserId == userId);
            if (configProp == null) {
                config.UserId = userId;
                await store.adminconfigProp.AddAsync (config);
                await store.SaveChangesAsync ();
            } else {
                configProp.IsManageAddress = config.IsManageAddress;
                configProp.IsManageFeild = config.IsManageFeild;
                configProp.IsSetPropImg = config.IsSetPropImg;
                configProp.IsManageCity = config.IsManageCity;
                configProp.IsManageLocality = config.IsManageLocality;
                configProp.IsManageSector = config.IsManageSector;
                configProp.IsManagePocket = config.IsManagePocket;
                configProp.UserId = userId;
                
                store.adminconfigProp.Update (configProp);
                store.SaveChanges ();
            }

            return NoContent ();

            throw new System.Exception ($"Updating Property Status with id {userId} failed");
        }

    }
}