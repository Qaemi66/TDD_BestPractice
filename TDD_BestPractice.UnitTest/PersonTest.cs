using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDD_Sample.UnitTest
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void SetNationalCode_WithInValidNationalCode() {
            //Arrange
            IPerson person = GetPersonStub();


            //Assign and Act
            try
            {
                person.SetNationalCode("a");
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            
            Assert.Fail();
        }

        public void SetNationalCode_LengthIsEqual10()
        {
            //Arrange
            IPerson person = GetPersonStub();


            //Assign and Act
            try
            {
                person.SetNationalCode("a");
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }

            Assert.Fail();
        }

        private IPerson GetPersonStub()
        {
           return new Person("ali", "kazemi");
        }
    }
}
