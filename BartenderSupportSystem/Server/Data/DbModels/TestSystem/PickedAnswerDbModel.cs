using System.ComponentModel.DataAnnotations.Schema;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class PickedAnswerDbModel
    {
        public int Id { get; private set; }
        public int CustomTestResultId { get; private set; }
        [ForeignKey("CustomTestResultId")] public CustomTestResultDbModel CustomTestResult { get; private set; }
        public int CustomAnswerId { get; private set; }
        public bool IsPicked { get; private set; }

        public PickedAnswerDbModel(int customTestResultId, int customAnswerId, bool isPicked)
        {
            CustomTestResultId = customTestResultId;
            CustomAnswerId = customAnswerId;
            IsPicked = isPicked;
        }

        public PickedAnswerDbModel(int id, int customTestResultId, int customAnswerId, bool isPicked) : this(
            customTestResultId, customAnswerId, isPicked)
        {
            Id = id;
        }
    }
}