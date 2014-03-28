namespace BrilliantStudentGUI
{
  partial class GameChooser
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
      this.gameListBox = new System.Windows.Forms.ListBox();
      this.OK = new System.Windows.Forms.Button();
      this.btn_cancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // gameListBox
      // 
      this.gameListBox.FormattingEnabled = true;
      this.gameListBox.Location = new System.Drawing.Point(12, 36);
      this.gameListBox.Name = "gameListBox";
      this.gameListBox.Size = new System.Drawing.Size(198, 199);
      this.gameListBox.TabIndex = 1;
      this.gameListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gameListBox_MouseDoubleClick);
      // 
      // OK
      // 
      this.OK.Location = new System.Drawing.Point(12, 241);
      this.OK.Name = "OK";
      this.OK.Size = new System.Drawing.Size(91, 21);
      this.OK.TabIndex = 2;
      this.OK.Text = "OK";
      this.OK.UseVisualStyleBackColor = true;
      this.OK.Click += new System.EventHandler(this.OK_Click);
      // 
      // btn_cancel
      // 
      this.btn_cancel.Location = new System.Drawing.Point(127, 241);
      this.btn_cancel.Name = "btn_cancel";
      this.btn_cancel.Size = new System.Drawing.Size(83, 21);
      this.btn_cancel.TabIndex = 3;
      this.btn_cancel.Text = "Cancel";
      this.btn_cancel.UseVisualStyleBackColor = true;
      this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(198, 24);
      this.label1.TabIndex = 4;
      this.label1.Text = "Please Select A Game";
      // 
      // GameChooser
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(222, 273);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btn_cancel);
      this.Controls.Add(this.OK);
      this.Controls.Add(this.gameListBox);
      this.Name = "GameChooser";
      this.Text = "Game Chooser";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox gameListBox;
    private System.Windows.Forms.Button OK;
    private System.Windows.Forms.Button btn_cancel;
    private System.Windows.Forms.Label label1;

  }
}

