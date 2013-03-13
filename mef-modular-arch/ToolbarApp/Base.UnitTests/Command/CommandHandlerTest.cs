using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base.Command;
using Moq;

namespace Base.UnitTests.Command
{

    class MyTestCommand : ICommand
    {
        public object Context { get; set; }
        public bool HasUndone { get; set; }
        public bool HasExecuted { get; set; }
        public bool ThrowExceptionOnExecuting { get; set; }

        public void Execute()
        {
            HasExecuted = true;
            if (ThrowExceptionOnExecuting)
            {
                throw new ApplicationException("something went wrong");
            }
        }

        public void Undo()
        {
            HasUndone = true;
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class CommandHandlerTest
    {
        private CommandHandler handler;
        private Mock<IUndoRedoStack<ICommand>> mockUndoRedo;

        [TestInitialize()]
        public void SetUp()
        {
            mockUndoRedo = new Mock<IUndoRedoStack<ICommand>>();
            handler = new CommandHandler(mockUndoRedo.Object);
        }

        [TestCleanup()]
        public void TearDown()
        {
            handler = null;
        }

        [TestClass]
        public class TheExecuteCommandMethod : CommandHandlerTest
        {

            [TestMethod]
            public void ShouldInvokeTheCommand()
            {
                //arrange
                var myCommand = new MyTestCommand();

                //act
                handler.Execute(myCommand);

                //assert
                Assert.IsTrue(myCommand.HasExecuted, "should have been executed");
            }

            [TestMethod]
            public void ShouldAddTheCommandToTheUndoStack()
            {
                //arrange
                var myCommand = new MyTestCommand();

                //act
                handler.Execute(myCommand);

                //assert
                mockUndoRedo.Verify(x => x.AddItem(myCommand), Times.Once(), "The command should have been added to the undo stack");
            }

            [TestMethod]
            public void ShouldNotAddTheCommandToTheUndoStackIfTheExecutionOfTheCommandThrowsAnException()
            {
                //arrange
                var myCommand = new MyTestCommand()
                {
                    ThrowExceptionOnExecuting = true
                };

                //act
                try
                {
                    handler.Execute(myCommand);
                }
                catch { } //we're not interested in the exception here

                //assert
                mockUndoRedo.Verify(x => x.AddItem(myCommand), Times.Never(), "The command should not have been added as it threw an exception");
            }

            [TestMethod]
            [ExpectedException(typeof(ApplicationException))]
            public void ShouldRethrowAnyExceptionFiredBy()
            {
                //arrange
                var myCommand = new MyTestCommand()
                {
                    ThrowExceptionOnExecuting = true
                };

                //act
                handler.Execute(myCommand);
            }

            [TestMethod]
            public void ShouldFireACorrespondingAddEvent()
            {
                //arrange
                var wasCalled = false;
                handler.OperationExecuted += (object s, OperationExecutionEventArgs e) =>
                                            {
                                                Assert.AreEqual(ExecutionOperation.Execute, e.Action);
                                                wasCalled = true;
                                            };

                //act
                handler.Execute(new MyTestCommand());

                //assert
                Assert.AreEqual(true, wasCalled, "The event should have been fired");
            }

        }

        [TestClass]
        public class TheUndoMethod : CommandHandlerTest
        {
            MyTestCommand myCommand;

            [TestInitialize()]
            public new void SetUp()
            {
                base.SetUp();
                myCommand = new MyTestCommand();
                handler.Execute(myCommand);

                mockUndoRedo.Setup(x => x.Undo()).Returns(myCommand);
            }

            [TestCleanup()]
            public new void TearDown()
            {
                base.TearDown();
                myCommand = null;
            }	

            [TestMethod]
            public void ShouldUndoTheCommandByInvokingTheProperMethodOnTheCommandObject()
            {
                //act
                handler.Undo();

                //assert
                Assert.IsTrue(myCommand.HasUndone, "The command should have been undone");
            }

            [TestMethod]
            [ExpectedException(typeof(ApplicationException))]
            public void ShouldThrowExceptionInCaseAnUndoIsMadeWhenNothingCanBeUndone()
            {
                mockUndoRedo.Setup(x => x.Undo()).Returns((ICommand)null);

                //act
                handler.Undo();
            }

            [TestMethod]
            public void ShouldFireACorrespondingUndoEvent()
            {
                //arrange
                var wasCalled = false;
                handler.OperationExecuted += (object s, OperationExecutionEventArgs e) =>
                                        {
                                            if (e.Action == ExecutionOperation.Undo)
                                                wasCalled = true;
                                        };

                //act
                handler.Undo();

                //assert
                Assert.AreEqual(true, wasCalled, "The event should have been fired");
            }
        
        }

        [TestClass]
        public class TheRedoMethod : CommandHandlerTest
        {
            MyTestCommand myCommand;

            [TestInitialize()]
            public new void SetUp()
            {
                base.SetUp();
                myCommand = new MyTestCommand();

                mockUndoRedo.Setup(x => x.Undo()).Returns(myCommand);
                mockUndoRedo.Setup(x => x.Redo()).Returns(myCommand);

                handler.Execute(myCommand);
                handler.Undo();

                myCommand.HasExecuted = false; //reset it
            }

            [TestCleanup()]
            public new void TearDown()
            {
                base.TearDown();
                myCommand = null;
            }

            [TestMethod]
            public void ShouldReExecuteTheLastCommand()
            {
                //act
                handler.Redo();

                //assert
                Assert.IsTrue(myCommand.HasExecuted, "The command should have been re-executed");
            }

            [TestMethod]
            [ExpectedException(typeof(ApplicationException))]
            public void ShouldThrowExceptionInCaseARedoIsMadeWhenNothingCanBeRedone()
            {
                mockUndoRedo.Setup(x => x.Redo()).Returns((ICommand)null);

                //act
                handler.Redo();
            }

            [TestMethod]
            public void ShouldFireACorrespondingRedoEvent()
            {
                //arrange
                var wasCalled = false;
                handler.OperationExecuted += (object s, OperationExecutionEventArgs e) =>
                                        {
                                            if (e.Action == ExecutionOperation.Redo)
                                                wasCalled = true;
                                        };

                //act
                handler.Redo();

                //assert
                Assert.AreEqual(true, wasCalled, "The event should have been fired");
            }    

        }
    }
}
