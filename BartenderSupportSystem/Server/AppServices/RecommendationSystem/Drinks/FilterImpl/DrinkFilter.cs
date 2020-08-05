using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Shared.Utils.RecommendationSystem.Drinks;

namespace BartenderSupportSystem.Server.AppServices.RecommendationSystem.Drinks.FilterImpl
{
    internal sealed class DrinkFilter : ICustomFilter<DrinkDbModel>
    {
        public IQueryable<DrinkDbModel> Filter(IQueryable<DrinkDbModel> items, params ISpecification<DrinkDbModel>[] specifications)
        {
            if (specifications != null && items != null)
            {
                var filteredItemIds = new List<Guid>();
                foreach (var item in items)
                {
                    var resultList = specifications.Select(specification => specification.IsSatisfied(item)).ToList();
                    if (resultList.Contains(true))
                    {
                        filteredItemIds.Add(item.Id);
                    }
                }

                items = items.Where(item => filteredItemIds.Contains(item.Id));
            }
            return items;
        }

        public IQueryable<DrinkDbModel> Filter(IQueryable<DrinkDbModel> items, List<ISpecification<DrinkDbModel>> specifications)
        {
            if (specifications != null && items != null)
            {
                var filteredItemIds = new List<Guid>();
                foreach (var item in items)
                {
                    var resultList = specifications.Select(specification => specification.IsSatisfied(item)).ToList();
                    if (resultList.Contains(true))
                    {
                        filteredItemIds.Add(item.Id);
                    }
                }
                items = items.Where(item => filteredItemIds.Contains(item.Id));
            }
            return items;
        }
    }
}
