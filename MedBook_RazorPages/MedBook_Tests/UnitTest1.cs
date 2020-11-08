using MedBook_RazorPages.Models;
using MedBook_RazorPages.Resources;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System;
using Xunit.Sdk;
using Xunit.Abstractions;

namespace MedBook_Tests
{
    public class QuerryDecoratorTests
    {
        private List<Location> locations;
        private List<MedicalService> services;

        private readonly ITestOutputHelper output;

        public QuerryDecoratorTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        void TearUp()
        {

            if (locations == null)
            {
                locations = new List<Location>();
            }
            if (services == null)
            {
                services = new List<MedicalService>();
            }

            // ============= Locations =====================
            Location location1 = new Location();
            location1.City = "Iasi";
            location1.StreenName = "T. Codrescu";
            location1.StreenNr = "12";
            location1.id = 1;

            Location location2 = new Location();
            location2.City = "Bucuresti";
            location2.StreenName = "Bl. Mircea Voda";
            location2.StreenNr = "50";
            location2.id = 2;

            // ================= MedicalService ==================
            MedicalService m1 = new MedicalService();
            m1.Description = "Radiologie dentara";
            m1.TargetBodySystem = "sistemul osos";
            m1.LocationId = 1;
            
            MedicalService m2 = new MedicalService();
            m2.Description = "Analiza sangelui";
            m2.LocationId = 2;
            m2.TargetBodySystem = "sistemul circulator";

            locations.Add(location1);
            locations.Add(location2);

            services.Add(m1);
            services.Add(m2);
        }

        void TearDown()
        {
            locations.Clear();
            services.Clear();
        }

        [Fact]
        public void TestEmty()
        {
            TearUp();
            QuerryDecorator q = new QuerryDecorator();
            Assert.NotEmpty(locations.ToList());
            TearDown();
        }

        [Fact]
        public void TestCityAdded()
        {
            TearUp();
            QuerryDecorator querryDecorator = new QuerryDecorator();
            FilterOfRows fl = new FilterOfRows();
            querryDecorator.mCity = "Iasi";
            List<MedicalService> list = fl.getAllMedicalSerices(services, locations, querryDecorator);
            foreach(var medServ in list)
            {
                output.WriteLine("This is output from {0}", medServ.Description);
                Assert.Equal("Iasi", locations.Where(l => l.id == medServ.LocationId).Single().City);
            }
            TearDown();

        }

        [Fact]
        public void TestDescAdded()
        {
            TearUp();
            QuerryDecorator querryDecorator = new QuerryDecorator();
            FilterOfRows fl = new FilterOfRows();

            querryDecorator.mDescription = "Analiza";

            List<MedicalService> list = fl.getAllMedicalSerices(services, querryDecorator);
            
            foreach (var medServ in list)
            {
                Assert.Contains("Analiza", medServ.Description);
            }
            TearDown();
        }

        [Fact]
        public void TestDescAndCityAdded()
        {
            TearUp();
            QuerryDecorator querryDecorator = new QuerryDecorator();
            FilterOfRows fl = new FilterOfRows();

            querryDecorator.mDescription = "Analiza";
            querryDecorator.mCity = "Iasi";

            List<MedicalService> list = fl.getAllMedicalSerices(services, locations, querryDecorator);
            
            foreach (var medServ in list)
            {
                Assert.Contains("Analiza", medServ.Description);
            }
            TearDown();
        }

        
        [Fact]
        public void TestPrividerNameSet()
        {
            TearUp();
            QuerryDecorator querryDecorator = new QuerryDecorator();
            FilterOfRows fl = new FilterOfRows();

            querryDecorator.mTargetBodySystem = "sistemul circulator";

            List<MedicalService> list = fl.getAllMedicalSerices(services, querryDecorator);

            foreach (var medServ in list)
            {
                Assert.Contains("sistemul circulator", medServ.TargetBodySystem);
            }
            TearDown();
        }
    }
}
