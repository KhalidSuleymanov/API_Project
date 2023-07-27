using Course.Core.Entities;
using Course.Core.Repositories;
using Course.Service.Dtos.GroupDtos;
using Course.Service.Exceptions;
using Course.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet("all")]
        public ActionResult<List<GroupGetAllItemDto>> GetAll()
        {
            return Ok(_groupService.GetAll());
        }
        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> Get(int id)
        {
            return Ok(_groupService.GetById(id));
        }
        [HttpPost("")]
        public IActionResult Create(GroupCreateDto groupDto)
        {
            try
            {
                var result = _groupService.Create(groupDto);
                return StatusCode(201, result);
            }
            catch (EntityDublicateException e)
            {
                ModelState.AddModelError("Name", e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, GroupEditDto brandDto)
        {
            try
            {
                _groupService.Edit(id, brandDto);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
            catch (EntityDublicateException e)
            {
                ModelState.AddModelError("Name", e.Message);
                return BadRequest(ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _groupService.Remove(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
