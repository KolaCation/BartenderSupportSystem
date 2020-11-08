using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using FluentValidation;

namespace BartenderSupportSystem.Server.Validators.RecommendationSystem
{
    public class CocktailValidator : AbstractValidator<CocktailDto>
    {
        private readonly ApplicationDbContext _context;

        public CocktailValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 chars long.")
                .MaximumLength(60).WithMessage("Name must not exceed 60 chars.");
            RuleFor(e => e.CocktailType)
                .NotEmpty().WithMessage("Cocktail type is required.")
                .Must(BeValidCocktailType).WithMessage("Provide a type from the list.");
            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(2).WithMessage("Description must be at least 2 chars long.")
                .MaximumLength(255).WithMessage("Description must not exceed 255 chars.");
            RuleForEach(e => e.Ingredients).SetValidator(new IngredientValidator(context));
        }

        private bool BeValidCocktailType(string cocktailType)
        {
            var cocktailTypeEnum = Enum.TryParse(typeof(CocktailType), cocktailType, out var result);
            return cocktailTypeEnum;
        }
    }
}
