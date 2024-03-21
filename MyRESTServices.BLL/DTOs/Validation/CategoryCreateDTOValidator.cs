using FluentValidation;

namespace MyRESTServices.BLL.DTOs.Validation
{
    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("isi dong");

        }
    }
}
