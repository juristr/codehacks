using Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormsClientApplication.UndoRedo.Commands
{
    public class MoveCommand : ICommand
    {
        private IList<string> Source;
        private IList<string> Destination;
        private string DataItem;

        public MoveCommand(IList<string> source, IList<string> destination, string dataItem)
        {
            this.Source = source;
            this.Destination = destination;
            this.DataItem = dataItem;
        }

        public object Context { get; set; }
        
        public void Execute()
        {
            if (DataItem != null)
            {
                Source.Remove(DataItem);
                Destination.Add(DataItem);
            }
        }

        public void Undo()
        {
            if (DataItem != null)
            {
                Source.Add(DataItem);
                Destination.Remove(DataItem);
            }
        }

        public string Description
        {
            get
            {
                return "Moved " + DataItem;
            }
        }
    }
}
