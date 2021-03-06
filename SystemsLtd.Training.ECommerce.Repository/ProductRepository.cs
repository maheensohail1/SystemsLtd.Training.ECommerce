using Microsoft.Extensions.Logging;
using SystemsLtd.Training.ECommerce.Repository.Interface;
using SystemsLtd.Training.ECommerce.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace SystemsLtd.Training.ECommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        #region Properties
        private readonly ILogger<ProductRepository> Logger;
        private readonly ECommerceDBContext ECommerceDBContext;
        private static List<Product> Products = new List<Product>()
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

        #region Constractor
        public ProductRepository(ILogger<ProductRepository> logger, ECommerceDBContext eCommerceDBContext)
        {
            this.Logger = logger;
            this.ECommerceDBContext = eCommerceDBContext;
        }
        #endregion

        #region Public Methods
        public IEnumerable<Product> GetProducts()
        {
            return this.ECommerceDBContext.Products.ToList();
        }

        public Product GetProductbyID(int id)
        {
            using (SqlConnection conn = new SqlConnection("Server=KHILT-8734;Database=ECommerce;Trusted_Connection=True;"))
            {
                conn.Open();

               var product = conn.QuerySingle<Product>("SELECT * FROM Product WHERE ProductId = @id", new { id = id });

                // var product = builder.Query<Product>().FirstOrDefault();


                 return product;
            }
        }
        public Product GetProduct(int id)
        {
            return ProductRepository.Products.FirstOrDefault(x => x.ProductId == id);
        }

        public IEnumerable<Product> GetAllProducts(Product product)
        {
            var products = new List<Product>();

            //var products = ProductRepository.Products; 

            if(product!=null)
            {
                if(!string.IsNullOrWhiteSpace(product.ProductName))
                {
                    products = this.ECommerceDBContext.Products.Where(x => x.ProductName.Contains(product.ProductName)).ToList();
                    //products = products.Where(x => x.ProductName.Contains(product.ProductName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                }

                if (product.CategoryId > 0)
                {
                    products= this.ECommerceDBContext.Products.Where(x => x.CategoryId == product.CategoryId).ToList();
                    //products = products.Where(x => x.CategoryId == product.CategoryId).ToList();
                }
                
                products = this.ECommerceDBContext.Products.Where(x => x.Active == product.Active).ToList();
                //products = products.Where(x => x.Active == product.Active).ToList();
            }

            return products;
             
        }

        public int AddProduct(Product product)
        {
            var res = this.ECommerceDBContext.Products.Add(product);
            
            var result = this.ECommerceDBContext.SaveChanges();

            return res.Entity.ProductId;   
        }

        public bool UpdateProduct(Product product)
        {
            var existingProduct = this.ECommerceDBContext.Products.FirstOrDefault(x=> x.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductDescription = product.ProductDescription;    
                existingProduct.PurchasePrice = product.PurchasePrice;  
                existingProduct.SalesPrice = product.SalesPrice;
                existingProduct.Active = product.Active;  
                existingProduct.CategoryId = product.CategoryId;

                var result = this.ECommerceDBContext.SaveChanges();

                return result > 0;
            }
            else
            {
                return false;
            }
        }


        public bool DeleteProduct(Product product)
        {
            var existingProduct = this.ECommerceDBContext.Products.Remove(product);

            var result = this.ECommerceDBContext.SaveChanges();

            return result > 0;
        }

        #endregion
    }
}
