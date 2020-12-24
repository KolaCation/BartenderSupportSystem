using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;
using BartenderSupportSystem.Shared.Utils;

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
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new CustomTestResultDbModel(_pickedAnswerMapper.ToDbModelList(item.PickedAnswers), item.CustomTestId, item.UserName, item.PersonalMark);
            }
            else
            {
                return new CustomTestResultDbModel(_pickedAnswerMapper.ToDbModelList(item.PickedAnswers), item.Id, item.CustomTestId, item.UserName, item.PersonalMark);
            }
        }

        public CustomTestResultDto ToDto(CustomTestResultDbModel item)
        {
            CustomValidator.ValidateObject(item);
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
