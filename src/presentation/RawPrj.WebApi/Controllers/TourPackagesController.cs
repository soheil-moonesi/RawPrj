using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RawPrj.Data.Contexts;
using RawPrj.Domain.Entities;

namespace RawPrj.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //The TourPackagesController class is derived from ControllerBase,
    // a base class for an MVC controller, but without the view support
    public class TourPackgesController : ControllerBase
    {

        private readonly TravelDbContext _context;
        public TourPackgesController(TravelDbContext context)
        {
            _context = context;
        }


        //api/tourpackages Get Request
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.tourPackages);

        }

        //api/tourpackages Post Request
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TourPackage tourPackage)
        {
            await _context.tourPackages.AddAsync(tourPackage);
            await _context.SaveChangesAsync();
            return Ok(tourPackage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var tourPackage = await _context.tourPackages.SingleOrDefaultAsync(tp => tp.Id == id);

            if (tourPackage == null)
            {
                return NotFound();
            }
            _context.tourPackages.Remove(tourPackage);
            await _context.SaveChangesAsync();
            return Ok(tourPackage);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TourPackage tourPackage)
        {

            _context.Update(tourPackage);

            await _context.SaveChangesAsync();

            return Ok(tourPackage);

        }




    }



}