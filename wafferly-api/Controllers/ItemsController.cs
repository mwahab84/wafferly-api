using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WafferlyApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WafferlyApi.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly BSaverContext _dbContext;

        public ItemsController(BSaverContext context)
        {
            
            _dbContext = context;
            if (_dbContext.Items.Count() == 0)
            {
                Vendor[] vendor = { new Vendor { Id = "78dfbdcb-f1df-4957-a19e-0083fb2f8616", Title = "Carrefour", Desc = "This is Carrefour Hypermarket", VendorLogoPath = "http://bit.ly/carrfqa" },
                new Vendor { Id = "e9446897-fbd8-4dfc-8610-a2291f6662ec", Title = "Al Meera", Desc = "Al Meera Hypermarket in Qatar", VendorLogoPath = "http://bit.ly/qameera" }};
             
                _dbContext.Items.AddRange(new List<Item> {new Item{Id="9c21249e-1fcc-4b82-8fd4-48290e36b170", Title="Potato", Desc="A description of the staple and how to use or save",Unit="Kg",
                    UnitPrice=13.75, Amount=1, OfferStartDate=DateTime.Today, Discount="http://bit.ly/sofferv",
                    OfferEndDate=DateTime.Today, ItemImagePath="http://bit.ly/ezaypotato",Vendor= vendor[0]},
                new Item{Id="0a5b9aa2-9d8c-4063-8e2a-7c157cd18288", Title="Onions", Desc="A description of the staple and how to use or save",Unit="Pack",
                    UnitPrice=9.35, Amount=1, OfferStartDate=DateTime.Today, Discount="",
                    OfferEndDate=DateTime.Today, ItemImagePath="http://bit.ly/onionsqa", Vendor=vendor[0]},
                new Item{Id="376c04bf-861a-4d6e-83c6-7213948809f8", Title="Milk", Desc="A description of the staple and how to use or save",Unit="Ltr",
                    UnitPrice=10.50, Amount=5, OfferStartDate=DateTime.Today, Discount="http://bit.ly/sofferv",
                    OfferEndDate=DateTime.Today, ItemImagePath="http://bit.ly/milkbqa", Vendor=vendor[1]}});
             _dbContext.SaveChanges();   
            }
        }

        [HttpGet]
        public IEnumerable<Item> GetAllItems()
        {
            var query = _dbContext.Items.Include(i => i.Vendor).OrderBy(i => i.UnitPrice).ToList();
            

            return query;
        }

        [HttpGet("{id}",Name ="ItemDetails")]
        public IActionResult GetItemById(string id){
            
            var item= _dbContext.Items.Include(i => i.Vendor).Where(i => i.Id == id).SingleOrDefault();
            if (item ==null)
                return NotFound();

            return new ObjectResult(item);
        }

        [HttpGet("Find/{keyword}", Name = "FilterItems")]
        public IEnumerable<Item> FindItems(string keyword)
        {
            return _dbContext.Items.Include(i => i.Vendor)
                                   .Where(i => i.Title.ToLower().Contains(keyword.ToLower()))
                                   .OrderBy(i =>i.UnitPrice).ToList();
        }

        [HttpGet("v/{vid}", Name ="VendorItems")]
        public IEnumerable<Item> GetItemsByVendor(string vid)
        {

            return _dbContext.Items.Include(i => i.Vendor).Where(iv => iv.Vendor.Id == vid).OrderBy(i =>i.UnitPrice).ToList();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Item item){

            if (item == null)
                return BadRequest();

            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtRoute("ItemDetails", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Item item){

            if (item == null || item.Id != id)
                return BadRequest();

            var selectedItem = _dbContext.Items.Where(i => i.Id == id).SingleOrDefault();

            if (selectedItem == null)
                return NotFound();

            selectedItem.Title = "Modified item through Update Method!";
            selectedItem.Desc = "Added description from Update method!";

            _dbContext.Items.Update(selectedItem);
            _dbContext.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id){
            var item = _dbContext.Items.Where(i => i.Id == id).SingleOrDefault();
            if (item == null)
                return NotFound();

            _dbContext.Items.Remove(item);
            _dbContext.SaveChanges();

            return new NoContentResult();
        }
    }
}
