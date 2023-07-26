using FluentValidation;

namespace Course.Api.Dtos.StudentDtos
{
    public class StudentCreateDto
    {
        public string FullName { get; set; }
        public decimal Point { get; set; }
        public int GroupId { get; set; }
    }

    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100).MinimumLength(6);
            RuleFor(x => x.Point).LessThanOrEqualTo(100).GreaterThanOrEqualTo(0);
            RuleFor(x => x.GroupId).GreaterThanOrEqualTo(1);
        }
    }
}
