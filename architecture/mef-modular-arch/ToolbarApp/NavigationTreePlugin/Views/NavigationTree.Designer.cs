namespace NavigationTreePlugin.Views
{
    partial class NavigationTree
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
            this.listBoxNavigation = new System.Windows.Forms.ListBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxNavigation
            // 
            this.listBoxNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxNavigation.FormattingEnabled = true;
            this.listBoxNavigation.Location = new System.Drawing.Point(0, 0);
            this.listBoxNavigation.Name = "listBoxNavigation";
            this.listBoxNavigation.Size = new System.Drawing.Size(350, 372);
            this.listBoxNavigation.TabIndex = 6;
            this.listBoxNavigation.DoubleClick += new System.EventHandler(this.listBoxNavigation_DoubleClick);
            // 
            // NavigationTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxNavigation);
            this.Name = "NavigationTree";
            this.Size = new System.Drawing.Size(350, 372);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxNavigation;
        private System.Windows.Forms.BindingSource bindingSource1;

    }
}
