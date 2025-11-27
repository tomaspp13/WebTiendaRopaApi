using FluentValidation;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Validations
{
    public class UsuarioRegistroValidations : AbstractValidator<UsuarioRegistroDto>
    {
        public UsuarioRegistroValidations()
        {

            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Nombre no ingresado");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail no ingresado");

            RuleFor(x => x.Contraseña).NotEmpty().WithMessage("Contraseña no ingresada");

        }

    }
}
