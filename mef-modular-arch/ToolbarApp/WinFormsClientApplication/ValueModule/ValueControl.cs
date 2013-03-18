using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Base;
using System.ComponentModel.Composition;
using Base.Command;
using Base.Events;
using Microsoft.Practices.Prism.Events;

namespace WinFormsClientApplication.ValueModule
{
    [Export(typeof(IValueProviderExtension))]
    [Export(typeof(ValueControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    partial class ValueControl : UserControl, IValueProviderExtension
    {
        private BindingList<string> Values = new BindingList<string>();

        [Import]
        public ICommandExecutionContext CommandHandler { get; set; }

        [Import]
        public IEventAggregator EventAggregator { get; set; }

        public ValueControl()
        {
            InitializeComponent();

            bindingSourceList.DataSource = Values;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Disposed += ValueControl_Disposed;
        }

        void ValueControl_Disposed(object sender, EventArgs e)
        {
            CommandHandler.Dispose();
        }

        public string Value
        {
            get { return textBoxValue.Text; }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var text = textBoxValue.Text;

            if (!String.IsNullOrEmpty(text))
            {
                CommandHandler.Execute(
                    new GenericCommand(
                        () =>
                        {
                            Values.Add(text);
                        },
                        () =>
                        {
                            Values.Remove(text);
                        }));
                textBoxValue.Text = "";

                EventAggregator.GetEvent<TestEvent>().Publish("hi there, I come from the main form");
            }

            textBoxValue.Focus();
        }
    }
}
