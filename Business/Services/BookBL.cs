using Business.Interface;
using Model.Entites;
using Model.Request;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class BookBL:IBookBL
    {
        private readonly IBookRL bookRepo;
        public BookBL(IBookRL repo)
        {
            bookRepo = repo;
        }

        public bool addBook(BookRequest request)
        {
            return bookRepo.addBook((MapToEntity(request)));
        }

        public List<Book> getAllBook()
        {
            return bookRepo.getAllBook();
        }

        public Book getBookById(int bId)
        {
            return bookRepo.getBookById(bId);
        }

        private Book MapToEntity(BookRequest request)
        {
            return new Book
            {
                AuthorName = request.AuthorName,
                BookImage = request.BookImage,
                BookName = request.BookName,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity
            };
        }
    }
}
