using Base.Command;
using MefContrib.Hosting.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Base
{
    [Export(typeof(IGenericContractRegistry))]
    public class GenericContractRegistry : GenericContractRegistryBase
    {

        protected override void Initialize()
        {
            Register(typeof(IUndoRedoStack<>), typeof(UndoRedoStack<>));
            Register(typeof(IPublicUndoRedoStack<>), typeof(UndoRedoStack<>));
        }
    }
}
