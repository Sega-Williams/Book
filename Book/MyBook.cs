using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    class MyBook
    {
        // Parameters
        private string _nameOfBook = "None";
        private string _author = "None";
        private string _publisher = "None";
        private DateTime _dateOfRelease = new DateTime();
        private int _proportionsLength = 0;
        private int _proportionsWidth = 0;
        private int _numberOfPages = 0;
        private string _materialOfCover = "None";
        private string[] _content = { "None" }; // Содержание
        private string _summary = "None"; // Краткое содержание
        private string[] _tableOfContents = {"None"}; // Оглавление

        public bool opened = false;

        public int ReadingPage { get; set; } = 1;

        //Save old console proportions
        private int oldWinH = Console.WindowHeight;
        private int oldBufH = Console.BufferHeight;
        private int oldWinW = Console.WindowWidth;
        private int oldBufW = Console.BufferWidth;

        int maxCharsOnPage = 0;

        //Constructor
        public MyBook(string nameOfBook, string author, string publisher, int length, int width, string material, string[] content, string summary, string[] tableOf)
        {
            //Checkers
            //Count maximum of chars on page
            for (int i = 0; i < content.Length; i++)
            {
                maxCharsOnPage = content[i].Length;
            }
            //Check minimal proportions             
            if (length < 30) length = 30;
            if (width < 25) width = 25;


            _nameOfBook = nameOfBook;
            _author = author;
            _publisher = publisher;
            _dateOfRelease = DateTime.Now;
            _proportionsLength = length;
            _proportionsWidth = width;
            _numberOfPages = content.Length;
            _materialOfCover = material;
            _content = content;
            _summary = summary;
            _tableOfContents = tableOf;
        }
        
        
        
        // Methods
        public void LookAt()
        {
            CloseBook();

            Console.WriteLine($"Name of book: {_nameOfBook}\nAuthor: {_author}\nPublisher: {_publisher}\nYear of release: {_dateOfRelease.Year}");
            
            switch (_numberOfPages)
            {
                case int i when (_numberOfPages < 200) :
                    Console.WriteLine("Book is thin, pages is: {0}", i);
                    break;
                case int i when (_numberOfPages >= 200 && _numberOfPages < 500) :
                    Console.WriteLine("This book has (0) pages", i);
                    break;
                case int i when (_numberOfPages >= 500) :
                    Console.WriteLine("Look how thick the book is. Pages is: {0}", i);
                    break;
            }

            Console.WriteLine("Material of cover is: {0}", _materialOfCover);
            //Short content
            Console.WriteLine("Summary: \n{0}", _summary);
        }   

        public void Read(int page = 0)
        {
            OpenBook();

            opened = true;
            Console.WriteLine("{0}\n\n", _content[page]);
            //Counter of pages
            Console.WriteLine("Page:[{0}/{1}]", ReadingPage, _numberOfPages);
        }

        public void Listing(int direction)
        {            
            Console.Clear();
            
            bool list = false;
            //Next
            if (direction == 1)
            {
                //Positive
                if (ReadingPage > 1)
                {
                    Console.WriteLine("Back page\n\n");
                    ReadingPage--;
                    list = true;

                }
                //Negative
                else 
                {
                    Console.WriteLine("You backed on cover\n\n");
                    ReadingPage = 0;
                    LookAt();
                }
            }
            //Back
            else if (direction == 2)
            {
                //Negative
                if (ReadingPage == _content.Length)
                {
                    Console.WriteLine("You done up and read my book!\nA summary of the content of that initial version of the book is attached as an annex to this note.:\n");
                    Console.WriteLine(_summary);
                    ReadingPage = 0;
                }
                //Positive
                else
                {
                    Console.WriteLine("Next page\n\n");
                    ReadingPage++;
                    list = true;
                }
            }
            //Close
            else if (direction == 0) { ReadingPage = 0; }
            
            //Check for positive listing
            if (list) Read(ReadingPage -1);
        }
        
        private void TableOfContents()
        {
            for(int i = 0; i < _tableOfContents.Length; i++)
            {
                Console.WriteLine("Chapter {0}: {1}", i,_tableOfContents[i]);
            }
        }

        //Change proportions of window and buffer
        private void OpenBook()
        {
            if (maxCharsOnPage > _proportionsLength * _proportionsWidth || _proportionsLength > Console.LargestWindowHeight)
            {
                Console.WindowHeight = Console.LargestWindowHeight;
                Console.WindowWidth = Console.LargestWindowWidth;
            }
            else
            {
                Console.WindowHeight = _proportionsLength;
                Console.BufferHeight = _proportionsLength;
                Console.WindowWidth = _proportionsWidth;
                Console.BufferWidth = _proportionsWidth;

            }
        }
        private void CloseBook()
        {
            Console.WindowHeight = oldWinH;
            Console.BufferHeight = oldBufH;
            Console.WindowWidth = oldWinW;
            Console.BufferWidth = oldBufW;
        }

        private void DrawLine()
        {
            int width;
            if (_proportionsWidth < Console.LargestWindowWidth) { width = _proportionsWidth; }
            else width = Console.LargestWindowWidth;
            for (int i = 0; i < width; i++)
            {
                Console.Write("_");
            }
            Console.WriteLine();
        }

        //Main process of book
        public void Processing()
        {
            Console.Clear();
            Console.WriteLine("Писатель сам решает, какие рамки ему выбирать, но всегда есть минимальные(С)");
            Console.WriteLine("Press \"Enter\" to look at book");
            Console.ReadLine();
            
            
            Console.Clear();
            LookAt();            
            Console.WriteLine("\nPress \"Enter\" to read the book");
            Console.ReadLine();

            Console.Clear();
            Read();

            
            //Main cycle
            while (opened)
            {
                //Navigation System

                DrawLine();
                Console.WriteLine("|Navigation system:|\n[1 to back]\n[2] to next\n*dont't write me and check the table of content*\n0 to close book");
                DrawLine();

                try
                {
                    //Check TableOf
                    string choicePrev = Console.ReadLine();
                    if (choicePrev == "")
                    {
                        Console.Clear();
                        TableOfContents();
                    }

                    //Read another page
                    else
                    {
                        int? choice = Convert.ToInt32(choicePrev);
                        //Console.WriteLine("Choice is: {0}, TypeOF: {1}", choice, choice.GetType());
                        switch (choice)
                        {
                            case 1:
                                Listing(1);
                                break;
                            case 2:
                                Listing(2);
                                break;
                            case 0:
                                opened = false;
                                Listing(0);
                                break;
                            default:
                                Console.WriteLine("\n\nWhat are you doing?)");
                                break;
                        }
                    }
                }
                catch
                {

                }
            }
        }
    }
}
