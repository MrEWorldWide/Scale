<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Scalefrm
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
        Label1 = New Label()
        TestPrintbtn = New Button()
        TestConnectionbtn = New Button()
        Exitbtn = New Button()
        ReadScalebtn = New Button()
        CurrentReadingtxt = New Label()
        ResetScalebtn = New Button()
        ContinuousTestbtn = New Button()
        ScalePortcombo = New ComboBox()
        Label2 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(568, 66)
        Label1.Margin = New Padding(6, 0, 6, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(391, 55)
        Label1.TabIndex = 1
        Label1.Text = "Current Reading:"
        ' 
        ' TestPrintbtn
        ' 
        TestPrintbtn.Location = New Point(29, 212)
        TestPrintbtn.Margin = New Padding(6)
        TestPrintbtn.Name = "TestPrintbtn"
        TestPrintbtn.Size = New Size(279, 63)
        TestPrintbtn.TabIndex = 6
        TestPrintbtn.Text = "Print Label"
        TestPrintbtn.UseVisualStyleBackColor = True
        ' 
        ' TestConnectionbtn
        ' 
        TestConnectionbtn.Location = New Point(29, 134)
        TestConnectionbtn.Margin = New Padding(6)
        TestConnectionbtn.Name = "TestConnectionbtn"
        TestConnectionbtn.Size = New Size(279, 66)
        TestConnectionbtn.TabIndex = 7
        TestConnectionbtn.Text = "Test Connection"
        TestConnectionbtn.UseVisualStyleBackColor = True
        ' 
        ' Exitbtn
        ' 
        Exitbtn.AutoSize = True
        Exitbtn.Location = New Point(678, 461)
        Exitbtn.Margin = New Padding(6)
        Exitbtn.Name = "Exitbtn"
        Exitbtn.Size = New Size(281, 76)
        Exitbtn.TabIndex = 8
        Exitbtn.Text = "Exit"
        Exitbtn.UseVisualStyleBackColor = True
        ' 
        ' ReadScalebtn
        ' 
        ReadScalebtn.Location = New Point(29, 428)
        ReadScalebtn.Margin = New Padding(6)
        ReadScalebtn.Name = "ReadScalebtn"
        ReadScalebtn.Size = New Size(279, 79)
        ReadScalebtn.TabIndex = 9
        ReadScalebtn.Text = "Print Scale"
        ReadScalebtn.UseVisualStyleBackColor = True
        ' 
        ' CurrentReadingtxt
        ' 
        CurrentReadingtxt.AutoSize = True
        CurrentReadingtxt.Font = New Font("Microsoft Sans Serif", 27.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CurrentReadingtxt.Location = New Point(755, 148)
        CurrentReadingtxt.Name = "CurrentReadingtxt"
        CurrentReadingtxt.Size = New Size(204, 95)
        CurrentReadingtxt.TabIndex = 10
        CurrentReadingtxt.Text = "0.00"
        ' 
        ' ResetScalebtn
        ' 
        ResetScalebtn.Location = New Point(29, 287)
        ResetScalebtn.Margin = New Padding(6)
        ResetScalebtn.Name = "ResetScalebtn"
        ResetScalebtn.Size = New Size(279, 62)
        ResetScalebtn.TabIndex = 11
        ResetScalebtn.Text = "Reset Scale"
        ResetScalebtn.UseVisualStyleBackColor = True
        ' 
        ' ContinuousTestbtn
        ' 
        ContinuousTestbtn.Font = New Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ContinuousTestbtn.Location = New Point(29, 352)
        ContinuousTestbtn.Margin = New Padding(6)
        ContinuousTestbtn.Name = "ContinuousTestbtn"
        ContinuousTestbtn.Size = New Size(279, 64)
        ContinuousTestbtn.TabIndex = 12
        ContinuousTestbtn.Text = "Continuous Test"
        ContinuousTestbtn.UseVisualStyleBackColor = True
        ' 
        ' ScalePortcombo
        ' 
        ScalePortcombo.FormattingEnabled = True
        ScalePortcombo.Location = New Point(163, 44)
        ScalePortcombo.Name = "ScalePortcombo"
        ScalePortcombo.Size = New Size(272, 62)
        ScalePortcombo.TabIndex = 13
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(29, 41)
        Label2.Margin = New Padding(6, 0, 6, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(125, 55)
        Label2.TabIndex = 14
        Label2.Text = "Port:"
        ' 
        ' Scalefrm
        ' 
        AutoScaleDimensions = New SizeF(216F, 216F)
        AutoScaleMode = AutoScaleMode.Dpi
        AutoSize = True
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        ClientSize = New Size(1019, 581)
        Controls.Add(Label2)
        Controls.Add(ScalePortcombo)
        Controls.Add(ContinuousTestbtn)
        Controls.Add(ResetScalebtn)
        Controls.Add(CurrentReadingtxt)
        Controls.Add(ReadScalebtn)
        Controls.Add(Exitbtn)
        Controls.Add(TestConnectionbtn)
        Controls.Add(TestPrintbtn)
        Controls.Add(Label1)
        Font = New Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        KeyPreview = True
        Margin = New Padding(6)
        Name = "Scalefrm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Scalefrm"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents TestPrintbtn As Button
    Friend WithEvents TestConnectionbtn As Button
    Friend WithEvents Exitbtn As Button
    Friend WithEvents ReadScalebtn As Button
    Friend WithEvents CurrentReadingtxt As Label
    Friend WithEvents ResetScalebtn As Button
    Friend WithEvents ContinuousTestbtn As Button
    Friend WithEvents ScalePortcombo As ComboBox
    Friend WithEvents Label2 As Label
End Class
