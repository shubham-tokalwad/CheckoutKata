using Kata.Core.Models;

namespace CheckoutKata
{
    public class Checkout
    {
        private readonly List<Item> _scannedItems = new List<Item>();
        private readonly List<SpecialOffer> _specialOffers;

        public Checkout(List<SpecialOffer> specialOffers)
        {
            _specialOffers = specialOffers;
        }

        public void Scan(Item item)
        {
            _scannedItems.Add(item);
        }

        public decimal Total()
        {
            var total = 0m;
            foreach (var group in _scannedItems.GroupBy(i => i.SKU))
            {
                var quantity = group.Count();
                var item = group.First();
                total += quantity * item.UnitPrice;
            }

            return total;
        }

    }
}