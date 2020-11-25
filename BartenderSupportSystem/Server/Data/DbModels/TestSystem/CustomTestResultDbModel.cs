using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class CustomTestResultDbModel
    {
        public int Id { get; private set; }
        public int CustomTestId { get; private set; }
        public string UserName { get; private set; }
        public List<PickedAnswerDbModel> PickedAnswers { get; private set; }
        public double PersonalMark { get; private set; }

        public CustomTestResultDbModel(int customTestId, string userName, double personalMark)
        {
            CustomValidator.ValidateNumber(personalMark, CustomValidatorDefaultValues.NonNegativeDouble, 10.0);
            CustomTestId = customTestId;
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            PersonalMark = personalMark;
            PickedAnswers = new List<PickedAnswerDbModel>();
        }

        public CustomTestResultDbModel(List<PickedAnswerDbModel> pickedAnswers, int customTestId, string userName,
            double personalMark) : this(customTestId, userName, personalMark)
        {
            CustomValidator.ValidateObject(pickedAnswers);
            PickedAnswers = pickedAnswers;
        }

        public CustomTestResultDbModel(int id, int customTestId, string userName, double personalMark) : this(
            customTestId, userName, personalMark)
        {
            Id = id;
            PickedAnswers = new List<PickedAnswerDbModel>();
        }

        public CustomTestResultDbModel(List<PickedAnswerDbModel> pickedAnswers, int id, int customTestId,
            string userName, double personalMark) : this(id, customTestId, userName, personalMark)
        {
            CustomValidator.ValidateObject(pickedAnswers);
            PickedAnswers = pickedAnswers;
        }
    }
}