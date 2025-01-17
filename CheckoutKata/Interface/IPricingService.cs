namespace Kata.Core.Interface
{
    public interface IPricingService
    {
        decimal CalculatePrice(IEnumerable<string> scannedItems);
    }
}
