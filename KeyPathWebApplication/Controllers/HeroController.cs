using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KeyPathWebApplication.Models;

namespace KeyPathWebApplication.Controllers
{
    public class HeroController : Controller
    {
        private readonly KeyPathContext _context;

        public HeroController(KeyPathContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Default view to list, sort and filter the Heroes
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="searchValue"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchValue,
            int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GenderSortParm"] = sortOrder == "gender" ? "gender_desc" : "gender";
            ViewData["EyeColorSortParm"] = sortOrder == "eyecolor" ? "eyecolor_desc" : "eyecolor";
            ViewData["HairColorSortParm"] = sortOrder == "haircolor" ? "haircolor_desc" : "haircolor";
            ViewData["HeightSortParm"] = sortOrder == "height" ? "height_desc" : "height";
            ViewData["AlignmentSortParm"] = sortOrder == "alignment" ? "alignment_desc" : "alignment";
            ViewData["PublisherSortParm"] = sortOrder == "publisher" ? "publisher_desc" : "publisher";
            ViewData["WeightSortParm"] = sortOrder == "weight" ? "weight_desc" : "weight";
            
            //this resets to the first page of the list if we have a new search value
            if (searchValue != null)
                pageNumber = 1;
            else
                searchValue = currentFilter;

            ViewData["CurrentFilter"] = searchValue;

            IQueryable<HeroModel> heroes;


            if (!String.IsNullOrEmpty(searchValue))
                heroes = from s in _context.Heroes.Where(s => s.Name.Contains(searchValue) || s.Name == searchValue) select s;
            else
                heroes = from s in _context.Heroes select s;

            //see if we have a sort order selected, if we do, apply it
            switch (sortOrder)
            {
                case "name_desc":
                    heroes = heroes.OrderByDescending(s => s.Name);
                    break;
                case "gender":
                    heroes = heroes.OrderBy(s => s.Gender);
                    break;
                case "gender_desc":
                    heroes = heroes.OrderByDescending(s => s.Gender);
                    break;
                case "eyecolor":
                    heroes = heroes.OrderBy(s => s.EyeColor);
                    break;
                case "eyecolor_desc":
                    heroes = heroes.OrderByDescending(s => s.EyeColor);
                    break;
                case "haircolor":
                    heroes = heroes.OrderBy(s => s.HairColor);
                    break;
                case "haircolor_desc":
                    heroes = heroes.OrderByDescending(s => s.HairColor);
                    break;
                case "height":
                    heroes = heroes.OrderBy(s => s.Height);
                    break;
                case "height_desc":
                    heroes = heroes.OrderByDescending(s => s.Height);
                    break;
                case "alignment":
                    heroes = heroes.OrderBy(s => s.Alignment);
                    break;
                case "alignment_desc":
                    heroes = heroes.OrderByDescending(s => s.Alignment);
                    break;
                case "publisher":
                    heroes = heroes.OrderBy(s => s.Publisher);
                    break;
                case "publisher_desc":
                    heroes = heroes.OrderByDescending(s => s.Publisher);
                    break;
                case "weight":
                    heroes = heroes.OrderBy(s => s.Weight);
                    break;
                case "weight_desc":
                    heroes = heroes.OrderByDescending(s => s.Weight);
                    break;

                default:
                    heroes = heroes.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10; //TODO: add page length to a configuration item
            return View(await PaginatedList<HeroModel>.CreateAsync(heroes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


    }
}
