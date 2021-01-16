using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BartenderSupportSystem.Server.Helpers;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class CustomAnswerDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }

        [Required]
        [StringLength(DefaultConstants.StringMaxLength, MinimumLength = 1)]
        public string Statement { get; set; }

        public bool IsCorrect { get; set; }
        [Range(0, int.MaxValue)] public int QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))] public CustomQuestionDbModel Question { get; set; }
    }
}