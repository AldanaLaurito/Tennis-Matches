using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Interfaces;

namespace Baufest.Tennis.Persistence
{
    public class JugadorRepository : EntityFrameworkRepository, IJugadorRepository
    {
        public IEnumerable<Jugador> ListJugadores()
        {
            return Context.Set<Jugador>().ToList();
        }

        public Jugador GetJugador(int id)
        {
            return Context.Set<Jugador>().Find(id);
        }

        public void AddJugador(Jugador jugador)
        {
            Context.Set<Jugador>().Add(jugador);
            Context.SaveChanges();
        }

        public void UpdateJugador(Jugador jugador)
        {
            Context.Set<Jugador>().Attach(jugador);
            Context.Entry(jugador).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
