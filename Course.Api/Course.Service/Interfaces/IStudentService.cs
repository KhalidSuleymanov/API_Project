using Course.Service.Dtos.Common;
using Course.Service.Dtos.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Service.Interfaces
{
    public interface IStudentService
    {
        CreatedResultGroupDto Create(StudentCreateDto dto);
        StudentGetDto GetById(int id);
        List<StudentGetAllItemDto> GetAll();
        void Edit(int id, StudentEditDto dto);
        void Delete(int id);
    }
}
