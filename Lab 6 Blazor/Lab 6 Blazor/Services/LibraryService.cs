using System;
using System.Collections.Generic;
using System.Linq;
using Lab_6_Blazor.Data;

namespace Lab_6_Blazor.Services
{
    public class LibraryService
    {
        private List<Book> books = new List<Book>();
        private List<User> users = new List<User>();
        private Dictionary<User, List<Book>> borrowedBooks = new Dictionary<User, List<Book>>();

        // Books Management
        public void AddBook(string title, string author, string isbn)
        {
            int id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
            books.Add(new Book { Id = id, Title = title, Author = author, ISBN = isbn });
        }

        public bool DeleteBook(int bookId)
        {
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (book == null) return false;

            books.Remove(book);
            return true;
        }

        public List<Book> ListBooks() => books;

        // Users Management
        public void AddUser(string name, string email)
        {
            int id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(new User { Id = id, Name = name, Email = email });
        }

        public bool EditUser(User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user == null) return false;

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            return true;
        }

        public bool DeleteUser(int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            users.Remove(user);
            return true;
        }

        public List<User> ListUsers() => users;

        // Borrowing and Returning Books
        public bool BorrowBook(int bookId, int userId)
        {
            var book = books.FirstOrDefault(b => b.Id == bookId);
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (book == null || user == null) return false;

            if (!borrowedBooks.ContainsKey(user))
            {
                borrowedBooks[user] = new List<Book>();
            }
            borrowedBooks[user].Add(book);
            books.Remove(book);

            return true;
        }

        public bool ReturnBook(int bookId, int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user == null || !borrowedBooks.ContainsKey(user)) return false;

            var book = borrowedBooks[user].FirstOrDefault(b => b.Id == bookId);
            if (book == null) return false;

            borrowedBooks[user].Remove(book);
            books.Add(book);
            return true;
        }
        public void EditBook(Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.ISBN = updatedBook.ISBN;
            }
        }

        public Dictionary<User, List<Book>> ListBorrowedBooks() => borrowedBooks;
    }

}
