
namespace GGuerra.Cardamatic.WinForm.Application
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cardReaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uidLabel = new System.Windows.Forms.Label();
            this.cardLabel = new System.Windows.Forms.Label();
            this.schemeLabel = new System.Windows.Forms.Label();
            this.schemaComboBox = new System.Windows.Forms.ComboBox();
            this.keysetComboBox = new System.Windows.Forms.ComboBox();
            this.keysetLabel = new System.Windows.Forms.Label();
            this.dataGroupBox = new System.Windows.Forms.GroupBox();
            this.dataTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.dataGroupBox.SuspendLayout();
            this.dataTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.cardReaderToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // cardReaderToolStripMenuItem
            // 
            this.cardReaderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readToolStripMenuItem,
            this.writeToolStripMenuItem,
            this.toolStripSeparator2,
            this.pickToolStripMenuItem});
            this.cardReaderToolStripMenuItem.Name = "cardReaderToolStripMenuItem";
            this.cardReaderToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.cardReaderToolStripMenuItem.Text = "CardReader";
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.readToolStripMenuItem.Text = "Read card";
            this.readToolStripMenuItem.Click += new System.EventHandler(this.ReadToolStripMenuItem_Click);
            // 
            // writeToolStripMenuItem
            // 
            this.writeToolStripMenuItem.Name = "writeToolStripMenuItem";
            this.writeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.writeToolStripMenuItem.Text = "Write card";
            this.writeToolStripMenuItem.Click += new System.EventHandler(this.WriteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(132, 6);
            // 
            // pickToolStripMenuItem
            // 
            this.pickToolStripMenuItem.Name = "pickToolStripMenuItem";
            this.pickToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.pickToolStripMenuItem.Text = "Card reader";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.uidLabel);
            this.panel1.Controls.Add(this.cardLabel);
            this.panel1.Controls.Add(this.schemeLabel);
            this.panel1.Controls.Add(this.schemaComboBox);
            this.panel1.Controls.Add(this.keysetComboBox);
            this.panel1.Controls.Add(this.keysetLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 35);
            this.panel1.TabIndex = 7;
            // 
            // uidLabel
            // 
            this.uidLabel.AutoSize = true;
            this.uidLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.uidLabel.Location = new System.Drawing.Point(408, 9);
            this.uidLabel.Name = "uidLabel";
            this.uidLabel.Size = new System.Drawing.Size(18, 19);
            this.uidLabel.TabIndex = 6;
            this.uidLabel.Text = "-";
            // 
            // cardLabel
            // 
            this.cardLabel.AutoSize = true;
            this.cardLabel.Location = new System.Drawing.Point(367, 12);
            this.cardLabel.Name = "cardLabel";
            this.cardLabel.Size = new System.Drawing.Size(35, 15);
            this.cardLabel.TabIndex = 5;
            this.cardLabel.Text = "Card:";
            // 
            // schemeLabel
            // 
            this.schemeLabel.AutoSize = true;
            this.schemeLabel.Location = new System.Drawing.Point(3, 12);
            this.schemeLabel.Name = "schemeLabel";
            this.schemeLabel.Size = new System.Drawing.Size(52, 15);
            this.schemeLabel.TabIndex = 1;
            this.schemeLabel.Text = "Schema:";
            // 
            // schemaComboBox
            // 
            this.schemaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.schemaComboBox.FormattingEnabled = true;
            this.schemaComboBox.Location = new System.Drawing.Point(61, 9);
            this.schemaComboBox.Name = "schemaComboBox";
            this.schemaComboBox.Size = new System.Drawing.Size(121, 23);
            this.schemaComboBox.TabIndex = 2;
            this.schemaComboBox.SelectedIndexChanged += new System.EventHandler(this.SchemaComboBox_SelectedIndexChanged);
            // 
            // keysetComboBox
            // 
            this.keysetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.keysetComboBox.FormattingEnabled = true;
            this.keysetComboBox.Location = new System.Drawing.Point(239, 9);
            this.keysetComboBox.Name = "keysetComboBox";
            this.keysetComboBox.Size = new System.Drawing.Size(121, 23);
            this.keysetComboBox.TabIndex = 4;
            this.keysetComboBox.SelectedIndexChanged += new System.EventHandler(this.KeysetCombobox_SelectedIndexChanged);
            // 
            // keysetLabel
            // 
            this.keysetLabel.AutoSize = true;
            this.keysetLabel.Location = new System.Drawing.Point(189, 12);
            this.keysetLabel.Name = "keysetLabel";
            this.keysetLabel.Size = new System.Drawing.Size(44, 15);
            this.keysetLabel.TabIndex = 3;
            this.keysetLabel.Text = "Keyset:";
            // 
            // dataGroupBox
            // 
            this.dataGroupBox.Controls.Add(this.dataTabControl);
            this.dataGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGroupBox.Location = new System.Drawing.Point(0, 65);
            this.dataGroupBox.Name = "dataGroupBox";
            this.dataGroupBox.Size = new System.Drawing.Size(1008, 664);
            this.dataGroupBox.TabIndex = 8;
            this.dataGroupBox.TabStop = false;
            this.dataGroupBox.Text = "Data";
            this.dataGroupBox.SizeChanged += new System.EventHandler(this.DataGroupBox_SizeChanged);
            // 
            // dataTabControl
            // 
            this.dataTabControl.Controls.Add(this.tabPage1);
            this.dataTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTabControl.Location = new System.Drawing.Point(3, 19);
            this.dataTabControl.Name = "dataTabControl";
            this.dataTabControl.SelectedIndex = 0;
            this.dataTabControl.Size = new System.Drawing.Size(1002, 642);
            this.dataTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(994, 614);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.dataGroupBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "GGuerra.Cardamatic";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.dataGroupBox.ResumeLayout(false);
            this.dataTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cardReaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label schemeLabel;
        private System.Windows.Forms.ComboBox schemaComboBox;
        private System.Windows.Forms.ComboBox keysetComboBox;
        private System.Windows.Forms.Label keysetLabel;
        private System.Windows.Forms.GroupBox dataGroupBox;
        private System.Windows.Forms.TabControl dataTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem pickToolStripMenuItem;
        private System.Windows.Forms.Label uidLabel;
        private System.Windows.Forms.Label cardLabel;
    }
}

