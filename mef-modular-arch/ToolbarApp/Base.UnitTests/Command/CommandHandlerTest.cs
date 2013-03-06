using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base.Command;

namespace Base.UnitTests.Command
{

    class MyTestCommand : ICommand
    {
        public object Context { get; set;}
        public bool HasExecuted { get; set; }

        public void Execute()
        {
            HasExecuted = true;
        }
    }

    public class CommandHandlerTest
    {
        [TestClass]
        public class TheExecuteCommandMethod
        {
            private CommandHandler handler;
            private IUndoRedoStack<ICommand> undoRedo;

            [TestInitialize()]
            public void SetUp()
            {
                undoRedo = new UndoRedoStack<ICommand>();
                handler = new CommandHandler(undoRedo);
            }

            [TestCleanup()]
            public void TearDown()
            {
                handler = null;
            }	

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
                Assert.Inconclusive("Not yet implemented");
            }

        }
    }
}
