﻿using iQuest.BooksAndNews.Application.DataAccess;
using iQuest.BooksAndNews.Application.Delegates;
using iQuest.BooksAndNews.Application.Publications;
using System;

namespace iQuest.BooksAndNews.Application.Publishers
{
    // todo: This class must be implemented.

    /// <summary>
    /// This is the class that will publish books and newspapers.
    /// It must offer a mechanism through which different other classes can subscribe ether
    /// to books or to newspaper.
    /// When a book or newspaper is published, the PrintingOffice must notify all the corresponding
    /// subscribers.
    /// </summary>

    public class PrintingOffice
    {
        private IBookRepository bookRepository;

        private INewspaperRepository newsPaperRepository;

        private ILog log;

        Book bookCreated = new Book();

        Newspaper newsPaperCreated = new Newspaper();

        public event creatingBook BookEvent;

        public event creatingNewspaper NewspaperEvent;

        public PrintingOffice(IBookRepository bookRepository, INewspaperRepository newspaperRepository, ILog log)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            this.newsPaperRepository = newspaperRepository ?? throw new ArgumentNullException(nameof(newspaperRepository));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public void PrintRandom(int bookCount, int newspaperCount)
        {
            for (int i = 0; i < bookCount; i++)
            {
                bookCreated = bookRepository.GetRandom();
                log.WriteInfo(bookCreated.Title + " by " + bookCreated.Author + ", " + bookCreated.Title +
                    " was printed.");
                OnRaiseCustomEventBook(bookCreated);
                Console.WriteLine();
            }
            Console.WriteLine();
            for (int i = 0; i < newspaperCount; i++)
            {
                newsPaperCreated = newsPaperRepository.GetRandom();
                log.WriteInfo(newsPaperCreated.Title + ", edition " + newsPaperCreated.Number + " was printed.");
                OnRaiseCustomEventNewspaper(newsPaperCreated);
                Console.WriteLine();
            }
        }
        protected virtual void OnRaiseCustomEventBook(Book book)
        {
            BookEvent?.Invoke(book);
        }
        protected virtual void OnRaiseCustomEventNewspaper(Newspaper newspaper)
        {
            NewspaperEvent?.Invoke(newspaper);
        }
    }
}
