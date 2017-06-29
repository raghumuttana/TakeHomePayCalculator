using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeHomeCalculatorStrategy.ComputeTakeHomeService
{
    public class TakeHomeCalculatorService
    {
        readonly ITakeHomeCalculatorStrategy _takeHomeCalculatorStrategy;

        public TakeHomeCalculatorService(ITakeHomeCalculatorStrategy takeHomeCalculatorStrategy)
        {
            _takeHomeCalculatorStrategy = takeHomeCalculatorStrategy;
        }

        public PayCheck GeneratePayCheck(EmployeeData employeeData)
        {
            return _takeHomeCalculatorStrategy.GeneratePayCheck(employeeData);
        }
    }
}
