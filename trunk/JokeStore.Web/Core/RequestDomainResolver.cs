using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JokeStore.Core.Repository;

namespace JokeStore.Web.Core
{
    public class RequestDomainResolver : IDomainResolver
    {
        public string DomainName { get; protected set; }

        public RequestDomainResolver(string domainName)
        {
            DomainName = domainName;
        }
    }
}