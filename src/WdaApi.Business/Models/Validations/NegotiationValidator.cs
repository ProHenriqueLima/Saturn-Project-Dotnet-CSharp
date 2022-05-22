using FluentValidation;

namespace WdaApi.Business.Models.Validations
{
    public class NegotiationValidator : AbstractValidator<Negotiation>
    {
        public NegotiationValidator()
        {
            //RuleFor(m => m.Users)
            //    .NotEmpty()
            //       .WithMessage("O campo Usuários precisa ser fornecido");

            RuleFor(m => m.StatusNegotiation)
                .NotEmpty()
                   .WithMessage("O campo Status Negociação precisa ser fornecido");

            RuleFor(m => m.Price)
                   .LessThan(0)
                     .WithMessage("O campo Preço precisa ser maior que 0");
        }
    }
}
