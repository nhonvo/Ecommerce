// using Application;
// using Application.Commons;
// using Application.Interfaces;
// using Application.ViewModels.Book;
// using AutoMapper;
// using Domain.Aggregate;
// using Domain.Aggregate.AppResult;
// using Domain.Entities;
// using Domain.Interfaces;
// using Microsoft.EntityFrameworkCore;

// namespace Infrastructures.Services
// {
//     public class BookService : IBookService
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly IMapper _mapper;
//         // private readonly IEmailService _emailService;

//         public BookService(
//             IUnitOfWork unitOfWork,
//             IEmailService emailService,
//             IMapper mapper)
//         {
//             _unitOfWork = unitOfWork;
//             _emailService = emailService;
//             _mapper = mapper;
//         }
//         /// <summary>
//         /// Gets paginating results from database. This is the asynchronous version of the get method. It returns the first page of results and the rest of the results.
//         /// </summary>
//         /// <param name="pageIndex">Index of the page. Must be greater than 0.</param>
//         /// <param name="pageSize">Size of the page. Must be greater than 0</param>
//         public async Task<ApiResult<Pagination<BookResponse>>> GetAsync(int pageIndex, int pageSize)
//         {
//             var result = await _unitOfWork.BookRepository.ToPagination(pageIndex, pageSize);
//             var books = _mapper.Map<Pagination<BookResponse>>(result);
//             /// Returns a list of books that can be retrieved from the books list.
//             if (books == null)
//                 return new ApiErrorResult<Pagination<BookResponse>>("Can't get book");
//             return new ApiSuccessResult<Pagination<BookResponse>>(books);
//         }
//         /// <summary>
//         /// Searches books by author title genre and price range. This is advanced and can be used for search by user's personal access
//         /// </summary>
//         /// <param name="request">Search request containing search parameters</param>
//         /// <param name="pageIndex">Index of page to return default is 0</param>
//         /// <param name="pageSize">Size of page to return default is 10 ( page</param>
//         public async Task<ApiResult<Pagination<BookResponse>>> AdvancedSearch(SearchRequest request, int pageIndex = 0, int pageSize = 10)
//         {
//             var query = _unitOfWork.BookRepository.AsQueryable();

//             /// Filter by author if the author is not null
//             if (!string.IsNullOrEmpty(request.Author))
//             {
//                 query = FilterByAuthor(query, request.Author);
//             }

//             /// Filter by title if not null
//             if (!string.IsNullOrEmpty(request.Title))
//             {
//                 query = FilterByTitle(query, request.Title);
//             }

//             /// Filter by genre if not null
//             if (!string.IsNullOrEmpty(request.Genre))
//             {
//                 query = FilterByGenre(query, request.Genre);
//             }

//             /// Filter by publication date.
//             if (request.PublicationDate.HasValue)
//             {
//                 query = FilterByPublicationDate(query, request.PublicationDate.Value);
//             }

//             /// Filter by price range.
//             if (request.StartPrice.HasValue && request.EndPrice.HasValue)
//             {
//                 query = FilterByPriceRange(query, request.StartPrice.Value, request.EndPrice.Value);
//             }

//             var book = await query.Skip((pageIndex) * pageSize).Take(pageSize).ToListAsync();
//             var result = _mapper.Map<List<BookResponse>>(book);
//             var books = new Pagination<BookResponse>
//             {
//                 Items = result,
//                 PageIndex = pageIndex,
//                 PageSize = pageSize,
//                 TotalItemsCount = await query.CountAsync()
//             };

//             /// Returns a list of books that can be retrieved from the books. Items field.
//             if (books.Items == null)
//             {
//                 return new ApiErrorResult<Pagination<BookResponse>>("Can't get book");
//             }

//             return new ApiSuccessResult<Pagination<BookResponse>>(books);
//         }

