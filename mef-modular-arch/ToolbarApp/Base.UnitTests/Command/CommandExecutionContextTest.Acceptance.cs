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
        private CommandExecutionContext context;
        private CommandHandler handler;
        private UndoRedoStack<ICommand> stack;

        [TestInitialize()]
        public void SetUp()
        {
            stack = new UndoRedoStack<ICommand>();
            handler = new CommandHandler(stack);

            context = new CommandExecutionContext(handler);
        }

        [TestCleanup()]
        public void TearDown()
        {
            handler = null;
        }

        [TestMethod]
        public void ShouldCorrectlyExecuteTheCommands()
        {
            var obj = new ValueObject() { Number = 1 };

            context.Execute(new AddOneCommand(obj));
            Assert.AreEqual(2, obj.Number);
        }

    }

}
