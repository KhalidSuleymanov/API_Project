using FluentValidation;

namespace Course.Service.Dtos.GroupDtos
{
    public class GroupEditDto
    {
        public string Name { get; set; }
    }

    public class GroupEditDtoValidatior : AbstractValidator<GroupEditDto>
    {
        public GroupEditDtoValidatior()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MinimumLength(2).MaximumLength(15);
        }
    }
}