//         /// <summary>
//         /// Filter by author. This is used to find books that belong to a particular author. For example if you have a book with a name " Bob " and the author name " John Doe " you would find all books in the database with the name " John Doe "
//         /// </summary>
//         /// <param name="query">The query to filter.</param>
//         /// <param name="author">The author to search for in the query. Note that this is a case</param>
//         private IQueryable<Product> FilterByAuthor(IQueryable<Product> query, string author)
//         {
//             return query.Where(x => x.Name.Contains(author));
//         }

//         /// <summary>
//         /// Filters a query by title. This is used to find books that have a title that contains the string passed as a parameter
//         /// </summary>
//         /// <param name="query">The query to filter.</param>
//         /// <param name="title">The title to search for in the query's</param>
//         private IQueryable<Product> FilterByTitle(IQueryable<Product> query, string title)
//         {
//             return query.Where(x => x.Name.Contains(title));
//         }

//         /// <summary>
//         /// Filter by genre. This is used to find books that have been added to the book list
//         /// </summary>
//         /// <param name="query">The query to filter.</param>
//         /// <param name="genre">The genre to search for in the query</param>
//         private IQueryable<Product> FilterByGenre(IQueryable<Product> query, string genre)
//         {
//             return query.Where(x => x.Name.Contains(genre));
//         }

//         /// <summary>
//         /// Filter by publication date. Used to get books that have been published at a certain date. This is a helper method for GetBook
//         /// </summary>
//         /// <param name="query">The query to filter.</param>
//         /// <param name="publicationDate">The date to filter by. Must be in the format YYYYMM</param>
//         private IQueryable<Product> FilterByPublicationDate(IQueryable<Product> query, DateTime publicationDate)
//         {
//             return query.Where(x => x.CreationDate.Year == publicationDate.Year
//                                     && x.CreationDate.Month == publicationDate.Month
//                                     && x.CreationDate.Day == publicationDate.Day);
//         }

//         /// <summary>
//         /// Filters a query by price range. This is used to get books that have been purchased in the last 24 hours
//         /// </summary>
//         /// <param name="query">The query to filter.</param>
//         /// <param name="startPrice">The price to start with. Must be greater than or equal to this value.</param>
//         /// <param name="endPrice">The price to end with. Must be greater than or equal to this value</param>
//         private IQueryable<Product> FilterByPriceRange(IQueryable<Product> query, decimal startPrice, decimal endPrice)
//         {
//             return query.Where(x => x.Price > startPrice && x.Price < endPrice);
//         }

//         /// <summary>
//         /// Search for books by title author genre. This is used to search for a book in the database
//         /// </summary>
//         /// <param name="query">The string to search for</param>
//         /// <param name="pageIndex">The page index to search. 0 for first page</param>
//         /// <param name="pageSize">The size of the page to search. 0 for</param>
//         public async Task<ApiResult<Pagination<BookResponse>>> Search(string query, int pageIndex, int pageSize)
//         {
//             var result = await _unitOfWork.BookRepository.GetAsync(x => x.Name.Contains(query)
//                                                                         || x.Name.Contains(query)
//                                                                         || x.Name.Contains(query), pageIndex, pageSize);
//             var books = _mapper.Map<Pagination<BookResponse>>(result);
//             /// Returns a list of books that can be retrieved from the books list.
//             if (books == null)
//                 return new ApiErrorResult<Pagination<BookResponse>>("Can't get book");
//             return new ApiSuccessResult<Pagination<BookResponse>>(books);
//         }
//         /// <summary>
//         /// Sends mail to users who have created a book. This is used to send books to users when they are created
//         /// </summary>
//         /// <param name="book">Book that is created</param>
//         public async Task SendMail(BookResponse book)
//         {
//             SendMailRequest email = new SendMailRequest();

//             var user = await _unitOfWork.UserRepository.GetAsync(x => x.Id != null);

//             var recipients = new List<ToRecipient>();
//             foreach (var item in user)
//             {
//                 var toRecipient = new ToRecipient
//                 {
//                     emailAddress = new EmailAddress
//                     {
//                         address = item.Email
//                     }
//                 };
//                 recipients.Add(toRecipient);
//             }

//             email.message.toRecipients = recipients;

