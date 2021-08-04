using NewsPortal.Model.Models;
using System.Collections.Generic;

namespace NewsPortal.Logic.Common.Services
{
    public interface IRateService
    {
        bool WasRated(int articleId, string userId);
        Dictionary<int, string> GetRating(int articleId);
        string GetAverageRating(int articleId);
        void CreateRate(Rate rate);
        void DeleteRate(int rateId);
        void SaveRate();
    }
}