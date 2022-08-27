using Program_PersonASP_NETCoreWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyTests
{
    public class ValidatorTests
    {
        [Fact]
        public void Give_A_String_Longer_Than_10_Valiade_Returns_False() 
        {
            //Arrange

            var sut = new NameValidator();
            

            //Act
            bool result=sut.Validate("123456789");

            //Assert
            Assert.True(result);
            
        }
    }
}
