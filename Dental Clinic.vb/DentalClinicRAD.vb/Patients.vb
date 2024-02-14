Imports System.Data.SqlClient

Public Class Patients

    Dim Con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DClinic.mdf;Integrated Security=True;User Instance=True")
    Dim cmd As SqlCommand
        Private Sub reset()
        PatNameTb.Text = ""
        PatAddTb.Text = ""
        PatPhoneTb.Text = ""
        AllTb.Text = ""

    End Sub
    Private Sub SaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveBtn.Click
        If PatNameTb.Text = "" Or PatAddTb.Text = "" Or PatPhoneTb.Text = "" Or AllTb.Text = "" Or GenCb.SelectedIndex = -1 Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = "Insert into PatientTbl values ('" + PatNameTb.Text + "','" + PatPhoneTb.Text + "','" + PatAddTb.Text + "','" + DOBDate.Value.Date + "','" + GenCb.SelectedItem.ToString() + "','" + AllTb.Text + "') "
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Patient saved Sucessfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub
    Private Sub Populate()
        Con.Close()
        Dim query = "Select * from PatientTbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim bulider As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        PatDGV.DataSource = ds.Tables(0)
        Con.Close()

    End Sub

    Private Sub EditBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditBtn.Click
        If PatNameTb.Text = "" Or PatAddTb.Text = "" Or PatPhoneTb.Text = "" Or AllTb.Text = "" Or GenCb.SelectedIndex = -1 Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = "Upadate PatientTbl set PatName ='" & PatNameTb.Text & "', PatPhone ='" & PatPhoneTb.Text & "', PatAdd ='" & PatAddTb.Text & "', PatDOB ='" & DOBDate.Value.Date & "', PatGen ='" & GenCb.SelectedItem.ToString & "',  PatAll ='" & AllTb.Text & "' where PatId =" & key & ""
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Patient Updated Sucessfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub


    Private Sub DeleteBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = " Delete from PatientTbl where PatId = " & key & " "
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Patient Deleted Sucessfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub

    Dim key = 0




    Private Sub Patients_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Populate()
    End Sub

    Private Sub PatDGV_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles PatDGV.CellMouseClick
        REM   Dim row As DataGridViewRow = PatDGV.Rows(e.RowIndex)
        REM   PatNameTb.Text = row.Cells(1).Value.ToString
        ' PatPhoneTb.Text = row.Cells(2).Value.ToString
        ' PatAddTb.Text = row.Cells(3).Value.ToString
        ' DOBDate.Text = row.Cells(4).Value.ToString
        '  GenCb.SelectedItem = row.Cells(5).Value.ToString
        ' AllTb.Text = row.Cells(6).Value.ToString

        '  If PatNameTb.Text = "" Then
        ' key = 0
        ' Else
        ' key = Convert.ToInt32(row.Cells(0).Value.ToString)
        ' End If
    End Sub


    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label18.Click
        Me.Hide()
        Dim obj = New Treatments
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

    Private Sub PatDGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles PatDGV.CellContentClick
        Dim row As DataGridViewRow = PatDGV.Rows(e.RowIndex)
        PatNameTb.Text = row.Cells(1).Value.ToString
        PatPhoneTb.Text = row.Cells(2).Value.ToString
        PatAddTb.Text = row.Cells(3).Value.ToString
        DOBDate.Text = row.Cells(4).Value.ToString
        GenCb.SelectedItem = row.Cells(5).Value.ToString
        AllTb.Text = row.Cells(6).Value.ToString

        If PatNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub
End Class