using BartenderSupportSystem.Server.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class CustomTestDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }

        [Required]
        [StringLength(DefaultConstants.StringMaxLength,
            MinimumLength = DefaultConstants.StringMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DefaultConstants.StringMaxLength,
            MinimumLength = DefaultConstants.StringMinLength)]
        public string Topic { get; set; }

        [Required]
        [StringLength(255,
            MinimumLength = DefaultConstants.StringMinLength)]
        public string Description { get; set; }

        [Required] public List<CustomQuestionDbModel> Questions { get; set; } = new List<CustomQuestionDbModel>();
        [Required] public string AuthorUsername { get; set; }
    }
}