namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces
{
    internal interface IMapper<TDto, TDbModel>
    {
        TDbModel ToDbModel(TDto item);
        TDto ToDto(TDbModel item);
    }
}
