using AutoMapper;
using Microsoft.AspNet.Identity;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using NewsPortal.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsPortal.Web.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly ILikeService _likeService;
        private readonly IRateService _rateService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;

        public NewsController(IArticleService articleService, ICommentService commentService, ILikeService likeService, IRateService rateService, ISubscriptionService subscriptionService, IMapper mapper)
        {
            _articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
            _likeService = likeService ?? throw new ArgumentNullException(nameof(likeService));
            _rateService = rateService ?? throw new ArgumentNullException(nameof(rateService));
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public ActionResult Index(int articleId)
        {
            var article = _articleService.GetArticleById(articleId);

            if (article == null)
                return HttpNotFound();

            var articleViewModel = _mapper.Map<Article, ArticleViewModel>(article);

            articleViewModel.Rating = _rateService.GetRating(articleId);
            articleViewModel.AverageRating = _rateService.GetAverageRating(articleId);
            articleViewModel.Rated = _rateService.WasRated(articleId, User.Identity.GetUserId());
            articleViewModel.Comments = (ICollection<CommentViewModel>)_mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(_commentService.GetTop10Comments(articleId));

            return View(articleViewModel);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            var createArticle = new ArticleFormViewModel();

            return View(createArticle);
        }

        [HttpPost]
        public ActionResult Create(ArticleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageUrl = SaveImage(model.File);

                var article = _mapper.Map<ArticleFormViewModel, Article>(model);
                _articleService.CreateArticle(article);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        
        [HttpGet]
        public ActionResult Edit(int articleId)
        {
            var article = _articleService.GetArticleById(articleId);
            if (article == null)
                return HttpNotFound();

            if (article.UserId != User.Identity.GetUserId())
                if (!User.IsInRole("admin"))
                    return View("_AccessDenied");

            var editArticle = _mapper.Map<Article, ArticleFormViewModel>(article);

            return View(editArticle);
        }

        [HttpPost]
        public ActionResult Edit(ArticleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserId != User.Identity.GetUserId())
                    if (!User.IsInRole("admin"))
                        return View("_AccessDenied");

                model.ImageUrl = SaveImage(model.File);
                var article = _mapper.Map<ArticleFormViewModel, Article>(model);
                _articleService.UpdateArticle(article);
                return RedirectToAction("PublicationsList", "News");
            }

            return View(model);
        }

        private string SaveImage(HttpPostedFileBase file)
        {
            if (file == null || !(file.ContentLength > 0))
                return null;

            string fileName = DateTime.Now.Millisecond + file.FileName;
            string fileExt = Path.GetExtension(file.FileName);

            if (fileExt == null)
                return null;

            string path = "~\\Content\\ArticlesImages\\" + fileName;

            file.SaveAs(Path.GetFullPath(System.Web.HttpContext.Current.Server.MapPath(path)));

            return path;
        }

        public ActionResult Delete(int articleId)
        {
            var article = _articleService.GetArticleById(articleId);
            if (article == null)
                return HttpNotFound();

            if (article.UserId != User.Identity.GetUserId())
                if (!User.IsInRole("admin"))
                    return View("_AccessDenied");

            return View(article);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int articleId)
        {
            var article = _articleService.GetArticleById(articleId);
            if (article == null)
                return HttpNotFound();

            if (article.UserId != User.Identity.GetUserId())
                if (!User.IsInRole("admin"))
                    return View("_AccessDenied");

            _articleService.DeleteArticle(articleId);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult PublicationsList()
        {
            var articles = _articleService.GetArticlesByUser(User.Identity.GetUserId());
            var publications = _mapper.Map<IEnumerable<Article>, IEnumerable<PublicationViewModel>>(articles);

            foreach (var publication in publications)
                publication.AverageRating = _rateService.GetAverageRating(publication.ArticleId);

            return View(publications);
        }

        public ActionResult NewsFeed()
        {
            var followings = _subscriptionService.GetFollowings(User.Identity.GetUserId());
            var articles = _articleService.GetArticlesByFollowings(followings);
            var publications = _mapper.Map<IEnumerable<Article>, IEnumerable<PublicationViewModel>>(articles);

            foreach (var publication in publications)
                publication.AverageRating = _rateService.GetAverageRating(publication.ArticleId);

            return View("PublicationsList", publications);
        }

        public ActionResult SetRate(int articleId, int mark)
        {
            _rateService.CreateRate(new Rate
            {
                Mark = mark,
                ArticleId = articleId,
                UserId = User.Identity.GetUserId()
            });

            return RedirectToAction("Index", new { articleId = articleId });
        }

        public ActionResult Comments(int articleId, int page = 0)
        {
            var comments = _commentService.GetComments(articleId, page, 10);
            var commentsView = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);

            if (Request.IsAjaxRequest())
            {
                if (commentsView.Count() != 0)
                    return PartialView("_CommentsPartial", commentsView);
                else 
                    return null;
            }

            return View(commentsView);
        }

        [HttpGet]
        public ActionResult CreateComment(int articleId)
        {
            var commentForm = new CommentFormViewModel() { ArticleId = articleId };

            return PartialView("_CreateCommentPartial", commentForm);
        }

        [HttpPost]
        public ActionResult CreateComment(CommentFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = _mapper.Map<CommentFormViewModel, Comment>(model);
                _commentService.CreateComment(comment);
                return RedirectToAction("Index", new { articleId = model.ArticleId });
            }

            return View("_CreateCommentPartial", model);
        }

        [HttpGet]
        public ActionResult EditComment(int commentId)
        {
            var comment = _commentService.GetCommentById(commentId);

            if (comment == null)
                return HttpNotFound();

            if (comment.UserId != User.Identity.GetUserId())
                if (!User.IsInRole("admin"))
                    return View("_AccessDenied");

            var commentView = _mapper.Map<Comment, CommentFormViewModel>(comment);
            return PartialView("_EditCommentPartial", commentView);
        }

        [HttpPost]
        public ActionResult EditComment(CommentFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserId != User.Identity.GetUserId())
                    if (!User.IsInRole("admin"))
                        return View("_AccessDenied");

                var comment = _mapper.Map(model, _commentService.GetCommentById(model.CommentId));
                _commentService.UpdateComment(comment);

                return RedirectToAction("Index", new { articleId = model.ArticleId });
            }

            return View("_EditCommentPartial", model);
        }

        public ActionResult DeleteComment(int commentId)
        {
            var comment = _commentService.GetCommentById(commentId);

            if (comment == null)
                return HttpNotFound();

            if (comment.UserId != User.Identity.GetUserId())
                if (!User.IsInRole("admin"))
                    return View("_AccessDenied");

            var commentView = _mapper.Map<Comment, CommentViewModel>(comment);

            return PartialView("_DeleteCommentPartial", commentView);
        }

        [HttpPost]
        [ActionName("DeleteComment")]
        public ActionResult DeleteCommentConfirmed(int commentId)
        {
            var comment = _commentService.GetCommentById(commentId);

            if (comment == null)
                return HttpNotFound();

            if (comment.UserId != User.Identity.GetUserId())
                if (!User.IsInRole("admin"))
                    return View("_AccessDenied");

            _commentService.DeleteComment(commentId);

            return RedirectToAction("Index", new { articleId = comment.ArticleId });
        }

        public ActionResult Like(int commentId, int articleId)
        {
            if (_likeService.IsLiked(User.Identity.GetUserId(), commentId))
            {
                _likeService.DeleteLike(User.Identity.GetUserId(), commentId);
            }
            else
            {
                _likeService.CreateLike(User.Identity.GetUserId(), commentId);
            }

            return RedirectToAction("Index", new { articleId = articleId });
        }
    }
}