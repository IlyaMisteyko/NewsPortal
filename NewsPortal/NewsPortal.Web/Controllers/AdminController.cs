using AutoMapper;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using NewsPortal.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NewsPortal.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public AdminController(IArticleService articleService, ICommentService commentService, IUserService userService, IRoleService roleService, IMapper mapper)
        {
            _articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public ActionResult Index()
        {
            var users = _userService.GetUsers();
            var usersView = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<AdminUserViewModel>>(users);

            foreach (var user in usersView)
                user.Role = _roleService.GetUserRole(user.RoleId);

            return View(usersView);
        }

        public ActionResult GetAllUsers()
        {
            var users = _userService.GetUsers();
            var usersView = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<AdminUserViewModel>>(users);

            foreach (var user in usersView)
                user.Role = _roleService.GetUserRole(user.RoleId);

            return PartialView("_UsersPartial", usersView);
        }

        public ActionResult GetAllArticles()
        {
            var articles = _articleService.GetArticles();
            var articlesView = _mapper.Map<IEnumerable<Article>, IEnumerable<AdminArticleViewModel>>(articles);

            return PartialView("_ArticlesPartial", articlesView);
        }

        public async Task<ActionResult> ChangeUserState(string userId)
        {
            var user = await _userService.GetUserById(userId);

            user.Activated = !user.Activated;
            _userService.UpdateUser(user);

            return RedirectToAction("GetAllUsers");
        }

        public async Task<ActionResult> ChangeRole(string userId)
        {
            var user = await _userService.GetUserById(userId);
            var userView = _mapper.Map<ApplicationUser, AdminUserViewModel>(user);
            userView.Role = _roleService.GetUserRole(userView.RoleId);
            userView.Roles = new SelectList(_roleService.GetAllRoles(), "Id", "Name");

            return PartialView("_ChangeRolePartial", userView);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeRole(AdminUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map(model, await _userService.GetUserById(model.UserId));
                _userService.ChangeRole(user.Id, model.Role, _roleService.GetUserRole(model.RoleId));
                _userService.UpdateUser(user);

                return RedirectToAction("GetAllUsers");
            }
            else
            {
                ModelState.AddModelError("", "Wrong data!");
            }

            model.Roles = new SelectList(_roleService.GetAllRoles(), "Id", "Name");
            return PartialView("_ChangeRolePartial", model);
        }

        [HttpPost]
        public ActionResult DeleteUser(string userId)
        {
            _commentService.DeleteUserComments(userId);
            _userService.DeleteUser(userId);
            return RedirectToAction("GetAllUsers");
        }
    }
}