using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaOnline.Data;
using PizzeriaOnline.Models;
using PizzeriaOnline.Models.Dto;
using PizzeriaOnline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzeriaOnline.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IProductsService _service;
        private readonly IMapper _mapper;

        public APIController(IProductsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //// GET: api/<APIController>
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var products =await _service.GetAll();

            return _mapper.Map<List<ProductDto>>(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product =await _service.GetById(id);
            return (product is null) ? NotFound() : Ok(_mapper.Map<ProductDto>(product));
        }

        //// POST api/<APIController>
        //[HttpPost]
        //public ActionResult CreateProduct([FromBody] CreateProductDto dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var id = _service.Create(dto);

        //    return Created($"/api/products/{id}", null);
        //}

        //// DELETE api/<APIController>/5
        //[HttpDelete("{id}")]
        //public ActionResult Delete(int id)
        //{
        //    var isDeleted = _service.Delete(id);
        //    return isDeleted ? NoContent() : NotFound();
        //}

        //// PUT api/<APIController>/5
        //[HttpPut("{id}")]
        //public ActionResult Put(int id,[FromBody] UpdateProductDto dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var isUpdated = _service.Update(id, dto);

        //    return isUpdated ? Ok() : NotFound();

        //}


        //// GET: api/<APIController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<APIController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<APIController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<APIController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<APIController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
