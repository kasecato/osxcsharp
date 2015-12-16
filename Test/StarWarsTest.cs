using System;
using StarWars.Domain;
using StarWars.Model;
using Xunit;

namespace StarWars.Test
{
    public class StarWarsCrudTest
    {
        [Fact]
        public void CreateTest()
        {
            using (var db = new StarWarsContext())
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