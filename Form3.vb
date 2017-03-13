Public Class Form3
    Dim table(10) As Integer, x As Integer
    Public nb2 As Integer
    Public nb_p As Integer

   
    Private Sub tracer(ByVal tab() As Integer)
        For x = 0 To 9
            tab(x) = 1
        Next
    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For x = 0 To 9
            table(x) = 0
        Next
        Dim ff As String = ""
        For x = 0 To 9
            ff = ff & table(x)
        Next
        'MsgBox(ff)
        tracer(table)
        ff = ""
        For x = 0 To 9
            ff = ff & table(x)
        Next
        'MsgBox(ff)




        Me.TextBox1.Visible = False
        Me.TextBox2.Visible = False
        Me.Label1.Visible = False
        Me.Label2.Visible = False
        Me.Button1.Visible = False

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        nb_p = System.Decimal.Parse(TextBox2.Text.ToString)
        nb2 = System.Decimal.Parse(TextBox1.Text.ToString)

        If nb2 < 3 Or nb_p >= nb2 Then
            MsgBox("Erreur, veuillez saisir des parametres adequates", MsgBoxStyle.Critical, "Erreur")
        Else
            Me.Enabled = False
            Form2.Show()


        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Me.TextBox1.Visible = True
        Me.TextBox2.Visible = True
        Me.Label1.Visible = True
        Me.Label2.Visible = True
        Me.Button1.Visible = True
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Form4.Show()
    End Sub
End Class



