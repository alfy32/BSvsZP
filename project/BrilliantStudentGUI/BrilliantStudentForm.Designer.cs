namespace BrilliantStudentGUI
{
  partial class BrilliantStudentForm
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
      this.btn_joinGame = new System.Windows.Forms.Button();
      this.lbl_currentGame = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.btn_selectGame = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btn_joinGame
      // 
      this.btn_joinGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_joinGame.Location = new System.Drawing.Point(31, 171);
      this.btn_joinGame.Name = "btn_joinGame";
      this.btn_joinGame.Size = new System.Drawing.Size(265, 98);
      this.btn_joinGame.TabIndex = 0;
      this.btn_joinGame.Text = "Join Game";
      this.btn_joinGame.UseVisualStyleBackColor = true;
      this.btn_joinGame.Click += new System.EventHandler(this.btn_joinGame_Click);
      // 
      // lbl_currentGame
      // 
      this.lbl_currentGame.AutoSize = true;
      this.lbl_currentGame.Location = new System.Drawing.Point(109, 142);
      this.lbl_currentGame.Name = "lbl_currentGame";
      this.lbl_currentGame.Size = new System.Drawing.Size(31, 13);
      this.lbl_currentGame.TabIndex = 1;
      this.lbl_currentGame.Text = "none";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(28, 142);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(75, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Current Game:";
      // 
      // btn_selectGame
      // 
      this.btn_selectGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_selectGame.Location = new System.Drawing.Point(31, 26);
      this.btn_selectGame.Name = "btn_selectGame";
      this.btn_selectGame.Size = new System.Drawing.Size(265, 98);
      this.btn_selectGame.TabIndex = 3;
      this.btn_selectGame.Text = "Select Game";
      this.btn_selectGame.UseVisualStyleBackColor = true;
      this.btn_selectGame.Click += new System.EventHandler(this.btn_selectGame_Click);
      // 
      // BrilliantStudentForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(571, 458);
      this.Controls.Add(this.btn_selectGame);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lbl_currentGame);
      this.Controls.Add(this.btn_joinGame);
      this.Name = "BrilliantStudentForm";
      this.Text = "BrilliantStudent";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btn_joinGame;
    private System.Windows.Forms.Label lbl_currentGame;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btn_selectGame;
  }
}