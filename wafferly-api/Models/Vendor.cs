using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SaveBudgetApi.Models
{
    
    public class Vendor
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
        public string VendorLogoPath
        {
            get;
            set;
        }
        //public string[] Locations
        //{
        //    get;
        //    set;
        //}
       
            [JsonIgnore]
        public ICollection<Item> Items { get; set; }
    }
}
