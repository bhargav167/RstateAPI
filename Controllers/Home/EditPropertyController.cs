using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RstateAPI.Data;
using RstateAPI.Entities;

namespace RstateAPI.Controllers.Home {
    [ApiController]
    [Route ("api/[controller]")]
    public class EditPropertyController : ControllerBase {
        private readonly StoreContext store;
        private readonly ISpacingRepo _repo;
        public EditPropertyController (StoreContext store, ISpacingRepo repo) {
            this.store = store;
            _repo = repo;
        }

        [HttpGet ("EditProp/{id}")]
        public async Task<ActionResult> GetEditProperty (int id) {
            var all = await store.PostPropertyModel.Include (c => c.basicDetailId).Include (c => c.locationId)
                .Where (c => c.Id == id).FirstOrDefaultAsync ();

            List<Images1> imgs = new List<Images1> ();
            imgs.AddRange (await store.images.Where (c => c.uniqueID == all.uniqueID).ToListAsync ());

            return Ok (new { all, imgs });
        }

        //Post UserDetails
        [HttpPost("Rent/{id}/{ageOfProp}/{bathroom}/{balcony}/{furnishedType}/{coverparking}/{openparking}/{maintainacecharge}/{teanentType}/{monthlyRent}/{IsSecurity}/{SecurityAmt}/{buildUpArea}")]
        public async Task<ActionResult> PostUserDetails (int id, string ageOfProp, string bathroom, string balcony,string furnishedType,string coverparking,string openparking,string maintainacecharge,string teanentType,string monthlyRent,string IsSecurity,string SecurityAmt,string buildUpArea,[FromBody] string avalbledate) {
            var basic = await store.basicDetails.FirstOrDefaultAsync (c => c.Id == id);
            if (basic == null)
                return NoContent ();

                if(monthlyRent==null || maintainacecharge==null || buildUpArea==null || SecurityAmt==null)
                return NotFound();
                
            basic.propertadyAge = ageOfProp;
            basic.Bathroom = bathroom;
            basic.Balconey = balcony;
            basic.furnishedType=furnishedType;
            basic.coverParking=coverparking;
            basic.openParking=openparking;
            basic.Maintainescharge=maintainacecharge;
            basic.teneandType=teanentType;
            basic.mothlyRent=monthlyRent;
            basic.SecurityDeposite=IsSecurity;
            basic.SecurityDepositeAmt=SecurityAmt;
            basic.BuildUpArea=buildUpArea;
            basic.AvalableFrom=avalbledate;
            store.basicDetails.Update (basic);
            await store.SaveChangesAsync ();
            return Ok (basic);
        }

         //Post UserDetails
        [HttpPost("Sell/{id}/{ageOfProp}/{bathroom}/{balcony}/{furnishedType}/{coverparking}/{openparking}/{constructionstatus}/{brokerage}/{brockrageamt}/{monthlyrent}/{buildUpArea}/{carpetArea}/{transactiontype}")]
        public async Task<ActionResult> PostUserDetailsSell (int id, string ageOfProp, string bathroom, string balcony,string furnishedType,string coverparking,string openparking,string constructionstatus,string brokerage,string brockrageamt, string monthlyRent,string buildUpArea,string carpetArea,string transactiontype) {
            var basic = await store.basicDetails.FirstOrDefaultAsync (c => c.Id == id);
            if (basic == null)
                return NoContent ();

            basic.propertadyAge = ageOfProp;
            basic.Bathroom = bathroom;
            basic.Balconey = balcony;
            basic.furnishedType=furnishedType;
            basic.coverParking=coverparking;
            basic.openParking=openparking;
            basic.ConstructionType=constructionstatus;
            basic.Brokerage=brokerage;
            basic.BrokerageAmt=brockrageamt;
            basic.mothlyRent=monthlyRent;
            basic.BuildUpArea=buildUpArea;
            basic.CarpetArea=carpetArea;
            basic.TransactionType=transactiontype;
            store.basicDetails.Update (basic);
            await store.SaveChangesAsync ();
            return Ok (basic);
        }


       //Post UserDetails
        [HttpPost("Plot/{id}/{transactionType}/{possesionType}/{plotprice}/{plotno}/{plotarea}/{sqfeet}/{length}/{width}/{wfr}/{builduparea}/{carpetarea}")]
        public async Task<ActionResult> PostUserDetailsPlot (int id,string transactionType,string possesionType,string plotprice,string plotno,string plotarea,string sqfeet,string length,string width,string wfr,string builduparea,string carpetarea) {
            var basic = await store.basicDetails.FirstOrDefaultAsync (c => c.Id == id);
            if (basic == null)
                return NoContent ();

             basic.TransactionType=transactionType;
             basic.PossesionType=possesionType;
             basic.plotPrice=plotprice;
             basic.PloatArea=plotarea;
             basic.FloorRange=plotno;
             basic.AreaUnit=sqfeet;
             basic.length=length;
             basic.Width=width;
             basic.Wfr=wfr;
             basic.BuildUpArea=builduparea;
             basic.CarpetArea=carpetarea;

            store.basicDetails.Update (basic);
            await store.SaveChangesAsync ();
            return Ok (basic);
        }

       //Post UserDetails
                                                                                                                                                         
        [HttpPost("PG/{id}/{pgname}/{totalbed}/{pgfor}/{mealavalable}/{bestsuitedfor}/{roomtype}/{totalbeds}/{rent}/{securitydepo}/{facilityoffer}/{notice}/{locks}/{commonarea}/{manageby}/{propstay}/{nonveg}/{oppositesex}/{visitorallowed}/{gaurgiunall}/{drinking}/{smoking}")]
        public async Task<ActionResult> PostUserDetailsPG (int id,string pgname ,int totalbed,string pgfor,string mealavalable,
        string bestsuitedfor,string roomtype, int totalbeds ,string rent,string securitydepo,string facilityoffer,int notice,int locks, 
        string commonarea,string manageby,string propstay,string nonveg, string oppositesex,string visitorallowed,string gaurgiunall,string drinking,string smoking) {
            var basic = await store.basicDetails.FirstOrDefaultAsync (c => c.Id == id);
            if (basic == null)
                return NoContent ();

                basic.PgName=pgname;
                basic.ToatalBed=totalbed;
                basic.PgFor=pgfor;
                basic.MealAvalable=mealavalable;
                basic.PgSuitedFor=bestsuitedfor;
                basic.RoomType=roomtype;
                basic.BedInRoom=totalbeds;
                basic.PGRent=rent;
                basic.PgSecurityDeposite=securitydepo;
                basic.FacilitiesOffered=facilityoffer;
                basic.NoticePeriod=notice;
                basic.LockPeriod=locks;
                basic.CommonArea=commonarea;
                basic.PropertyManageBy=manageby;
                basic.PropertyManageStay=propstay;
                basic.NonVegAllowed=nonveg;
                basic.OppositeSex=oppositesex;
                basic.VisitorAllowed=visitorallowed;
                basic.GardianAllowed=gaurgiunall;
                basic.DrinkingAllowed=drinking;
                basic.SmokingAllowed=smoking; 

            store.basicDetails.Update (basic);
            await store.SaveChangesAsync ();
            return Ok (basic);
        }

    }
}