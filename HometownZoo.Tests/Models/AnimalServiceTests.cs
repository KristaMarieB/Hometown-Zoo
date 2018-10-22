﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using HometownZoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;

namespace HometownZoo.Models.Tests
{
    [TestClass()]
    public class AnimalServiceTests
    {
        private IQueryable<Animal> animals;

        [TestInitialize]
        public void BeforeTest()
        {
            animals = new List<Animal>()
            {
                new Animal(){AnimalID=1, Species="Tiger", Name="Shere Khan"},
                new Animal(){AnimalID=2, Species="Lion", Name="Simba"}
            }.AsQueryable();
        }

        [TestMethod]
        public void GetAnimals_ShouldReturnAllAnimalsSortedBySpecies()
        {
            // Set up Mock database and mock animal table

            // Create a mock of Animals
            var mockAnimals = new Mock<DbSet<Animal>>();

            mockAnimals.As<IQueryable<Animal>>()
                       .Setup(m => m.Provider)
                       .Returns(animals.Provider);

            mockAnimals.As<IQueryable<Animal>>()
                      .Setup(m => m.Expression)
                      .Returns(animals.Expression);

            mockAnimals.As<IQueryable<Animal>>()
                      .Setup(m => m.GetEnumerator())
                      .Returns(animals.GetEnumerator);

            // Create mock database
            var mockDb = new Mock<ApplicationDbContext>();
            mockDb.Setup(db => db.Animals)
                  .Returns(mockAnimals.Object);

            // ACT
            IEnumerable<Animal> allAnimals = AnimalService.GetAnimals(mockDb.Object);

            // ASSERT
            Assert.AreEqual(2, allAnimals.Count());

        }
    }
}