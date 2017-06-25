Public Class EmployeeHomeForm



    Private _loretto As LorettoContext

    Public Sub New(ByVal loretto As LorettoContext)

        ' This call is required by the designer.
        InitializeComponent()


        _loretto = loretto

        RefreshGrid()

    End Sub

    Public Sub RefreshGrid()
        DataGridEmployee.DataSource = _loretto.GetEmployees()
    End Sub






    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub EmployeeHomeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'LorettoDataSet.employee' table. You can move, or remove it, as needed.
        Me.EmployeeTableAdapter.Fill(Me.LorettoDataSet.employee)

    End Sub

    Private Sub AddNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewToolStripMenuItem.Click
        Dim EditForm As New EmployeeEditForm(Me, _loretto, Nothing)
        EditForm.Show()
    End Sub

    Private Sub EditSelectedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditSelectedToolStripMenuItem.Click
        If (DataGridEmployee.SelectedRows.Count() = 1) Then
            Dim employeeId = CType(DataGridEmployee.SelectedRows.Item(0).Cells.Item(0).Value, Integer)

            Dim editForm As New EmployeeEditForm(Me, _loretto, employeeId)
            editForm.Show()
        Else
            MessageBox.Show(Me, "Please select a customer to edit.", "No Customer Selected")
        End If
    End Sub

    Private Sub DeleteSelectedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteSelectedToolStripMenuItem.Click
        If (DataGridEmployee.SelectedRows.Count() = 1) Then
            Dim selectedRow = DataGridEmployee.SelectedRows.Item(0)
            Dim selectedName = CType(selectedRow.Cells.Item(1).Value, String)
            Dim selectedId = CType(selectedRow.Cells.Item(0).Value, Integer)

            Dim result = MessageBox.Show(Me, "Are you sure you want to delete " & selectedName & "(" & selectedId.ToString() & ")?", "Confirm Delete", MessageBoxButtons.YesNo)

            If (result = DialogResult.Yes) Then
                _loretto.DeleteEmployee(selectedId)
                RefreshGrid()
            End If
        Else
            MessageBox.Show(Me, "Please select an employee to delete.", "No Employee Selected")
        End If
    End Sub
End Class