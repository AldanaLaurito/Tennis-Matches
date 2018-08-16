using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Interfaces;

namespace Baufest.Tennis.Persistence
{
    public class PartidoRepository : EntityFrameworkRepository, IPartidoRepository
    {
        public IEnumerable<Partido> ListPartidos()
        {
            return Context.Set<Partido>()
                .Include(p => p.JugadorLocal )
                .Include(p => p.JugadorVisitante)

                //.Include("JugadorLocal")
                //.Include("JugadorVisitante")
                .AsQueryable();
        }

        public Partido GetPartido(int id)
        {
            return Context.Set<Partido>().Find(id);
        }

        public void AddPartido(Partido partido)
        {
            Context.Set<Partido>().Add(partido);
            Context.SaveChanges();
        }

        public void UpdatePartido(Partido partido)
        {
            Context.Set<Partido>().Attach(partido);
            Context.Entry(partido).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void DeletePartido(Partido partido)
        {
            Context.Set<Partido>().Remove(partido);
            Context.SaveChanges();
        }
    }
}
