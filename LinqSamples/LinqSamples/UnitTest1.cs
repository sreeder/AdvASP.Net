using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LinqSamples
{


    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var numList = new List<int>
            {
               1, 2, 7, 3, 4, 5
            };
            Assert.AreEqual(5, numList.Max());

            
            //LINQ - lazy loading. save a list of steps to get what we want. never store the intermediate calculations
            var odds = numList.Where(i => i % 2 == 1);

            //recalculate list every time
            var oddMin = odds.Min();
            var oddMax = odds.Max();

            //automatically recalculates the list when we call max because it is dynamically evaluated.

            numList.Add(19);
            var oddMax2 = odds.Max();


            //essentially the same as the calculation for odds
            var odds2 = new List<int>(0);
            foreach (int i in numList)
            {
                if (i % 2 == 1)
                    odds2.Add(i);
            }

            //iterate through num list a second time
            var evens = numList.Where(i => i % 2 == 0);

            //LINQ issues
            //make sure you are not tampering the data in the middle of the calculations
            //am I doing the list over and over and over
            //to initially write a complicate linq statement break it up into smaller parts, convert each part to a list so you can actually see the results



            var words = new List<string>
            {
               "balloon", "Misssissippi", "my jacket", "5", "train"
            };

            //Order the list
            var ordered = numList.OrderBy(i => i).ToList();

            var words2 = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "balloon"),
                new KeyValuePair<int, string>(2, "train"),
                new KeyValuePair<int, string>(2, "my jacket"),
                new KeyValuePair<int, string>(-6, "Mississippi"),
                new KeyValuePair<int, string>(5, "5"),
            };
            var ordered2 = words2.OrderBy(i => i.Key)
                .ThenBy(i => i.Value)
                .ToList();


            //only select the word, not the key but order by the key
            var ordered3 = words2.OrderBy(i => i.Key)
                .Select(kv=>kv.Value)
                .ToList();

            var groups = words2.GroupBy(kv => kv.Key)
                .ToList();











            var placeholder = "";

        }
    }
}
