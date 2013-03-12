using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base.Command;
using System.Linq;
using System.Diagnostics;

namespace Base.UnitTests.Command
{
    [TestClass]
    public class UndoRedoStackTest_Performance
    {
        private UndoRedoStack<TestObject> stack;

        [TestInitialize()]
        public void SetUp()
        {
            stack = new UndoRedoStack<TestObject>();
        }

        [TestCleanup()]
        public void TearDown()
        {
            stack = null;
        }

        //just to have get alerted about potential performance impacts in the implementation
        [TestMethod]
        public void ShouldProcess100000ItemsUnder15Ms()
        {
            //arrange
            var stopw = new Stopwatch();
            stopw.Start();

            //act
            for (int i = 0; i < 100000; i++)
            {
                stack.AddItem(new TestObject(i));
            }

            //assert
            stopw.Stop();

            Assert.IsTrue(stopw.ElapsedMilliseconds < 15, "Should be faster than 100 ms (actual:" + stopw.ElapsedMilliseconds + ")");
        }

    }
}
