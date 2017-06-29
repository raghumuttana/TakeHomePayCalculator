using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeHomeCalculatorStrategy.ComputeTakeHomeService
{
    public interface ITakeHomeCalculatorStrategy
    {
        PayCheck GeneratePayCheck(EmployeeData employeeData);        
    }
}
