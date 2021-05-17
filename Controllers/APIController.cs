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

        public APIController(IProductsService service)
        {
            _service = service;
        }

        //// GET: api/<APIController>
        [HttpGet]
        public IEnumerable<ProductDto> GetAll()
        {
            var productsdto = _service.GetAll();
            return productsdto;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get(int id)
        {
            var productdto = _service.GetById(id);
            return (productdto is null) ? NotFound() : Ok(productdto);
        }

        // POST api/<APIController>
        [HttpPost]
        public ActionResult CreateProduct([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _service.Create(dto);

            return Created($"/api/products/{id}", null);
        }

        // DELETE api/<APIController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var isDeleted = _service.Delete(id);
            return isDeleted ? NoContent() : NotFound();
        }

        // PUT api/<APIController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _service.Update(id, dto);

            return isUpdated ? Ok() : NotFound();

        }


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
