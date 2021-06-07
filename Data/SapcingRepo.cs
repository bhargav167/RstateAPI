using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RstateAPI.Entities;
using RstateAPI.Entities.CityData;
using RstateAPI.Entities.ContactDetails;
using RstateAPI.Entities.PropertyConfiguration;
using RstateAPI.Helpers;

namespace RstateAPI.Data {
    public class SapcingRepo : ISpacingRepo {
        private readonly StoreContext _context;

        public SapcingRepo (StoreContext context) {
            _context = context;
        }
        public void Add<T> (T entity) where T : class {
            _context.Add (entity);
        }
        public void Delete<T> (T entity) where T : class {
            _context.Remove (entity);
        }
        public async Task<PropertyConfig> propertyConfiguration (PropertyConfig proConfig) {
            _context.PropertyConfig.Update (proConfig);
            await _context.SaveChangesAsync ();
            return proConfig;
        }
        public async Task<AdminAccessProp> propertyConfiguration (AdminAccessProp adminproConfig) {
            _context.adminconfigProp.Update (adminproConfig);
            await _context.SaveChangesAsync ();
            return adminproConfig;
        }
        public async Task<SaveModel> FormSubmit (SaveModel form) {
            await _context.PostPropertyModel.AddAsync (form);
            await _context.SaveChangesAsync ();

            return form;
        }
        public async Task<bool> SaveAll () {
            return await _context.SaveChangesAsync () > 0;
        }
        public async Task<Sector> GetSector (int localityid) {
            var sector = await _context.sector.FindAsync (localityid);
            return sector;
        }
        public async Task<Sector> SaveSector (Sector sector) {
            await _context.sector.AddAsync (sector);
            await _context.SaveChangesAsync ();
            return sector;
        }

        public async Task<PageList<SaveModel>> AllPropertyList (PropertyParams proParams) {
            var propList = _context.PostPropertyModel.Include (c => c.basicDetailId).Include (c => c.locationId)
                .Where (c => c.isConfirmed == false && c.isDecline == false).OrderByDescending (x => x.Id).AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (propList, proParams.PageNumber, proParams.PageSize);
        }

        //All propo
        public async Task<PageList<SaveModel>> AllProperty (PropertyParams proParams) {
            var rentlist = _context.PostPropertyModel
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (rentlist, proParams.PageNumber, proParams.PageSize);
        }
        public async Task<PageList<SaveModel>> AllPendingProperty (PropertyParams proParams) {
            var rentlist = _context.PostPropertyModel
                .Where (c => c.isConfirmed == false && c.isDecline == false)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (rentlist, proParams.PageNumber, proParams.PageSize);
        }
        public async Task<PageList<SaveModel>> AllConfirmProperty (PropertyParams proParams) {
            var rentlist = _context.PostPropertyModel
                .Where (c => c.isConfirmed == true && c.isDecline == false)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (rentlist, proParams.PageNumber, proParams.PageSize);
        }
        public async Task<PageList<SaveModel>> AllRejectProperty (PropertyParams proParams) {
            var rentlist = _context.PostPropertyModel
                .Where (c => c.isConfirmed == false && c.isDecline == true)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (rentlist, proParams.PageNumber, proParams.PageSize);

        }

        // All Rent API 
        public async Task<PageList<SaveModel>> AllRentPropertyList (PropertyParams proParams) {
            var rentlist = _context.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "Rent")
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (rentlist, proParams.PageNumber, proParams.PageSize);

        }

