using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SaveBudgetApi.Models
{

    public class Item
    {
        public string Id
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public string Desc
        {
            get;
            set;
        }
        public double Amount
        {
            get;
            set;
        }
        public string Unit
        {
            get;
            set;
        }
        public double UnitPrice
        {
            get;
            set;
        }
        public string Discount
        {
            get;
            set;

        }
        public DateTime OfferStartDate
        {
            get;
            set;
        }
        public DateTime OfferEndDate
        {
            get;
            set;
        }
        public string ItemImagePath
        {
            get;
            set;
        }
        //public List<string> Tags
        //{
        //    get;
        //    set;
        //}
        //public List<string> Receipes
        //{
        //    get;
        //    set;
        //}
        //public string VendorId
        //{
        //    get;
        //    set;
        //}
      
        public Vendor Vendor
        {
            get;
            set;
        }
    }
}
