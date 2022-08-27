using FluentAssertions;
using MyTests.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Net.Http;
using Program_PersonASP_NETCoreWebAPI;
using Program_PersonASP_NETCoreWebAPI.Services;
using System.Text;
using Newtonsoft.Json;

namespace MyTests
{
    public class DoctorApiTests
    {
        private readonly ITestOutputHelper _outputHelper;
        public DoctorApiTests(ITestOutputHelper outputHelper) => _outputHelper = outputHelper;

        [Fact]
        public async Task Given_A_Call_To_GetAll_The_Response_Is_Sucessful()
        {
            using var context = new TestContext(_outputHelper).Start();
            var response = await context.Client.GetAsync("Doctor");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsAsync<IEnumerable<Doctor>>();
            content.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Given_A_Call_To_DeleteDoctor_The_Response_Is_Sucessful()
        {
            using var context = new TestContext(_outputHelper).Start();
            var response = await context.Client.DeleteAsync("Doctor/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeEmpty();

        }

        [Fact]
        public async Task Given_A_Call_To_PostDoctor_The_Response_Is_Sucessful()
        {
            using var context = new TestContext(_outputHelper).Start();
            var doctor = new Doctor("John", "Doe", 35, "Dentist", 5);

            var json = JsonConvert.SerializeObject(doctor);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await context.Client.PostAsync("Doctor", data);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeEmpty();
            content.Should().Contain("Doctor");
            doctor.Name.Should().Contain("John");

            var response2 = await context.Client.GetAsync("Doctor");
            var content2 = await response2.Content.ReadAsStringAsync();
            content2.Should().Contain("John");

            var doctors = new List<Doctor>
           {
               //new Doctor("John", "Doe", 35, "Dentist", 5)
               //new Doctor("Hasan", "Abbasi", 61, "Oncologist", 8)
               new Doctor("ALi", "Rezayi", 36, "Dermatologist")
            };
            var content2_1 = await response2.Content.ReadAsAsync<IEnumerable<Doctor>>();
            content2_1.Should().Contain(doctors);
            



        }


        [Fact]
        public async Task Given_A_Call_To_PutDoctor_The_Response_Is_Sucessful()
        {
            using var context = new TestContext(_outputHelper).Start();
            var doctor = new Doctor("John", "Doe", 35, "Dentist", 1);

            var json = JsonConvert.SerializeObject(doctor);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await context.Client.PutAsync("Doctor", data);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeEmpty();
            //content.Should().Contain("John", "Doe");
        }
    }
}