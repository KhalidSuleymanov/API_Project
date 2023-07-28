using AutoMapper;
using Course.Core.Entities;
using Course.Core.Repositories;
using Course.Service.Dtos.Common;
using Course.Service.Dtos.StudentDtos;
using Course.Service.Exceptions;
using Course.Service.Interfaces;

namespace Course.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentService(IGroupRepository groupRepository, IStudentRepository studentRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        public CreatedResultGroupDto Create(StudentCreateDto dto)
        {
            if (!_groupRepository.IsExist(x => x.Id == dto.GroupId))
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "GroupId", $"Group not found by id: {dto.GroupId}");
            }
            if (_studentRepository.IsExist(x => x.FullName == dto.FullName))
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", $"Name already taken");
            }
            var entity = _mapper.Map<Student>(dto);
            _studentRepository.Add(entity);
            _studentRepository.Commit();
            return new CreatedResultGroupDto { Id = entity.Id };
        }
        public void Edit(int id, StudentEditDto dto)
        {
            var entity = _studentRepository.Get(x => x.Id == id);
            if (entity == null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Student not found by id: {id}");
            }
            if (entity.GroupId != dto.GroupId && !_groupRepository.IsExist(x => x.Id == dto.GroupId))
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "GroupId", "GroupId not found");
            }
            if (entity.FullName != dto.Name && _studentRepository.IsExist(x => x.FullName == dto.Name))
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name already taken");
            }
            entity.FullName = dto.Name;
            entity.GroupId = dto.GroupId;
            entity.Point = dto.Point;
            _studentRepository.Commit();
        }
        public List<StudentGetAllItemDto> GetAll()
        {
            var entities = _studentRepository.GetQueryable(x => true, "Group").ToList();
            return _mapper.Map<List<StudentGetAllItemDto>>(entities);
        }
        public StudentGetDto GetById(int id)
        {
            var entity = _studentRepository.Get(x => x.Id == id, "Group");
            if (entity == null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Student not found by id: {id}");
            }
            return _mapper.Map<StudentGetDto>(entity);
        }
        public void Delete(int id)
        {
            var entity = _studentRepository.Get(x => x.Id == id);
            if (entity == null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Student not found by id: {id}");
            }
            _studentRepository.Remove(entity);
            _studentRepository.Commit();
        }
        public PaginatedListDto<PaginatedListItemDto> GetAllPaginated(int page)
        {
            if (page < 1)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Page", "Page not found");
            }
            var query = _studentRepository.GetQueryable(x => true, "Group");
            var entities = query.Skip((page - 1) * 3).Take(3).ToList();
            var items = _mapper.Map<List<PaginatedListItemDto>>(entities);
            return new PaginatedListDto<PaginatedListItemDto>(items, page, 3, query.Count());
        }
    }
}
