using System;
using System.Text.Json;

namespace Book
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = new string[] { "Text by me", "Filler on this", "What is this", "Dont look for deep mind in this fillers", "Huh", "Are u seriously?", "Okay, let's go", "How are you?", "Are you know that finishing an entire my book doesn't prove anything?", "Go to the end", "End coming soon)))"};
            string[] tableOf = new string[] { "Autobiography", "What's next?" };
            
            MyBook trying = new MyBook("\"Trying\"", "Sergey Koksharov", "\"Myself\"", 40, 25, "Paper", content, "\"Book of my life and trying to stand up Programmer\"(c)", tableOf);
            
            //JSON
            string json = JsonSerializer.Serialize<MyBook>(trying);
            Console.WriteLine(json);

            Console.ReadLine();
            trying.Processing();            
        }
    }
}
