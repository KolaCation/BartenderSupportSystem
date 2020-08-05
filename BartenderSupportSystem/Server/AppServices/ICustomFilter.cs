using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.AppServices
{
    internal interface ICustomFilter<T> where T : class
    {
        IQueryable<T> Filter(IQueryable<T> items, params ISpecification<T>[] specifications);
        IQueryable<T> Filter(IQueryable<T> items, List<ISpecification<T>> specifications);
    }
}
