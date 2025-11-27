using FluentValidation;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Validations
{
    public class CompraValidations : AbstractValidator<CompraDto>
    {
        public CompraValidations()
        {
            RuleFor(x => x.IdCompras).NotEmpty().WithMessage("Listado de id compras invalido");
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("Fecha invalido");
            RuleFor(x => x.IdUsuario).GreaterThan(0).WithMessage("IdUsuario invalido");
            RuleFor(x => x.Precio).NotEmpty().WithMessage("Precio invalido");
        }
    }
}
