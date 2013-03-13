using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base.Command;
using Moq;
using System.Linq;

namespace Base.UnitTests.Command
{

    [TestClass]
    public class CommandExecutionContextTest_Acceptance
    {
        private CommandHandler handler;
        private UndoRedoStack<ICommand> stack;

        [TestInitialize()]
        public void SetUp()
        {
            stack = new UndoRedoStack<ICommand>();
            handler = new CommandHandler(stack);
        }

        [TestCleanup()]
        public void TearDown()
        {
            handler = null;
        }

        [TestMethod]
        public void ShouldCorrectlyExecuteTheCommands()
        {
            Assert.Inconclusive("Delete?");

            ////arrange
            //var obj = new ValueObject() { Number = 1 };
            //var context = new CommandExecutionContext(handler);

            ////act
            //context.Execute(new AddOneCommand(obj));

            ////assert
            //Assert.AreEqual(2, obj.Number);
        }


        [TestMethod]
        public void ShouldProperlyRemoveCommandsBoundToAGivenExecutionContext()
        {
            Assert.Inconclusive("Delete?");

            ////arrange
            //var cmd1 = new AddOneCommand(new ValueObject() { Number = 1 });
            //var cmd2 = new AddOneCommand(new ValueObject() { Number = 1 });
            //var context1 = new CommandExecutionContext(handler);
            //var context2 = new CommandExecutionContext(handler);

            ////act
            //context1.Execute(cmd1);
            //context2.Execute(cmd2);

            ////assert
            //Assert.AreEqual(2, stack.UndoItems().Count(), "there should be 2 undo entries");
            //context1.Dispose();
            //Assert.AreEqual(1, stack.UndoItems().Count(), "there should be 1 undo entry left");
            //Assert.AreEqual(cmd2, stack.UndoItems().ElementAt(0), "should be the correct command obj");
        }

    }

}
