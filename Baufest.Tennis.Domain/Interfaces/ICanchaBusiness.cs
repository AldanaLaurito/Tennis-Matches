using Baufest.Tennis.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Baufest.Tennis.Domain.Interfaces
{
    public interface ICanchaBusiness
    {
        IEnumerable<Cancha> ListCanchas();

        Cancha GetCancha(int id);

        void AddCancha(Cancha cancha);

        void UpdateCancha(Cancha cancha);

        int CantPartidosEnFecha(Cancha cancha, DateTime date);
    }
}
