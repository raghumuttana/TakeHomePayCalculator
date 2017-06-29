using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeHomeCalculatorStrategy.ComputeTakeHomeService
{
    public class GermanyTakeHomeCalculatorStrategy : ITakeHomeCalculatorStrategy, IComputeIncomeTax, ICompulsoryPensionContribution
    {

        readonly IComputeGrossPay _grossPayService;

        public GermanyTakeHomeCalculatorStrategy(IComputeGrossPay grossPayService)
        {
            _grossPayService = grossPayService;
        }

        public double ComputeCompulsoryPensionContribution(double grossPay)
        {
            if (grossPay <= 0)
                return 0.0;
            return grossPay * 0.02;
        }

        public double ComputeIncomeTax(double grossPay)
        {

            double amountEligibleForHigherTax = 0;
            //Given the employee is located in Germany, income tax at a rate of 25 % is applied on the
            //first $400 and 32 % thereafter
            if (grossPay <= 0)
                return 0.0;

            if (grossPay > 400)
                amountEligibleForHigherTax = grossPay - 400D;

            if (amountEligibleForHigherTax > 0)
            {
                //100 is 25% of 400.
                //TODO : Make these values also configurable and declare these properties as part of IComputeIncomeTax as well so that they can be implemented as class level variables for reuse
                return 100 + (amountEligibleForHigherTax * 0.32);
            }
            else
                return grossPay * 0.25;
        }

        public PayCheck GeneratePayCheck(EmployeeData employeeData)
        {
            if (employeeData.HoursWorked <= 0 || employeeData.HourlyRate <= 0)
                throw new Exception("Pay check can't be generated for zero hours or zero hourly rate!");
            var grossPay = _grossPayService.ComputeGrossPay(employeeData.HoursWorked, employeeData.HourlyRate);
            var computedIncomeTax = ComputeIncomeTax(grossPay);
            var computedCompulsoryPensionContribution = ComputeCompulsoryPensionContribution(grossPay);
            return new PayCheck()
            {
                GrossAmount = grossPay,
                IncomeTax = computedIncomeTax,
                Pension = computedCompulsoryPensionContribution,
                NetAmount = grossPay - computedIncomeTax - computedCompulsoryPensionContribution,
                Location = employeeData.Location
            };
        }
    }
}