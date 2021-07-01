namespace PokemonSimulator
{
  partial class MainView
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
      this.logBox = new System.Windows.Forms.ListBox();
      this.SuspendLayout();
      // 
      // logBox
      // 
      this.logBox.FormattingEnabled = true;
      this.logBox.Location = new System.Drawing.Point(28, 187);
      this.logBox.Name = "logBox";
      this.logBox.Size = new System.Drawing.Size(747, 238);
      this.logBox.TabIndex = 0;
      // 
      // MainView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.logBox);
      this.Name = "MainView";
      this.Text = "Pokemon Battle Simulator";
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.ListBox logBox;
  }
}

