using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem.Enums;
using FluentValidation;
using System;
using System.Linq;

namespace BartenderSupportSystem.Server.Validators.RecommendationSystem
{
    public sealed class IngredientValidator : AbstractValidator<IngredientDto>
    {
        private readonly ApplicationDbContext _context;

        public IngredientValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(e => e.ProportionType)
                .NotEmpty().WithMessage("Proportion type is required.")
                .Must(BeValidProportionType).WithMessage("Provide a type from the list.");
            RuleFor(e => e.ProportionValue)
                .GreaterThanOrEqualTo(0).WithMessage("Min value: 0.")
                .LessThanOrEqualTo(10000).WithMessage("Max value: 10000.");
            RuleFor(e => e.ComponentId).Must(ExistComponent).WithMessage("Component not found.");
            RuleFor(e => e.CocktailId).Must(ExistCocktail).WithMessage("Cocktail not found.");
        }

        private bool ExistComponent(int id)
        {
            return _context.DrinksSet.Any(e => e.Id.Equals(id)) ||
                   _context.MealsSet.Any(e => e.Id.Equals(id));
        }

        private bool ExistCocktail(int id)
        {
            if (id != 0)
            {
                return _context.CocktailsSet.Any(e => e.Id.Equals(id));
            }
            else
            {
                return true;
            }
        }
        private bool BeValidProportionType(string proportionType)
        {
            var proportionTypeEnum = Enum.TryParse(typeof(ProportionType), proportionType, out var result);
            return proportionTypeEnum;
        }
    }
}