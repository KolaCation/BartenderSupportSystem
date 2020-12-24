using System.Collections.Generic;
using System.Linq;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class CustomQuestionMapper : ICustomQuestionMapper
    {
        private readonly ICustomAnswerMapper _customAnswerMapper;

        public CustomQuestionMapper()
        {
            _customAnswerMapper = new CustomAnswerMapper();
        }

        public CustomQuestionDbModel ToDbModel(CustomQuestionDto item)
        {
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new CustomQuestionDbModel(_customAnswerMapper.ToDbModelList(item.Answers), item.Statement, item.TestId);
            }
            else
            {
                return new CustomQuestionDbModel(_customAnswerMapper.ToDbModelList(item.Answers), item.Id, item.Statement, item.TestId);
            }
        }

        public CustomQuestionDto ToDto(CustomQuestionDbModel item)
        {
            CustomValidator.ValidateObject(item);
            return new CustomQuestionDto
            {
                Answers = _customAnswerMapper.ToDtoList(item.Answers),
                Id = item.Id,
                Statement = item.Statement,
                TestId = item.TestId

            };
        }

        public List<CustomQuestionDbModel> ToDbModelList(List<CustomQuestionDto> items)
        {
            CustomValidator.ValidateObject(items);
            return (from item in items select ToDbModel(item)).ToList();
        }

        public List<CustomQuestionDto> ToDtoList(List<CustomQuestionDbModel> items)
        {
            CustomValidator.ValidateObject(items);
            return (from item in items select ToDto(item)).ToList();
        }
    }
}
