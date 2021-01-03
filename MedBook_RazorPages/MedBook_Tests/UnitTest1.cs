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
    public class DataBaseMock :  IDataAccess
    {
        public List<Appointment> appointments { get; set; }
        public List<Location> locations { get; set; }

        public List<MedicalService> medicalServices { get; set; }
        public List<Review> reviews { get; set; }
        public List<Users> users { get; set; }

        public List<Appointment> getAppointment()
        {
            return appointments;
        }

        public List<Location> getLocation()
        {
            return locations;
        }

        public List<MedicalService> getMedicalService()
        {
            return medicalServices;
        }

        public List<Review> getReview()
        {
            return reviews;
        }

        public List<Users> getUsers()
        {
            return users;
        }
    }

    public class QuerryDecoratorTests
    {
        private readonly ITestOutputHelper output;

        public QuerryDecoratorTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        private FilterOfRows filterOfRows;
        private DataBaseMock dataBaseMock;

        void TearUp()
        {

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
            m1.Location = location1;
            m1.id = 1;

            MedicalService m2 = new MedicalService();
            m2.Description = "Analiza sangelui";
            m2.Location = location2;
            m2.id = 2;

            // ============= Review =====================
            Review rev1 = new Review();
            rev1.Rating = 3;
            rev1.MedicalServiceId = m1.id;
            rev1.MedicalService = m1;

            Review rev2 = new Review();
            rev2.Rating = 1;
            rev2.MedicalServiceId = m2.id;
            rev2.MedicalService = m2;


            List<Location> locations = new List<Location>();
            locations.Add(location1);
            locations.Add(location2);

            List<MedicalService> services = new List<MedicalService>();
            services.Add(m1);
            services.Add(m2);

            List<Review> reviews = new List<Review>();
            reviews.Add(rev1);
            reviews.Add(rev2);

            List<Users> users = new List<Users>();

           dataBaseMock = new DataBaseMock();
            
            dataBaseMock.locations = locations;
            dataBaseMock.medicalServices = services;
            dataBaseMock.reviews = reviews;
            dataBaseMock.users = users;
            filterOfRows = new FilterOfRows((IDataAccess)dataBaseMock);
        }

        void TearDown()
        {
            dataBaseMock.locations.Clear();
            dataBaseMock.medicalServices.Clear();
            dataBaseMock.reviews.Clear();
            filterOfRows = new FilterOfRows((IDataAccess)dataBaseMock);
        }

        [Fact]
        public void TestEmty()
        {
            TearUp();
            QuerryDecorator q = new QuerryDecorator();
            Assert.NotEmpty(filterOfRows.getMedicalService());
            Assert.NotEmpty(filterOfRows.getLocation());
            Assert.NotEmpty(filterOfRows.getReview());
            Assert.Empty(filterOfRows.getUsers());
            TearDown();
        }

        [Fact]
        public void TestCityAdded()
        {
            TearUp();
            QuerryDecorator querryDecorator = new QuerryDecorator();
            querryDecorator.mCity = "Iasi";
            List<MedicalService> list = filterOfRows.getMedicalService(querryDecorator);
            foreach(var medServ in list)
            {
                output.WriteLine("This is output from {0}", medServ.Description);
                Assert.Equal("Iasi", medServ.Location.City);
            }
            TearDown();

        }

        [Fact]
        public void TestDescAdded()
        {
            TearUp();
            QuerryDecorator querryDecorator = new QuerryDecorator();

            querryDecorator.mDescription = "Analiza";

            List<MedicalService> list = filterOfRows.getMedicalService(querryDecorator);
            
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

            querryDecorator.mDescription = "Analiza";
            querryDecorator.mCity = "Iasi";

            List<MedicalService> list = filterOfRows.getMedicalService(querryDecorator);
            
            foreach (var medServ in list)
            {
                Assert.Contains("Analiza", medServ.Description);
                Assert.Equal("Iasi", medServ.Location.City);
            }
            TearDown();
        }

        
        [Fact]
        public void TestMinRaitingSet()
        {
            TearUp();
            QuerryDecorator querryDecorator = new QuerryDecorator();
            querryDecorator.mMinRating = 3;

            List<MedicalService> list = filterOfRows.getMedicalService(querryDecorator);
            foreach (var medServ in list)
            {
                Assert.True(3 <= filterOfRows.getReview().Where(r => r.MedicalServiceId == medServ.id).Average(r => r.Rating));
            }

            TearDown();
        }

        [Fact]
        public void TestMinRaitingSet2()
        {
            TearUp();
            QuerryDecorator querryDecorator = new QuerryDecorator();
            querryDecorator.mMinRating = 3;

            List<MedicalService> list = filterOfRows.getMedicalService(querryDecorator);
            foreach (var medServ in list)
            {
                Assert.False(3 > filterOfRows.getReview().Where(r => r.MedicalServiceId == medServ.id).Average(r => r.Rating));
            }

            TearDown();
        }

        [Fact]
        public void TestMinRaitingNameSet2()
        {
            TearUp();
            QuerryDecorator querryDecorator = new QuerryDecorator();
            querryDecorator.mMinRating = 3;
            querryDecorator.mDescription = "Analiza";

            List<MedicalService> list = filterOfRows.getMedicalService(querryDecorator);
            foreach (var medServ in list)
            {
                Assert.False(3 > filterOfRows.getReview().Where(r => r.MedicalServiceId == medServ.id).Average(r => r.Rating));
                Assert.Contains("Analiza", medServ.Description);
            }

            TearDown();
        }

        [Fact]
        public void TestMinRaitingNameLocSet2()
        {
            TearUp();
            QuerryDecorator querryDecorator = new QuerryDecorator();
            querryDecorator.mMinRating = 3;
            querryDecorator.mDescription = "Analiza";
            querryDecorator.mCity = "Iasi";

            List<MedicalService> list = filterOfRows.getMedicalService(querryDecorator);
            foreach (var medServ in list)
            {
                Assert.False(3 > filterOfRows.getReview().Where(r => r.MedicalServiceId == medServ.id).Average(r => r.Rating));
                Assert.Contains("Analiza", medServ.Description);
                Assert.Equal("Iasi", medServ.Location.City);
            }

            TearDown();
        }
    }
}
