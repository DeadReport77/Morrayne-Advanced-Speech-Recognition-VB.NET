<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Morrigan
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lstboxCommands = New System.Windows.Forms.ListBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstboxCommands
        '
        Me.lstboxCommands.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lstboxCommands.ForeColor = System.Drawing.Color.Red
        Me.lstboxCommands.FormattingEnabled = True
        Me.lstboxCommands.Location = New System.Drawing.Point(12, 184)
        Me.lstboxCommands.Name = "lstboxCommands"
        Me.lstboxCommands.Size = New System.Drawing.Size(10, 4)
        Me.lstboxCommands.TabIndex = 1
        Me.lstboxCommands.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Morrayne.My.Resources.Resources.U9n0
        Me.PictureBox1.Location = New System.Drawing.Point(0, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(359, 330)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Morrigan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(355, 328)
        Me.Controls.Add(Me.lstboxCommands)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Morrigan"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Morrigan"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lstboxCommands As ListBox
End Class
