using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLanguageFeaturesTest
{
    [TestClass]
    public class Answers
    {
        [TestMethod]
        public void Question1()
        {
            var tests = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("cow", "Cow"),
                new Tuple<string, string>("sAMple titLE", "Sample Title"),
                new Tuple<string, string>("Ye.t,'anothER Title", "Ye.t,'another Title")
            };

            foreach (var test in tests)
            {
                var result = Questions.Question1(test.Item1);
                Assert.AreEqual(test.Item2, result);
            }
        }

        [TestMethod]
        public void Question2()
        {
            var tests = new List<Tuple<List<string>, List<string>>>
            {
                
                new Tuple<List<string>, List<string>>(
                    new List<string> {"cat", "dog", "AnimAl"},
                    new List<string> {"dog", "cat", "animal"}
                    ),
                new Tuple<List<string>, List<string>>(
                    new List<string> {"aAA", "AA", "a"},
                    new List<string> {"a", "AA", "aAA"}
                    ),
                new Tuple<List<string>, List<string>>(
                    new List<string> {"a a a ab a a a a", "aaaaba", "a", "b"},
                    new List<string> {"b", "a", "aaaaba", "a a a ab a a a a"}
                    )
            };

            foreach (var test in tests)
            {
                var result = Questions.Question2(test.Item1);
                Assert.AreEqual(test.Item2.Count, result.Count);

                for (int i = 0; i < 0; i++)
                    Assert.AreEqual(test.Item2[i], result[i]);
            }
        }

        [TestMethod]
        public void Question3()
        {
            var result = Questions.Question3();
            var type = result.GetType();
            var publicSetters = type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite && p.GetSetMethod(false) != null);
            var publicSetterNames = publicSetters.Select(p => p.Name);
            var allSetters = type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite && p.GetSetMethod(true) != null);
            var propNamesAndTypes = allSetters.ToDictionary(p => p.Name, p => p.PropertyType.Name);

            Assert.AreEqual(2, publicSetters.Count());
            Assert.AreEqual(3, allSetters.Count());

            Assert.IsTrue(publicSetterNames.Contains("First"));
            Assert.IsTrue(publicSetterNames.Contains("Second"));

            Assert.AreEqual(propNamesAndTypes["First"], "Int32");
            Assert.AreEqual(propNamesAndTypes["Second"], "String");
            Assert.IsTrue(propNamesAndTypes.ContainsKey("Third"));
        }

        [TestMethod]
        public void Question4()
        {
            var q3Type = Questions.Question3().GetType();
            var result = Questions.Question4();
            var type = result.GetType();

            Assert.IsTrue(q3Type.IsAssignableFrom(type), "The result of Question4() is not a subclass of the result of Question3().");

            var obj = Activator.CreateInstance(type);

            Assert.IsNotNull(obj);
            Assert.AreEqual(type.GetProperty("First").GetValue(obj), 7);

            var method = type.GetMember("GetFirstPlusParam")[0] as MethodInfo;

            Assert.IsNotNull(method);
            Assert.AreEqual(method.Invoke(obj, new object[] { 0 }), 7);
            Assert.AreEqual(method.Invoke(obj, new object[] { 3 }), 10);
            Assert.AreEqual(method.Invoke(obj, new object[] { -20 }), -13);
        }

        

        [TestMethod]
        public void Question5()
        {
            var tests = new List<Tuple<DateTime, DateTime, DateTime?>>
            {
                new Tuple<DateTime, DateTime, DateTime?>(
                    new DateTime(2017, 1, 17),
                    new DateTime(2015, 12, 25, 8, 27, 36),
                    new DateTime(2015, 12, 26, 15, 0, 0)
                    ),
                new Tuple<DateTime, DateTime, DateTime?>(
                    new DateTime(2099, 3, 17),
                    new DateTime(2015, 1, 6, 8, 27, 36),
                    new DateTime(2099, 3, 18, 15, 0, 0)
                    ),
                new Tuple<DateTime, DateTime, DateTime?>(
                    DateTime.MinValue, 
                    DateTime.MaxValue,
                    null
                    )
            };

            foreach (var test in tests)
            {
                var result = Questions.Question5(test.Item1, test.Item2);
                Assert.AreEqual(test.Item3, result);
            }
        }

        [TestMethod]
        public void Question6()
        {
            var tests = new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(4, 6, 16.0),
                new Tuple<int, int, double>(-4, 6, 16.0),
                new Tuple<int, int, double>(8, 6, 64.0),
                new Tuple<int, int, double>(7, 7, 93.733627955847),
                new Tuple<int, int, double>(2, -3, 0.5)
            };

            foreach (var test in tests)
            {
                var result = Questions.Question6(test.Item1, test.Item2);
                Assert.IsTrue(Math.Abs(1.0 - (test.Item3 / result)) < /* epsilon */ 0.05);
            }
        }

        [TestMethod]
        public void Question7()
        {
            var tests = new List<Tuple<List<int>, int>>
            {
                new Tuple<List<int>, int>(new List<int> { 7, 4, 6, -1, 20}, 7),
                new Tuple<List<int>, int>(new List<int> { 3, 3, 3, 1}, 3),
                new Tuple<List<int>, int>(new List<int> { -20, -10, -5, -3, 152}, -3),
                new Tuple<List<int>, int>(new List<int> { 2, 4, 6 }, Int32.MinValue)
            };

            foreach (var test in tests)
            {
                var result = Questions.Question7(test.Item1);
                Assert.AreEqual(test.Item2, result);
            }
        }

        [TestMethod]
        public void Question8()
        {
            var tests = new List<Tuple<List<string>, List<List<string>>>>
            {
                new Tuple<List<string>, List<List<string>>>(
                    new List<string>
                    {
                        "TUBS",
                        "bust",
                        "sTUb",
                        "bat",
                        "TAb",
                        "cab",
                        "bot",
                        "TOt"
                    },
                    new List<List<string>>
                    {
                        new List<string> {"bat", "tab"},
                        new List<string> {"bust", "stub", "tubs"}
                    }),
                new Tuple<List<string>, List<List<string>>>(
                    new List<string>
                    {
                        "mom",
                        "mOm",
                        "bag",
                        "gab",
                        "MOM"
                    },
                    new List<List<string>>
                    {
                        new List<string> {"bag", "gab"}
                    })     
            };

            foreach (var test in tests)
            {
                var result = Questions.Question8(test.Item1);
                Assert.AreEqual(test.Item2.Count, result.Count);

                for (var i = 0; i < result.Count; i++)
                {
                    var actual = result[i];
                    var expected = test.Item2[i];

                    Assert.AreEqual(expected.Count, actual.Count);

                    for (var j = 0; j < result.Count; j++)
                        Assert.IsTrue(String.Compare(expected[j], actual[j], StringComparison.InvariantCultureIgnoreCase) == 0);
                }
            }
        }
    }
}
