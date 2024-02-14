Imports System.Data.SqlClient

Public Class Dashboard
    Dim Con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DClinic.mdf;Integrated Security=True;User Instance=True")
    Private Sub countpat()
        Con.Open()
        Dim cmd As SqlCommand
        cmd = New SqlCommand("select count(*)from PatientTbl", Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dt As DataTable
        dt = New DataTable()
        adapter.Fill(dt)
        Pamount.Text = dt.Rows(0)(0).ToString
        Con.Close()

    End Sub
    Private Sub countTreat()
        Con.Open()
        Dim cmd As SqlCommand
        cmd = New SqlCommand("select count(*)from TreatmentsTbl", Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dt As DataTable
        dt = New DataTable()
        adapter.Fill(dt)
        Tamount.Text = dt.Rows(0)(0).ToString
        Con.Close()

    End Sub
    Private Sub countpres()
        Con.Open()
        Dim cmd As SqlCommand
        cmd = New SqlCommand("select count(*)from PrescriptionTbl", Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim dt As DataTable
        dt = New DataTable()
        adapter.Fill(dt)
        Presamount.Text = dt.Rows(0)(0).ToString
        Con.Close()

    End Sub
    Private Sub Dashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        countpat()
        countTreat()
        countpres()
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

    Private Sub Label24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label24.Click
        Me.Hide()
        Dim obj = New Appoinments
        obj.Show()
    End Sub

    Private Sub Label22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label22.Click
        Me.Hide()
        Dim obj = New Prescription
        obj.Show()
    End Sub

    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label20.Click
        End
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class