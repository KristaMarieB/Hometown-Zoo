using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HometownZoo.Models
{
    public static class AnimalService
    {
        /// <summary>
        /// Returns ALL animals from the database
        /// sorted by name in ascending order
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IEnumerable<Animal> GetAnimals(ApplicationDbContext db)
        {
            // Query syntax
            return (from a in db.Animals
                    orderby a.Species
                    select a).ToList();

            //method syntax
            //return db.Animals
            //    .OrderBy(a => a.Species)
            //    .ToList();
        }

        public static void AddAnimal(Animal animal, ApplicationDbContext db)
        {
            if(animal is null)
            {
                throw new ArgumentNullException($"Parameter {nameof(animal)} cannot be null"); // used explicit syntax so if we renmae variable then this updates too
            }

            // TODO: Ensure duplicate names are disallowed... (for me it would same species & same name disallowed)

            db.Animals.Add(animal);
            db.SaveChanges();
        }
    }
}