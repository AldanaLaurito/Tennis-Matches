using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Interfaces;

namespace Baufest.Tennis.Persistence
{
    public class CanchaRepository : EntityFrameworkRepository, ICanchaRepository
    {
        public IEnumerable<Cancha> ListCanchas()
        {
            return Context.Set<Cancha>().ToList();
        }

        public Cancha GetCancha(int id)
        {
            return Context.Set<Cancha>().Find(id);
        }

        public void AddCancha(Cancha cancha)
        {
            Context.Set<Cancha>().Add(cancha);
            Context.SaveChanges();
        }

        public void UpdateCancha(Cancha cancha)
        {
            Context.Set<Cancha>().Attach(cancha);
            Context.Entry(cancha).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
