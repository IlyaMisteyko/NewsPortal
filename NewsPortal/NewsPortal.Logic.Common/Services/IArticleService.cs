using NewsPortal.Model.Models;
using System.Collections.Generic;

namespace NewsPortal.Logic.Common.Services
{
    public interface IArticleService
    {
        Article GetArticleById(int articleId);
        IList<Article> GetArticles();
        IList<Article> GetArticlesByUser(string userId);
        IList<Article> GetArticlesByFollowings(IEnumerable<ApplicationUser> followings);
        IList<Article> SearchArticles(string search);
        void CreateArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(int articleId);
        void SaveArticle();
    }
}
