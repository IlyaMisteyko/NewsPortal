using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using NewsPortal.Logic.Common.Services;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using NewsPortal.Web.ViewModels;
using System.Security.Claims;
using AutoMapper;
using NewsPortal.Model.Models;
using NewsPortal.Logic.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace NewsPortal.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserProfileService _userProfileService;
        private readonly IMapper _mapper;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IUserService userService, IRoleService roleService, ISubscriptionService subscriptionService, IUserProfileService userProfileService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _userProfileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity claim = await _userService.Authenticate(model.Email, model.Password);
                if (claim == null)
                    ModelState.AddModelError("", "Wrong login or password!");
                else
                {
                    if (!(await _userService.GetUserByEmail(claim.Name)).Activated)
                        return View("_BlockedProfile");

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTime.UtcNow.AddHours(24)
                    }, claim);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<ApplicationUser>(model);
                OperationDetails operationDetails = await _userService.CreateUser(user, model.Password);

                if (operationDetails.Succedeed)
                {
                    _userProfileService.CreateUserProfile(user.Id);
                    AuthenticationManager.SignOut();
                    ClaimsIdentity claim = await _userService.Authenticate(model.Email, model.Password);
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(model);
        }

        public ActionResult PersonalProfile(string message = "")
        {
            ViewBag.Status = message;

            return View();
        }

        [HttpGet]
        public ActionResult PersonalData()
        {
            var userInfo = _userProfileService.GetProfile(User.Identity.GetUserId());
            var personalData = _mapper.Map<UserProfile, PersonalDataViewModel>(userInfo);

            return PartialView("_PersonalDataPartial", personalData);
        }

        public ActionResult ImageUpload(ImageUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = SaveImage(model.File);

                if (path == null)
                    return View("_ImageUploadPartial", model);

                _userService.SaveAvatar(User.Identity.GetUserId(), path);

                return RedirectToAction("PersonalProfile");
            }

            return View("_ImageUploadPartial", model);
        }

        private string SaveImage(HttpPostedFileBase file)
        {
            if (file == null || !(file.ContentLength > 0))
                return null;

            string fileName = DateTime.Now.Millisecond + file.FileName;
            string fileExt = Path.GetExtension(file.FileName);

            if (fileExt == null) 
                return null;

            string path = "~\\Content\\Avatars\\" + fileName;

            file.SaveAs(Path.GetFullPath(System.Web.HttpContext.Current.Server.MapPath(path)));

            return path;
        }

        [HttpGet]
        public ActionResult EditPersonalData(PersonalDataViewModel model)
        {
            return PartialView("_EditPersonalDataPartial", model);
        }

        [HttpPost]
        [ActionName("EditPersonalData")]
        public ActionResult EditPersonalDataConfirmed(PersonalDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map(model, _userProfileService.GetProfile(model.UserId));

                _userProfileService.UpdateUserProfile(user);

                return RedirectToAction("PersonalProfile");
            }

            return View("_EditPersonalDataPartial", model);
        }

        [HttpGet]
        public async Task<ActionResult> SecurityData()
        {
            var userInfo = await _userService.GetUserById(User.Identity.GetUserId());
            var securityData = _mapper.Map<ApplicationUser, SecurityDataViewModel>(userInfo);
            securityData.Role = _roleService.GetUserRole(userInfo.RoleId);

            return PartialView("_SecurityDataPartial", securityData);
        }

        [HttpGet]
        public ActionResult EditSecurityData(SecurityDataViewModel model)
        {
            return PartialView("_EditSecurityDataPartial", model);
        }

        [HttpPost]
        [ActionName("EditSecurityData")]
        public async Task<ActionResult> EditSecurityDataConfirmed(SecurityDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map(model, await _userService.GetUserById(model.UserId));

                _userService.UpdateUser(user);

                return RedirectToAction("PersonalProfile");
            }

            return View("_EditSecurityDataPartial", model);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return PartialView("_ChangePasswordPartial");
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                OperationDetails operationDetails = await _userService.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

                if (operationDetails.Succedeed)
                    return RedirectToAction("PersonalProfile", new { message = operationDetails.Message });
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View("_ChangePasswordPartial", model);
        }

        [HttpGet]
        public async Task<ActionResult> UserProfile(string userId)
        {
            var user = await _userService.GetUserById(userId);
            var profile = _mapper.Map<ApplicationUser, UserProfileViewModel>(user);

            profile.IsFollowing = _subscriptionService.IsFollowing(userId, User.Identity.GetUserId());

            return View(profile);
        }

        [HttpGet]
        public ActionResult Follow(string followingId)
        {
            _subscriptionService.CreateSubscription(new Subscription
            {
                FollowerId = User.Identity.GetUserId(),
                FollowingId = followingId
            });

            return RedirectToAction("UserProfile", "Account", new { userId = followingId });
        }

        [HttpGet]
        public ActionResult Unfollow(string followingId)
        {
            _subscriptionService.DeleteSubscription(User.Identity.GetUserId(), followingId);
            return RedirectToAction("UserProfile", "Account", new { userId = followingId });
        }

        public ActionResult Followers(int page = 0)
        {
            var users = _subscriptionService.GetFollowers(User.Identity.GetUserId(), page, 10);
            var followers = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<FollowersViewModel>>(users);

            if (Request.IsAjaxRequest())
            {
                if (followers.Count() != 0)
                    return PartialView("_FollowersPartial", followers);
                else
                    return null;
            }

            return View(followers);
        }

        public ActionResult Followings(int page = 0)
        {
            var users = _subscriptionService.GetFollowings(User.Identity.GetUserId(), page, 10);
            var followings = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<FollowingsViewModel>>(users);

            if (Request.IsAjaxRequest())
            {
                if (followings.Count() != 0)
                    return PartialView("_FollowingsPartial", followings);
                else
                    return null;
            }

            return View(followings);
        }
    }
}