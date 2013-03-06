﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Base.Command;
using WinFormsClientApplication.UndoRedo.Commands;

namespace WinFormsClientApplication.UndoRedo
{
    [Export(typeof(UndoRedoView))]
    partial class UndoRedoView : UserControl
    {
        private IList<string> SourceItems = new BindingList<string>
                {
                    "Paul", "Fritz", "Heinz", "Sepp"
                };
        private IList<string> DestinationItems = new BindingList<string>();

        public UndoRedoView()
        {
            InitializeComponent();

            bindingSourceList.DataSource = SourceItems;
            bindingDestinationList.DataSource = DestinationItems;
        }

        private void buttonMoveToDest_Click(object sender, EventArgs e)
        {
            var selectedItem = listBoxSource.SelectedItem;

            var command = new MoveCommand(SourceItems, DestinationItems, selectedItem as string);
            command.Execute();
        }

        private void buttonMoveToSource_Click(object sender, EventArgs e)
        {
            var selectedItem = listBoxDestination.SelectedItem;

            var command = new MoveCommand(DestinationItems, SourceItems, selectedItem as string);
            command.Execute();
        }
    }
}