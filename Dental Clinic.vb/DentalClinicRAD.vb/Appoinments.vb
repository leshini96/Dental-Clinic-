Imports System.Data.SqlClient

Public Class Appoinments

    Dim Con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DClinic.mdf;Integrated Security=True;User Instance=True")
    Dim cmd As SqlCommand
    Private Sub FillPatients()
        Con.Open()
        Dim sql = "Select * from PatientTbl"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        PatCb.DataSource = Tbl
        PatCb.DisplayMember = "PatName"
        PatCb.ValueMember = "PatName"
        Con.Close()
    End Sub
    Private Sub FillTreatments()
        Con.Open()
        Dim sql = "Select * from TreatmentsTbl"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        TreatCb.DataSource = Tbl
        TreatCb.DisplayMember = "TrName"
        TreatCb.ValueMember = "TrName"
        Con.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label25.Click
        Me.Hide()
        Dim obj = New Patients
        obj.Show()
    End Sub

    Private Sub Label23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label23.Click
        Me.Hide()
        Dim obj = New Treatments
        obj.Show()
    End Sub

    Private Sub Label22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label22.Click
        Me.Hide()
        Dim obj = New Prescription
        obj.Show()
    End Sub

    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label21.Click
        Me.Hide()
        Dim obj = New Dashboard
        obj.Show()
    End Sub

    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label20.Click
        End
    End Sub
    Private Sub Populate()
        Con.Close()
        Dim query = "Select * from AppoinmentTbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim bulider As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        AppoinmentDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub reset()
        PatCb.SelectedIndex = -1
        TreatCb.SelectedIndex = -1
    End Sub
    Private Sub SaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveBtn.Click
        If PatCb.SelectedIndex = -1 Or TreatCb.SelectedIndex = -1 Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = "Insert into AppoinmentTbl values ('" + PatCb.SelectedValue.ToString() + "','" + TreatCb.SelectedValue.ToString() + "','" + ApDate.Value.Date + "','" + ApTime.Text + "') "
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Appoinment saved Sucessfully")
            Con.Close()
            Populate()
            reset()
        End If
    End Sub

    Private Sub Appoinments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillPatients()
        FillTreatments()
        Populate()
    End Sub

    Private Sub DeleteBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = " Delete from AppoinmentTbl where ApId = " & key & " "
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Appoinment Deleted Sucessfully")
            Con.Close()
            Populate()
            reset()
        End If
    End Sub
    Dim key = 0
    Private Sub Appoinment_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles AppoinmentDGV.CellMouseClick
        Dim row As DataGridViewRow = AppoinmentDGV.Rows(e.RowIndex)
        PatCb.SelectedValue = row.Cells(1).Value.ToString
        TreatCb.SelectedValue = row.Cells(2).Value.ToString
        ApDate.Text = row.Cells(3).Value.ToString
        ApTime.Text = row.Cells(4).Value.ToString

        If PatCb.SelectedIndex = -1 Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub EditBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditBtn.Click
        If PatCb.SelectedIndex = -1 Or TreatCb.SelectedIndex = -1 Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = "Upadate AppoinmentTbl set ApPat='" & PatCb.SelectedValue.ToString() & "', ApTreat='" & TreatCb.SelectedValue.ToString() & "', ApDate='" & ApDate.Value.Date & "' "
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Appoinment Updated Sucessfully")
            Con.Close()
            Populate()
            reset()
        End If
    End Sub
End Class