        public async Task<PageList<SaveModel>> AllRentConfirmedPropertyList (PropertyParams proParams) {
            var all = _context.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "Rent" && c.isConfirmed == true && c.isDecline == false)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }

        public async Task<PageList<SaveModel>> AllRentPendingPropertyList (PropertyParams proParams) {
            var all = _context.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "Rent" && c.isConfirmed == false && c.isDecline == false)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }

        public async Task<PageList<SaveModel>> AllRentRejectedPropertyList (PropertyParams proParams) {
            var all = _context.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "Rent" && c.isConfirmed == false && c.isDecline == true)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }

        // All Sell API
        public async Task<PageList<SaveModel>> AllSellPropertyList (PropertyParams proParams) {
            var all = _context.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "Sell")
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }

        public async Task<PageList<SaveModel>> AllSellConfirmedPropertyList (PropertyParams proParams) {
            var all = _context.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "Sell" && c.isConfirmed == true && c.isDecline == false)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);
        }

        public async Task<PageList<SaveModel>> AllSellPendingPropertyList (PropertyParams proParams) {
            var all = _context.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "Sell" && c.isConfirmed == false && c.isDecline == false)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);
        }

        public async Task<PageList<SaveModel>> AllSellRejectedPropertyList (PropertyParams proParams) {
            var all = _context.PostPropertyModel.Where (c => c.basicDetailId.WantTo == "Sell" && c.isConfirmed == false && c.isDecline == true)
                .Include (v => v.basicDetailId)
                .Include (b => b.locationId).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }

        public async Task<PropertyConfig> GetpropertyConfiguration (int id) {
            var currentPropConfig = await _context.PropertyConfig.FindAsync (id);
            return currentPropConfig;
        }
        public async Task<AdminAccessProp> GetpropertyAdminConfiguration (string adminId) {
            var currentPropConfig = await _context.adminconfigProp.FirstOrDefaultAsync (c => c.UserId == adminId);
            return currentPropConfig;
        }

        public async Task<PageList<SaveModel>> AllHomePropertyList (PropertyParams proParams) {
            var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                .Include (c => c.locationId)
                .Where (m => m.isConfirmed == true && m.isDecline == false).OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);
        }
        public async Task<PageList<SaveModel>> AllPropertyListByAddress (PropertyParams proParams, string address, string rentorsell) {
            if (rentorsell == "All" && address.Contains (",")) {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.locationId.Locality.ToLower ()
                        .Contains (address.Substring (0, address.IndexOf (",")).ToLower ()) &&
                        m.locationId.SectorNo.ToLower ().Contains (address.Substring (address.LastIndexOf (',') + 1).ToLower ()) &&
                        m.isConfirmed == true && m.isDecline == false)
                    .OrderByDescending (x => x.Id).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
            }
            if (rentorsell != "All" && address.Contains (",")) {
                var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.locationId.Locality.ToLower ().Contains (address.Substring (0, address.IndexOf (",")).ToLower ()) &&
                        m.locationId.SectorNo.ToLower ().Contains (address.Substring (address.LastIndexOf (',') + 1)) 
                        &&
                        m.isConfirmed == true && m.isDecline == false).Where(m=> m.basicDetailId.WantTo.ToLower() == rentorsell.ToLower())
                    .OrderByDescending (x => x.Id).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);
            }

            if (rentorsell == "All" && !address.Contains (",")) {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.locationId.Locality.ToLower ()
                        .Contains (address.ToLower ()) ||
                        m.locationId.SectorNo.ToLower ().Contains (address.ToLower ()) &&
                        m.isConfirmed == true && m.isDecline == false)
                    .OrderByDescending (x => x.Id).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
            }

            if (rentorsell != "All" && !address.Contains (",")) {
                var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.locationId.Locality.ToLower ().Contains (address.ToLower ()) ||
                        m.locationId.SectorNo.ToLower ().Contains (address.ToLower()) && 
                        m.isConfirmed == true && m.isDecline == false).Where(m=> m.basicDetailId.WantTo.ToLower() == rentorsell.ToLower())
                    .OrderByDescending (x => x.Id).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);
            }
            return null;
        }
        public async Task<PageList<SaveModel>> AllPropertyListByType (PropertyParams proParams, string type, string rentorsell, string address) {
            if (rentorsell != "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.basicDetailId.propertyType.ToLower () == type.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                    .Where(c=>c.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                    .OrderByDescending (x => x.Id)
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

            }
            if (rentorsell == "All") {
                var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.basicDetailId.propertyType.ToLower () == type.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                    .OrderByDescending (x => x.Id).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

            }
            return null;

        }

        public async Task<PageList<SaveModel>> AllPropertyListByType2 (PropertyParams proParams, string type, string type2, string rentorsell, string address) {
            if (rentorsell != "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.basicDetailId.propertyType.ToLower () == type.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type2.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                     .Where(c=>c.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                    .OrderByDescending (x => x.Id)
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

            }
            if (rentorsell == "All") {
                var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.basicDetailId.propertyType.ToLower () == type.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type2.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                    .OrderByDescending (x => x.Id).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

            }
            return null;

        }
        public async Task<PageList<SaveModel>> AllPropertyListByType3 (PropertyParams proParams, string type, string type2, string type3, string rentorsell, string address) {
            if (rentorsell != "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.basicDetailId.propertyType.ToLower () == type.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type2.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type3.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                     .Where(c=>c.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                    .OrderByDescending (x => x.Id)
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

            }
            if (rentorsell == "All") {
                var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.basicDetailId.propertyType.ToLower () == type.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type2.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type3.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                    .OrderByDescending (x => x.Id).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

            }
            return null;

        }

        public async Task<PageList<SaveModel>> AllPropertyListByType4 (PropertyParams proParams, string type, string type2, string type3, string type4, string rentorsell, string address) {
            if (rentorsell != "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.basicDetailId.propertyType.ToLower () == type.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type2.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type3.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type4.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                     .Where(c=>c.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                    .OrderByDescending (x => x.Id)
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

            }
            if (rentorsell == "All") {
                var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.basicDetailId.propertyType.ToLower () == type.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type2.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type3.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type4.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                    .OrderByDescending (x => x.Id).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

            }
            return null;

        }

        public async Task<PageList<SaveModel>> AllPropertyListByType5 (PropertyParams proParams, string type, string type2, string type3, string type4, string type5, string rentorsell, string address) {
            if (rentorsell != "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.basicDetailId.propertyType.ToLower () == type.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type2.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type3.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type4.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type5.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                     .Where(c=>c.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                    .OrderByDescending (x => x.Id)
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

            }
            if (rentorsell == "All") {
                var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.basicDetailId.propertyType.ToLower () == type.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type2.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type3.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type4.ToLower () ||
                        m.basicDetailId.propertyType.ToLower () == type5.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                    .OrderByDescending (x => x.Id).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

            }
            return null;

        }

        public async Task<PageList<SaveModel>> AllPropertyListByAddressAndType (PropertyParams proParams, string address, string type, string rentorsell) {
            if (rentorsell == "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.basicDetailId.propertyType.Contains (type) && m.locationId.City.Contains (address))
                    .OrderByDescending (x => x.Id).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

            }
            var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                .Include (c => c.locationId)
                .Where (m => m.basicDetailId.propertyType.Contains (type) && m.basicDetailId.WantTo == rentorsell && m.locationId.City.Contains (address))
                .OrderByDescending (x => x.Id).AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }

        public async Task<PageList<SaveModel>> AllPropertyListByBGAddress (PropertyParams proParams, string address) {
            var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                .Include (c => c.locationId)
                .Where (m => m.basicDetailId.WantTo == "PG" && m.locationId.City.Contains (address))
                .OrderByDescending (x => x.Id).AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }
        public async Task<PageList<SaveModel>> AllPropertyListByBG (PropertyParams proParams) {

            var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                .Include (c => c.locationId)
                .Where (m => m.basicDetailId.WantTo == "PG")
                .OrderByDescending (x => x.Id).AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }

        public async Task<PageList<SaveModel>> AllPropertyListByPGFor (PropertyParams proParams, string pgFor, string address) {
            var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                .Include (c => c.locationId)
                .Where (m => m.basicDetailId.PgFor == pgFor && m.locationId.City == address)
                .OrderByDescending (x => x.Id).AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }

        public async Task<PageList<SaveModel>> AllPropertyListByOnlyPGFor (PropertyParams proParams, string pgFor) {
            var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                .Include (c => c.locationId)
                .Where (m => m.basicDetailId.PgFor == pgFor)
                .OrderByDescending (x => x.Id).AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }
        public async Task<PageList<SaveModel>> UserData (PropertyParams proParams, string userId, string propType) {
            if (propType != "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.UserId == userId && m.basicDetailId.WantTo == propType && m.isDecline == false)
                    .OrderByDescending (x => x.Id)
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
            }
            var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                .Include (c => c.locationId)
                .Where (m => m.UserId == userId && m.isDecline == false)
                .OrderByDescending (x => x.Id)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

        }

        public async Task<bool> AdminExist (string userName, string email) {
            if (await _context.appuser.AnyAsync (e => e.UserName == userName || e.Email == email))
                return true;

            return false;
        }

        //Photo
        public async Task<IEnumerable<Images1>> getPhoto () {
            var photos = await _context.images.ToListAsync ();
            return photos;
        }

        public async Task<Images1> getPhoto (int id) {
            var photos = await _context.images.FirstOrDefaultAsync (p => p.Id == id);
            return photos;
        }

        public async Task<Images1> GetMainPhotoForUser (int uniqueID) {
            return await _context.images.Where (u => u.uniqueID == uniqueID).FirstOrDefaultAsync (p => p.cover);
        }

        public async Task<UserContact> PostUserContact (UserContact userContact) {
            await _context.userContact.AddAsync (userContact);
            await _context.SaveChangesAsync ();
            return userContact;
        }

        public async Task<PageList<SaveModel>> AllPropertyListByLocality (PropertyParams proParams, string locality, string rentorsell) {
            if (rentorsell == "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.locationId.Locality == locality)
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
            }
            var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                .Include (c => c.locationId)
                .Where (m => m.locationId.Locality == locality && m.basicDetailId.WantTo == rentorsell)
                .AsQueryable ();
            return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);
        }
        public async Task<bool> UserContactExist (string email, string phone, int uniqueId, string OwnerId) {
            if (await _context.userContact.AnyAsync (e => e.Email == email && e.PhoneNumber == phone &&
                    e.UniqueID == uniqueId && e.OwnerId == OwnerId))
                return true;

            return false;
        }

        public async Task<PageList<SaveModel>> AllPropertyListBySector (PropertyParams proParams, string sector, string rentorsell) {
            if (rentorsell == "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.locationId.SectorNo == sector)
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
            } else {
                var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.locationId.SectorNo == sector && m.basicDetailId.WantTo == rentorsell)
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);

            }
        }

        public async Task<IList<UserContact>> AllLeadProperty (string userId) {
            var leads = await _context.userContact.Where (u => u.OwnerId == userId).ToListAsync ();
            return leads;
        }

        public async Task<PageList<SaveModel>> AllPropertyListByBHK (PropertyParams proParams, string type, string rentorsell, string address) {
            if (rentorsell == "All" && type != "4 Bhk") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.basicDetailId.Bhk.ToLower () == type.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false)
                    .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
            }
            if (type == "4 Bhk" && rentorsell == "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.basicDetailId.Bhk == "4 BHK" ||
                        m.basicDetailId.Bhk == "5 BHK" ||
                        m.basicDetailId.Bhk == "6 BHK" ||
                        m.basicDetailId.Bhk == "7 BHK" ||
                        m.basicDetailId.Bhk == "8 BHK" ||
                        m.basicDetailId.Bhk == "9 BHK" &&
                        m.isConfirmed == true && m.isDecline == false)
                    .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
            }
            if (type != "4 Bhk" && rentorsell != "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.basicDetailId.Bhk == type.ToLower () &&
                        m.basicDetailId.WantTo.ToLower () == rentorsell.ToLower () &&
                        m.isConfirmed == true && m.isDecline == false)
                    .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
            }
            if (type == "4 Bhk" && rentorsell != "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        m.basicDetailId.Bhk == "4 BHK" ||
                        m.basicDetailId.Bhk == "5 BHK" ||
                        m.basicDetailId.Bhk == "6 BHK" ||
                        m.basicDetailId.Bhk == "7 BHK" ||
                        m.basicDetailId.Bhk == "8 BHK" ||
                        m.basicDetailId.Bhk == "9 BHK" &&
                        m.isConfirmed == true && m.isDecline == false)
                    .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                    .Where(m=> m.basicDetailId.WantTo.ToLower () == rentorsell.ToLower ()).AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
            }
            return null;
        }

        public async Task<PageList<SaveModel>> AllPropertyListByBHK2 (PropertyParams proParams, string type, string type2, string rentorsell, string address) {
            if (rentorsell == "All") {
                if (type == "4 Bhk" && type2 != "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                if (type != "4 Bhk" && type2 == "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                            m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

                }
                if (type != "4 Bhk" && type2 != "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk == type.ToLower () &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

                }
            }
            if (rentorsell != "All") {
                if (type == "4 Bhk" && type2 != "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK"
                            &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                        .Where(m=> m.basicDetailId.WantTo.ToLower () == rentorsell.ToLower ())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                if (type != "4 Bhk" && type2 == "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                            m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                         .Where(m=> m.basicDetailId.WantTo.ToLower () == rentorsell.ToLower ())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

                }
                if (type != "4 Bhk" && type2 != "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk == type.ToLower () &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                         .Where(m=> m.basicDetailId.WantTo.ToLower () == rentorsell.ToLower ())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
            }
            return null;
        }

        public async Task<PageList<SaveModel>> AllPropertyListByBHK3 (PropertyParams proParams, string type, string type2, string type3, string rentorsell, string address) {
            if (rentorsell == "All") {
                if (type != "4 Bhk" && type2 != "4 Bhk" && type3 != "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type3.ToLower () &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                if (type == "4 Bhk" && type2 != "4 Bhk" && type3 != "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type3.ToLower () ||
                            m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

                }
                if (type != "4 Bhk" && type2 == "4 Bhk" && type3 != "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type3.ToLower () ||
                            m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

                }
                if (type != "4 Bhk" && type2 != "4 Bhk" && type3 == "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

                }

            }
            if (rentorsell != "All") {
             if (type != "4 Bhk" && type2 != "4 Bhk" && type3 != "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type3.ToLower () &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                        .Where(m=>m.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                if (type == "4 Bhk" && type2 != "4 Bhk" && type3 != "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type3.ToLower () ||
                            m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                        .Where(m=>m.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

                }
                if (type != "4 Bhk" && type2 == "4 Bhk" && type3 != "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type3.ToLower () ||
                            m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                        .Where(m=>m.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

                }
                if (type != "4 Bhk" && type2 != "4 Bhk" && type3 == "4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                        .Where(m=>m.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);

                }
    
            }
            return null;
        }
        public async Task<PageList<SaveModel>> AllPropertyListByBHK4 (PropertyParams proParams, string type, string type2, string type3, string type4, string rentorsell, string address) {
             if (rentorsell == "All") {
                if (type == "4 Bhk" && type2 != "4 Bhk" && type3 != "4 Bhk" && type4!="4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type4.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type3.ToLower () ||
                             m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" 
                            &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                if (type != "4 Bhk" && type2 == "4 Bhk" && type3 != "4 Bhk" && type4!="4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                             m.basicDetailId.Bhk.ToLower () == type3.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type4.ToLower () ||
                             m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" 
                            &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                 if (type != "4 Bhk" && type2 != "4 Bhk" && type3 == "4 Bhk" && type4!="4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                             m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type4.ToLower () ||
                             m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" 
                            &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                 if (type != "4 Bhk" && type2 != "4 Bhk" && type3 != "4 Bhk" && type4=="4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                             m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type3.ToLower () ||
                             m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" 
                            &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ()).AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                
            }
            if (rentorsell != "All") {
                 if (type == "4 Bhk" && type2 != "4 Bhk" && type3 != "4 Bhk" && type4!="4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type4.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type3.ToLower () ||
                             m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                        .Where(m=>m.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                if (type != "4 Bhk" && type2 == "4 Bhk" && type3 != "4 Bhk" && type4!="4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                             m.basicDetailId.Bhk.ToLower () == type3.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type4.ToLower () ||
                             m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" && 
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                        .Where(m=>m.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                 if (type != "4 Bhk" && type2 != "4 Bhk" && type3 == "4 Bhk" && type4!="4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                             m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type4.ToLower () ||
                             m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" && 
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                        .Where(m=>m.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                 if (type != "4 Bhk" && type2 != "4 Bhk" && type3 != "4 Bhk" && type4=="4 Bhk") {
                    var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                        .Include (c => c.locationId)
                        .Where (m =>
                            m.basicDetailId.Bhk.ToLower () == type.ToLower () ||
                             m.basicDetailId.Bhk.ToLower () == type2.ToLower () ||
                            m.basicDetailId.Bhk.ToLower () == type3.ToLower () ||
                             m.basicDetailId.Bhk == "4 BHK" ||
                            m.basicDetailId.Bhk == "5 BHK" ||
                            m.basicDetailId.Bhk == "6 BHK" ||
                            m.basicDetailId.Bhk == "7 BHK" ||
                            m.basicDetailId.Bhk == "8 BHK" ||
                            m.basicDetailId.Bhk == "9 BHK" &&
                            m.isConfirmed == true && m.isDecline == false)
                        .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                        .Where(m=>m.basicDetailId.WantTo.ToLower()==rentorsell.ToLower())
                        .AsQueryable ();
                    return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
                }
                
            }
            return null;
        }

        public async Task<PageList<SaveModel>> AllPropertyListByPrice (PropertyParams proParams, int minprice, int maxprice, string rentorsell, string address) {
            if (rentorsell == "All") {
                var all1 = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m =>
                        Convert.ToInt32 (m.price.Replace (",", "")) >= minprice &&
                        Convert.ToInt32 (m.price.Replace (",", "")) <= maxprice &&
                        m.isConfirmed == true && m.isDecline == false
                    )
                    .Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all1, proParams.PageNumber, proParams.PageSize);
            }
            if (rentorsell != "All") {
                var all = _context.PostPropertyModel.Include (c => c.basicDetailId)
                    .Include (c => c.locationId)
                    .Where (m => m.basicDetailId.WantTo == rentorsell &&
                        Convert.ToInt32 (m.price.Replace (",", "")) >= minprice &&
                        Convert.ToInt32 (m.price.Replace (",", "")) <= maxprice &&
                        m.isConfirmed == true && m.isDecline == false
                    ).Where (c => c.locationId.Locality.ToLower () == address.ToLower ())
                    .AsQueryable ();
                return await PageList<SaveModel>.CreateAsync (all, proParams.PageNumber, proParams.PageSize);
            }
            return null;
        }

    }
}