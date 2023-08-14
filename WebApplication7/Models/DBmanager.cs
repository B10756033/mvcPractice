using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.WebPages;

namespace WebApplication7.Models
{
    public class DBmanager
    {
        private readonly string ConnStr = Convert.ToString(ConfigurationManager.ConnectionStrings["WKODBConnectionString"].ConnectionString);

        public class Book
        {
            public string Barcode { get; set; }
            public string BookName { get; set; }
            public string Author { get; set; }
            public string PublishingHouse { get; set; }
            public DateTime PublicationDate { get; set; }
            public int Price { get; set; }
        }
        public List<Book> GetBooks() 
        {
            List<Book> bookk = new List<Book>();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Book");
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    Book card = new Book
                    {
                    };
                    int Barcode = reader.GetOrdinal("Barcode");
                    int BookName = reader.GetOrdinal("BookName");
                    int Author = reader.GetOrdinal("Author");
                    int PublishingHouse = reader.GetOrdinal("PublishingHouse");
                    int publicationDate = reader.GetOrdinal("PublicationDate");
                    int Price = reader.GetOrdinal("Price");
                    if (!reader.IsDBNull(Barcode))
                    {
                        card.Barcode = reader.GetString(Barcode);
                    }
                    if (!reader.IsDBNull(BookName))
                    {
                        card.BookName = reader.GetString(BookName);
                    }
                    if (!reader.IsDBNull(Author))
                    {
                        card.Author = reader.GetString(Author);
                    }
                    if (!reader.IsDBNull(PublishingHouse))
                    {
                        card.PublishingHouse = reader.GetString(PublishingHouse);
                    }
                    string priceStr = reader.GetValue(Price).ToString();
                    if (double.TryParse(priceStr, out double price2))
                    {
                        double asd = Math.Round(price2);
                        if (int.TryParse(asd.ToString(), out int price3))
                        { 
                            card.Price = price3;
                        }
                    }
                    if (!reader.IsDBNull(publicationDate))
                    {
                        card.PublicationDate = reader.GetDateTime(publicationDate);
                    }

                    bookk.Add(card);
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            sqlConnection.Close();
            return bookk;
        }
        public void NewBook(Book book) {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(
                @"INSERT INTO Book (Barcode, BookName, Author, PublishingHouse, PublicationDate, Price)
                    VALUES (@Barcode, @BookName, @Author, @PublishingHouse, @PublicationDate, @Price)");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@Barcode", book.Barcode));
            sqlCommand.Parameters.Add(new SqlParameter("@BookName", book.BookName));
            sqlCommand.Parameters.Add(new SqlParameter("@Author", book.Author));
            sqlCommand.Parameters.Add(new SqlParameter("@PublishingHouse", book.PublishingHouse));
            sqlCommand.Parameters.Add(new SqlParameter("@PublicationDate", book.PublicationDate));
            sqlCommand.Parameters.Add(new SqlParameter("@Price", book.Price));
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public Book GetBookId(string id)
        {
            Book book = new Book();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(
                @"SELECT * FROM Book WHERE Barcode = @id");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                //while (reader.Read())
                //{
                    
                //}
                book = new Book { };
                int Barcode = reader.GetOrdinal("Barcode");
                int BookName = reader.GetOrdinal("BookName");
                int Author = reader.GetOrdinal("Author");
                int PublishingHouse = reader.GetOrdinal("PublishingHouse");
                int publicationDate = reader.GetOrdinal("PublicationDate");
                int Price = reader.GetOrdinal("Price");
                if (!reader.IsDBNull(Barcode))
                {
                    book.Barcode = reader.GetString(Barcode);
                }
                if (!reader.IsDBNull(BookName))
                {
                    book.BookName = reader.GetString(BookName);
                }
                if (!reader.IsDBNull(Author))
                {
                    book.Author = reader.GetString(Author);
                }
                if (!reader.IsDBNull(PublishingHouse))
                {
                    book.PublishingHouse = reader.GetString(PublishingHouse);
                }
                if (!reader.IsDBNull(Price) && int.TryParse(reader.GetValue(Price).ToString(), out int price2))
                {
                    book.Price = reader.GetInt32(price2);
                }
                if (!reader.IsDBNull(publicationDate))
                {
                    book.PublicationDate = reader.GetDateTime(publicationDate);
                }
            }
            else {
                book.BookName = "未找到該筆資料";
            }
            sqlConnection.Close();
            return book;
        }
        public void updBook(Book book)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(
                @"UPDATE Book SET BookName = @BookName, Author = @Author,
                    PublishingHouse = @PublishingHouse, PublicationDate = @PublicationDate, Price = @Price
                    WHERE Barcode = @Barcode");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@Barcode", book.Barcode));
            sqlCommand.Parameters.Add(new SqlParameter("@BookName", book.BookName));
            sqlCommand.Parameters.Add(new SqlParameter("@Author", book.Author));
            sqlCommand.Parameters.Add(new SqlParameter("@PublishingHouse", book.PublishingHouse));
            sqlCommand.Parameters.Add(new SqlParameter("@PublicationDate", book.PublicationDate));
            sqlCommand.Parameters.Add(new SqlParameter("@Price", book.Price));
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void delBookById(string id)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(
                @"DELETE FROM Book
                    WHERE Barcode = @Barcode");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@Barcode", id));
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}