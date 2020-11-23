using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class CustomTestMapper : ICustomTestMapper
    {
        private readonly ICustomQuestionMapper _customQuestionMapper;

        public CustomTestMapper()
        {
            _customQuestionMapper = new CustomQuestionMapper();
        }

        public CustomTestDbModel ToDbModel(CustomTestDto item)
        {
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new CustomTestDbModel(_customQuestionMapper.ToDbModelList(item.Questions), item.Name, item.Topic, item.Description);
            }
            else
            {
                return new CustomTestDbModel(_customQuestionMapper.ToDbModelList(item.Questions), item.Id, item.Name, item.Topic, item.Description);
            }
        }

        public CustomTestDto ToDto(CustomTestDbModel item)
        {
            CustomValidator.ValidateObject(item);
            return new CustomTestDto
            {
                Id = item.Id,
                Description = item.Description,
                Name = item.Name,
                Topic = item.Topic,
                Questions = _customQuestionMapper.ToDtoList(item.Questions)
            };
        }
    }
}
