using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using WorldDetails.Data;
using WorldDetails.Models;
using WorldDetails.Models.Entities;

namespace WorldDetails.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(DataContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public JsonResult GetData()
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var draw = Request.Form["draw"].FirstOrDefault();


            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();


            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();


            var searchValue = Request.Form["search[value]"].FirstOrDefault();


            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");


            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");



            var data = _context.Set<Country>().AsQueryable();

            //get total count of data in table
            totalRecord = data.Count();

            // search data when search value found
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x =>
                  x.Code.ToLower().Contains(searchValue.ToLower())
                  || x.Name.ToLower().Contains(searchValue.ToLower())
                  || x.FullName.ToLower().Contains(searchValue.ToLower())
                  || x.Continent_code.ToString().ToLower().Contains(searchValue.ToLower())

                );
            }

            // get total count of records after search 
            filterRecord = data.Count();

            ////sort data
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                data = data.OrderBy(sortColumn + " " + sortColumnDirection);


            //pagination
            var empList = data.Skip(skip).Take(pageSize).ToList();

            var returnObj = new { draw = draw, recordsTotal = totalRecord, recordsFiltered = filterRecord, data = empList };
            return Json(returnObj);

        }
    }
}