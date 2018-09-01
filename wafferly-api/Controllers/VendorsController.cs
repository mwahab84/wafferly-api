using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WafferlyApi.Models;

namespace WafferlyApi.Controllers
{
    [Route("api/[controller]")]
    public class VendorsController : Controller
    {
        private readonly BSaverContext _dbContext;

        public VendorsController(BSaverContext context)
        {
            _dbContext = context;
            if (_dbContext.Vendors.Count() == 0)
            {
                _dbContext.Vendors.Add(new Vendor { Id = "testVendor", Title = "Carrefour" });
                _dbContext.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Vendor> GetAllVendors()
        {
            return _dbContext.Vendors.ToList();
        }

        [HttpGet("{id}", Name = "VendorDetails")]
        public IActionResult GetVendorById(string id)
        {

            var vendor = _dbContext.Vendors.Where(v => v.Id == id).SingleOrDefault();

            if (vendor == null)
                return NotFound();

            return new ObjectResult(vendor);
        }

        [HttpGet("Find/{keyword}", Name = "FindVendors")]
        public IEnumerable<Vendor> FindVendors(string keyword)
        {

            return _dbContext.Vendors.Where(v => v.Title.ToLower().StartsWith(keyword.ToLower())).ToList();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Vendor vendor)
        {

            if (vendor == null)
                return BadRequest();

            _dbContext.Vendors.Add(vendor);
            _dbContext.SaveChanges();

            return new CreatedAtRouteResult("VendorDetails", new { id = vendor.Id }, vendor);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Vendor vendor)
        {

            if (vendor == null || id != vendor.Id)
                return BadRequest();

            var selectedVendor = _dbContext.Vendors.Where(v => v.Id == id).SingleOrDefault();

            if (selectedVendor == null)
                return NotFound();

            selectedVendor.Title = "Modified title";
            selectedVendor.Desc = "Moified Description";

            _dbContext.Vendors.Update(selectedVendor);
            _dbContext.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id){

            var vendor = _dbContext.Vendors.Where(v => v.Id == id).SingleOrDefault();

            if (vendor == null)
                return BadRequest();
            
            _dbContext.Vendors.Remove(vendor);
            _dbContext.SaveChanges();

            return new NoContentResult();
        }
    }
}