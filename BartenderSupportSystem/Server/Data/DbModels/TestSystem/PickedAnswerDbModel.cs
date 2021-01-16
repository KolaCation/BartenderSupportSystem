using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class PickedAnswerDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }
        [Range(0, int.MaxValue)] public int CustomTestResultId { get; set; }

        [ForeignKey(nameof(CustomTestResultId))]
        public CustomTestResultDbModel CustomTestResult { get; set; }

        [Range(0, int.MaxValue)] public int CustomAnswerId { get; set; }
        public bool IsPicked { get; set; }
    }
}