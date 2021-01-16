using System.Collections.Generic;
using System.Linq;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class PickedAnswerMapper : IPickedAnswerMapper
    {
        public PickedAnswerDbModel ToDbModel(PickedAnswerDto item)
        {
            if (item.Id == 0)
            {
                return new PickedAnswerDbModel
                {
                    CustomTestResultId = item.CustomTestResultId, CustomAnswerId = item.CustomAnswerId,
                    IsPicked = item.IsPicked
                };
            }
            else
            {
                return new PickedAnswerDbModel
                {
                    Id = item.Id, CustomTestResultId = item.CustomTestResultId, CustomAnswerId = item.CustomAnswerId,
                    IsPicked = item.IsPicked
                };
            }
        }

        public PickedAnswerDto ToDto(PickedAnswerDbModel item)
        {
            return new PickedAnswerDto
            {
                Id = item.Id,
                CustomTestResultId = item.CustomTestResultId,
                CustomAnswerId = item.CustomAnswerId,
                IsPicked = item.IsPicked
            };
        }

        public List<PickedAnswerDbModel> ToDbModelList(List<PickedAnswerDto> items)
        {
            return items.Select(ToDbModel).ToList();
        }

        public List<PickedAnswerDto> ToDtoList(List<PickedAnswerDbModel> items)
        {
            return items.Select(ToDto).ToList();
        }
    }
}