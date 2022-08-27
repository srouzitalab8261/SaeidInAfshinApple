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
        
    }
}
