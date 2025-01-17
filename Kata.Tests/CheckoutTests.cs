using CheckoutKata;
using Kata.Core.Models;
using Kata.Core.Services;

namespace Kata.Tests
{
    public class CheckoutTests
    {
        [Fact]
        public void Add_ItemToCheckout()
        {
            //Arrange
            var items = new List<Item>
            {
                new Item("A99", 0.50m)
            };
            var offers = new List<SpecialOffer>();
            var pricingService = new PricingService(items, offers);
            var checkout = new Checkout(pricingService);

            //Act
            checkout.Scan("A99");

            //Assert
            Assert.Equal(0.50m, checkout.GetTotal());
        }

        [Fact]
        public void Total_CalculateWithoutOffers()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item("A99", 0.50m),
                new Item("B15", 0.30m)
            };
            var offers = new List<SpecialOffer>();
            var pricingService = new PricingService(items, offers);
            var checkout = new Checkout(pricingService);

            checkout.Scan("A99");
            checkout.Scan("B15");

            // Act
            var total = checkout.GetTotal();

            // Assert
            Assert.Equal(0.80m, total);
        }
    }
}
