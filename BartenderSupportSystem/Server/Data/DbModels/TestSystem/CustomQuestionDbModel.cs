using BartenderSupportSystem.Server.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class CustomQuestionDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }

        [Required]
        [StringLength(DefaultConstants.StringMaxLength, MinimumLength = 1)]
        public string Statement { get; set; }

        [Range(0, int.MaxValue)] public int TestId { get; set; }
        [ForeignKey(nameof(TestId))] public CustomTestDbModel Test { get; set; }
        [Required] public List<CustomAnswerDbModel> Answers { get; set; } = new List<CustomAnswerDbModel>();
    }
}