using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmCollection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        public FilmController()
        {
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            var films = new List<Film>
            {
                {
                    new Film
                    {
                        Id = 1,
                        Title = "Harry Potter",
                        Director = "David Yates",
                        Score = 3,
                    } 
                }
            };

            return Ok(films);
        }
    }
}
