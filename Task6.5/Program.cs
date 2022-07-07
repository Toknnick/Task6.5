﻿using System;
using System.Collections.Generic;

namespace Task6._5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StorageOfBooks storageOfBooks = new StorageOfBooks();
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("1.Добавить книгу в хранилище.\n2.Убрать книгу.\n3.Показать все книги.\n4.Показать книгу по определенному параметру.\n5.Выход");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        storageOfBooks.AddBook();
                        break;
                    case "2":
                        storageOfBooks.DeleteBook();
                        break;
                    case "3":
                        storageOfBooks.ShowAllBooksInfo();
                        break;
                    case "4":
                        storageOfBooks.ShowSomeBookInfo();
                        break;
                    case "5":
                        isWork = false;
                        break;
                    default:
                        storageOfBooks.WriteError();
                        break;
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу:");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class StorageOfBooks
    {
        private List<Book> _books = new List<Book>();

        public void AddBook()
        {
            Console.Clear();
            string nameOfBook = GetSomeString("Введите название книги:");
            string author = GetSomeString("Введите автора книги:");
            int yearOfIssue = GetSomeInt("Введите год написания книги:");
            int numbersOfPages = GetSomeInt("Введите количество страниц в книге:");
            _books.Add(new Book(nameOfBook, author, yearOfIssue, numbersOfPages));
        }

        public void DeleteBook()
        {
            ShowAllBooksInfo();

            if (_books.Count <= 0)
            {
                Console.WriteLine("Книг нет!");
            }
            else
            {
                TryDeleteBook();
            }
        }

        public void ShowAllBooksInfo()
        {
            Console.Clear();

            foreach (Book book in _books)
            {
                book.ShowAllInfo();
            }
        }

        public void ShowSomeBookInfo()
        {
            Console.Clear();
            ChooseInfo();
        }

        private void ChooseInfo()
        {
            Console.WriteLine("Выберите параметр для поиска книг: \n1.По названию книги. \n2.По автору книги. \n3.По году написания книги. \n4.По количеству листов в книге");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    ShowChoosedInfo("Введите название:", "Название:", userInput);
                    break;
                case "2":
                    ShowChoosedInfo("Введите автора:", "Автор:", userInput);
                    break;
                case "3":
                    ShowChoosedInfo("Введите год написания:", "Год написания:", userInput);
                    break;
                case "4":
                    ShowChoosedInfo("Введите количество страниц:", "Количество страниц:", userInput);
                    break;
                default:
                    WriteError();
                    break;
            }
        }

        private void ShowChoosedInfo(string text1, string text2, string userInput)
        {
            Console.WriteLine(text1);
            string word = Console.ReadLine();

            if (userInput == "1")
                CheckExistingInfoNames(text2, word);
            else if (userInput == "2")
                CheckExistingInfoAuthors(text2, word);
            else if (userInput == "3")
                CheckExistingInfoYearOfIssues(text2, word);
            else
                CheckExistingInfoNumbersOfPages(text2, word);
        }

        private void CheckExistingInfoNames(string text, string word)
        {
            for (int i = 0; i < _books.Count; i++)
                if (_books[i].Name == word)
                    _books[i].ShowSomeInfoInStrings(text, word);
        }
        private void CheckExistingInfoAuthors(string text, string word)
        {
            foreach (var book in _books)
                if (book.Author == word)
                    book.ShowSomeInfoInStrings(text, word);
        }

        private void CheckExistingInfoYearOfIssues(string text, string word)
        {
            if (int.TryParse(word, out int number))
            {
                for (int i = 0; i < _books.Count; i++)
                    if (_books[i].YearOfIssue == number)
                        _books[i].ShowSomeInfoInStrings(text, word);
            }
            else
            {
                WriteError();
            }
        }
        private void CheckExistingInfoNumbersOfPages(string text, string word)
        {
            if (int.TryParse(word, out int number))
            {
                for (int i = 0; i < _books.Count; i++)
                    if (_books[i].NumbersOfPages == number)
                        _books[i].ShowSomeInfoInStrings(text, word);
            }
            else
            {
                WriteError();
            }
        }

        private string GetSomeString(string text)
        {
            Console.WriteLine(text);
            string someString = Console.ReadLine();
            return someString;
        }

        private int GetSomeInt(string text)
        {
            bool isRepeating = true;

            while (isRepeating)
            {
                Console.WriteLine(text);
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                else
                {
                    WriteError();
                }
            }

            return 0;
        }

        private void TryDeleteBook()
        {
            Console.WriteLine("Введите порядковый номер книги:");

            if (int.TryParse(Console.ReadLine(), out int numberOfBook))
            {
                _books.RemoveAt(numberOfBook - 1);
                Console.WriteLine("Успешно!");
            }
            else
            {
                WriteError();
            }
        }

        public void WriteError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Введите корректные данные.");
            Console.ResetColor();
        }
    }

    class Book
    {
        public string Name { get; private set; }
        public string Author { get; private set; }
        public int YearOfIssue { get; private set; }
        public int NumbersOfPages { get; private set; }

        public Book(string name, string author, int yearOfIssue, int numbersOfPages)
        {
            Name = name;
            Author = author;
            YearOfIssue = yearOfIssue;
            NumbersOfPages = numbersOfPages;
        }
        public Book() { }

        public void ShowAllInfo()
        {
            Console.WriteLine($"Название - {Name}, автор - {Author}, год написания - {YearOfIssue}, количество страниц - {NumbersOfPages}.");
        }
        public void ShowSomeInfoInStrings(string text, string word)
        {
            Console.WriteLine($"{text} {word}");
        }

        public void ShowSomeInfoInNumbers(string text, int number)
        {
            Console.WriteLine($"{text} {number}");
        }
    }
}