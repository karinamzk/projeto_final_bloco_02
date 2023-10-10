using farmacia.Model;
using FluentValidation;

namespace farmacia.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(t => t.Tipo)
                .NotEmpty();
        }
    }
}
