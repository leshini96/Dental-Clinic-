Imports System.Data.SqlClient

Public Class Treatments

    Dim Con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DClinic.mdf;Integrated Security=True;User Instance=True")
    Dim cmd As SqlCommand

    Private Sub Populate()
        Con.Close()
        Dim query = "Select * from TreatmentsTbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim bulider As New SqlCommandBuilder(adapter)
        Dim DS As DataSet
        DS = New DataSet
        adapter.Fill(DS)
        TreatmentDGV.DataSource = DS.Tables(0)
        Con.Close()

    End Sub
    Private Sub Reset()
        TrNameTb.Text = ""
        TrCostTb.Text = ""
        TrDisTb.Text = ""
        key = 0
    End Sub
    Private Sub SaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveBtn.Click
        If TrNameTb.Text = "" Or TrCostTb.Text = "" Or TrDisTb.Text = "" Then
            MsgBox("Missing Informations")
        Else
            Try
                Con.Open()
                Dim query = "Insert into TreatmentsTbl values ('" + TrNameTb.Text + "','" + TrCostTb.Text + "','" + TrDisTb.Text + "') "
                Dim cmd As New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Treatment saved Sucessfully")
                Con.Close()
                Populate()
                Reset()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Private Sub Treatments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Populate()
    End Sub
    Dim key = 0
    Private Sub TreatmentDGV_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles TreatmentDGV.CellMouseClick
        Dim row As DataGridViewRow = TreatmentDGV.Rows(e.RowIndex)
        TrNameTb.Text = row.Cells(1).Value.ToString
        TrCostTb.Text = row.Cells(2).Value.ToString
        TrDisTb.Text = row.Cells(3).Value.ToString

        If TrNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub DeleteBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = " delete from TreatmentsTbl where TrId = " & key & " "
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Treatment deleted Sucessfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Private Sub EditBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditBtn.Click
        If TrNameTb.Text = "" Or TrCostTb.Text = "" Or TrDisTb.Text = "" Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = "Upadate TreatmentsTbl set TrName='" & TrNameTb.Text & "', Trcost='" & TrCostTb.Text & "', TrDesc='" & TrDisTb.Text & "' where TrId= " & key & ""
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Treatment updated Sucessfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label14.Click

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click

    End Sub

    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label20.Click
        Me.Hide()
        Dim obj = New Patients
        obj.Show()
    End Sub

    Private Sub Label19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label19.Click
        Me.Hide()
        Dim obj = New Appoinments
        obj.Show()
    End Sub

    Private Sub Label17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label17.Click
        Me.Hide()
        Dim obj = New Prescription
        obj.Show()
    End Sub

    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label16.Click
        Me.Hide()
        Dim obj = New Dashboard
        obj.Show()
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click
        End
    End Sub
End Class