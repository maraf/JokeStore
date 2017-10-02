using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JokeStore.Core.Entity
{
    public class Vote
    {
        public int ID { get; set; }
        public Entry Entry { get; set; }
        public string UserIdentifier { get; set; }
        public int Value { get; set; }
    }
}
