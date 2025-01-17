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
            var groupedItems = _scannedItems.GroupBy(i => i.SKU);

            foreach (var group in groupedItems)
            {
                var sku = group.Key;
                var quantity = group.Count();
                var item = group.First();
                var offer = _specialOffers.FirstOrDefault(o => o.SKU == sku);

                if (offer != null)
                {
                    var offerBundles = quantity / offer.Quantity;
                    var remainingItems = quantity % offer.Quantity;

                    total += offerBundles * offer.OfferPrice + remainingItems * item.UnitPrice;
                }
                else
                {
                    total += quantity * item.UnitPrice;
                }
            }

            return total;
        }

    }
}