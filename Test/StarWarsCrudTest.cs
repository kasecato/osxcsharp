using System;
using Xunit;
using StarWars.Models;
using StarWars.ViewModels;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace StarWars.Test
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            // Set up DbContext
            var dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseNpgsql(configuration["Data:ConnectionString"]);
            Db = new StarWarsContext(dbOptions.Options);

            // ... initialize data in the test database ...
            foreach (var row in Db.Directors)
            {
                Db.Remove(row);
            }
            Db.SaveChanges();
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }

        public StarWarsContext Db { get; private set; }
    }

    public class StarWarsCrudTest : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture fixture;

        public StarWarsCrudTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void CreateTest()
        {
            using (var db = fixture.Db)
            {
                // arrange
                var dirEp7 = new Director()
                {
                    DirectorId = 4,
                    Episode = 7,
                    Name = "J. J. Abrams",
                    Born = DateTime.Parse("June 27, 1966")
                };

                // act
                db.Directors.Add(dirEp7);
                var count = db.SaveChanges();

                // assert
                Assert.Equal(1, count);
            }
        }
    }
}

