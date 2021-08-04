using NewsPortal.Model.Models;
using System.Collections.Generic;

namespace NewsPortal.Logic.Common.Services
{
    public interface ICommentService
    {
        Comment GetCommentById(int commentId);
        IEnumerable<Comment> GetCommentsByArticleId(int articleId);
        IEnumerable<Comment> GetTop10Comments(int articleId);
        IEnumerable<Comment> GetComments(int articleId, int page, int entityCount);
        IEnumerable<Comment> SearchComments(string search);
        void CreateComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int commentId);
        void DeleteUserComments(string userId);
        void SaveComment();
    }
}
