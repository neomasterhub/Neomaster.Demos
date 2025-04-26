namespace Neomaster.Demos.Apps.Tasks.TaskConfigureAwait;

partial class Form1
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
    button_ca_true = new Button();
    button_ca_false = new Button();
    textBox_time = new TextBox();
    SuspendLayout();
    // 
    // button_ca_true
    // 
    button_ca_true.Font = new Font("Lucida Console", 10F);
    button_ca_true.Location = new Point(24, 22);
    button_ca_true.Name = "button_ca_true";
    button_ca_true.Size = new Size(207, 38);
    button_ca_true.TabIndex = 0;
    button_ca_true.Text = "ConfigureAwait(true)";
    button_ca_true.UseVisualStyleBackColor = true;
    button_ca_true.Click += button_ca_true_Click;
    // 
    // button_ca_false
    // 
    button_ca_false.Font = new Font("Lucida Console", 10F);
    button_ca_false.Location = new Point(24, 66);
    button_ca_false.Name = "button_ca_false";
    button_ca_false.Size = new Size(207, 38);
    button_ca_false.TabIndex = 1;
    button_ca_false.Text = "ConfigureAwait(false)";
    button_ca_false.UseVisualStyleBackColor = true;
    button_ca_false.Click += button_ca_false_Click;
    // 
    // textBox_time
    // 
    textBox_time.Font = new Font("Lucida Console", 10F);
    textBox_time.Location = new Point(24, 110);
    textBox_time.Name = "textBox_time";
    textBox_time.ReadOnly = true;
    textBox_time.Size = new Size(207, 21);
    textBox_time.TabIndex = 2;
    textBox_time.Text = "result";
    textBox_time.TextAlign = HorizontalAlignment.Center;
    // 
    // Form1
    // 
    AutoScaleDimensions = new SizeF(7F, 15F);
    AutoScaleMode = AutoScaleMode.Font;
    ClientSize = new Size(257, 166);
    Controls.Add(textBox_time);
    Controls.Add(button_ca_false);
    Controls.Add(button_ca_true);
    Name = "Form1";
    Text = "ConfigureAwait()";
    ResumeLayout(false);
    PerformLayout();
  }

  #endregion

  private Button button_ca_true;
  private Button button_ca_false;
  private TextBox textBox_time;
}
