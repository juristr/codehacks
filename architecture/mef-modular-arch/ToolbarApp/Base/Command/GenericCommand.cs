using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Command
{
    public class GenericCommand : ICommand
    {
        private Action execAction;
        private Action undoAction;

        public GenericCommand(Action execAction, Action undoAction)
        {
            this.execAction = execAction;
            this.undoAction = undoAction;
        }
        
        public object Context { get; set;}

        public void Execute()
        {
            execAction.Invoke();
        }

        public void Undo()
        {
            undoAction.Invoke();
        }


        public string Description
        {
            get { return "Generic Action"; }
        }
    }
}
