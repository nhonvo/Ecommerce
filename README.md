# API-only Project: Online Bookstore

Build a simple RESTful API for an online bookstore that allows users to view a list of books, search for books, and add books to their shopping cart. The API should have the following features:

1. Books Endpoint: The API should have an endpoint that returns a list of all books "available" in the store. Each book should have a title, author, cover image, and price. (trong kho phải còn thì mới trả về)
<!-- // done: use global query filter x=>x.Invetory > 0  -->

>DONE

2. Search Functionality: The API should allow users to search for books based on title, author, or keyword. The search results should return a list of books that match the search criteria. (miễn sao tên cuốn sách hoặc tên tác giả chứa từ tìm kiếm là được)
<!-- done: add paging for search  -->
>DONE

3. Shopping Cart Endpoint: Users should be able to add books to their shopping cart, view their cart, and remove books from their cart. (lưu trữ thông tin của giỏ hàng)

>DONE

4. Payment Integration: The API should allow users to pay for their purchases using their credit balance. The API should deduct the purchase amount from the user's credit balance. (thanh toán bằng số tiền trong tài khoản của user)

> DONE

5. ASP.NET Core: The API should be built using ASP.NET Core and should utilize dependency injection for managing services and repositories.

>DONE

6. Review and Rating: Allow users to leave reviews and ratings for the books they purchase. Display these reviews on the book's detail page. (chỉ được review và rating sách đã mua, mua 1 lần thì review, rating 1 lần, tương tự cho 2 lần…)

> DONE

7. Wish List: Implement a wish list feature that allows users to save books for later purchase. (đánh dấu sách yêu thích)

> DONE

8. Pagination: Implement pagination for the books endpoint, allowing users to retrieve a subset of the results at a time.(phân trang cho dữ liệu)

>DONE

9. Order History: Allow users to view their past orders and order details, including purchase date, order total, and book titles.(lưu trữ lịch sử mua hàng, thông tin thanh toán..)

>Done

10. Advanced Search: Implement an advanced search feature that allows users to search for books based on multiple criteria, such as author, title, genre, and publication date. (search trên nhiều điều kiện, theo tác giả, theo ngày xuất bản).

>DONE

11. Email Notifications: Send email notifications to users for order confirmation and new book releases. (bất cứ khi nào đặt hàng thì user sẽ nhận được thông tin đơn hàng đó qua email, khi sách mới xuất bản thì sẽ thông báo cho tất cả các user trong hệ thống).

12. User Profile: Allow users to update their profile information, such as their name, email address, and password.(thay đổi thông tin user)

>DONE

13. Book Inventory Management: Implement a feature that allows the bookstore administrator to manage the book inventory, including adding new books, updating book details, and removing books from the inventory. (nhập hàng vô trong kho, tạo sách mới,)

>DONE

14. User Authentication: Users should be able to create an account, login. Only authenticated users should be able to add books to their cart. (with JWT)

>DONE

15. Postman Collection: Provide a Postman collection for testing the API endpoints.

>DONE

16. Unit Tests: Write unit tests to ensure the functionality of the API.

> DONE a part just for bookServices

Each question is 1 point you had to have at least 14/16 point to pass this module

## Architecture

- Project design pattern: Domain driven design (DDD).
  - WebApi
  - Application
  - Domain
  - Infrastructure

## How to run project

- Restore project `dotnet restore`

- rename connection string in `appsettings.json` consist of: `DatabaseConnection`

- use postmain collection in \Document\BookStore.postman_collection.json
---
TODO: add Ivalidator 
