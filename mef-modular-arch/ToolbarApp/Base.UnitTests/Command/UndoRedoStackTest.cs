using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base.Command;
using System.Linq;

namespace Base.UnitTests.Command
{
    class TestObject
    {

    }

    public class UndoRedoStackTest
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

        [TestClass]
        public class TheAddItemMethod : UndoRedoStackTest
        {
            [TestMethod]
            public void ShouldCorrectlyAddANewItemAndHaveItInTheRedoList()
            {
                //arrange
                var item = new TestObject();

                //act
                stack.AddItem(item);
                var resultItem = stack.UndoItems().ElementAt(0);

                //assert
                Assert.AreEqual(item, resultItem, "the item should be the same");
            }
        }


        [TestClass]
        public class TheUndoMethod : UndoRedoStackTest
        {
            private TestObject addedObject;

            [TestInitialize()]
            public new void SetUp()
            {
                base.SetUp();
            }

            [TestCleanup()]
            public new void TearDown()
            {
                base.TearDown();

                addedObject = null;
                stack = null;
            }

            private void PrepareStackToPerformUndo()
            {
                addedObject = new TestObject();
                stack.AddItem(addedObject);
            }

            [TestMethod]
            public void ShouldCorrectlyReturnAPreviouslyAddedUndoItem()
            {
                //arrange
                PrepareStackToPerformUndo();

                //act
                var returnItem = stack.Undo();

                //
                Assert.AreEqual(addedObject, returnItem, "the item should be the same");
            }

            [TestMethod]
            public void ShouldHaveAnItemInTheRedoListWhenPerformingAnUndo()
            {
                //arrange
                PrepareStackToPerformUndo();

                //act
                var returnItem = stack.Undo();

                //assert
                Assert.AreEqual(addedObject, stack.RedoItems().ElementAt(0), "the object should be in the redo list");                
            }

            [TestMethod]
            public void ShouldReturnNullWhenPerformingAnUndoAndTheStackIsEmpty()
            {
                //act
                var item = stack.Undo();

                //assert
                Assert.IsNull(item, "the returned item should be null as there is nothing to be redone");
            }
        }

        [TestClass]
        public class TheRedoMethod : UndoRedoStackTest
        {
            private TestObject undoneObject;

            [TestInitialize()]
            public new void SetUp()
            {
                base.SetUp();
            }

            [TestCleanup()]
            public new void TearDown()
            {
                base.TearDown();

                undoneObject = null;
                stack = null;
            }

            private void PrepareStackWithItemToPerformRedo()
            {
                undoneObject = new TestObject();
                stack.AddItem(undoneObject);
                stack.Undo(); //so we now should have it in the redo list
            }

            [TestMethod]
            public void ShouldReturnTheObjectToRedo()
            {
                //arrange
                PrepareStackWithItemToPerformRedo();

                //act
                var item = stack.Redo();

                //assert
                Assert.AreEqual(undoneObject, item);
            }

            [TestMethod]
            public void ShouldHaveTheItemInTheUndoListWhenPerformingAgainARedo()
            {
                //arrange
                PrepareStackWithItemToPerformRedo();

                //act
                stack.Redo();

                //assert
                Assert.AreEqual(undoneObject, stack.UndoItems().ElementAt(0));
            }

            [TestMethod]
            public void ShouldReturnNullWhenPerformingAnUndoAndTheStackIsEmpty()
            {
                //act
                var item = stack.Redo();

                //assert
                Assert.IsNull(item, "the returned item should be null as there is nothing to be redone");
            }
        }

    }
}
