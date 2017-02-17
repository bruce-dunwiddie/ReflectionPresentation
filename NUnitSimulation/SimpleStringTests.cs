using System;
using System.Linq;

using NUnit.Framework;

namespace NUnitSimulation
{
    [TestFixture]
    public class SimpleStringTests
    {
        private string testString = null;

        [OneTimeSetUp]
        public void SimpleStringTests_Setup()
        {
            testString = "This is a test.";
        }

        [Test]
        [Description("Tests to make sure the case-insensitive string comparison works using the invariant culture.")]
        public void String_CaseSensitive()
        {
			// will fail because it's missing the period
            Assert.IsTrue(
                testString.Equals(
					"THIS IS A TEST",
					StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        [Description("Tests to make sure the Reverse method correctly returns the characters from a string in reverse order.")]
        public void String_Reverse()
        {
            Assert.AreEqual(
                ".tset a si sihT", 
                new string(testString.Reverse().ToArray()));
        }

        [OneTimeTearDown]
        public void SimpleStringTests_TearDown()
        {

        }
    }
}
