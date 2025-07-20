﻿using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi
{
    public class SeedData
    {
        public static void SetInitialData()
        {
            var ernestMonkjack = new Author
            {
                Name = "Ernest Monkjack"
            };
            var sarahKennedy = new Author
            {
                Name = "Sarah Kennedy"
            };
            var margaretJones = new Author
            {
                Name = "Margaret Jones"
            };

            var clayBook = new Book
            {
                Name = "The Importance of Clay",
                Format = BookFormat.Paperback,
                Author = ernestMonkjack,
                ISBN = "1305718181"
            };

            var agileBook = new Book
            {
                Name = "Agile Project Management - A Primer",
                Format = BookFormat.Hardback,
                Author = sarahKennedy,
                ISBN = "1293910102"
            };

            var rustBook = new Book
            {
                Name = "Rust Development Cookbook",
                Format = BookFormat.Paperback,
                Author = margaretJones,
                ISBN = "3134324111"
            };

            var daveSmith = new Borrower
            {
                Name = "Dave Smith",
                EmailAddress = "dave@smithy.com"
            };

            var lianaJames = new Borrower
            {
                Name = "Liana James",
                EmailAddress = "liana@gmail.com"
            };

            var jonesMayer = new Borrower
            {
                Name = "Jones Mayer",
                EmailAddress = "mayer@gmail.com"
            };


            var bookOnLoanUntilToday = new BookStock
            {
                Book = clayBook,
                OnLoanTo = daveSmith,
                LoanEndDate = DateTime.Now.Date
            };

            var bookNotOnLoan = new BookStock
            {
                Book = clayBook,
                OnLoanTo = null,
                LoanEndDate = null
            };

            var bookOnLoanUntilNextWeek = new BookStock
            {
                Book = agileBook,
                OnLoanTo = lianaJames,
                LoanEndDate = DateTime.Now.Date.AddDays(7)
            };

            var rustBookStock = new BookStock
            {
                Book = rustBook,
                OnLoanTo = null,
                LoanEndDate = null
            };

            var reservationAgileBookToday = new Reservation
            {
                Book = rustBook,
                Borrower = jonesMayer,
                IsActive = true,
                ReservationDate = DateTime.Now.Date,
            };

            var reservationAgileBookNextDay = new Reservation
            {
                Book = rustBook,
                Borrower = daveSmith,
                IsActive = true,
                ReservationDate = DateTime.Now.Date.AddDays(1),
            };

            var reservationAgileBookThreeDays = new Reservation
            {
                Book = rustBook,
                Borrower = lianaJames,
                IsActive = true,
                ReservationDate = DateTime.Now.Date.AddDays(3),
            };


            var reservationNoActive = new Reservation
            {
                Book = clayBook,
                Borrower = lianaJames,
                IsActive = false,
                ReservationDate = DateTime.Now.Date.AddMonths(-2),
            };

          


            var PayFine = new Fine
            {
                Borrower = lianaJames,
                Amount = 2.5m,
                CreatedDate = DateTime.Now,
            };


            using (var context = new LibraryContext())
            {
                context.Authors.Add(ernestMonkjack);
                context.Authors.Add(sarahKennedy);
                context.Authors.Add(margaretJones);


                context.Books.Add(clayBook);
                context.Books.Add(agileBook);
                context.Books.Add(rustBook);

                context.Borrowers.Add(daveSmith);
                context.Borrowers.Add(lianaJames);
                context.Borrowers.Add(jonesMayer);

                context.Catalogue.Add(bookOnLoanUntilToday);
                context.Catalogue.Add(bookNotOnLoan);
                context.Catalogue.Add(bookOnLoanUntilNextWeek);
                context.Catalogue.Add(rustBookStock);

                context.Reservations.Add(reservationAgileBookThreeDays);
                context.Reservations.Add(reservationAgileBookToday);
                context.Reservations.Add(reservationAgileBookNextDay);
                context.Reservations.Add(reservationNoActive);

                context.Fines.Add(PayFine);

                context.SaveChanges();

            }
        }
    }
}
