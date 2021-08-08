using NewsPortal.Model.Models;
using System.Collections.Generic;

namespace NewsPortal.Logic.Common.Services
{
    public interface ICommentService
    {
        Comment GetCommentById(int commentId);
        IList<Comment> GetCommentsByArticleId(int articleId);
        IList<Comment> GetTop10Comments(int articleId);
        IList<Comment> GetComments(int articleId, int page, int entityCount);
        IList<Comment> SearchComments(string search);
        void CreateComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int commentId);
        void DeleteUserComments(string userId);
        void SaveComment();
    }
}
