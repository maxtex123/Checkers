namespace KSU.CIS300.Checkers
{
    partial class UserInterface
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
            this.uxMenuStrip = new System.Windows.Forms.MenuStrip();
            this.uxFileToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.uxNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.uxFlowLayoutPanel_board = new System.Windows.Forms.FlowLayoutPanel();
            this.uxStatusStrip = new System.Windows.Forms.StatusStrip();
            this.uxToolStripStatusLabel_Turn = new System.Windows.Forms.ToolStripStatusLabel();
            this.uxMenuStrip.SuspendLayout();
            this.uxStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxMenuStrip
            // 
            this.uxMenuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.uxMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.uxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxFileToolStripMenu});
            this.uxMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.uxMenuStrip.Name = "uxMenuStrip";
            this.uxMenuStrip.Size = new System.Drawing.Size(790, 33);
            this.uxMenuStrip.TabIndex = 0;
            this.uxMenuStrip.Text = "menuStrip1";
            // 
            // uxFileToolStripMenu
            // 
            this.uxFileToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxNewGame});
            this.uxFileToolStripMenu.Name = "uxFileToolStripMenu";
            this.uxFileToolStripMenu.Size = new System.Drawing.Size(54, 29);
            this.uxFileToolStripMenu.Text = "File";
            // 
            // uxNewGame
            // 
            this.uxNewGame.Name = "uxNewGame";
            this.uxNewGame.Size = new System.Drawing.Size(200, 34);
            this.uxNewGame.Text = "New Game";
            this.uxNewGame.Click += new System.EventHandler(this.UxNewGame_Click);
            // 
            // uxFlowLayoutPanel_board
            // 
            this.uxFlowLayoutPanel_board.Location = new System.Drawing.Point(12, 36);
            this.uxFlowLayoutPanel_board.Name = "uxFlowLayoutPanel_board";
            this.uxFlowLayoutPanel_board.Size = new System.Drawing.Size(480, 510);
            this.uxFlowLayoutPanel_board.TabIndex = 1;
            // 
            // uxStatusStrip
            // 
            this.uxStatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.uxStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxToolStripStatusLabel_Turn});
            this.uxStatusStrip.Location = new System.Drawing.Point(0, 897);
            this.uxStatusStrip.Name = "uxStatusStrip";
            this.uxStatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.uxStatusStrip.Size = new System.Drawing.Size(790, 22);
            this.uxStatusStrip.TabIndex = 2;
            this.uxStatusStrip.Text = "statusStrip1";
            // 
            // uxToolStripStatusLabel_Turn
            // 
            this.uxToolStripStatusLabel_Turn.Name = "uxToolStripStatusLabel_Turn";
            this.uxToolStripStatusLabel_Turn.Size = new System.Drawing.Size(0, 15);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 919);
            this.Controls.Add(this.uxStatusStrip);
            this.Controls.Add(this.uxFlowLayoutPanel_board);
            this.Controls.Add(this.uxMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.uxMenuStrip;
            this.MinimizeBox = false;
            this.Name = "UserInterface";
            this.Text = "Form1";
            this.uxMenuStrip.ResumeLayout(false);
            this.uxMenuStrip.PerformLayout();
            this.uxStatusStrip.ResumeLayout(false);
            this.uxStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip uxMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem uxFileToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem uxNewGame;
        private System.Windows.Forms.FlowLayoutPanel uxFlowLayoutPanel_board;
        private System.Windows.Forms.StatusStrip uxStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel uxToolStripStatusLabel_Turn;
    }
}

