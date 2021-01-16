using System.ComponentModel.DataAnnotations;
using BartenderSupportSystem.Server.Helpers;

namespace BartenderSupportSystem.Server.Data.DbModels.Users
{
    internal sealed class CustomerDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }

        [Required]
        [StringLength(DefaultConstants.StringMaxLength,
            MinimumLength = DefaultConstants.StringMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(DefaultConstants.StringMaxLength,
            MinimumLength = DefaultConstants.StringMinLength)]
        public string LastName { get; set; }
    }
}