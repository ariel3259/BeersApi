using BeersApi.Dto;
using FluentValidation;

namespace BeersApi.Validatords
{
    public class DrinksRequestValidator: AbstractValidator<DrinksRequest>
    {
        public DrinksRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.AlcoholRate).NotEmpty();
            RuleFor(x => x.Price).NotEmpty(); 
            RuleFor(x => x.DrinkTypeId).NotEmpty();
        }
    }
}
