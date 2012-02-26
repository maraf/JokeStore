using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JokeStore.Core.Entity;

namespace JokeStore.Core.Repository
{
    public interface IEntryRepository
    {
        IQueryable<Entry> Entries { get; }

        IQueryable<Entry> NotApprovedEntries { get; }

        IQueryable<Vote> Votes { get; }

        void Save(Entry entry);

        bool AddVote(int entryID, Vote vote);

        void Delete(Entry entry);
    }
}
