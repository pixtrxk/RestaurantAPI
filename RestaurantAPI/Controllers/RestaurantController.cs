using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant); 
            _dbContext.SaveChanges();

            return Created($"/api/restaurant/{restaurant.Id}",null);
        }

        [HttpGet]
        public ActionResult <IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r=>r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var restaurantsDtos = _mapper
                .Map<List<RestaurantDto>>(restaurants);
                

            return(Ok(restaurantsDtos));
        }

        [HttpGet("{id}")]
        public ActionResult <RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == id);

            var restaurantDtos = _mapper
                .Map<RestaurantDto>(restaurant);

            if(restaurantDtos is null)
            {
                return NotFound();
            }
            return(Ok(restaurantDtos));
        }
    }

}
