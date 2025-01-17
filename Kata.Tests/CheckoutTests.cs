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

        [Fact]
        public void Scan_ApplyOffers()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item("A99", 0.50m),
                new Item("B15", 0.30m)
            };

            var offers = new List<SpecialOffer>
            {
                new SpecialOffer("A99", 3, 1.30m),
                new SpecialOffer("B15", 2, 0.45m)
            };

            var pricingService = new PricingService(items, offers);
            var checkout = new Checkout(pricingService);

            // Act
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("B15");
            checkout.Scan("B15");

            var total = checkout.GetTotal();

            // Assert
            Assert.Equal(1.75m, total);
        }

        [Fact]
        public void Scan_WithAndWithoutOffers()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item("A99", 0.50m),
                new Item("B15", 0.30m),
                new Item("C40", 1.20m)
            };

            var offers = new List<SpecialOffer>
            {
                new SpecialOffer("A99", 3, 1.30m),
                new SpecialOffer("B15", 2, 0.45m)
            };

            var pricingService = new PricingService(items, offers);
            var checkout = new Checkout(pricingService);

            // Act
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("B15");
            checkout.Scan("B15");
            checkout.Scan("C40");

            var total = checkout.GetTotal();

            // Assert
            Assert.Equal(2.95m, total);
        }

        [Fact]
        public void Total_MixedItemsWithOffers()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item("A99", 0.50m),
                new Item("B15", 0.30m),
                new Item("C40", 1.20m)
            };
            var offers = new List<SpecialOffer>
            {
                new SpecialOffer("A99", 3, 1.30m),
                new SpecialOffer("B15", 2, 0.45m)
            };
            var pricingService = new PricingService(items, offers);
            var checkout = new Checkout(pricingService);

            checkout.Scan("A99");
            checkout.Scan("B15");
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("B15");
            checkout.Scan("C40");
            checkout.Scan("A99");

            // Act
            var total = checkout.GetTotal();

            // Assert
            Assert.Equal(3.45m, total);
        }

        [Fact]
        public void Total_MixedOffersAndNonOffers()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item("A99", 0.50m),
                new Item("B15", 0.30m),
                new Item("C40", 1.30m)
            };
            var offers = new List<SpecialOffer>
            {
                new SpecialOffer("A99", 3, 1.30m),
                new SpecialOffer("B15", 2, 0.45m)
            };
            var pricingService = new PricingService(items, offers);
            var checkout = new Checkout(pricingService);

            checkout.Scan("A99");
            checkout.Scan("B15");
            checkout.Scan("C40");

            // Act
            var total = checkout.GetTotal();

            // Assert
            Assert.Equal(2.10m, total);
        }

        [Fact]
        public void Total_NoItemsScanned()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item("A99", 0.50m)
            };
            var offers = new List<SpecialOffer>
            {
                new SpecialOffer("A99", 3, 1.30m)
            };
            var pricingService = new PricingService(items, offers);
            var checkout = new Checkout(pricingService);

            // Act
            var total = checkout.GetTotal();

            // Assert
            Assert.Equal(0.00m, total);
        }

    }
}
