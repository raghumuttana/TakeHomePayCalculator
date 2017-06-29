using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeHomeCalculatorStrategy.ComputeTakeHomeService
{
    public class GrossPayCalculatorService : IComputeGrossPay
    {
        public double ComputeGrossPay(int hours, double hourlyRate)
        {
            return hours * hourlyRate;
        }
    }
}
