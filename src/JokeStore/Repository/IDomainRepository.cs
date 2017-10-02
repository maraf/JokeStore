using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JokeStore.Core.Entity;

namespace JokeStore.Core.Repository
{
    public interface IDomainRepository
    {
        IQueryable<Domain> Domains { get; }

        Domain Current { get; }

        void Save(Domain domain);

        void Delete(Domain domain);
    }
}
