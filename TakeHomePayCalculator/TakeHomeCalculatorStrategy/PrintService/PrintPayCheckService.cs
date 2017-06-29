using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeHomeCalculatorStrategy.ComputeTakeHomeService
{
    public class PrintPayCheckService : IPrintPayCheckService
    {
        public void PrintPayCheck(PayCheck payCheck)
        {
            Console.WriteLine($"Employee Location: {payCheck.Location}");
            Console.WriteLine($"Gross Amount: $ {payCheck.GrossAmount}");
            Console.WriteLine("Less Deductions");
            Console.WriteLine($"Income Tax: $ {payCheck.IncomeTax}");
            Console.WriteLine($"Universal Social Charge: $ {payCheck.UniversalSocialCharge}");
            Console.WriteLine($"Pension: $ {payCheck.Pension}");
            Console.WriteLine($"Social Security Contribution: $ {payCheck.SocialSecurityContribution}");
            Console.WriteLine($"Net Amount: $ {payCheck.NetAmount}");
        }
    }
}
