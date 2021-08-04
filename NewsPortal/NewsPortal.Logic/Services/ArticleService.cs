using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsPortal.Logic.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        #region Methods of Article Service

        public Article GetArticleById(int articleId)
        {
            return _unitOfWork.Articles.GetById(articleId);
        }

        public IEnumerable<Article> GetArticles()
        {
            return _unitOfWork.Articles.GetAll()
                .OrderByDescending(article => article.Rates.Count == 0 ? 0 : article.Rates.Average(rate => rate.Mark))
                .ThenBy(date => date.PublishingDate).ToList();
        }

        public IEnumerable<Article> GetArticlesByUser(string userId)
        {
            return _unitOfWork.Articles.GetMany(article => article.UserId == userId).OrderByDescending(date => date.PublishingDate).ToList();
        }

        public IEnumerable<Article> GetArticlesByFollowings(IEnumerable<ApplicationUser> followings)
        {
            var articles = new List<Article>();

            foreach (var following in followings)
            {
                articles.AddRange(_unitOfWork.Articles.GetMany(article => article.UserId == following.Id));
            }

            return articles
                .OrderByDescending(article => article.Rates.Count == 0 ? 0 : article.Rates.Average(rate => rate.Mark))
                .ThenBy(date => date.PublishingDate);
        }

        public IEnumerable<Article> SearchArticles(string search)
        {
            return _unitOfWork.Articles
                .GetMany(article => article.Title.ToLower().Contains(search.ToLower()) || article.Description.ToLower().Contains(search.ToLower()))
                .OrderByDescending(article => article.PublishingDate)
                .ThenBy(article => article.Title).ToList();
        }
        public void CreateArticle(Article article)
        {
            _unitOfWork.Articles.Create(article);
            SaveArticle();
        }

        public void UpdateArticle(Article article)
        {
            _unitOfWork.Articles.Update(article);
            SaveArticle();
        }

        public void DeleteArticle(int articleId)
        {
            _unitOfWork.Articles.Delete(article => article.ArticleId == articleId);
            SaveArticle();
        }

        public void SaveArticle()
        {
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        #endregion
    }
}
