using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Service.Dtos.StudentDtos
{
    public class StudentEditDto
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public decimal Point { get; set; }
    }

    public class StudentEditDtoValidator : AbstractValidator<StudentEditDto>
    {
        public StudentEditDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).MinimumLength(6);
            RuleFor(x => x.GroupId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Point).LessThanOrEqualTo(100).GreaterThanOrEqualTo(0);
        }
    }
}
