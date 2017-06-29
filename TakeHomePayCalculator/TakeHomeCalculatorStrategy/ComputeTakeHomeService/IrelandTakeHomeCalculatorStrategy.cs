using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeHomeCalculatorStrategy.ComputeTakeHomeService
{
    public class IrelandTakeHomeCalculatorStrategy : ITakeHomeCalculatorStrategy, IComputeIncomeTax, IUniversalSocialCharge, ICompulsoryPensionContribution
    {
        readonly IComputeGrossPay _grossPayService;

        public IrelandTakeHomeCalculatorStrategy(IComputeGrossPay grossPayService)
        {
            _grossPayService = grossPayService;
        }

        public double ComputeCompulsoryPensionContribution(double grossPay)
        {
            if (grossPay <= 0)
                return 0;
            return grossPay * 0.04;
        }

        public double ComputeIncomeTax(double grossPay)
        {

            double amountEligibleForHigherTax = 0;
            //Given the employee is located in Ireland, income tax at a rate of 25% is applied for the
            //first $600 and 40 % thereafter
            if (grossPay <= 0)
                return 0.0;

            if (grossPay > 600)
                amountEligibleForHigherTax = grossPay - 600D;

            if (amountEligibleForHigherTax > 0)
            {
                //150 is 25% of 600.
                //TODO : Make these values also configurable and declare these properties as part of IComputeIncomeTax as well so that they can be implemented as class level variables for reuse
                return 150 + (amountEligibleForHigherTax * 0.4);
            }
            else
                return grossPay * 0.25;
        }

        public double ComputeUniversalSocialCharge(double grossPay)
        {
            double amountEligibleForHigherSocialCharge = 0;
            //Given the employee is located in Ireland, income tax at a rate of 25% is applied for the
            //first $600 and 40 % thereafter
            if (grossPay <= 0)
                return 0.0;

            if (grossPay > 500)
                amountEligibleForHigherSocialCharge = grossPay - 500D;

            if (amountEligibleForHigherSocialCharge > 0)
            {
                //35 is 7% of 500.
                //TODO : Make these values also configurable and declare these properties as part of IComputeUniversalSocialCharge as well so that they can be implemented as class level variables for reuse
                return 35 + (amountEligibleForHigherSocialCharge * 0.08);
            }
            else
                return grossPay * 0.07;
        }

        public PayCheck GeneratePayCheck(EmployeeData employeeData)
        {
            if (employeeData.HoursWorked <= 0 || employeeData.HourlyRate <= 0)
                throw new Exception("Pay check can't be generated for zero hours or zero hourly rate!");
            var grossPay = _grossPayService.ComputeGrossPay(employeeData.HoursWorked, employeeData.HourlyRate);
            var computedIncomeTax = ComputeIncomeTax(grossPay);
            var computedCompulsoryPensionContribution = ComputeCompulsoryPensionContribution(grossPay);
            var computedUniversalSocialCharge = ComputeUniversalSocialCharge(grossPay);
            return new PayCheck()
            {
                GrossAmount = grossPay,
                IncomeTax = computedIncomeTax,
                UniversalSocialCharge = computedUniversalSocialCharge,
                Pension = computedCompulsoryPensionContribution,
                NetAmount = grossPay - computedIncomeTax - computedUniversalSocialCharge - computedCompulsoryPensionContribution,
                Location = employeeData.Location
            };
        }
    }
}
