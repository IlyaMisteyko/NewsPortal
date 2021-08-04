namespace NewsPortal.Logic.Common.Services
{
    public interface ILikeService
    {
        bool IsLiked(string userId, int commentId);
        void CreateLike(string userId, int commentId);
        void DeleteLike(string userId, int commentId);
    }
}
