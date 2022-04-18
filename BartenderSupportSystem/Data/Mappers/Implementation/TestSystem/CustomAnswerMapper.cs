using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using System.Collections.Generic;
using System.Linq;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class CustomAnswerMapper : ICustomAnswerMapper
    {
        public CustomAnswerDbModel ToDbModel(CustomAnswerDto item)
        {
            if (item.Id == 0)
            {
                return new CustomAnswerDbModel
                {
                    Statement = item.Statement,
                    IsCorrect = item.IsCorrect,
                    QuestionId = item.QuestionId
                };
            }
            else
            {
                return new CustomAnswerDbModel
                {
                    Id = item.Id,
                    Statement = item.Statement,
                    IsCorrect = item.IsCorrect,
                    QuestionId = item.QuestionId
                };
            }
        }

        public CustomAnswerDto ToDto(CustomAnswerDbModel item)
        {
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
            return items.Select(ToDbModel).ToList();
        }

        public List<CustomAnswerDto> ToDtoList(List<CustomAnswerDbModel> items)
        {
            return items.Select(ToDto).ToList();
        }
    }
}