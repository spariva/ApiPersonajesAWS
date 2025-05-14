using ApiPersonajesAWS.Models;
using ApiPersonajesAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonajesAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController: ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Personaje>>> GetPersonajes()
        {
            return await this.repo.GetPersonajesAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Personaje personaje)
        {
            await this.repo.CreatePersonajeAsync(personaje.Nombre, personaje.Imagen);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Personaje>> GetPersonaje(int id)
        {
            Personaje p = await this.repo.GetPersonajeByIdAsync(id);
            if(p == null)
            {
                return NotFound();
            }
            return p;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdatePersonaje(Personaje personaje)
        {
            Personaje p = await this.repo.GetPersonajeByIdAsync(personaje.IdPersonaje);
            if(p == null){
                return NotFound(); 
            }

            await this.repo.UpdatePersonajeAsync(personaje.IdPersonaje, personaje.Nombre, personaje.Imagen);

            return Ok();
        }

    }
}
