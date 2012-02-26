using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JokeStore.Core.Entity;
using System.Data;

namespace JokeStore.Core.Repository.EntityFramework
{
    public class Repository : BaseRepository, IRepository
    {
        public IDomainResolver Resolver { get; protected set; }

        public IQueryable<Entity.Entry> Entries
        {
            get { return context.Entries.Where(e => e.Approved && e.Domain.Url == Resolver.DomainName); }
        }

        public IQueryable<Entity.Entry> NotApprovedEntries
        {
            get { return context.Entries.Where(e => !e.Approved && e.Domain.Url == Resolver.DomainName); }
        }

        public IQueryable<Entity.Vote> Votes
        {
            get { return context.Votes; }
        }

        public IQueryable<Domain> Domains
        {
            get { return context.Domains; }
        }

        public Domain Current
        {
            get { return context.Domains.FirstOrDefault(d => d.Url == Resolver.DomainName); }
        }

        public Repository(IDomainResolver resolver)
        {
            Resolver = resolver;
        }

        public void Save(Entry entry)
        {
            if (entry.ID == 0)
            {
                if (entry.Domain == null)
                    entry.Domain = Domains.First(d => d.Url == Resolver.DomainName);

                context.Entries.Add(entry);
            }
            else
            {
                context.Entry(entry).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public bool AddVote(int entryID, Vote vote)
        {
            Entry entry = Entries.FirstOrDefault(e => e.ID == entryID);
            IQueryable<Vote> old = Votes.Where(v => v.Entry.ID == entryID && v.UserIdentifier == vote.UserIdentifier);
            if (entry != null && old.Count() == 0)
            {
                vote.Entry = entry;
                context.Votes.Add(vote);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Save(Domain domain)
        {
            if (domain.ID == 0)
                context.Domains.Add(domain);
            else
                context.Entry<Domain>(domain).State = EntityState.Modified;

            context.SaveChanges();
        }

        public void Delete(Entry entry)
        {
            context.Entries.Remove(entry);
            context.SaveChanges();
        }

        public void Delete(Domain domain)
        {
            context.Domains.Remove(domain);
            context.SaveChanges();
        }
    }
}
