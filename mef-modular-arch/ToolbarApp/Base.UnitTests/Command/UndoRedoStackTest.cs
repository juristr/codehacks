using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base.Command;
using System.Linq;
using System.Diagnostics;

namespace Base.UnitTests.Command
{
    class TestObject
    {
        public int Index { get; private set; }

        public TestObject(int idx = 0)
        {
            Index = idx;
        }
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

            [TestMethod]
            public void ShouldFireACorrespondingAddEvent()
            {
                //arrange
                var wasCalled = false;
                stack.UndoRedoStackOperationExecuted += (object s, UndoRedoStackOperationEventArgs<TestObject> e) => 
                                            {
                                                Assert.AreEqual(UndoStackOperation.Added, e.Action);
                                                wasCalled = true;
                                            };

                //act
                stack.AddItem(new TestObject());

                //assert
                Assert.AreEqual(true, wasCalled, "The event should have been fired");
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

            [TestMethod]
            public void ShouldFireACorrespondingUndoEvent()
            {
                //arrange
                var wasCalled = false;
                stack.UndoRedoStackOperationExecuted += (object s, UndoRedoStackOperationEventArgs<TestObject> e) =>
                                        {
                                            if(e.Action == UndoStackOperation.Undone)
                                                wasCalled = true;
                                        };

                PrepareStackToPerformUndo();

                //act
                stack.Undo();

                //assert
                Assert.AreEqual(true, wasCalled, "The event should have been fired");
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

            [TestMethod]
            public void ShouldFireACorrespondingRedoEvent()
            {
                //arrange
                var wasCalled = false;
                stack.UndoRedoStackOperationExecuted += (object s, UndoRedoStackOperationEventArgs<TestObject> e) =>
                                                        {
                                                            if(e.Action == UndoStackOperation.Redone)
                                                                wasCalled = true;
                                                        };
                PrepareStackWithItemToPerformRedo();

                //act
                stack.Redo();

                //assert
                Assert.AreEqual(true, wasCalled, "The event should have been fired");
            }    
        }

        [TestClass]
        public class TheStackSizeLimit : UndoRedoStackTest
        {

            [TestMethod]
            public void ShouldLimitTheStackSizeAccordingly()
            {
                //arrange & act
                for (int i = 0; i < stack.MaxStackSize + 10; i++)
                {
                    stack.AddItem(new TestObject(i));
                }

                //assert
                Assert.AreEqual(stack.MaxStackSize, stack.UndoItems().Count(), "There should be 10 undo objects in the stack");
                Assert.AreEqual(stack.MaxStackSize - 10, stack.UndoItems().ElementAt(0).Index, "Id should match");

                var objToUndo = stack.Undo();
                Assert.AreEqual(stack.MaxStackSize + 10 - 1, objToUndo.Index, "Should be the last added object");
            }

        }
    }
}
