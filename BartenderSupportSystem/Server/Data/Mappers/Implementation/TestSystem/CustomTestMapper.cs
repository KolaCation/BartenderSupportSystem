using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;

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
            if (item.Id == 0)
            {
                return new CustomTestDbModel
                {
                    Questions = _customQuestionMapper.ToDbModelList(item.Questions), Name = item.Name,
                    Topic = item.Topic,
                    Description = item.Description, AuthorUsername = item.AuthorUsername
                };
            }
            else
            {
                return new CustomTestDbModel
                {
                    Questions = _customQuestionMapper.ToDbModelList(item.Questions), Id = item.Id, Name = item.Name,
                    Topic = item.Topic, Description = item.Description, AuthorUsername = item.AuthorUsername
                };
            }
        }

        public CustomTestDto ToDto(CustomTestDbModel item)
        {
            return new CustomTestDto
            {
                Id = item.Id,
                Description = item.Description,
                Name = item.Name,
                Topic = item.Topic,
                Questions = _customQuestionMapper.ToDtoList(item.Questions),
                AuthorUsername = item.AuthorUsername
            };
        }
    }
}