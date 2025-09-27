using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RawPrj.Data.Contexts;
using RawPrj.Domain.Entities;

namespace RawPrj.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //The TourListsController class is derived from ControllerBase,
    // a base class for an MVC controller, but without the view support
    public class TourListsController : ControllerBase
    {

        private readonly TravelDbContext _context;
        public TourListsController(TravelDbContext context)
        {
            _context = context;
        }


        //api/TourLists Get Request
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.tourLists);

        }

        //api/TourLists Post Request
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TourList tourList)
        {
            await _context.tourLists.AddAsync(tourList);
            await _context.SaveChangesAsync();
            return Ok(tourList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var tourList = await _context.tourLists.SingleOrDefaultAsync(tp => tp.Id == id);

            if (tourList == null)
            {
                return NotFound();
            }
            _context.tourLists.Remove(tourList);
            await _context.SaveChangesAsync();
            return Ok(tourList);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TourList tourList)
        {

            _context.Update(tourList);

            await _context.SaveChangesAsync();

            return Ok(tourList);

        }




    }



}