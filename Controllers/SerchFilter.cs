using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RstateAPI.Data;

namespace RstateAPI.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class SerchFilter : ControllerBase {
        private readonly StoreContext store;
        private readonly ISpacingRepo _repo;
        public SerchFilter (StoreContext store, ISpacingRepo repo) {
            this.store = store;
            _repo = repo;
        }

        [HttpGet ("SearchBy/{City}/{locality}/{Price}/{Bhk}")]
        public ActionResult GetSearchResult (string Price = "0", string City = "all", string locality = "all", string Bhk = "all") {

            if (City == "all" && locality == "all" && Price == "0" && Bhk == "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId).ToList ();
                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City == "all" && locality != "all" && Price != "0" && Bhk != "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.locationId.Locality == locality && c.basicDetailId.mothlyRent == Price && c.basicDetailId.Bhk == Bhk).ToList ();
                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City == "all" && locality == "all" && Price != "0" && Bhk != "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.basicDetailId.mothlyRent == Price && c.basicDetailId.Bhk == Bhk).ToList ();
                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City == "all" && locality == "all" && Price == "0" && Bhk == "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.basicDetailId.Bhk == Bhk).ToList ();

                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City != "all" && locality == "all" && Price == "0" && Bhk == "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.locationId.City == City).ToList ();
                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City != "all" && locality != "all" && Price != "0" && Bhk != "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.locationId.City == City && c.locationId.Locality == locality && c.basicDetailId.mothlyRent == Price &&
                        c.basicDetailId.Bhk == Bhk).ToList ();
                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City == "all" && locality == "all" && Price == "0" && Bhk != "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.basicDetailId.Bhk == Bhk).ToList ();
                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City == "all" && locality == "all" && Price != "0" && Bhk == "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.basicDetailId.mothlyRent == Price).ToList ();
                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City != "all" && locality == "all" && Price != "0" && Bhk != "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.basicDetailId.mothlyRent == Price && c.locationId.City == City && c.basicDetailId.mothlyRent == Price).ToList ();

                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City == "all" && locality != "all" && Price == "0" && Bhk == "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.locationId.Locality == locality).ToList ();
                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City == "all" && locality != "all" && Price == "0" && Bhk != "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.locationId.Locality == locality && c.basicDetailId.Bhk == Bhk).ToList ();
                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            if (City == "all" && locality != "all" && Price != "0" && Bhk == "all") {
                var AllDetails = store.PostPropertyModel.Include (v => v.basicDetailId).Include (c => c.locationId)
                    .Where (c => c.locationId.Locality == locality && c.basicDetailId.mothlyRent == Price).ToList ();

                if (AllDetails.Count == 0)
                    return NoContent ();

                var imgs = store.images.ToList ();
                return Ok (new { AllDetails, imgs });
            }
            return NoContent ();
        }
    
      [HttpGet ("SearchBy/{City}")]
        public ActionResult GetAddressResult (string City) {
            var all =store.addressSearchData
                .Where (m => m.Adddresdata.ToLower().Contains(City.ToLower()))
                .AsQueryable ();
           return Ok (all);
        }
    
    
    }
}