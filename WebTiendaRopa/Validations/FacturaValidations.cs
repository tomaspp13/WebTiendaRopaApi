using FluentValidation;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Validations
{
    public class FacturaValidations : AbstractValidator<FacturaRespuestaDto>
    {
        public FacturaValidations()
        {
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("Fecha no ingresada");
            RuleFor(x => x.Precio).NotEmpty().WithMessage("Precio no ingresado");
        }
    }
}