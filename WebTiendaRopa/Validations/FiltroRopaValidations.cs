using FluentValidation;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Validations
{
    public class FiltroRopaValidations: AbstractValidator<FiltroRopaDto>
    {
        public FiltroRopaValidations()
        {

            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Nombre no ingresado");

            RuleFor(x => x.Tela).NotEmpty().WithMessage("Tela no ingresada");

            RuleFor(x => x.PrecioMax).NotEmpty().WithMessage("Precio maximo no ingresado");

            RuleFor(x => x.PrecioMin).NotEmpty().WithMessage("Precio minimo no ingresado");

            RuleFor(x => x.Tipo).NotEmpty().WithMessage("Tipo no ingresado");

            RuleFor(x => x).Must(x => x.PrecioMin <= x.PrecioMax).WithMessage("Precio mínimo no puede ser mayor al precio máximo");

            RuleFor(x => x).Must(x => x.PrecioMax >= x.PrecioMin).WithMessage("El precio máximo no puede ser menor al precio mínimo");

        }

    }
}
