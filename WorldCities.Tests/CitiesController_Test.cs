using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCities.Data.Models;
using WorldCities.Data;
using WorldCities.Controllers;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace WorldCities.Tests
{
    public class CitiesController_Test
    {
        /// <summary>
        /// Test the GetCity() method
        /// </summary>
        [Fact]
        public async void GetCity()
        {
            #region Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "WorldCities")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                context.Add(new City()
                {
                    Id = 1,
                    CountryId = 1,
                    Lat = 1,
                    Lon = 1,
                    Name = "TestCity1"
                });
                context.SaveChanges();
            }
            City city_existing = null;
            City city_nonExisting = null;
            #endregion

            #region Act
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new CitiesController(context);
                city_existing = (await controller.GetCity(1)).Value;
                city_nonExisting = (await controller.GetCity(2)).Value;
            }
            #endregion

            #region Assert
            Assert.NotNull(city_existing);
            Assert.Null(city_nonExisting);
            #endregion
        }
    }
}
