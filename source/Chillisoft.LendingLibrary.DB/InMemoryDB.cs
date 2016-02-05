using System.Collections.Generic;
using System.IO;
using Chillisoft.LendingLibrary.Core.Domain;

namespace Chillisoft.LendingLibrary.DB
{
    public static class InMemoryDB
    {
        private static string dataFile = @"c:\data\borrowers.json";
        static InMemoryDB()
        {
            if (File.Exists(dataFile))
            {
                Borrowers = JsonHelpers.ConvertFromJson<List<Borrower>>(File.ReadAllText(dataFile));
            }
            Borrowers = new List<Borrower>();
            Titles = new List<Title>
            {
                new Title {Id = 1,Description = "Mr"},
                new Title {Id = 2,Description = "Mrs"},
                new Title {Id = 3,Description = "Dr"},
            };
        }

        public static List<Borrower> Borrowers { get; set; }
        public static List<Title> Titles { get; set; }

        public static int SaveChanges()
        {
            var json = JsonHelpers.ConvertToJson(Borrowers);
            File.WriteAllText(dataFile, json);
            return Borrowers.Count;
        }
    }
}