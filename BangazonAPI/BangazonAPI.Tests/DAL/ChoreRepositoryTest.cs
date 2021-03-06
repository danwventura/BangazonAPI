﻿using System;
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
        public void EnsureCanCreateInstanceOfRepo()
        {
            ChoreRepository repo = new ChoreRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureCanAddInstanceOfChore()
        {
            //Arrange
            Chore chore1 = new Chore {ChoreID = 1, Name = "Mow the Lawn", status = Chore.Status.InProgress, Description = "Duh", CompletedOn = DateTime.Now };

            //Act
            repo.AddNewChore(chore1);
            int expected_count = 1;
            int actual_count = repo.Context.Chores.Count();

            //Assert
            Assert.AreEqual(expected_count, actual_count);

        }

        [TestMethod]
        public void EnsureCanGetAllInstancesOfChore()
        {

            //Arrange
            Chore chore1 = new Chore { ChoreID = 1, Name = "Mow the Lawn", status = Chore.Status.InProgress, Description = "Duh", CompletedOn = DateTime.Now };
            Chore chore2 = new Chore { ChoreID = 2, Name = "Water the Cat", status = Chore.Status.Complete, Description = "Meow", CompletedOn = DateTime.Now };

            //Act
            repo.AddNewChore(chore1);
            repo.AddNewChore(chore2);
            int expected_count = 2;
            int actual_count = repo.GetAllChores().Count;

            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void EnsureCanGetChoreByID()
        {
            //Arrange
            Chore chore1 = new Chore { ChoreID = 1, Name = "Mow the Lawn", status = Chore.Status.InProgress, Description = "Duh", CompletedOn = DateTime.Now };
            Chore chore2 = new Chore { ChoreID = 2, Name = "Water the Cat", status = Chore.Status.Complete, Description = "Meow", CompletedOn = DateTime.Now };

            //Act
            repo.AddNewChore(chore1);
            repo.AddNewChore(chore2);
            int expected_id = 2;
            int actual_id = repo.GetChoreById(2).ChoreID;

            //Assert
            Assert.AreEqual(expected_id, actual_id);

        }

        [TestMethod]
        public void EnsureCanUpdateChoreDetails()
        {
            //Arrange
            Chore oldChore = new Chore { ChoreID = 1, Name = "oldChore", Description = "Duh", CompletedOn =  new DateTime(2016, 12, 20)};

            //Act
            repo.AddNewChore(oldChore);
            Chore updatedChore = new Chore { ChoreID = 1, Name = "newChore", Description = "Yup", CompletedOn = DateTime.Now };
            repo.UpdateChore(updatedChore);
            //Assert
            Assert.AreEqual("newChore", chores.FirstOrDefault(c => c.ChoreID == 1).Name);
            Assert.AreEqual("Yup", chores.FirstOrDefault(c => c.ChoreID ==1).Description);
            Assert.AreEqual(updatedChore.CompletedOn, chores.FirstOrDefault(c => c.ChoreID == 1).CompletedOn);

        
        }



        [TestMethod]
        public void EnsureCanDeleteInstanceOfChore()
        {
            //Arrange
            Chore chore1 = new Chore { ChoreID = 1, Name = "Mow the Lawn", status = Chore.Status.InProgress, Description = "Duh", CompletedOn = DateTime.Now };
            Chore chore2 = new Chore { ChoreID = 2, Name = "Water the Cat", status = Chore.Status.Complete, Description = "Meow", CompletedOn = DateTime.Now };

            //Act
            repo.AddNewChore(chore1);
            repo.AddNewChore(chore2);
            repo.DeleteChore(2);
            int expected_count = 1;
            int actual_count = repo.GetAllChores().Count;
            //Assert
            Assert.AreEqual(expected_count, actual_count);
        }




    }

}

