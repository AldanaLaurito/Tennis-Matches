using System.Collections.Generic;

using Baufest.Tennis.Domain.Entities;

namespace Baufest.Tennis.Domain.Interfaces
{
    public interface IPartidoRepository
    {
        IEnumerable<Partido> ListPartidos();

        Partido GetPartido(int id);

        void AddPartido(Partido partido);

        void UpdatePartido(Partido partido);

        void DeletePartido(Partido partido);
    }
}
