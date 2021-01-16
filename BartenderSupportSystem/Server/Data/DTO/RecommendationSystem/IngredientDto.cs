namespace BartenderSupportSystem.Server.Data.DTO.RecommendationSystem
{
    public sealed class IngredientDto
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public MealDto Meal { get; set; }
        public DrinkDto Drink { get; set; }
        public int CocktailId { get; set; }
        public string ProportionType { get; set; }
        public double ProportionValue { get; set; }
    }
}