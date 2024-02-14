Public Class Login

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginBtn.Click
        If UnameTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Enter username and password")
        ElseIf UnameTb.Text = "Admin" And PasswordTb.Text = "Password" Then

            Me.Hide()
            Dim obj = New Patients
            obj.Show()

        Else
            MsgBox("wrong username and password")
        End If
    End Sub

    Private Sub ResetBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetBtn.Click
        UnameTb.Text = ""
        PasswordTb.Text = ""
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then

            PasswordTb.UseSystemPasswordChar = False

        Else

            PasswordTb.UseSystemPasswordChar = True

        End If

    End Sub
End Class