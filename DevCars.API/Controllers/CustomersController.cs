using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;

        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _dbContext.Customers;

            var customersViewModel = customers.Select(c => new CustomerItemViewModel(c.Id, c.FullName, c.Document, c.BirthDate));

            return Ok(customersViewModel);
        }

        //POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModel model)
        {
            var customer = new Customer(model.FullName, model.Document, model.BirthDate);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //POST api/customers/1/orders
        [HttpPost("{id}/orders")]
        public IActionResult PostOrders(int id, [FromBody] AddOrderInputModel model)
        {
            var extraItems = model.ExtraItems.Select(e=> new ExtraOrderItem(e.Description, e.Price)).ToList();

            var car = _dbContext.Cars.SingleOrDefault(c=> c.Id == model.IdCar);

            var order = new Order(model.IdCar, model.IdCustomer, car.Price, extraItems);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetOrder),
                new {id = order.IdCustomer, orderid = order.Id},
                model
                );
        }

        //GET api/customers/1/orders/3
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {
            var order = _dbContext.Orders
                .Include(o => o.ExtraItems)
                .SingleOrDefault(o => o.Id == orderid);


            if (order == null)
            {
                return NotFound();
            }

            var extraItems = order.ExtraItems.Select(e => e.Description).ToList();

            var orderViewModel = new OrderDetailsViewModel(order.Id, order.IdCustomer,order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }

        //PUT api/customers/1
        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok();
        }

        //Delete api/customers/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
