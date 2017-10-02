using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JokeStore.Core.Repository.EntityFramework
{
    public abstract class BaseRepository
    {
        protected readonly DataContext context;

        public BaseRepository(DataContext context)
        {
            this.context = context;
        }
    }
}
