using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void AddAnimal_NewAnimalShouldCallAddAndSaveChanges()
        {
            // Arrange
            Mock<DbSet<Animal>> mockAnimals = GetMockAnimalDBSet();
            Mock<ApplicationDbContext> mockDb = GetMockDB(mockAnimals);

            Animal a = new Animal() { Species = "Elephant", Name = "Tantor" };

            // Act
            AnimalService.AddAnimal(a, mockDb.Object);

            // Assert
            mockAnimals.Verify(m => m.Add(a), Times.Once);
            mockDb.Verify(m => m.SaveChanges());


        }

        private static Mock<ApplicationDbContext> GetMockDB(Mock<DbSet<Animal>> mockAnimals)
        {
            var mockDb = new Mock<ApplicationDbContext>();
            mockDb.Setup(db => db.Animals)
                  .Returns(mockAnimals.Object);
            return mockDb;
        }

        [TestMethod]
        public void GetAnimals_ShouldReturnAllAnimalsSortedBySpecies()
        {
            // Set up Mock database and mock animal table

            // Create a mock of Animals
            Mock<DbSet<Animal>> mockAnimals = GetMockAnimalDBSet();

            // Create mock database
            var mockDb = GetMockDB(mockAnimals);

            // ACT
            IEnumerable<Animal> allAnimals = AnimalService.GetAnimals(mockDb.Object);

            // ASSERT - all animals are returned
            Assert.AreEqual(2, allAnimals.Count());

            // ASSERT - animals are sorted by Species (ascending)
            Assert.AreEqual("Lion", allAnimals.ElementAt(0).Species);
            Assert.AreEqual("Tiger", allAnimals.ElementAt(1).Species);

        }

        [TestMethod]
        public void AddAnimal_ShouldThrowNullArgumentException()
        {
            Animal a = null;

            // Assert -> Act
            Assert.ThrowsException<ArgumentNullException> ( () => AnimalService.AddAnimal( a, new ApplicationDbContext() ) );
        }

        private Mock<DbSet<Animal>> GetMockAnimalDBSet()
        {
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
            return mockAnimals;
        }
    }
}