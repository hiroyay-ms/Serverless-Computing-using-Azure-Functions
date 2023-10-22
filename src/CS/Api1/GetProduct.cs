using System.Data;
using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api1
{
    public class GetProduct
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public GetProduct(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<GetProduct>();
            _configuration = configuration;
        }

        [Function("GetProduct")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string jsonStr;

            if (int.TryParse(req.Query["id"], out int id))
            {
                string connectionString = _configuration.GetValue<string>("SqlConnectionString");
                string query = "select" +
                    " p.ProductID," +
                    " pc.Name as CategoryName," +
                    " p.Name as ProductName," + 
                    " p.Color," +
                    " p.StandardCost," +
                    " p.SellStartDate " +
                    "from [SalesLT].[Product] p" +
                    " inner join [SalesLT].[ProductCategory] pc on p.ProductCategoryID = pc.ProductCategoryID" +
                    " where pc.ProductCategoryId = @id";

                var products = new List<Product>();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(
                                    new Product
                                    {
                                        ProductId = reader.GetInt32(0),
                                        CategoryName = reader.GetString(1),
                                        ProductName = reader.GetString(2),
                                        Color = reader.GetString(3),
                                        StandardCost = reader.GetDecimal(4),
                                        SellStartDate = reader.GetDateTime(5)
                                    }
                                );
                            }
                        }
                    }
                }

                jsonStr = JsonSerializer.Serialize(products);
            }
            else
            {
                jsonStr = JsonSerializer.Serialize(new { error = "Please pass a valid id on the query string" });
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            response.WriteString(jsonStr);

            return response;
        }
    }
}
