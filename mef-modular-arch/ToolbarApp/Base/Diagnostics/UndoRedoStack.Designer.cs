namespace Base.Diagnostics
{
    partial class UndoRedoStack
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxUndoStack = new System.Windows.Forms.ListBox();
            this.bindingSourceUndoStack = new System.Windows.Forms.BindingSource(this.components);
            this.listBoxRedoStack = new System.Windows.Forms.ListBox();
            this.bindingSourceRedoStack = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUndoStack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRedoStack)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxUndoStack
            // 
            this.listBoxUndoStack.DataSource = this.bindingSourceUndoStack;
            this.listBoxUndoStack.DisplayMember = "Description";
            this.listBoxUndoStack.FormattingEnabled = true;
            this.listBoxUndoStack.Location = new System.Drawing.Point(38, 33);
            this.listBoxUndoStack.Name = "listBoxUndoStack";
            this.listBoxUndoStack.Size = new System.Drawing.Size(296, 329);
            this.listBoxUndoStack.TabIndex = 0;
            // 
            // bindingSourceUndoStack
            // 
            this.bindingSourceUndoStack.DataSource = typeof(Base.Command.ICommand);
            // 
            // listBoxRedoStack
            // 
            this.listBoxRedoStack.DataSource = this.bindingSourceRedoStack;
            this.listBoxRedoStack.DisplayMember = "Description";
            this.listBoxRedoStack.FormattingEnabled = true;
            this.listBoxRedoStack.Location = new System.Drawing.Point(370, 33);
            this.listBoxRedoStack.Name = "listBoxRedoStack";
            this.listBoxRedoStack.Size = new System.Drawing.Size(296, 329);
            this.listBoxRedoStack.TabIndex = 1;
            // 
            // bindingSourceRedoStack
            // 
            this.bindingSourceRedoStack.DataSource = typeof(Base.Command.ICommand);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UndoRedoStack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 405);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBoxRedoStack);
            this.Controls.Add(this.listBoxUndoStack);
            this.Name = "UndoRedoStack";
            this.Text = "UndoRedoStack";
            this.Load += new System.EventHandler(this.UndoRedoStack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUndoStack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRedoStack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxUndoStack;
        private System.Windows.Forms.ListBox listBoxRedoStack;
        private System.Windows.Forms.BindingSource bindingSourceUndoStack;
        private System.Windows.Forms.BindingSource bindingSourceRedoStack;
        private System.Windows.Forms.Button button1;
    }
}