using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace JobSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        public async Task<IActionResult> Index()
        {
            var cityList = await _cityService.SGetListOrderByAsync();
            return View(cityList);
        }

        public async Task<IActionResult> ReadJsonAndSaveToDb()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "az.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("JSON file not found.");
            }

            var jsonData = System.IO.File.ReadAllText(filePath);
            var jsonObject = JObject.Parse(jsonData);

            var names = jsonObject["features"]
             .Select(feature => feature["properties"]["name"].ToString())
             .OrderBy(name =>  name)
             .ToList();

            if (names.Count == 0)
            {
                return NotFound("No name found.");
            }

            foreach (var name in names)
            {
                var cityName = await _cityService.SGetAllAsync(x => x.Name == name);
                if (cityName.Count == 0)
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        var city = new City
                        {
                            Name = name,
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        await _cityService.SCreateAsync(city);
                    }
                }
            }

            return Ok("Names created successfully");
        }

    }
}
