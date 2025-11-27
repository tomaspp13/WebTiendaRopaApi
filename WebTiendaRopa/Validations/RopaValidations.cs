using FluentValidation;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Validations
{
    public class RopaValidations : AbstractValidator<RopaIngresoDto>
    {
        public RopaValidations() { 
        


            RuleFor(x=>x.Name).NotEmpty().WithMessage("Nombre no ingresado");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Descripcion no ingresada");

            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stock no ingresado");

            RuleFor(x => x.UrlRopa).NotEmpty().WithMessage("Imagen no ingresada");

            RuleFor(x => x.Tipo).NotEmpty().WithMessage("Tipo no ingresado");

            RuleFor(x => x.Precio).NotEmpty().WithMessage("Precio no ingresado");

        }

    }
}
