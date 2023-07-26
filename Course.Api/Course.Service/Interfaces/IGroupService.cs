using Course.Service.Dtos.Common;
using Course.Service.Dtos.GroupDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Service.Interfaces
{
    public interface IGroupService
    {
        CreatedResultGroupDto Create(GroupCreateDto createDto);
        void Edit(int id, GroupEditDto editDto);
        GroupGetDto GetById(int id);
        List<GroupGetAllItemDto> GetAll();
        void Remove(int id);
    }
}
