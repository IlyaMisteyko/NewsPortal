using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace NewsPortal.Logic.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region Methods of Comment Service

        public Comment GetCommentById(int commentId)
        {
            return _unitOfWork.Comments.GetById(commentId);
        }

        public IEnumerable<Comment> GetCommentsByArticleId(int articleId)
        {
            return _unitOfWork.Comments.GetMany(comment => comment.ArticleId == articleId).OrderByDescending(date => date.PublishingDate).ToList();
        }

        public IEnumerable<Comment> GetTop10Comments(int articleId)
        {
            return SortComments(_unitOfWork.Comments.GetMany(comment => comment.ArticleId == articleId && comment.ParentCommentId == null));
        }

        public IEnumerable<Comment> GetComments(int articleId, int page, int entityCount)
        {
            int skip = entityCount * page;

            var comments = _unitOfWork.Comments.GetMany(comment => comment.ArticleId == articleId && comment.ParentCommentId == null);

            comments = SortComments(comments);

            return comments.Skip(skip).Take(entityCount);
        }

        private IEnumerable<Comment> SortComments(IEnumerable<Comment> comments)
        {
            if (comments.Count() != 0)
            {
                comments = comments.OrderByDescending(comment => comment.PublishingDate);

                foreach (var comment in comments)
                    comment.ChildComments = SortComments(comment.ChildComments).ToList();
            }

            return comments;
        }

        public IEnumerable<Comment> SearchComments(string search)
        {
            return _unitOfWork.Comments
                .GetMany(comment => comment.Message.ToLower().Contains(search.ToLower()))
                .OrderByDescending(comment => comment.PublishingDate).ToList();
        }

        public void CreateComment(Comment comment)
        {
            _unitOfWork.Comments.Create(comment);
            SaveComment();
        }

        public void UpdateComment(Comment comment)
        {
            _unitOfWork.Comments.Update(comment);
            SaveComment();
        }

        public void DeleteComment(int commentId)
        {
            var comment = GetCommentById(commentId);

            RecursiveDelete(comment);

            SaveComment();
        }

        public void DeleteUserComments(string userId)
        {
            var comments = _unitOfWork.Comments.GetMany(comment => comment.UserId == userId);

            foreach (var comment in comments)
                RecursiveDelete(comment);

            SaveComment();
        }

        private void RecursiveDelete(Comment parentComment)
        {
            if (parentComment.ChildComments != null)
            {
                var children = _unitOfWork.Comments.GetMany(childComment => childComment.ParentCommentId == parentComment.CommentId);

                foreach (var child in children)
                    RecursiveDelete(child);
            }

            _unitOfWork.Comments.Delete(parentComment);
        }

        public void SaveComment()
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
