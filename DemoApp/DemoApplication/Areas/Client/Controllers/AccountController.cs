using DemoApplication.Areas.Client.ViewModels.Account;
using DemoApplication.Areas.Client.ViewModels.Account.Address;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;


namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AccountController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        #region Dashboard
        [HttpGet("dashboard", Name = "client-account-dashboard")]
        public IActionResult Dashboard()
        {


            return View();
        }
        #endregion

        #region Order
        [HttpGet("orders", Name = "client-account-orders")]
        public IActionResult Orders()
        {


            return View();
        }
        #endregion

        #region Download
        [HttpGet("download", Name = "client-account-downloads")]
        public IActionResult Download()
        {


            return View();
        }
        #endregion

        #region Payment

        [HttpGet("payment", Name = "client-account-payments")]
        public IActionResult Payment()
        {


            return View();
        }

        #endregion

        #region Address

        [HttpGet("address", Name = "client-account-addresses")]
        public IActionResult Address()
        {
            var user = _userService.CurrentUser;

            var address = _dataContext.Addresses.FirstOrDefault(a => a.UserId == user.Id);

            if (address is null)
            {
                return RedirectToRoute("client-account-add-addresses");
            }

            var model = new ItemViewModel
            {
                AcceptorFirstName = address.AcceptorFirstName,
                AcceptorLastName = address.AcceptorLastName,
                AddressName = address.AddressName,
                PhoneNumber = address.PhoneNumber
            };

            return View(model);
        }



        [HttpGet("add-address", Name = "client-account-add-addresses")]
        public async Task<IActionResult> AddAddress()
        {

            return View();
        }


        [HttpPost("add-address", Name = "client-account-add-addresses")]
        public async Task<IActionResult> AddAddress(AddAddressViewModel model)
        {
            var user = _userService.CurrentUser;

            var address = new Address
            {
                UserId = user.Id,
                AcceptorFirstName = model.AcceptorFirstName,
                AcceptorLastName = model.AcceptorLastName,
                AddressName = model.AddressName,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            


            await _dataContext.Addresses.AddAsync(address);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-addresses");
        }
        [HttpGet("update-address", Name = "client-account-update-address")]
        public IActionResult UpdateAddress()
        {
            var user = _userService.CurrentUser;
            var address = _dataContext.Addresses.FirstOrDefault(a => a.UserId == user.Id);


            if (address is null)
            {
                return RedirectToRoute("client-account-add-addresses");
            }

            var model = new UpdateAddressViewModel
            {
                AcceptorFirstName = address.AcceptorFirstName,
                AcceptorLastName = address.AcceptorLastName,
                AddressName = address.AddressName,
                PhoneNumber = address.PhoneNumber,

            };

            return View(model);
        }



        [HttpPost("update-address", Name = "client-account-update-addresses")]
        public async Task<IActionResult> UpdateAddress(UpdateAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.CurrentUser;
            var address = _dataContext.Addresses.FirstOrDefault(a => a.UserId == user.Id);

            if (address is null)
            {
                return RedirectToRoute("client-account-add-addresses");
            }

            address.AcceptorFirstName = model.AcceptorFirstName;
            address.AcceptorLastName = model.AcceptorLastName;
            address.AddressName = model.AddressName;
            address.PhoneNumber = model.PhoneNumber;


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-addresses");

        }

        #endregion

        #region Detail

        [HttpGet("detail", Name = "client-account-details")]
        public async Task<IActionResult> Detail()
        {
            var user = _userService.CurrentUser;

            var model = new UpdateDetailsViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return View(model);
        }

        [HttpPost("detail", Name = "client-account-details")]
        public async Task<IActionResult> Detail(UpdateDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.CurrentUser;

            if (user is null)
            {
                return NotFound();
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-details");
        }

        #endregion

        #region ChangePassword

        [HttpGet("password", Name = "client-account-change-password")]
        public async Task<IActionResult> ChangePasswordAsync()
        {
            return View();
        }

        [HttpPost("password", Name = "client-account-change-password")]
        public async Task<IActionResult> ChangePasswordAsync(UpdatePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.CurrentUser;

            if (!BC.Verify(model.CurrentPassword, user.Password))
            {
                ModelState.AddModelError(string.Empty, "Password don't match");
                return View(model);
            }

            user.Password = BC.HashPassword(model.Password);

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("client-account-change-password");



        }
        #endregion
    }
}
