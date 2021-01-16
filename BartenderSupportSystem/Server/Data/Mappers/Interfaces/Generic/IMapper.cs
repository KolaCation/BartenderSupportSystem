namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.Generic
{
    internal interface IMapper<TDto, TDbModel>
    {
        TDbModel ToDbModel(TDto item);
        TDto ToDto(TDbModel item);
    }
}
