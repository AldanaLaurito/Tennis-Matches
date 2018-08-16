using System.Collections.Generic;

using Baufest.Tennis.Domain.Entities;

namespace Baufest.Tennis.Domain.Interfaces
{
    public interface ICanchaRepository
    {
        IEnumerable<Cancha> ListCanchas();

        Cancha GetCancha(int id);

        void AddCancha(Cancha cancha);

        void UpdateCancha(Cancha cancha);
    }
}
