using NewsPortal.Model.Models;
using System.Collections.Generic;

namespace NewsPortal.Logic.Common.Services
{
    public interface IArticleService
    {
        Article GetArticleById(int articleId);
        IEnumerable<Article> GetArticles();
        IEnumerable<Article> GetArticlesByUser(string userId);
        IEnumerable<Article> GetArticlesByFollowings(IEnumerable<ApplicationUser> followings);
        IEnumerable<Article> SearchArticles(string search);
        void CreateArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(int articleId);
        void SaveArticle();
    }
}
