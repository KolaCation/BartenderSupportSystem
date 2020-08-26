using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.AppServices.RecommendationSystem
{
    internal interface ISpecification<T> where T : class
    {
        bool IsSatisfied(T item);
    }
}
