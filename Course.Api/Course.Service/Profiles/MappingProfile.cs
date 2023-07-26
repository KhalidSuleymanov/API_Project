using AutoMapper;
using Course.Core.Entities;
using Course.Service.Dtos.GroupDtos;
using Course.Service.Dtos.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Service.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GroupCreateDto, Group>();
            CreateMap<Group, GroupGetDto>();
            CreateMap<Group, GroupInStudentGetDto>();
            CreateMap<Group, GroupGetAllItemDto>();
            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentGetDto>();
            CreateMap<Student, StudentGetAllItemDto>();
        }
    }
}
