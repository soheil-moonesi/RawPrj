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


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.tourPackages);

        }



    }



}