//             string content = "<h3>New Book Release:</h3>" +
//                   $"<p><strong>Title:</strong> {book.Title}</p>" +
//                   $"<p><strong>Author:</strong> {book.Author}</p>" +
//                   $"<p><strong>Genre:</strong> {book.Genre}</p>" +
//                   $"<p><strong>Price:</strong> {book.Price} Đ</p>" +
//                   $"<p><strong>Inventory:</strong> {book.Inventory}</p>" +
//                   $"<img src=\"{book.Image}\" alt=\"Book Cover Image\" style=\"max-width: 200px;\" alt=\"{book.Image}\">" +
//                   "<p><a href=\"#\">Visit our website</a> to purchase this book and browse our other titles.</p>";
//             email.message.body.content = content;
//             var mail = await _emailService.SendEmailAsync(email);
//         }
//         /// <summary>
//         /// Add a book to the database. This will create a new book if it doesn't exist.
//         /// </summary>
//         /// <param name="request">The request to add a book to the database</param>
//         public async Task<ApiResult<BookResponse>> AddAsync(CreateBook request)
//         {
//             var book = _mapper.Map<Product>(request);
//             try
//             {
//                 _unitOfWork.BeginTransaction();
//                 await _unitOfWork.BookRepository.AddAsync(book);
//                 await _unitOfWork.CommitAsync();
//                 var result = _mapper.Map<BookResponse>(book);

//                 await SendMail(result);

//                 return new ApiSuccessResult<BookResponse>(result);
//             }
//             catch (Exception ex)
//             {
//                 _unitOfWork.Rollback();
//                 return new ApiErrorResult<BookResponse>("Can't add book", new List<string> { ex.ToString() });
//             }
//         }
//         /// <summary>
//         /// Update a book in the database. This will create a new record if it doesn't exist.
//         /// </summary>
//         /// <param name="request">Information about the book to be updated ( id and other fields</param>
//         public async Task<ApiResult<BookResponse>> Update(UpdateBook request)
//         {
//             var bookExist = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
//             /// Book is not found.
//             if (bookExist == null)
//                 return new ApiErrorResult<BookResponse>("Book not found");
//             var book = _mapper.Map<Product>(request);
//             // book.AverageRating = bookExist.AverageRating;
//             // book.RatingCount = bookExist.RatingCount;
//             // book.TotalRating = bookExist.TotalRating;
//             try
//             {
//                 _unitOfWork.BeginTransaction();
//                 _unitOfWork.BookRepository.Update(book);
//                 await _unitOfWork.CommitAsync();
//                 var result = _mapper.Map<BookResponse>(book);
//                 return new ApiSuccessResult<BookResponse>(result);
//             }
//             catch (Exception ex)
//             {
//                 _unitOfWork.Rollback();
//                 return new ApiErrorResult<BookResponse>("Can't update book", new List<string> { ex.ToString() });
//             }
//         }
//         public async Task<ApiResult<BookResponse>> Delete(string Id)
//         {
//             var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id.ToString() == Id);
//             /// Return a BookResponse object if book is null
//             if (book == null)
//                 return new ApiErrorResult<BookResponse>("Book not found");
//             try
//             {
//                 _unitOfWork.BeginTransaction();
//                 _unitOfWork.BookRepository.Delete(book);
//                 await _unitOfWork.CommitAsync();
//                 var result = _mapper.Map<BookResponse>(book);
//                 return new ApiSuccessResult<BookResponse>(result);
//             }
//             catch (Exception ex)
//             {
//                 _unitOfWork.Rollback();
//                 return new ApiErrorResult<BookResponse>("Can't delete book", new List<string> { ex.ToString() });
//             }
//         }
//         public async Task<ApiResult<BookResponse>> GetByIdAsync(string Id)
//         {
//             var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id.ToString() == Id);
//             var result = _mapper.Map<BookResponse>(book);
//             /// Returns the result of the book.
//             if (result == null)
//                 return new ApiErrorResult<BookResponse>("Not found the book");
//             return new ApiSuccessResult<BookResponse>(result);
//         }
//     }
// }
