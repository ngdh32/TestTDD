using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTddCore.Entities;
using TestTddInfra.Entities.EF;

namespace TestTddInfra
{
    public static class TestData
    {
        private static List<EfAuthor> _efAuthors = null;
        private static List<EfBook> _efBooks = null;

        public static List<EfAuthor> EfAuthors
        {
            get
            {
                if (_efAuthors == null)
                {
                    GenerateSeedDate();
                }

                return _efAuthors;
            }
        }

        public static List<EfBook> EfBooks
        {
            get
            {
                if (_efBooks == null)
                {
                    GenerateSeedDate();
                }

                return _efBooks;
            }
        }

        public static void GenerateSeedDate()
        {
            var bookIds = 1;
            var authorIds = 1;
            _efAuthors = new Faker<EfAuthor>()
                .StrictMode(false)
                .RuleFor(o => o.Id, f => authorIds++)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .GenerateBetween(1, 10);

            _efBooks = new Faker<EfBook>()
                .StrictMode(false)
                .RuleFor(o => o.Id, f => bookIds++)
                .RuleFor(o => o.Title, (f, u) => f.Random.String())
                .RuleFor(o => o.CreateDate, f => f.Date.Past())
                .GenerateBetween(1, 100);



            _efAuthors.ForEach(o =>
            {
                o.Books = _efBooks.Where(x => x.Author.Id == o.Id).ToList();
            });
        }
    }
}
