using System.Collections.Generic;
using System.Linq;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class PickedAnswerMapper : IPickedAnswerMapper
    {
        public PickedAnswerDbModel ToDbModel(PickedAnswerDto item)
        {
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new PickedAnswerDbModel(item.CustomTestResultId, item.CustomAnswerId, item.IsPicked);
            }
            else
            {
                return new PickedAnswerDbModel(item.Id, item.CustomTestResultId, item.CustomAnswerId, item.IsPicked);
            }
        }

        public PickedAnswerDto ToDto(PickedAnswerDbModel item)
        {
            CustomValidator.ValidateObject(item);
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
            CustomValidator.ValidateObject(items);
            return (from item in items select ToDbModel(item)).ToList();
        }

        public List<PickedAnswerDto> ToDtoList(List<PickedAnswerDbModel> items)
        {
            CustomValidator.ValidateObject(items);
            return (from item in items select ToDto(item)).ToList();
        }
    }
}
