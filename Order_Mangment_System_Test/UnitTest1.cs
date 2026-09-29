using Xunit;
using Week2_Task_2;
using Week2_Task_2.models;
using Week2_Task_2.Controllers;
using Week2_Task_2.services;
using Week2_Task_2.Data;
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week2_Task_2.Dto.order_item;
using Week2_Task_2.Dto.orders;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.AspNetCore.Http.HttpResults;
//ing Week2_Task_2.
namespace Order_Mangment_System_Test
{
    public class UnitTest1
    {
        private readonly data_base db;
        private readonly iservices s;

        [Fact]
        public void CreateOrder_CustomerNotFound_ReturnsBadRequest()
        {

            List<order> orders = new List<order>();


       /*   customer c = new customer
            {
                id = 10,
                name = "Ahmad mohee",
                email = "ahmad@gmail.com",
                orders = orders
            };*/
         // db.AddAsync(c);

            order o = new();
            orders.Add(o);
            db.order.AddAsync(o);
            db.SaveChanges();

            

        }
        [Fact]
        public async Task CreateOrder_QuantityZero_ReturnsBadRequest()
        {

            var connection = new SqliteConnection("DataSource=:memory:");
            await connection.OpenAsync();

            var options = new DbContextOptionsBuilder<data_base>().UseSqlite(connection) .Options;

            await using var db = new data_base(options);

            await db.Database.EnsureCreatedAsync();

            var customer = new customer
            {
                id = 1,
                name = "Ahmad",
                email = "ahmad@gmail.com"
            };

            await db.Customers.AddAsync(customer);

            var product = new product
            {
                id = 11,
                name = "Laptop",
                description = "a nice and good laptop",
                price = 1000,
                ksu = "L1",
                stock = 10,
                active = true
            };

            await db.prod.AddAsync(product);

            await db.SaveChangesAsync();

            var service = new services(db);

            var controller = new orderi(db, service);

            var dto = new add_order
            {
                customer_id = 1,

                items = new List<add_order_item>
        {
            new add_order_item
            {
                product_id = 11,
                quantity = 0
            }
        }
            };

            var result = await controller.Create(dto);

            var o =Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal("the quantity must be above 0", o.Value);
            

            await connection.CloseAsync();
        }
        [Fact]
        public async Task CreateOrder_QuantityAboveStock_ReturnsBadRequest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            await connection.OpenAsync();

            var options = new DbContextOptionsBuilder<data_base>().UseSqlite(connection).Options;

            await using var db = new data_base(options);

            await db.Database.EnsureCreatedAsync();
            var customer = new customer
            {
                id = 12,
                name = "shadi",
                email = "shadi@hotmail.com"
            };

            var product = new product
            {
                id = 15,
                name = "apple",
                description = "a red and fresh apple",
                price = 2,
                ksu = "Aa-00",
                stock = 1000,
                active = true
            };

            var service = new services(db);

            var controller = new orderi(db, service);
            await db.Customers.AddAsync(customer);
            await db.prod.AddAsync(product);

            await db.SaveChangesAsync();

            var dto = new add_order
            {
                customer_id = 12,

                items = new List<add_order_item>
                {
                    new add_order_item
                    {
                        product_id = 15,
                        quantity = 10001
                    }
                }
            };

            var result = await controller.Create(dto);
            var c = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal("the quatity of the order is above the stock ", c.Value);

            await connection.CloseAsync();
        }
    }
}
    
    
