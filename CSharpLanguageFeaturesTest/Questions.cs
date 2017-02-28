using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace CSharpLanguageFeaturesTest
{
    /// <summary>
    /// INSTRUCTIONS:
    /// 
    /// Follow the directions in the docstring for each question. Do not change the method signature
    /// in any way, including the output type. You may run the provided unit tests in Answers.cs to
    /// check your progress. If you need to create any additional classes, make sure that they are public
    /// and are defined inside the CSharpLanguageFeaturesTest namespace. You may include any private helper
    /// methods you like in Questions.cs.
    /// </summary>
    public static class Questions
    {
        /// <summary>
        /// STRINGS
        /// 
        /// Convert the given string into Title Case. I.e., the first letter of each word
        /// is uppercase, and all other letters are lowercase. Words are separated by spaces.
        /// Punctuation and other whitespace characters do not constitute word boundaries.
        /// </summary>
        public static string Question1(string input)
        {

            var listOfStrings = input.Split(' ').ToList();

            var formattedString = new List<string>();
            foreach (var word in listOfStrings)
                formattedString.Add(word.ToTitleCase());
            return String.Join(" ", formattedString);
        }

        public static string ToTitleCase(this string s)
        {
            var charArray = s.ToLower().ToCharArray();
            if (charArray.Length > 0)
                charArray[0] = Char.ToUpper(charArray[0]);
            return new string(charArray);
        }


        /// <summary>
        /// STRINGS & COLLECTIONS
        /// 
        /// Sort the given list of strings by the number of A's ("a" or "A") it contains,
        /// ascending. Don't worry about breaking ties.
        /// </summary>
        public static List<string> Question2(List<string> input)
        {
            Dictionary<string, int> numA = new Dictionary<string, int>();

            foreach (string value in input)
            {
                var chararray = value.ToLower().ToCharArray();
                if (chararray.Length > 0)
                {
                    Boolean foundA = false;
                    var g = chararray.GroupBy(i => i);
                    foreach (var grp in g)
                    {
                        if (grp.Key == 'a')
                        {
                            numA.Add(value, grp.Count());
                            foundA = true;
                        }
                    }
                    if (!foundA)
                        numA.Add(value, 0);
                    foundA = false;
                }
            }

            List<string> ordered =
                numA.OrderByDescending(pair => pair.Value)
                    .ToDictionary(pair => pair.Key, pair => pair.Value)
                    .Keys.ToList();
            return ordered;
        }

        /// <summary>
        /// CLASSES
        /// 
        /// Create a new class with three public getter/setter properties named First,
        /// Second, and Third. First should be of type int. Second should be of type string.
        /// Third should have a private setter.
        /// 
        /// In the question method, instantiate this class and return the instantiated object.
        /// </summary>
        public static object Question3()
        {
            return new MyNewClass();
        }

        public class MyNewClass
        {
            public int First { get; set; }
            public string Second { get; set; }
            public string Third { get; private set; }
        }

        /// <summary>
        /// CLASSES & INHERITANCE
        /// 
        /// Create a subclass of the class from Question3(). The subclass should have a
        /// parameterless constructor that sets the value of First to 7, and it should have
        /// a method named GetFirstPlusParam() that takes one integer parameter and returns
        /// the value of First plus the value of that parameter.
        /// 
        /// In the question method, instantiate this class and return the instantiated object.
        /// </summary>
        public static object Question4()
        {
            return new YourClass();
        }

        public class YourClass : MyNewClass
        {
            public YourClass()
            {
                First = 7;
            }

            public int GetFirstPlusParam(int i)
            {
                return First + i;
            }
        }

        /// <summary>
        /// DATETIMES & NULLABLE TYPES & ERROR-HANDLING
        /// 
        /// Given two DateTimes, take whichever DateTime comes later in its respective calendar year
        /// and return a DateTime representing the day after that date, at 3:00 p.m.
        /// 
        /// If the return value would fall out of the possible range of DateTime values, return null.
        /// </summary>
        public static DateTime? Question5(DateTime d1, DateTime d2)
        {
            DateTime d2sameYear = new DateTime(d1.Year, d2.Month, d2.Day, d2.Hour, d2.Minute, d2.Second);
            int comp = DateTime.Compare(d1, d2sameYear);
            DateTime later;
            if (comp <= 0)
                later = d2;
            else
                later = d1;
            try
            {
                DateTime date = later.AddDays(1).Date;
                TimeSpan time = new TimeSpan(15, 0, 0);
                DateTime newday = date + time;
                return newday;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// MATH & TYPE CONVERSION
        /// 
        /// Given two integers, return the cubed root of the first integer raised to the power of the second integer.
        /// The answer should not be rounded.
        /// </summary>
        public static double Question6(int i1, int i2)
        {
            return Math.Pow(Math.Pow(Math.Abs(i1), (1d / 3d)), i2);
        }

        /// <summary>
        /// COLLECTION SEARCH
        /// 
        /// Given a list of integer, return the largest odd integer. If there are no odd
        /// integers in the input list, return the smallest possible int.
        /// </summary>
        public static int Question7(List<int> ints)
        {
            int largestodd = ints[0];
            Boolean foundOdd = false;

            foreach (var val in ints)
            {
                if (Math.Abs(val) % 2 == 1)
                {
                    foundOdd = true;
                    if (val > largestodd)
                        largestodd = val;
                }
            }

            if (foundOdd)
                return largestodd;
            else
                return Int32.MinValue;
        }

        /// <summary>
        /// EXTRA CREDIT - ALGORITHM DESIGN
        /// 
        /// Given a list of strings, return all sets of anagrams (a set of words that
        /// contain the same letters in a different order) as lists of strings.
        /// 
        /// Do not allow words to be an anagram of themselves, even if repeated in the
        /// input list. Ignore case. The tests will be case-insensitive and will not
        /// include non-alpha characters. Sort each anagram set in alphabetical order,
        /// and sort the list of anagram sets in alphabetical order, based on the first
        /// word of each set.
        /// </summary>
        public static List<List<string>> Question8(List<string> words)
        {
            List<List<string>> anagrams = new List<List<string>>();
            foreach (var word in words)
            {
                if (anagrams.Count == 0)
                {
                    anagrams.Add(new List<string> {"primer"});
                }
                foreach (var a in anagrams)
                {
                    if (AreAnagram(word, a[0]))
                        a.Add(word);
                    else anagrams.Add(new List<string>{word});
                }
            }
            foreach (var a in anagrams)
            {
                if (a.Count == 1)
                    anagrams.Remove(a);
            }
            return anagrams;
        }

        public static bool AreAnagram(string word, string match)
        {
            if (word == match)
                return false;
            if (word.Length != match.Length)
                return false;
            char[] char1 = word.ToLower().ToCharArray();
            char[] char2 = match.ToLower().ToCharArray();

            //Step 2  
            Array.Sort(char1);
            Array.Sort(char2);

            //Step 3  
            string newWord1 = new string(char1);
            string newWord2 = new string(char2);
            if (newWord1 == newWord2)
                return true;
            else
                return false;
        }
    }
}

