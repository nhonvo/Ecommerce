## Entity


1. `cart` table:

   - `cart_id` (int, foreign key to `carts.id`)
   - `book_id` (int, foreign key to `books.id`)
   - `quantity` (int)

   <!-- Relationship: One cart item belongs to one cart, and one cart has many cart items. One cart item corresponds to one book, and one book can have many cart items. -->

2. `books` table:

   - `id` (int, primary key)
   - `title` (string)
   - `author` (string)
   - `cover_image` (string)
   - `price` (decimal)
   - `genre_id` (int, foreign key to `genres.id`)

   Relationship: One book belongs to one genre, and one genre has many books.

3. `users` table:

   - `id` (int, primary key)
   - `name` (string)
   - `email` (string)
   - `password` (string)
   - `credit_balance` (decimal)

   Relationship: One user can have many orders, but one order belongs to one user. One user can have many reviews, but one review belongs to one user. One user can have many books in their wish list, but one book can be in many wish lists.

4. `reviews` table:

   - `id` (int, primary key)
   - `book_id` (int, foreign key to `books.id`)
   - `user_id` (int, foreign key to `users.id`)
   - `rating` (int)
   - `comments` (string)

   Relationship: One review belongs to one book, and one book has many reviews. One review belongs to one user, and one user has many reviews.

5. `orders` table:

   - `id` (int, primary key)
   - `user_id` (int, foreign key to `users.id`)
   - `purchase_date` (DateTime)
   - `order_total` (decimal)

   Relationship: One order has many order details, and one order detail belongs to one order. One order belongs to one user, and one user has many orders.

6. `order_details` table:

   - `id` (int, primary key)
   - `book_id` (int, foreign key to `books.id`)
   - `order_id` (int, foreign key to `orders.id`)
   - `quantity` (int)

   Relationship: One order detail corresponds to one book, and one book can have many order details. One order detail belongs to one order, and one order has many order details.

7. `wishlists` table:

   - `id` (int, primary key)
   - `book_id` (int, foreign key to `books.id`)
   - `user_id` (int, foreign key to `users.id`)

   Relationship: One wish list belongs to one user, and one user has many wish lists. One book can be in many wish lists.

---

## Behavior method:

1. Book Entity:

   - getAllBooks(): Return a list of all available books.

   - getBookById(int bookId): Return book details for a specific book by its ID.

   - searchBooks(String keyword): Return a list of books that match the search criteria based on title, author, or keyword.

   - addBook(Book book): Add a new book to the inventory.

   - updateBook(int bookId, Book book): Update the details of an existing book.

   - deleteBook(int bookId): Remove a book from .

2. Shopping Cart Entity:

   - getCart(int userId): Return the contents of the shopping cart for a specific user.

   - addToCart(int userId, int bookId): Add a book to the shopping cart for a specific user.

   - removeFromCart(int userId, int bookId): Remove a book from the shopping cart for a specific user.

3. Payment Entity:
   - pay(int userId, double amount): Deduct the purchase amount from the user's credit balance.

4. Review and Rating Entity:

   - addReviewAndRating(int userId, int bookId, ReviewAndRating reviewAndRating): Allow a user to leave a review and rating for a book they have purchased.

   - getReviewsAndRatings(int bookId): Return a list of all reviews and ratings for a specific book.

5. Wish List Entity:

   - addToWishList(int userId, int bookId): Add a book to the user's wish list.

   - removeFromWishList(int userId, int bookId): Remove a book from the user's wish list.

6. Order History Entity:
   - getOrderHistory(int userId): Return a list of all past orders and order details for a specific user.

7. User Entity:

   - createUser(User user): Create a new user account.

   - updateUser(int userId, User user): Update the details of an existing user account.

   - getUserById(int userId): Return user details for a specific user by their ID.

   - authenticateUser(String email, String password): Authenticate a user login.

   - updateUserCreditBalance(int userId, double amount): Update the user's credit balance.

8. Book Inventory Management Entity:

   - addBooksToInventory(List<Book> books): Add multiple books to the inventory.

   - updateBookInventory(int bookId, int quantity): Update the quantity of a specific book in the inventory.

   - removeBookFromInventory(int bookId): Remove a book from the inventory.

9. Email Notification Entity:

   - sendOrderConfirmationEmail(int userId, Order order): Send an email notification to a user for order confirmation.

   - sendNewBookReleaseEmail(List<User> users, Book book): Send an email notification to all users in the system for a new book release.

10. Advanced Search Entity:
   - searchBooksByMultipleCriteria(String title, String author, String genre, Date publicationDate): Return a list of books that match the search criteria based on multiple parameters.

11. User Profile Entity:
   - updateUserProfile(int userId, UserProfile userProfile): Update the user's profile information, such as their name, email address, and password.

12. User Authentication Entity:

   - generateJWT(User user): Generate a JWT token for a user.

   - validateJWT(String token): Validate a JWT token for authentication.

13. Postman Collection Entity:
   - exportPostmanCollection(): Export a Postman collection for testing the API endpoints.

14. Unit Test Entity:
   - testFunctionality(): Write unit tests to ensure the functionality of the API endpoints.