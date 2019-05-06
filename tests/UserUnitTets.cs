using FluentAssertions;
using KetoPal.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KetoPal.Tests
{
    [TestClass]
    public class UserUnitTets
    {
        [TestMethod]
        public void RecordConsumption_NoPreviousConsumption_IsAdded()
        {
            //Arrange
            var user = new User();

            //Act
            user.RecordConsumption(1.1);

            //Assert
            user.TotalCarbConsumption.Should().Be(1.1);
        }

        [TestMethod]
        public void RecordConsumption_HasPreiousConsumption_TotalsIsSum()
        {
            //Arrange
            var user = new User();

            //Act
            user.RecordConsumption(1.1);
            user.RecordConsumption(5);

            //Assert
            user.TotalCarbConsumption.Should().Be(6.1);
        }
    }
}