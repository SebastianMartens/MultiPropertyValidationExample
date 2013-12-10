using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MultiPropertyValidationExample.Model;
using NUnit.Framework;

namespace MultiPropertyValidationExample.Framework.Test
{

    [TestFixture]
    public class DomainObjectTest
    {
        private ErrorsAwareDomainObject _domainObject;

        [SetUp]
        public void SetUp()
        {
            _domainObject = new GroupAdress(42); // The base class is abstract. For now I use any given implementation for testing...
        }

        [Test]
        public void HasErrors_should_return_true_when_errors_exist()
        {
            // Arrange            
            _domainObject.SetError("X", "X");

            // Act            
            var result = _domainObject.HasErrors;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HasErrors_should_return_true_when_no_errors_exist()
        {
            // Arrange
            // collection will be empty at this point

            // Act
            var result = _domainObject.HasErrors;

            // Assert
            Assert.IsFalse(result);            
        }

        //[Test]
        //public void Should_be_able_to_clear_error_from_property()
        //{
        //    // Arrange
        //    _domainObject.SetError("X", "X");

        //    // Act
        //    _domainObject.ClearErrorOfProperty("X");
        //    var result = _domainObject.GetErrors("X") as List<string>;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(0, result.Count());
        //}

        [Test]
        public void Should_be_able_to_get_error_for_property()
        {
            // Arrange
            const string errorMessage = "ErrorMessage";
            _domainObject.SetError("X", "ErrorMessage");

            // Act           
            var result = _domainObject.GetErrors("X");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.OfType<ValidationResult>().FirstOrDefault().ErrorMessage, errorMessage);
        }
    }

}
