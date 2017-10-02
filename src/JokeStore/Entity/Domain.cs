using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JokeStore.Core.Entity
{
    public class Domain
    {
        public int ID { get; set; }
        public string Url { get; set; }

        public bool AutoApprove { get; set; }

        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public string CssUrl { get; set; }
    }
}
