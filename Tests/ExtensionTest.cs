using Bump;
using Entities;
using NUnit.Framework;
using static Bump.Extensions;

namespace Tests
{
    public class ExtensionTest
    {
        [Test]
        [TestCase("1", ExpectedResult = "Not null")]
        [TestCase(null, ExpectedResult = "Null")]
        public string RunTests(string s)
        {
            return s?.Run(str => "Not null") ?? Run(() => "Null");
        }

        [Test]
        public void AlsoTest()
        {
            const string newContent = "New Content";

            var message = new Message(
                    id: 0,
                    author: null,
                    content: "Old Content",
                    media: new int[0],
                    theme: 0)
                .Also(it => { it.Content = newContent; });

            Assert.AreEqual(message.Content, newContent);
        }
    }
}