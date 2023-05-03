# DOMAIN-DRIVEN-TEMPLATE

// TODO: CHECK THE MAPPING configuration

## Architecture

- Project design pattern: Domain driven design (DDD).
  - WebApi
  - Application
  - Domain
  - Infrastructure

## Some patterns

- Unit of work
- Generic repository
- Fluent api configuration
- ...

## How to run project

- Restore project `dotnet restore`

- rename connection string in `appsettings.json` consist of: `DatabaseConnection`

1. Create a controller class for each entity with endpoints for CRUD operations.

2. Implement the following endpoints:
  
   GET /api/customers: Return a list of all customers.
   GET /api/customers/{id}: Return a single customer by ID.
   POST /api/customers: Create a new customer.
   PUT /api/customers/{id}: Update an existing customer by ID.
   DELETE /api/customers/{id}: Delete a customer by ID.

   GET /api/orders: Return a list of all orders.
   GET /api/orders/{id}: Return a single order by ID.
   POST /api/orders: Create a new order for a customer.
   PUT /api/orders/{id}: Update an existing order by ID.
   DELETE /api/orders/{id}: Delete an order by ID.

   GET /api/products: Return a list of all products.
   GET /api/products/{id}: Return a single product by ID.
   POST /api/products: Create a new product.
   PUT /api/products/{id}: Update an existing product by ID.
   DELETE /api/products/{id}: Delete a product by ID.

3. Implement the following additional endpoints to support the relationships between entities:
   GET /api/customers/{id}/orders: Return a list of all orders for a customer.
   POST /api/customers/{id}/orders: Create a new order for a customer.
   PUT /api/customers/{id}/orders/{orderId}: Update an existing order for a customer.
   DELETE /api/customers/{id}/orders/{orderId}: Delete an order for a customer.

   GET /api/orders/{id}/orderItems: Return a list of all order items for an order.
   POST /api/orders/{id}/orderItems: Create a new order item for an order.
   PUT /api/orders/{id}/orderItems/{orderItemId}: Update an existing order item for an order.
   DELETE /api/orders/{id}/orderItems/{orderItemId}: Delete an order item for an order.
  
4. Test the endpoints using a tool like Postman & export it save to the git repository.

5. Add validation to the create and update endpoints to ensure that the data being submitted is valid (using fluent validation).

6. Add error handling to the endpoints to return appropriate HTTP error codes and error messages when something goes wrong.

7. Using unit of work, generic repository, repository to complete this exercise

8. Consider adding pagination, filtering, and sorting to the endpoints that return lists of entities to improve performance and usability.
// TODO: test getasync() which has sort feature

9. Search functionality a search endpoint for customers might allow searching by name, email, or phone number. This would allow users to easily find the customer they are looking for. GET /api/customers/search
a) Example: /api/customers/search?name=John%20Doe

10. Implement similier feature for orders and products
// TODO: TEST this api too

11. Statistics and reports: Create endpoints that allow users to view statistics and reports on customer orders. For example, an endpoint that returns _the total number of orders placed by a customer,_ or an endpoint that _returns the top-selling products_ in a given time period.
a) Implement endpoints for retrieving statistics and reports on customer orders.
b) For example, an endpoint that returns the total number of orders placed by a customer might look like this: GET /api/customers/{id}/orders/total
c) For example, an endpoint that returns the top-selling products in a given time period might look like this: GET /api/products/top-sellers?start=2022-01-01&end=2022-12-31

12. Feature: View customer orders with product details
   As a customer, I want to view my order history with details about the products I purchased so that I can keep track of my purchases.
   Implement a new endpoint: GET /api/customers/{id}/orders/details
   The endpoint should return a list of all orders for a customer with details about the products in each order.
   The product details should include the name, description, and price of each product.
   The endpoint should support pagination to limit the number of orders returned per page.
   The endpoint should return a 404 error if the customer ID is not found in the database.

13. Create an endpoint to retrieve total sales for a given time period by month:
a) GET /api/reports/sales/{year}/{month} This endpoint will accept the year and month for which the report is requested.
b) The response will include the total sales for each day of the month, as well as the overall total for the month.
c) Add support for filtering the report by date range:
d) Modify the existing endpoint to accept a start and end date range instead of just a month and year.
e) The response will include the total sales for each day within the date range, as well as the overall total for the range.
