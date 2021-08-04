using AutoMapper;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using NewsPortal.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NewsPortal.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IRateService _rateService;
        private readonly IMapper _mapper;

        public HomeController(IArticleService articleService, IRateService rateService, IMapper mapper)
        {
            _articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
            _rateService = rateService ?? throw new ArgumentNullException(nameof(rateService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("NewsFeed", "News");
            }

            var articles = _articleService.GetArticles();
            var publications = _mapper.Map<IEnumerable<Article>, IEnumerable<PublicationViewModel>>(articles);

            foreach (var publication in publications)
                publication.AverageRating = _rateService.GetAverageRating(publication.ArticleId);

            return View("~/Views/News/PublicationsList.cshtml", publications);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }
    }
}