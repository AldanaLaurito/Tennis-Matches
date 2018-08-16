using System.Configuration;
using System.Data.Entity;

namespace Baufest.Tennis.Persistence
{
    public abstract class EntityFrameworkRepository
    {
        private DbContext context;

        protected DbContext Context
        {
            get
            {
                return this.context ?? (this.context = new TennisContext());
            }

            set
            {
                this.context = value;
            }
        }
    }
}
