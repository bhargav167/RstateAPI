using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RstateAPI.Data;
using RstateAPI.Entities.CityData;

namespace RstateAPI.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    // [Authorize (Roles = "Admin")]
    public class CityManageController : ControllerBase {
        private readonly StoreContext store;

        public CityManageController (StoreContext store) {
            this.store = store;
        }

        [HttpGet ("city/{id}")]
        public async Task<ActionResult> GetCity (int id) {
            var city = await store.city.FindAsync (id);
            return Ok (city);
        }

        [HttpGet ("Suggestcity/{cityName}")]
        public ActionResult GetCitySuggetion (string cityName) {
            var city = store.city.Where (c => c.CitynName.ToLower ().Contains (cityName.ToLower ())).ToList ();
            return Ok (city);
        }

        [HttpGet ("Suggestlocality/{localityName}/{cityName}")]
        public ActionResult GetLocalitySuggetion (string localityName, string cityName) {
            var locality = store.AddressLocality.Where (c => c.CityName.ToLower () == cityName && c.LocalityName.ToLower ().Contains (localityName.ToLower ())).ToList ();
            return Ok (locality);
        }

        [HttpGet ("Popularcity")]
        public ActionResult GetPopularCity () {
            var city = store.city.Where (c => c.Id == 1 || c.Id == 2 || c.Id == 3).ToList ();
            return Ok (city);
        }

        [HttpGet ("PopularLocality/{cityName}")]
        public ActionResult GetPopularLocality (string cityName) {
            var city = store.AddressLocality.Where (c => c.CityName.ToLower ().Contains (cityName.ToLower ())).ToList ();
            return Ok (city);
        }

        [HttpGet ("AllPopularLocality")]
        public ActionResult GetAllPopularLocality () {
            var city = store.AddressLocality.Where (c => c.cityId == 1 || c.cityId == 2 || c.cityId == 3).ToList ();
            return Ok (city);
        }

        [HttpGet ("locality/{id}")]
        public async Task<ActionResult> GetLocality (int id) {
            var locality = await store.AddressLocality.FindAsync (id);
            return Ok (locality);
        }

        // Sector Module 
        [HttpGet ("Sector/{localityid}")]
        public ActionResult GetSector (int localityid) {
            var sector = store.sector.Where (c => c.localityId == localityid).ToList ();
            return Ok (sector);
        }

        [HttpGet ("SectorEdit/{id}")]
        public ActionResult GetSectorEdit (int id) {
            var sector = store.sector.FirstOrDefault (c => c.Id == id);
            return Ok (sector);
        }

        [HttpPost ("Sector")]
        public async Task<ActionResult> CreateSector ([FromBody] Sector sector) {
            try {
                //Duplication Check
                var cityData = store.sector.FirstOrDefault (c => c.SectorName.ToLower () == sector.SectorName.ToLower ());
                if (cityData != null)
                    return BadRequest (new { message = "Duplicate value not allowed." });

                await store.sector.AddAsync (sector);
                await store.SaveChangesAsync ();
            } catch (Exception e) {
                throw e;
            }
            return Ok (sector);
        }

        [HttpPut ("Sector/{id}")]
        public IActionResult updateSector (int id, [FromBody] Sector sector) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            var city = store.sector.FirstOrDefault (c => c.Id == id);
            if (city == null)
                return NotFound ($"Could not find Sector with id of {id}");

            city.SectorName = sector.SectorName;
            store.Update (city);
            store.SaveChanges ();
            return NoContent ();

            throw new System.Exception ($"Updating Sector with id {id} failed");
        }

        // Pocket Module 
        [HttpGet ("Pocket/{pocketid}")]
        public ActionResult GetPocket (int pocketid) {
            var sector = store.Pockets.Where (c => c.sectorId == pocketid).ToList ();
            return Ok (sector);
        }

        [HttpGet ("PocketEdit/{id}")]
        public ActionResult GetPocketEdit (int id) {
            var sector = store.Pockets.FirstOrDefault (c => c.Id == id);
            return Ok (sector);
        }

        [HttpPost ("Pocket")]
        public async Task<ActionResult> CreatePocket ([FromBody] Pocket pocket) {
            try {
                //Duplication Check
                var cityData = store.Pockets.FirstOrDefault (c => c.PocketAddress.ToLower () == pocket.PocketAddress.ToLower ());
                if (cityData != null)
                    return BadRequest (new { message = "Duplicate value not allowed." });
                await store.Pockets.AddAsync (pocket);
                await store.SaveChangesAsync ();
            } catch (Exception e) {
                throw e;
            }
            return Ok (pocket);
        }

        [HttpPut ("Pocket/{id}")]
        public IActionResult updatePocket (int id, [FromBody] Pocket sector) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            var city = store.Pockets.FirstOrDefault (c => c.Id == id);
            if (city == null)
                return NotFound ($"Could not find Pocket with id of {id}");

            city.PocketAddress = sector.PocketAddress;
            store.Pockets.Update (city);
            store.SaveChanges ();
            return NoContent ();

            throw new System.Exception ($"Updating Pocket with id {id} failed");
        }

        [HttpPost]
        public async Task<ActionResult> CreateCity ([FromBody] City city) {
            try {
                //Duplication Check
                var cityData = store.city.FirstOrDefault (c => c.CitynName.ToLower () == city.CitynName.ToLower ());
                if (cityData != null)
                    return BadRequest (new { message = "Duplicate value not allowed." });

                await store.city.AddAsync (city);
                await store.SaveChangesAsync ();
            } catch (Exception e) {
                throw e;
            }

            return Ok (city);
        }

        [HttpPost ("locality")]
        public async Task<ActionResult> CreateLocality ([FromBody] AddressLocality city) {
            try {
                //Duplication Check
                var cityData = store.AddressLocality.FirstOrDefault (c => c.LocalityName.ToLower () == city.LocalityName.ToLower ());
                if (cityData != null)
                    return BadRequest (new { message = "Duplicate value not allowed." });

                await store.AddressLocality.AddAsync (city);
                await store.SaveChangesAsync ();
            } catch (Exception e) {
                throw e;
            }

            return Ok (city);
        }

        [HttpPost ("localityforsearchSuggetion/{search}")]
        public async Task<ActionResult> CreateLocalitySugg (string search) {
            try {
                //Duplication Check
                var cityData = store.addressSearchData.FirstOrDefault (c => c.Adddresdata.ToLower () == search.ToLower ());
                if (cityData != null)
                    return BadRequest (new { message = "Duplicate value not allowed." });

                SearchAddresData ss = new SearchAddresData{
                    Adddresdata=search
                }; 
                await store.addressSearchData.AddAsync (ss);
                await store.SaveChangesAsync ();
            } catch (Exception e) {
                throw e;
            }

            return Ok (search);
        }
         [HttpPost ("sectorforsearchSuggetion/{locality}/{sector}")]
        public async Task<ActionResult> CreateSctorSugg (string locality,string sector) {
            try {
                SearchAddresData ss=new SearchAddresData(){
                    Adddresdata=locality +","+sector
                };
               await store.addressSearchData.AddAsync (ss);
                await store.SaveChangesAsync ();
            } catch (Exception e) {
                throw e;
            }

            return Ok (200);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> updateCity (int id, [FromBody] City session) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            var city = await store.city.FindAsync (id);
            if (city == null)
                return NotFound ($"Could not find City with id of {id}");

            city.CitynName = session.CitynName;

            store.city.Update (city);
            store.SaveChanges ();
            return NoContent ();

            throw new System.Exception ($"Updating City with id {id} failed");
        }

        [HttpPut ("Locality/{id}")]
        public async Task<IActionResult> updateLocality (int id, [FromBody] AddressLocality session) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            var city = await store.AddressLocality.FindAsync (id);
            if (city == null)
                return NotFound ($"Could not find City with id of {id}");

            city.LocalityName = session.LocalityName;

            store.AddressLocality.Update (city);
            store.SaveChanges ();
            return NoContent ();

            throw new System.Exception ($"Updating Locality with id {id} failed");
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> deleteCity (int id) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            var city = await store.city.FindAsync (id);
            if (city == null)
                return NotFound ($"Could not find City with id of {id}");

            store.Remove (city);
            await store.SaveChangesAsync ();
            return NoContent ();

            throw new System.Exception ($"Updating City with id {id} failed");
        }

        [HttpDelete ("locality/{id}")]
        public async Task<IActionResult> deleteLocality (int id) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            var city = await store.AddressLocality.FindAsync (id);
            if (city == null)
                return NotFound ($"Could not find Locality with id of {id}");

            store.AddressLocality.Remove (city);
            await store.SaveChangesAsync ();
            return NoContent ();

            throw new System.Exception ($"Deleting Locality with id {id} failed");
        }

        [HttpDelete ("Sector/{id}")]
        public async Task<IActionResult> deleteSector (int id) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            var city = await store.sector.FindAsync (id);
            if (city == null)
                return NotFound ($"Could not find Sector with id of {id}");

            store.sector.Remove (city);
            await store.SaveChangesAsync ();
            return NoContent ();

            throw new System.Exception ($"Deleting Sector with id {id} failed");
        }

        [HttpDelete ("Pocket/{id}")]
        public async Task<IActionResult> deletePocket (int id) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            var city = await store.Pockets.FindAsync (id);
            if (city == null)
                return NotFound ($"Could not find Pocket with id of {id}");

            store.Pockets.Remove (city);
            await store.SaveChangesAsync ();
            return NoContent ();

            throw new System.Exception ($"Deleting Pocket with id {id} failed");
        }

    }
}