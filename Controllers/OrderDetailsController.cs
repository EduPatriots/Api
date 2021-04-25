using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseFirstDWB_Sabado.Backend;
using DBFirstBack_End.DataAccess;
using DBFirstBack_End.Models;
using Microsoft.AspNetCore.Http;

namespace ApiRestNorthwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private OrdersSC orderDetailsService = new OrdersSC();

        [HttpGet]
        public IActionResult Get()
        {
            var orders = orderDetailsService.GetData().Select(s => new OrderDetails
            {
                OrderId = s.OrderId,
                ProductId = s.ProductId,
                UnitPrice = s.UnitPrice,
                Quantity = s.Quantity
            }).ToList();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            try
            {
                var order = orderDetailsService.GetDataByID(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorServer(ex);
            }
        }

        [HttpPost]

        public IActionResult Post([FromBody] OrderDetailsModel newOrder)
        {
            orderDetailsService.AddOrder(newOrder);
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] short newQuantity)
        {
            try
            {
                orderDetailsService.UpdateQuantityOrderById(id, newQuantity);
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowInternalErrorServer(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                orderDetailsService.DeleteOrderById(id);
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
