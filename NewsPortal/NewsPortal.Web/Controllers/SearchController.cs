using AutoMapper;
using Microsoft.AspNet.Identity;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using NewsPortal.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NewsPortal.Web.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly IUserService _userService;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly IRateService _rateService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;

        public SearchController(IUserService userService, IArticleService articleService, ICommentService commentService, IRateService rateService, ISubscriptionService subscriptionService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
            _rateService = rateService ?? throw new ArgumentNullException(nameof(rateService));
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public ActionResult SearchUsers(string searchString)
        {
            var users = _userService.SearchUsers(searchString);
            var searchUsers = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<SearchUserViewModel>>(users);

            foreach (var searchUser in searchUsers)
            {
                searchUser.IsFollowing = _subscriptionService.IsFollowing(searchUser.UserId, User.Identity.GetUserId());
            }

            ViewBag.SearchString = searchString;
            return View(searchUsers);
        }

        [AllowAnonymous]
        public ActionResult SearchArticles(string searchString)
        {
            var articles = _articleService.SearchArticles(searchString);
            var searchArticles = _mapper.Map<IEnumerable<Article>, IEnumerable<SearchArticleViewModel>>(articles);

            foreach (var searchArticle in searchArticles)
                searchArticle.AverageRating = _rateService.GetAverageRating(searchArticle.ArticleId);

            ViewBag.SearchString = searchString;
            return View(searchArticles);
        }

        public ActionResult SearchComments(string searchString)
        {
            var comments = _commentService.SearchComments(searchString);
            var searchComments = _mapper.Map<IEnumerable<Comment>, IEnumerable<SearchCommentViewModel>>(comments);

            ViewBag.SearchString = searchString;
            return View(searchComments);
        }
    }
}