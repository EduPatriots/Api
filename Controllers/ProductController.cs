using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseFirstDWB_Sabado.Backend;
using DBFirstBack_End.Models;
using DBFirstBack_End.DataAccess;
using Microsoft.AspNetCore.Http;


namespace ApiRestNorthwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductsSC productsService = new ProductsSC();

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            var products = productsService.GetData().Select(s => new Products
            {
                ProductId = s.ProductId,
                ProductName = s.ProductName,
                Discontinued = s.Discontinued
            }).ToList();
            return Ok(products);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            try
            {
                var product = productsService.GetDataByID(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorServer(ex);
            }

        }


        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductModel newProduct)
        {
            productsService.AddProduct(newProduct);
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string newName)
        {
            try
            {
                productsService.updateProductNameByID(id, newName);
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorServer(ex);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                productsService.DeleteProductByID(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorServer(ex);
            }

        }

        #region helpers
        private IActionResult ThrowInternalErrorServer(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        #endregion
    }
}
