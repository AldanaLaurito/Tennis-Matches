using Baufest.Tennis.Domain.Entities;
using System.Collections.Generic;

namespace Baufest.Tennis.Web.Models
{
    public class CanchasListViewModel
    {
        public IEnumerable<CanchaViewModel> List { get; set; }
    }
}