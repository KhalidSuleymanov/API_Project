using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Course.Service.Dtos.GroupDtos
{
    public class GroupCreateDto
    {
        public string Name { get; set; }
    }
    public class GroupCreateDtoValidatior : AbstractValidator<GroupCreateDto>
    {
        public GroupCreateDtoValidatior()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MinimumLength(2).MaximumLength(15);
        }
    }
}
