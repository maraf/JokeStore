using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JokeStore.Core.Repository
{
    public interface IDomainResolver
    {
        string DomainName { get; }
    }
}
