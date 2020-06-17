﻿namespace BartenderSupportSystem.Server.Mappers.Interfaces
{
    internal interface IMapper<TDomain, TDbModel>
    {
        public TDomain DbToDomain(TDbModel dbModel);
        public TDbModel DomainToDb(TDomain domain);
    }
}