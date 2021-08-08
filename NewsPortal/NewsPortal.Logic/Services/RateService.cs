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

        public IDictionary<int, string> GetRating(int articleId)
        {
            var ratesCount = _unitOfWork.Rates.GetRatesCount(rate => rate.ArticleId == articleId);
            var totalRates = ratesCount.Sum(item => item.Value);

            if (totalRates == 0)
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
                [0] = totalRates.ToString(),
                [1] = (ratesCount.ContainsKey(1) ? ratesCount[1] : 0 / totalRates * 100).ToString("#"),
                [2] = (ratesCount.ContainsKey(2) ? ratesCount[2] : 0 / totalRates * 100).ToString("#"),
                [3] = (ratesCount.ContainsKey(3) ? ratesCount[3] : 0 / totalRates * 100).ToString("#"),
                [4] = (ratesCount.ContainsKey(4) ? ratesCount[4] : 0 / totalRates * 100).ToString("#"),
                [5] = (ratesCount.ContainsKey(5) ? ratesCount[5] : 0 / totalRates * 100).ToString("#")
            };
        }

        public string GetAverageRating(int articleId)
        {
            var averageRating = _unitOfWork.Rates.GetAverageRating(rate => rate.ArticleId == articleId);
            return averageRating == 0 ? "0" : averageRating.ToString("#.##");
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
