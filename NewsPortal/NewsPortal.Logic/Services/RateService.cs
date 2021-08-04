using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsPortal.Logic.Services
{
    public class RateService : IRateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        #region  Methods of Subscription Service

        public bool WasRated(int articleId, string userId)
        {
            return _unitOfWork.Rates.Get(rate => rate.ArticleId == articleId && rate.UserId == userId) != null;
        }

        public Dictionary<int, string> GetRating(int articleId)
        {
            var average = (double)_unitOfWork.Rates.GetMany(rate => rate.ArticleId == articleId).Count();
            if (average == 0)
            {
                return new Dictionary<int, string>
                {
                    [0] = "0",
                    [1] = "0",
                    [2] = "0",
                    [3] = "0",
                    [4] = "0",
                    [5] = "0"
                };
            }

            return new Dictionary<int, string>
            {
                [0] = average.ToString(),
                [1] = (_unitOfWork.Rates.GetMany(rate => rate.ArticleId == articleId && rate.Mark == 1).Count() / average * 100).ToString("#"),
                [2] = (_unitOfWork.Rates.GetMany(rate => rate.ArticleId == articleId && rate.Mark == 2).Count() / average * 100).ToString("#"),
                [3] = (_unitOfWork.Rates.GetMany(rate => rate.ArticleId == articleId && rate.Mark == 3).Count() / average * 100).ToString("#"),
                [4] = (_unitOfWork.Rates.GetMany(rate => rate.ArticleId == articleId && rate.Mark == 4).Count() / average * 100).ToString("#"),
                [5] = (_unitOfWork.Rates.GetMany(rate => rate.ArticleId == articleId && rate.Mark == 5).Count() / average * 100).ToString("#")
            };
        }

        public string GetAverageRating(int articleId)
        {
            var average = _unitOfWork.Rates.GetMany(rate => rate.ArticleId == articleId);
            return average.Count() == 0 ? "0" : average.Average(rate => rate.Mark).ToString("#.##");
        }

        public void CreateRate(Rate rate)
        {
            _unitOfWork.Rates.Create(rate);
            SaveRate();
        }

        public void DeleteRate(int rateId)
        {
            _unitOfWork.Rates.Delete(rate => rate.RateId == rateId);
            SaveRate();
        }

        public void SaveRate()
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
