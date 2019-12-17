using NUnit.Framework;

namespace KSU.CIS300.Checkers.Tests
{
    public class LinkedListCellTests
    {

        [Test]
        public void LinkedListCellTest_Next()
        {
            LinkedListCell<int> cell = new LinkedListCell<int>();
            Assert.That(cell, Has.Property("Next"));
        }

        [Test]
        public void LinkedListCellTest_Data()
        {
            LinkedListCell<int> cell = new LinkedListCell<int>();
            Assert.That(cell, Has.Property("Data"));
        }
    }
}