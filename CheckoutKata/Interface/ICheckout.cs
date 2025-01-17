namespace Kata.Core.Interface
{
    public interface ICheckout
    {
        void Scan(string sku);
        decimal GetTotal();
    }
}
