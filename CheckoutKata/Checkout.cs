using Kata.Core.Interface;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private readonly List<string> _items = new List<string>();
        private readonly IPricingService _pricingService;

        public Checkout(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public void Scan(string item)
        {
            if (!string.IsNullOrEmpty(item))
                _items.Add(item);
        }

        public decimal GetTotal()
        {
            var total = 0m;

            return total;
        }
    }
}