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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Undo Stack";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Redo Stack";
            // 
            // UndoRedoStack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 405);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxRedoStack);
            this.Controls.Add(this.listBoxUndoStack);
            this.Name = "UndoRedoStack";
            this.Text = "UndoRedoStack";
            this.Load += new System.EventHandler(this.UndoRedoStack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUndoStack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRedoStack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxUndoStack;
        private System.Windows.Forms.ListBox listBoxRedoStack;
        private System.Windows.Forms.BindingSource bindingSourceUndoStack;
        private System.Windows.Forms.BindingSource bindingSourceRedoStack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}