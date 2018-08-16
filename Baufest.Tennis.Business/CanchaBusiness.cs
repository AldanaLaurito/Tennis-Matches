using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Interfaces;
using Baufest.Tennis.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Baufest.Tennis.Business
{
    public class CanchaBusiness : ICanchaBusiness
    {
        private readonly ICanchaRepository repository;

        public CanchaBusiness()
        {
            this.repository = new CanchaRepository();
        }

        public CanchaBusiness(ICanchaRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Cancha> ListCanchas()
        {
            return this.repository.ListCanchas();
        }

        public Cancha GetCancha(int id)
        {
            return this.repository.GetCancha(id);
        }

        public void AddCancha(Cancha cancha)
        {
            this.repository.AddCancha(cancha);
        }

        public void UpdateCancha(Cancha cancha)
        {
            this.repository.UpdateCancha(cancha);
        }

        public int CantPartidosEnFecha(Cancha cancha, DateTime date)
        {
            return cancha.Partidoes.Where(m => m.FechaComienzo.Date == date.Date).Count();
        }
    }
}