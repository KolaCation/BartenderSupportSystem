using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class CustomTestResultMapper : ICustomTestResultMapper
    {
        private readonly IPickedAnswerMapper _pickedAnswerMapper;

        public CustomTestResultMapper()
        {
            _pickedAnswerMapper = new PickedAnswerMapper();
        }

        public CustomTestResultDbModel ToDbModel(CustomTestResultDto item)
        {
            if (item.Id == 0)
            {
                return new CustomTestResultDbModel
                {
                    PickedAnswers = _pickedAnswerMapper.ToDbModelList(item.PickedAnswers),
                    CustomTestId = item.CustomTestId,
                    UserName = item.UserName,
                    PersonalMark = item.PersonalMark
                };
            }
            else
            {
                return new CustomTestResultDbModel
                {
                    PickedAnswers = _pickedAnswerMapper.ToDbModelList(item.PickedAnswers),
                    Id = item.Id,
                    CustomTestId = item.CustomTestId,
                    UserName = item.UserName,
                    PersonalMark = item.PersonalMark
                };
            }
        }

        public CustomTestResultDto ToDto(CustomTestResultDbModel item)
        {
            return new CustomTestResultDto
            {
                Id = item.Id,
                CustomTestId = item.CustomTestId,
                PersonalMark = item.PersonalMark,
                PickedAnswers = _pickedAnswerMapper.ToDtoList(item.PickedAnswers),
                UserName = item.UserName
            };
        }
    }
}