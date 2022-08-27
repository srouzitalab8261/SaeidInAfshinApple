using Program_PersonASP_NETCoreWebAPI;
using Program_PersonASP_NETCoreWebAPI.Services;
using System.Collections.Generic;
using Xunit;

namespace MyTests
{
    public class StubValidator : IValidator {
        public bool Validate(string name)
        {
            return true;
        }
    }
    public class DoctorServicesTests
    {
        IValidator stubValidator;
        public DoctorServicesTests() {
            stubValidator = new StubValidator();
        }
        private List<Doctor> GetSampleData()
        {
            return new List<Doctor>()
            {
                new Doctor{ Name = "DoctorWithLongName",Family= "D1Family", Id = 5 , Age=30,Speciality = Specialities.Dentist},
                new Doctor{ Name = "D2",Family= "D2Family", Id = 6 , Age=40,Speciality = Specialities.Cardiologist},
                new Doctor{ Name = "D3",Family= "D3Family", Id = 7 , Age=50,Speciality = Specialities.Anesthesiologist},
                new Doctor{ Name = "D4",Family= "D4Family", Id = 8 , Age=60,Speciality = Specialities.Endocrinologist},
            };
        }

        [Fact]
        public void Give_List_Of_Doctors_GetAll_Returns_All_The_List()
        {
            //Arrange
            var doctors = GetSampleData();
            

            var sut = new DoctorServices(stubValidator , doctors);

            //Act

            var result = sut.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Count);
            Assert.Contains(result, x => x.Name == "D2");
        }

        [Fact]
        public void Given_List_Of_Doctors_DeleteDoctor_Works_As_Expected()
        {
            //Arrange
            var doctors = GetSampleData();
            var sut = new DoctorServices(stubValidator, doctors);

            //Act
            sut.DeleteDoctor(5);
            var result = sut.GetAll();

            //Assert
            Assert.DoesNotContain(result, x => x.Id == 5);
        }
    }
}
