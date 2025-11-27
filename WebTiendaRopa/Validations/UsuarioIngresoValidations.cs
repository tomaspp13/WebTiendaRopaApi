using FluentValidation;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Validations
{
    public class UsuarioIngresoValidations : AbstractValidator<UsuarioIngresoDto>
    {
        public UsuarioIngresoValidations()
        {

            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail no ingresado");

            RuleFor(x => x.Contraseña).NotEmpty().WithMessage("Contraseña no ingresada");

        }
            
    }
}
