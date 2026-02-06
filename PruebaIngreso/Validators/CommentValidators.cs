using FluentValidation;
using PruebaIngreso.DTOs;

namespace PruebaIngreso.Validators
{
    public class CommentCreateValidator : AbstractValidator<CommentCreateDto>
    {
        public CommentCreateValidator()
        {
            RuleFor(x => x.CommentText)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }

    public class CommentUpdateValidator : AbstractValidator<CommentUpdateDto>
    {
        public CommentUpdateValidator()
        {
            RuleFor(x => x.CommentText)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
