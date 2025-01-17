namespace Kata.Core.Models
{
    public class SpecialOffer
    {
        public string? SKU { get; set; }
        public int Quantity { get; set; }
        public decimal OfferPrice { get; set; }

        public SpecialOffer(string? sku, int quantity, decimal offerPrice)
        {
            SKU = sku;
            Quantity = quantity;
            OfferPrice = offerPrice;
        }
    }
}
