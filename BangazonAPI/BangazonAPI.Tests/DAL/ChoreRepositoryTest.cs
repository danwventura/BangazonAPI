using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BangazonAPI.DAL;
using Moq;
using BangazonAPI.Providers.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BangazonAPI.Tests.DAL
{
    [TestClass]
    public class ChoreRepositoryTest
    {
        private Mock<ChoreManagerContext> mock_context { get; set; }
        private ChoreRepository repo { get; set; }
        private Mock<DbSet<Chore>> mock_chores { get; set; }
        private List<Chore> chores { get; set; }

        public void ConnectMocksToDatastore()
        {
            var query_chores = chores.AsQueryable();

            mock_chores.As<IQueryable<Chore>>().Setup(m => m.Provider).Returns(query_chores.Provider);
            mock_chores.As<IQueryable<Chore>>().Setup(m => m.Expression).Returns(query_chores.Expression);
            mock_chores.As<IQueryable<Chore>>().Setup(m => m.ElementType).Returns(query_chores.ElementType);
            mock_chores.As<IQueryable<Chore>>().Setup(m => m.GetEnumerator()).Returns(() => query_chores.GetEnumerator());

            mock_context.Setup(c => c.Chores).Returns(mock_chores.Object);

            mock_chores.Setup(d => d.Add(It.IsAny<Chore>())).Callback((Chore d) => chores.Add(d));
            mock_chores.Setup(d => d.Remove(It.IsAny<Chore>())).Callback((Chore d) => chores.Remove(d));
        }



        [TestInitialize]
        public void Initialize()
        {
            mock_chores = new Mock<DbSet<Chore>>();
            chores = new List<Chore>();
            mock_context = new Mock<ChoreManagerContext>();
            repo = new ChoreRepository(mock_context.Object);
            ConnectMocksToDatastore();

        }


        [TestMethod]
        public void EnsureCanAddInstanceOfChore()
        {
            Chore chore1 = new Chore {ChoreID = 1, Name = "Mow the Lawn", status = Chore.Status.InProgress, Description = "Duh", CompletedOn = DateTime.Now };
            repo.AddNewTask(chore1);
            int expected_count = 1;
            int actual_count = repo.Context.Chores.Count();
            Assert.AreEqual(expected_count, actual_count);

        }


        [TestMethod]
        public void EnsureCanCreateInstanceOfRepo()
        {
            ChoreRepository repo = new ChoreRepository();
            Assert.IsNotNull(repo);
        }


    }

}

