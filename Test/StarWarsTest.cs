using System;
using StarWars.Data;
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
          Born = DateTime.Parse("June 27, 1966").ToUniversalTime(),
        };

        // act

        // SELECT
        var dirEp7Db = db.Directors.Find(dirEp7.DirectorId);

        // DELETE
        if (dirEp7Db != null)
        {
          db.Directors.Remove(dirEp7Db);
          db.SaveChanges();
        }

        // INSERT
        db.Directors.Add(dirEp7);
        var countIns = db.SaveChanges();

        // SELECT
        dirEp7Db = db.Directors.Find(dirEp7.DirectorId);

        // assert
        Assert.Equal(1, countIns);
        Assert.Equal("J. J. Abrams", dirEp7Db.Name);
      }
    }
  }
}