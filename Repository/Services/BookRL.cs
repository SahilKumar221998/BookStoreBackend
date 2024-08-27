using Dapper;
using Model.Entites;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class BookRL:IBookRL
    {
        private readonly DapperContext context;

        public BookRL(DapperContext context)
        {
            this.context = context;
        }
        public bool addBook(Book book)
        {
            IDbConnection con = context.CreateConnection();
            var parameters = new
            {
                BookName = book.BookName,
                BookImage = book.BookImage,
                Description = book.Description,
                AuthorName = book.AuthorName,
                Quantity = book.Quantity,
                Price = book.Price
            };
            try
            {
                int rows = con.Execute("AddBooks", parameters, commandType: CommandType.StoredProcedure);
                return rows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the book.", ex);
            }
        }
        public List<Book> getAllBook()
        {
            IDbConnection con = context.CreateConnection();
            try
            {
                return con.Query<Book>("GetAllBooks", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all books.", ex);
            }
        }
        public Book getBookById(int bId)
        {
            IDbConnection con = context.CreateConnection();
            var parameters = new { BookId = bId };
            try
            {
                return con.Query<Book>("GetBookById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the book by ID.", ex);
            }
        }

    }
}
