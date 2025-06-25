using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MusicStore.Domain;
using MusicStore.Domain.Domain_Models;
using MusicStore.Service.Interface;
using Stripe;
using Stripe.Checkout;

namespace MusicStore.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;


        public ShoppingCartsController(IShoppingCartService shoppingCartService, IOptions<StripeSettings> stripeSettings)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userShoppingCart = _shoppingCartService.GetByUserIdWithIncludedAlbums(Guid.Parse(userId));
            return View(userShoppingCart);
        }

        // DELETE: Remove Album from ShoppingCart
        public IActionResult Delete(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _shoppingCartService.DeleteAlbumFromShoppingCart(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult OrderNow()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return RedirectToAction("Login", "Account");

            _shoppingCartService.OrderAlbums(Guid.Parse(userId));

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult CreateCheckoutSession()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = _shoppingCartService.GetByUserIdWithIncludedAlbums(Guid.Parse(userId));

            var domain = $"{Request.Scheme}://{Request.Host}";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = cart.Albums.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Album.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Album.Title
                        }
                    },
                    Quantity = item.Quantity
                }).ToList(),
                Mode = "payment",
                SuccessUrl = domain + "/ShoppingCarts/SuccessPayment",
                CancelUrl = domain + "/ShoppingCarts/Index"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }
        public IActionResult SuccessPayment()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                _shoppingCartService.OrderAlbums(Guid.Parse(userId));
            }
            return View();
        }
        // Helper method
        private Guid? GetUserId()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
                return null;

            return Guid.TryParse(userIdString, out var id) ? id : null;
        }
    }
}
