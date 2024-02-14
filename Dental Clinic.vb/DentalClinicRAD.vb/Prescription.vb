Imports System.Data.SqlClient

Public Class Prescription
    Dim Con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DClinic.mdf;Integrated Security=True;User Instance=True")
    Dim cmd As SqlCommand
    Private Sub FillPatients()
        Con.Open()
        Dim sql = "Select * from AppoinmentTbl"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        PatCb.DataSource = Tbl
        PatCb.DisplayMember = "ApPat"
        PatCb.ValueMember = "ApPat"
        Con.Close()
    End Sub
    Private Sub reset()
        PatCb.SelectedIndex = -1
        TreatTxt.Text = ""
        QtyTxt.Text = ""
        medTxt.Text = ""
        CostTxt.Text = ""
    End Sub
    Private Sub Populate()
        Con.Open()
        Dim query = "Select * from PrescriptionTbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim bulider As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        PressDGV.DataSource = ds.Tables(0)
        Con.Close()

    End Sub
    Private Sub Filter()
        If search.Text = "" Then
            MsgBox("Enter the patient name")
        Else
            Con.Open()
            Dim query = "Select * from PrescriptionTbl where PatName='" & search.Text & "'"
            Dim adapter As SqlDataAdapter
            adapter = New SqlDataAdapter(query, Con)
            Dim bulider As New SqlCommandBuilder(adapter)
            Dim ds As DataSet
            ds = New DataSet
            adapter.Fill(ds)
            PressDGV.DataSource = ds.Tables(0)
            Con.Close()
        End If
        
    End Sub
    Private Sub SaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveBtn.Click
        If TreatTxt.Text = "" Or QtyTxt.Text = "" Or medTxt.Text = "" Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = "Insert into PrescriptionTbl values ('" + PatCb.SelectedValue + "','" + TreatTxt.Text + "','" + CostTxt.Text + "','" + medTxt.Text + "','" + QtyTxt.Text + "') "
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Prescription saved Sucessfully")
            Con.Close()
            Populate()
            reset()
        End If
    End Sub

    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label20.Click
        Me.Hide()
        Dim obj = New Patients
        obj.Show()
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

    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label16.Click
        Me.Hide()
        Dim obj = New Dashboard
        obj.Show()
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click
        End
    End Sub

    Private Sub Prescription_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillPatients()
        Populate()
    End Sub
    Private Sub fetchData()
        Con.Open()
        Dim Query = "Select * from AppoinmentTbl where ApPat='" + PatCb.SelectedValue.ToString() + "' "
        Dim cmd As New SqlCommand(Query, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            TreatTxt.Text = reader(2).ToString
        End While
        Con.Close()

    End Sub
    Private Sub fetchCost()
        Con.Open()
        Dim Query = "Select * from TreatmentsTbl where TrName='" + TreatTxt.Text + "' "
        Dim cmd As New SqlCommand(Query, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            CostTxt.Text = reader(2).ToString
        End While
        Con.Close()

    End Sub

    Private Sub PatCb_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatCb.SelectionChangeCommitted
        fetchData()
        fetchCost()
    End Sub
    Dim key = 0

    Private Sub DeleteBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = " Delete from PrescriptionTbl where PId = " & key & " "
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Prescription Deleted Sucessfully")
            Con.Close()
            Populate()
            reset()
        End If
    End Sub


    Private Sub TreaPressDGV_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles PressDGV.CellMouseClick
        Dim row As DataGridViewRow = PressDGV.Rows(e.RowIndex)
        PatCb.SelectedValue = row.Cells(1).Value.ToString
        TreatTxt.Text = row.Cells(2).Value.ToString
        CostTxt.Text = row.Cells(3).Value.ToString
        medTxt.Text = row.Cells(4).Value.ToString
        QtyTxt.Text = row.Cells(5).Value.ToString
        If PatCb.SelectedIndex = -1 Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub EditBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditBtn.Click
        If TreatTxt.Text = "" Or QtyTxt.Text = "" Or medTxt.Text = "" Then
            MsgBox("Missing Informations")
        Else
            Con.Open()
            Dim query = "Update PrescriptionTbl set PatName='" + PatCb.SelectedValue.ToString() + "', TreatName='" + TreatTxt.Text + "', Medicines='" + medTxt.Text + "', Qty='" + QtyTxt.Text + "', Costxt='" + CostTxt.Text + "' where PId=" + key + ""
            Dim cmd As New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Prescription update Sucessfully")
            Con.Close()
            Populate()
            reset()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Filter()
    End Sub
End Class