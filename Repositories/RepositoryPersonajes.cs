﻿using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajesAWS.Repositories
{
    public class RepositoryPersonajes
    {
        private PersonajesContext context;

        public RepositoryPersonajes(PersonajesContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await this.context.Personajes.ToListAsync();
        }

        private async Task<int> GetMaxIdPersonajeAsync()
        {
            return await this.context.Personajes.MaxAsync(x => x.IdPersonaje) + 1;
        }

        public async Task CreatePersonajeAsync(string nombre, string imagen)
        {
            Personaje p = new Personaje();
            p.IdPersonaje = await this.GetMaxIdPersonajeAsync();
            p.Nombre = nombre;
            p.Imagen = imagen;
            await this.context.Personajes.AddAsync(p);
            await this.context.SaveChangesAsync();
        }

        public async Task<Personaje> GetPersonajeByIdAsync(int id)
        {
            return await this.context.Personajes.FirstOrDefaultAsync(x => x.IdPersonaje == id);
        }
        
        public async Task UpdatePersonajeAsync(int id, string nombre, string imagen)
        {
            Personaje p = await this.GetPersonajeByIdAsync(id);
            if(p != null)
            {
                p.Nombre = nombre;
                p.Imagen = imagen;
                this.context.Personajes.Update(p);
                await this.context.SaveChangesAsync();
            }
        }
    }

}
