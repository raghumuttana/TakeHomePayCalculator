using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeHomeCalculatorStrategy;
using TakeHomeCalculatorStrategy.ComputeTakeHomeService;

namespace TakeHomePayCalculator.Tests
{
    [TestFixture]
    public class TakeHomeSalaryCalculatorTests
    {
        private GrossPayCalculatorService _grossPayService;

        [OneTimeSetUp]
        public void GrossPayCalculatorService()
        {
            _grossPayService = new GrossPayCalculatorService();
        }

        [TestCase(0, 1, "Ireland")]
        [TestCase(1, 0, "Ireland")]
        public void ComputeTakeHomeForIreland_Throws_Exception_When_Either_HourlyRate_Or_HoursWorked_IsZero(int hoursWorked, double hourlyRate, string location)
        {
            //arrange
            var taxStrategy = new IrelandTakeHomeCalculatorStrategy(_grossPayService);
            var employeeData = new EmployeeData() { HourlyRate = hourlyRate, HoursWorked = hoursWorked, Location = location };
            //act            
            //assert
            Assert.That(() => taxStrategy.GeneratePayCheck(employeeData), Throws.TypeOf<Exception>());
        }


        [TestCase(20, 10, "Ireland", 128)]      
        public void ComputeTakeHomeForIreland(int hoursWorked, double hourlyRate, string location, double netAmount)
        {
            //arrange
            var taxStrategy = new IrelandTakeHomeCalculatorStrategy(_grossPayService);
            var employeeData = new EmployeeData() { HourlyRate = hourlyRate, HoursWorked = hoursWorked, Location = location };
            //act
            var payCheck = taxStrategy.GeneratePayCheck(employeeData);
            //assert
            Assert.That(payCheck.NetAmount, Is.EqualTo(netAmount));
        }

       [OneTimeTearDown]
       public void TearDown()
        {
            _grossPayService = null;
        }

        //[TestCase(20, 10, "Italy")]
        //[TestCase(160, 6.25, "Italy")]
        //[TestCase(176, 12, "Italy")]
        //[TestCase(180, 60, "Italy")]
        //[TestCase(200, 35, "Italy")]
        //[TestCase(168, 50, "Italy")]
        //public void ComputeTakeHomeForItaly()
        //{
        //    //arrange

        //    //act

        //    //assert
        //}

        //[TestCase(20, 10, "Germany")]
        //[TestCase(160, 6.25, "Germany")]
        //[TestCase(176, 12, "Germany")]
        //[TestCase(180, 60, "Germany")]
        //[TestCase(200, 35, "Germany")]
        //[TestCase(168, 50, "Germany")]
        //public void ComputeTakeHomeForGermany()
        //{
        //    //arrange

        //    //act

        //    //assert
        //}
    }
}
