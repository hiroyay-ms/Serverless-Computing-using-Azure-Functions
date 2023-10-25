import azure.functions as func
import logging
import os
import pyodbc
import product as p
import json

app = func.FunctionApp(http_auth_level=func.AuthLevel.ANONYMOUS)

@app.route(route="GetProduct")
def GetProduct(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    id = req.params.get('id')
    if id:
        connectionString = os.environ['SqlConnectionString']
        query = "select" \
                    " p.ProductID," \
                    " pc.Name as CategoryName," \
                    " p.Name as ProductName," \
                    " p.Color," \
                    " p.StandardCost," \
                    " p.SellStartDate " \
                    "from [SalesLT].[Product] p" \
                    " inner join [SalesLT].[ProductCategory] pc on p.ProductCategoryID = pc.ProductCategoryID" \
                    f" where pc.ProductCategoryId = {id}"
        with pyodbc.connect(connectionString) as conn:
            with conn.cursor() as cursor:
                cursor.execute(query)
                rows = cursor.fetchall()
                products = []
                for row in rows:
                    products.append(json.dumps(vars(p.Product(row.ProductID, row.CategoryName, row.ProductName, row.Color, row.StandardCost, row.SellStartDate))))
                return func.HttpResponse(str(products), status_code=200, headers={"Content-Type": "application/json; charset=utf-8"})
    else:
        return func.HttpResponse("Please pass a valid id on the query string", status_code=400, headers={"Content-Type": "application/json; charset=utf-8"})