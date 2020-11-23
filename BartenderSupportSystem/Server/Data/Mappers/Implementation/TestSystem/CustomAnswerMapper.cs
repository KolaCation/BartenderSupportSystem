using System.Collections.Generic;
using System.Linq;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class CustomAnswerMapper : ICustomAnswerMapper
    {
        public CustomAnswerDbModel ToDbModel(CustomAnswerDto item)
        {
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new CustomAnswerDbModel(item.Statement, item.IsCorrect, item.QuestionId);
            }
            else
            {
                return new CustomAnswerDbModel(item.Id, item.Statement, item.IsCorrect, item.QuestionId);
            }
        }

        public CustomAnswerDto ToDto(CustomAnswerDbModel item)
        {
            CustomValidator.ValidateObject(item);
            return new CustomAnswerDto
            {
                Id = item.Id,
                Statement = item.Statement,
                IsCorrect = item.IsCorrect,
                QuestionId = item.QuestionId
            };
        }

        public List<CustomAnswerDbModel> ToDbModelList(List<CustomAnswerDto> items)
        {
            CustomValidator.ValidateObject(items);
            return (from item in items select ToDbModel(item)).ToList();
        }

        public List<CustomAnswerDto> ToDtoList(List<CustomAnswerDbModel> items)
        {
            CustomValidator.ValidateObject(items);
            return (from item in items select ToDto(item)).ToList();
        }
    }
}
