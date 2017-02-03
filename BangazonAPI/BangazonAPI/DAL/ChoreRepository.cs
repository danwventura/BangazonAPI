using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BangazonAPI.Providers.Models;

namespace BangazonAPI.DAL
{
    public class ChoreRepository
    {
        public ChoreManagerContext Context { get; set; }
        public ChoreRepository(ChoreManagerContext _context)
        {
            Context = _context;
        }
        public ChoreRepository() {
            Context = new ChoreManagerContext();
        }

        public void AddNewChore(Chore chore)
        {
            Context.Chores.Add(chore);
            Context.SaveChanges();
        }

        public List<Chore> GetAllChores()
        {
            return Context.Chores.ToList();
        }

        public void DeleteChore(int choreId)
        {
            Chore found_chore = Context.Chores.FirstOrDefault(c => c.ChoreID == choreId);
            Context.Chores.Remove(found_chore);
            Context.SaveChanges();
        }

        public Chore GetChoreById(int chore_ID)
        {
            Chore found_chore = Context.Chores.FirstOrDefault(c => c.ChoreID == chore_ID);
            return found_chore;
        }

        public Chore UpdateChore(Chore updatedChore)
        {
            Chore found_chore = Context.Chores.FirstOrDefault(c => c.ChoreID == updatedChore.ChoreID);

            if (found_chore != null) {
                found_chore.ChoreID = updatedChore.ChoreID;
                found_chore.Name = updatedChore.Name;
                found_chore.Description = updatedChore.Description;
                found_chore.status = updatedChore.status;
                found_chore.CompletedOn = updatedChore.CompletedOn;
                Context.SaveChanges();
                return updatedChore;
            }
            else
            {
                return null;
            }
        }
    }
}