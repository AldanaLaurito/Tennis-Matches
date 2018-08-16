using System.Collections.Generic;
using Baufest.Tennis.Domain.Entities;

namespace Baufest.Tennis.Domain.Interfaces
{
    public interface IJugadorBusiness
    {
        IEnumerable<Jugador> ListJugadores();

        Jugador GetJugador(int id);

        void AddJugador(Jugador jugador);

        void UpdateJugador(Jugador jugador);

        void ReCalculateRanking(int id);
    }
}
