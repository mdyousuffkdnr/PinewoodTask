using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pinewood.Api.DataAccess;
using Pinewood.Api.Models;

namespace Pinewood.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContextInMemory _dbContext;

        public CustomerController(AppDbContextInMemory dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var customers = _dbContext.Customers.ToList();

                if (!customers.Any())
                {
                    return NotFound("No Customer is found");
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            try
            {
                var customer = _dbContext.Customers.SingleOrDefault(x => x.Id == id);
                if (customer == null)
                {
                    return NotFound($"Customer with id {id} not found");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult Post(Customer model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid customer detail");
                }
                _dbContext.Customers.Add(model);
                _dbContext.SaveChanges();
                return Ok("Customer Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Customer model)
        {
            try
            {
                if (model == null || model.Id == 0)
                {
                    return BadRequest("Invalid customer detail");
                }

                var customer = _dbContext.Customers.Find(model.Id);
                if (customer == null)
                {
                    return BadRequest("Invalid customer detail");
                }

                customer.Name = model.Name;
                customer.Addr1 = model.Addr1;
                customer.Addr2 = model.Addr2;
                customer.City = model.City;
                customer.PostCode = model.PostCode;

                _dbContext.SaveChanges();

                return Ok("Updated!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var customer = _dbContext.Customers.Find(id);

                if (customer == null)
                {
                    return BadRequest("Customer not found");
                }

                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
                return Ok("Customer Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
