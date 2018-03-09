/* Learning C# by Example 
The code first uses ReadAllText(inFileName) to copy the file’s text into a string. 
Next, the code uses regular expressions to replace non-letter and non-number characters with spaces. It uses the pattern [^a-zA-Z0-9].
The ^ means “not the following characters.

The a-zA-Z0-9 part means any lowercase or uppercase letter or a digit.

$ The code uses the Regex object’s Replace method to replace the characters that match the pattern with a space character. 
$ The code then uses Split to break the text into an array of words, removing any duplicates.
$ The code uses LINQ to select all of the words from the array and sort them. It uses the Distinct method to remove duplicates 
*/
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
class WordCounter
{
    static void Main()
    {
        string inFileName = "myFile.txt";
        StreamReader sr = new StreamReader(inFileName);
        string text = System.IO.File.ReadAllText("myFile.txt");
        Regex reg_exp = new Regex("[^a-zA-Z0-9]");
        text = reg_exp.Replace(text, " ");
        string[] words = text.Split(new char[] {
            ' '
        }, StringSplitOptions.RemoveEmptyEntries);
        var word_query = (from string word in words orderby word select word).Distinct();
        string[] result = word_query.ToArray();
        int counter = 0;
        string delim = " ,.";
        string[] fields = null;
        string line = null;
        while (!sr.EndOfStream)
        {
            line = sr.ReadLine(); //each time you read a line you should split it into the words  
            line.Trim();
            fields = line.Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            counter += fields.Length; //and just add how many of them there is  
            foreach (string word in result)
            {
                CountStringOccurrences(text, word);
            }
        }
        sr.Close();
        Console.WriteLine("The total word count is {0}", counter);
        Console.ReadLine();
    }
    //Count the frequency of each unique word.  
    public static void CountStringOccurrences(string text, string word)
    {
        int count = 0;
        int i = 0;
        while ((i = text.IndexOf(word, i)) != -1)
        {
            i += word.Length;
            count++;
        }
        Console.WriteLine("{0} {1}", count, word);
    }
}