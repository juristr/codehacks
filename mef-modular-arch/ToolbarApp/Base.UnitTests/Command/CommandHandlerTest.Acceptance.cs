using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base.Command;
using Moq;
using System.Linq;

namespace Base.UnitTests.Command
{

    [TestClass]
    public class CommandHandlerTestAcceptance
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
        public void ShouldCorrectlyUndoAndRedoActions()
        {
            var obj = new ValueObject() { Number = 1 };

            handler.Execute(new AddOneCommand(obj));
            Assert.AreEqual(2, obj.Number);

            handler.Undo();
            Assert.AreEqual(1, obj.Number);

            handler.Redo();
            Assert.AreEqual(2, obj.Number);
        }

        [TestMethod]
        public void ShouldProperlyLimitTheAmountOfOperationsThatCanBeUndone()
        {
            //execute more than 10 operations
            for (int i = 0; i < stack.MaxStackSize + 10; i++)
            {
                handler.Execute(new AddOneCommand(new ValueObject()));    
            }

            Assert.AreEqual(stack.MaxStackSize, stack.UndoItems().Count(), "There should be <= 10 items in the undo stack");
        }

        [TestMethod]
        public void ShouldFailSilentlyWhenUndoingMultipleTimes()
        {
            //arrange
            var hasFailed = false;
            var valueObj = new ValueObject();
            
            handler.Execute(new AddOneCommand(valueObj));

            //act
            //execute a couple of times
            handler.Undo();
            try
            {
                handler.Undo(); //this should fail as there is nothing to be undone
            }
            catch (ApplicationException)
            {
                hasFailed = true;
            }
            

            //assert
            Assert.AreEqual(0, valueObj.Number);
            Assert.IsTrue(hasFailed, "The 2nd undo operation should have failed");

        }
    }

    //nothing more than an object holding a number
    class ValueObject
    {
        public int Number { get; set; }
    }

    //very simple demo implementation of a command
    class AddOneCommand : ICommand
    {
        private ValueObject valObj;

        public AddOneCommand(ValueObject valObj)
        {
            this.valObj = valObj;
        }

        public object Context { get; set; }

        public void Execute()
        {
            valObj.Number++;
        }

        public void Undo()
        {
            valObj.Number--;
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }
    }

    class AddTwoCommand : ICommand
    {
        private ValueObject valObj;

        public AddTwoCommand(ValueObject valObj)
        {
            this.valObj = valObj;
        }

        public object Context { get; set; }

        public void Execute()
        {
            valObj.Number = valObj.Number + 2;
        }

        public void Undo()
        {
            valObj.Number = valObj.Number - 2;
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }
    }
}
