namespace WinFormsClientApplication.UndoRedo
{
    partial class UndoRedoView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxSource = new System.Windows.Forms.ListBox();
            this.bindingSourceList = new System.Windows.Forms.BindingSource(this.components);
            this.listBoxDestination = new System.Windows.Forms.ListBox();
            this.bindingDestinationList = new System.Windows.Forms.BindingSource(this.components);
            this.buttonMoveToDest = new System.Windows.Forms.Button();
            this.buttonMoveToSource = new System.Windows.Forms.Button();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.buttonRedo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingDestinationList)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxSource
            // 
            this.listBoxSource.DataSource = this.bindingSourceList;
            this.listBoxSource.FormattingEnabled = true;
            this.listBoxSource.Location = new System.Drawing.Point(40, 44);
            this.listBoxSource.Name = "listBoxSource";
            this.listBoxSource.Size = new System.Drawing.Size(283, 368);
            this.listBoxSource.TabIndex = 0;
            // 
            // listBoxDestination
            // 
            this.listBoxDestination.DataSource = this.bindingDestinationList;
            this.listBoxDestination.FormattingEnabled = true;
            this.listBoxDestination.Location = new System.Drawing.Point(412, 44);
            this.listBoxDestination.Name = "listBoxDestination";
            this.listBoxDestination.Size = new System.Drawing.Size(283, 368);
            this.listBoxDestination.TabIndex = 1;
            // 
            // buttonMoveToDest
            // 
            this.buttonMoveToDest.Location = new System.Drawing.Point(331, 80);
            this.buttonMoveToDest.Name = "buttonMoveToDest";
            this.buttonMoveToDest.Size = new System.Drawing.Size(75, 23);
            this.buttonMoveToDest.TabIndex = 2;
            this.buttonMoveToDest.Text = "- >";
            this.buttonMoveToDest.UseVisualStyleBackColor = true;
            this.buttonMoveToDest.Click += new System.EventHandler(this.buttonMoveToDest_Click);
            // 
            // buttonMoveToSource
            // 
            this.buttonMoveToSource.Location = new System.Drawing.Point(331, 109);
            this.buttonMoveToSource.Name = "buttonMoveToSource";
            this.buttonMoveToSource.Size = new System.Drawing.Size(75, 26);
            this.buttonMoveToSource.TabIndex = 3;
            this.buttonMoveToSource.Text = "< -";
            this.buttonMoveToSource.UseVisualStyleBackColor = true;
            this.buttonMoveToSource.Click += new System.EventHandler(this.buttonMoveToSource_Click);
            // 
            // buttonUndo
            // 
            this.buttonUndo.Location = new System.Drawing.Point(40, 3);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(75, 23);
            this.buttonUndo.TabIndex = 4;
            this.buttonUndo.Text = "Undo";
            this.buttonUndo.UseVisualStyleBackColor = true;
            // 
            // buttonRedo
            // 
            this.buttonRedo.Location = new System.Drawing.Point(121, 3);
            this.buttonRedo.Name = "buttonRedo";
            this.buttonRedo.Size = new System.Drawing.Size(75, 23);
            this.buttonRedo.TabIndex = 5;
            this.buttonRedo.Text = "Redo";
            this.buttonRedo.UseVisualStyleBackColor = true;
            // 
            // UndoRedoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonRedo);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.buttonMoveToSource);
            this.Controls.Add(this.buttonMoveToDest);
            this.Controls.Add(this.listBoxDestination);
            this.Controls.Add(this.listBoxSource);
            this.Name = "UndoRedoView";
            this.Size = new System.Drawing.Size(826, 436);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingDestinationList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxSource;
        private System.Windows.Forms.ListBox listBoxDestination;
        private System.Windows.Forms.Button buttonMoveToDest;
        private System.Windows.Forms.Button buttonMoveToSource;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.Button buttonRedo;
        private System.Windows.Forms.BindingSource bindingSourceList;
        private System.Windows.Forms.BindingSource bindingDestinationList;
    }
}
