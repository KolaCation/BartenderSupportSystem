using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class CustomTestResultDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }
        [Range(0, int.MaxValue)] public int CustomTestId { get; set; }
        [Required] public string UserName { get; set; }

        [Required] public List<PickedAnswerDbModel> PickedAnswers { get; set; } = new List<PickedAnswerDbModel>();
        [Range(0, 100)] public double PersonalMark { get; set; }
    }
}