namespace TakeHomeCalculatorStrategy.ComputeTakeHomeService
{
    public interface IComputeGrossPay
    {
        double ComputeGrossPay(int hours, double hourlyRate);
    }
}