using System.Collections.Generic;
using System.Threading.Tasks;
using RstateAPI.Entities;
using RstateAPI.Entities.CityData;
using RstateAPI.Entities.ContactDetails;
using RstateAPI.Entities.PropertyConfiguration;
using RstateAPI.Helpers;
using RstateAPI.Identity;

namespace RstateAPI.Data {
    public interface ISpacingRepo {

        //lead
        Task<IList<UserContact>> AllLeadProperty (string userId);
        void Add<T> (T entity) where T : class;
        void Delete<T> (T entity) where T : class;
        Task<bool> SaveAll ();
        Task<SaveModel> FormSubmit (SaveModel form);
        Task<PropertyConfig> propertyConfiguration (PropertyConfig proConfig);
        Task<AdminAccessProp> propertyConfiguration (AdminAccessProp adminproConfig);
        Task<PageList<SaveModel>> AllPropertyListByAddress (PropertyParams proParams, string address,string rentorsell);

        Task<PageList<SaveModel>> AllPropertyListByLocality (PropertyParams proParams, string locality,string rentorsell);
        Task<PageList<SaveModel>> AllPropertyListBySector (PropertyParams proParams, string sector,string rentorsell);
        Task<PageList<SaveModel>> AllPropertyListByBGAddress (PropertyParams proParams, string address);
        Task<PageList<SaveModel>> AllPropertyListByBG (PropertyParams proParams);
        Task<PageList<SaveModel>> AllPropertyListByPGFor (PropertyParams proParams, string pgFor, string address);
        Task<PageList<SaveModel>> AllPropertyListByOnlyPGFor (PropertyParams proParams, string pgFor);

        Task<PageList<SaveModel>> AllPropertyListByType (PropertyParams proParams, string type,string rentorsell,string address);

        Task<PageList<SaveModel>> AllPropertyListByType2 (PropertyParams proParams, string type,string type2,string rentorsell,string address);
        Task<PageList<SaveModel>> AllPropertyListByType3 (PropertyParams proParams, string type,string type2,string type3,string rentorsell,string address);
        Task<PageList<SaveModel>> AllPropertyListByType4 (PropertyParams proParams, string type,string type2,string type3,string type4, string rentorsell,string address);
        Task<PageList<SaveModel>> AllPropertyListByType5 (PropertyParams proParams, string type,string type2,string type3,string type4,string type5, string rentorsell,string address);

        Task<PageList<SaveModel>> AllPropertyListByPrice (PropertyParams proParams, int minprice,int maxprice,string rentorsell,string address);
         Task<PageList<SaveModel>> AllPropertyListByBHK (PropertyParams proParams, string type,string rentorsell,string address);
        Task<PageList<SaveModel>>  AllPropertyListByBHK2 (PropertyParams proParams, string type,string type2,string rentorsell,string address);
        Task<PageList<SaveModel>>  AllPropertyListByBHK3 (PropertyParams proParams, string type,string type2,string type3,string rentorsell,string address);
        Task<PageList<SaveModel>>  AllPropertyListByBHK4 (PropertyParams proParams, string type,string type2,string type3,string type4, string rentorsell,string address);

        Task<PageList<SaveModel>> AllPropertyListByAddressAndType (PropertyParams proParams, string address, string type,string rentorsell);
        Task<PropertyConfig> GetpropertyConfiguration (int id);
        Task<AdminAccessProp> GetpropertyAdminConfiguration (string Adminid);

        // SectorModule
        Task<Sector> GetSector (int localityid);
        Task<Sector> SaveSector (Sector sector);
        Task<PageList<SaveModel>> AllPropertyList (PropertyParams proParams);
        Task<PageList<SaveModel>> AllProperty (PropertyParams proParams);
        
        Task<PageList<SaveModel>> AllPendingProperty (PropertyParams proParams);
        Task<PageList<SaveModel>> AllConfirmProperty (PropertyParams proParams);
        Task<PageList<SaveModel>> AllRejectProperty (PropertyParams proParams);
        Task<PageList<SaveModel>> AllHomePropertyList (PropertyParams proParams);

        // Rent Filter API
        Task<PageList<SaveModel>> AllRentPropertyList (PropertyParams proParams);
        Task<PageList<SaveModel>> AllRentConfirmedPropertyList (PropertyParams proParams);
        Task<PageList<SaveModel>> AllRentPendingPropertyList (PropertyParams proParams);
        Task<PageList<SaveModel>> AllRentRejectedPropertyList (PropertyParams proParams);

        // SELL Filter API
        Task<PageList<SaveModel>> AllSellPropertyList (PropertyParams proParams);
        Task<PageList<SaveModel>> AllSellConfirmedPropertyList (PropertyParams proParams);
        Task<PageList<SaveModel>> AllSellPendingPropertyList (PropertyParams proParams);
        Task<PageList<SaveModel>> AllSellRejectedPropertyList (PropertyParams proParams);

        //User Details
        Task<PageList<SaveModel>> UserData (PropertyParams propertyParams, string userId,string propType);
        Task<bool> AdminExist (string userName, string email);

        Task<bool> UserContactExist (string email, string phone, int uniqueId, string OwnerId);

        //Photo
        Task<IEnumerable<Images1>> getPhoto ();
        Task<Images1> getPhoto (int id);
        Task<Images1> GetMainPhotoForUser (int uniqueID);

        //UserContacts
        Task<UserContact> PostUserContact (UserContact userContact);
    }
}