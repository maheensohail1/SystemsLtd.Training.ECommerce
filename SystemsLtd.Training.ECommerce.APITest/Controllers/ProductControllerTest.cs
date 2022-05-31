﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemsLtd.Training.ECommerce.API.Controllers;
using SystemsLtd.Training.ECommerce.Model;
using SystemsLtd.Training.ECommerce.Service.Interface;
using Xunit;
using SystemsLtd.Training.ECommerce.API.Models;

namespace SystemsLtd.Training.ECommerce.APITests.Controllers
{
    public class ProductControllerTest
    {
        #region
        public List<Product> ProductData = new List<Product>
                {
                    new Product()
                    {
                        ProductId =1,
                        ProductName="Dell Laptop",
                        ProductDescription = "Dell Laptop E6410",
                        PurchasePrice = 1000.50m,
                        SalesPrice = 1200.00m,
                        Active = true,
                        CategoryId = 1
                    },
                    new Product()
                    {
                        ProductId =2,
                        ProductName="HP Laptop",
                        ProductDescription = "HP Laptop E6410",
                        PurchasePrice = 1000.50m,
                        SalesPrice = 1200.00m,
                        Active = true,
                        CategoryId = 1
                    },
                    new Product()
                    {
                        ProductId =3,
                        ProductName="Lenovo Laptop",
                        ProductDescription = "Lenovo Laptop E6410",
                        PurchasePrice = 1000.50m,
                        SalesPrice = 1200.00m,
                        Active = true,
                        CategoryId = 1
                    }
                };
        #endregion
        [Fact]
        public void GetProductsTest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            service.Setup(s => s.GetProducts()).Returns(
                this.ProductData
                );


            var controller = new ProductController(null, service.Object);

            // Act
            var res = controller.GetProducts();

            //Arrange
            Assert.NotNull(res);
            Assert.Equal(3, res.Count());
        }

        /*  public void AddProductsTest()
          {
              //Arrange
              var service = new Mock<IProductService>();
              var product = new ProductAddVM
              {
                  ProductName = "Haier Laptop",
                  ProductDescription = "Haier Laptop",
                  PurchasePrice = 1000.50m,
                  SalesPrice = 1200.00m,
                  Active = true,
                  CategoryId = 1
              };
              service.Setup(s => s.AddProduct(product)).Returns(
                  this.ProductData.Count() + 1
                  ) ;


              var controller = new ProductController(null, service.Object);

              // Act
              var res = controller.AddProduct(product);

              //Arrange
              Assert.NotNull(res);
              Assert.Equal(3, res.Count());
          }
        */
        [Fact]
        public void GetProductbyIDtest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            var id = 1;
            service.Setup(s => s.GetProductbyID(id)).Returns(
                this.ProductData[0]
                );
            var controller = new ProductController(null, service.Object);

            // Act
            var res = controller.GetProductbyID(id);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(id, res.ProductId);
        }

        

    }
}