using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeHomeCalculatorStrategy.ComputeTakeHomeService
{
    public class ItalyTakeHomeCalculatorStrategy : ITakeHomeCalculatorStrategy, IComputeIncomeTax, ISocialSecurityContribution
    {
        readonly IComputeGrossPay _grossPayService;

        public ItalyTakeHomeCalculatorStrategy(IComputeGrossPay grossPayService)
        {
            _grossPayService = grossPayService;
        }

        public double ComputeIncomeTax(double grossPay)
        {
            if (grossPay <= 0)
                return 0.0;
            return grossPay * .25;
        }

        public double ComputeSocialSecurityContribution(double grossPay)
        {
            if (grossPay <= 0)
                return 0.0;
            return grossPay * 9.19;
        }

        public PayCheck GeneratePayCheck(EmployeeData employeeData)
        {
            if (employeeData.HoursWorked <= 0 || employeeData.HourlyRate <= 0)
                throw new Exception("Pay check can't be generated for zero hours or zero hourly rate!");
            var grossPay = _grossPayService.ComputeGrossPay(employeeData.HoursWorked, employeeData.HourlyRate);
            var computedIncomeTax = ComputeIncomeTax(grossPay);
            var computedSocialSecurityContribution = ComputeSocialSecurityContribution(grossPay);
            return new PayCheck()
            {
                GrossAmount = grossPay,
                IncomeTax = computedIncomeTax,
                SocialSecurityContribution = computedSocialSecurityContribution,
                NetAmount = grossPay - computedIncomeTax - computedSocialSecurityContribution,
                Location = employeeData.Location
            };
        }
    }
}
