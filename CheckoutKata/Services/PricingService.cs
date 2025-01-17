using Kata.Core.Interface;
using Kata.Core.Models;

namespace Kata.Core.Services
{
    public class PricingService : IPricingService
    {
        private readonly List<Item> _items;
        private readonly List<SpecialOffer> _specialOffers;

        public PricingService(List<Item> items, List<SpecialOffer> specialOffers)
        {
            _items = items;
            _specialOffers = specialOffers;
        }

        public decimal CalculatePrice(IEnumerable<string> scannedItems)
        {
            try
            {
                var total = 0m;

                if (scannedItems.Any())
                {
                    var groupedItems = scannedItems.GroupBy(item => item);

                    foreach (var group in groupedItems)
                    {
                        var sku = group.Key;
                        var quantity = group.Count();

                        var item = _items.FirstOrDefault(i => i.SKU == sku);

                        if (item == null)
                            throw new InvalidOperationException($"Invalid SKU scanned: {sku}");

                        var offer = _specialOffers.FirstOrDefault(o => o.SKU == item!.SKU);

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
                }
                return total;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error calculating total.", ex);
            }
        }
    }
}
