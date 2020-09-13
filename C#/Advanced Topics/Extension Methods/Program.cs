using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension_Methods
{
    //What are extension methods?
    //Allows use to add methods to an existing class without:
    //1. change its sourse code, or
    //2. creating a new class that inherits from it
    //Note: use extension methods only when you really have too.
    //If the class has a method with the same name as your extension
    //method, the class method gets priority.

    class Program
    {
        static void Main(string[] args)
        {
            string post = "This is suppose to be a very long blog post";
            var shortenedPost = post.Shorten(5);

            Console.WriteLine(shortenedPost);
        }
    }

    //Extension methods should be static
    public static class StringExtensions
    {
        public static string Shorten(this String str, int numberOfWords)
        {
            if (numberOfWords < 0)
                throw new ArgumentOutOfRangeException("numberOfWords should be >= zero");

            if (numberOfWords == 0)
                return string.Empty;

            var words = str.Split(' ');
            if (words.Length <= numberOfWords)
                return str;

            return string.Join(" ", words.Take(numberOfWords));
        }
    }
}
