using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using System;

namespace NewsPortal.Logic.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        #region  Methods of Like Service

        public bool IsLiked(string userId, int commentId)
        {
            return _unitOfWork.Likes.Get(like => like.UserId == userId && like.CommentId == commentId) != null;
        }

        public void CreateLike(string userId, int commentId)
        {
            _unitOfWork.Likes.Create(new Like { UserId = userId, CommentId = commentId });
            SaveLike();
        }

        public void DeleteLike(string userId, int commentId)
        {
            _unitOfWork.Likes.Delete(like => like.UserId == userId && like.CommentId == commentId);
            SaveLike();
        }

        public void SaveLike()
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
