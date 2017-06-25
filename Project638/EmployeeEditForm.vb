Public Class EmployeeEditForm
    Private Property _loretto As LorettoContext
    Private Property _home As EmployeeHomeForm

    Public Sub New(ByVal home As EmployeeHomeForm, ByVal loretto As LorettoContext, ByVal EmployeeID As Nullable(Of Integer))


        InitializeComponent()

        _loretto = loretto
        _home = home

        If (EmployeeID.HasValue) Then
            Dim emp = _loretto.GetEmployee(EmployeeID.Value)

            TextBox_EmployeeID.Text = emp.EmployeeId
            TextBox_FirstName.Text = emp.FirstName
            TextBox_LastName.Text = emp.LastName
            TextBox_Address.Text = emp.Address
            TextBox_City.Text = emp.City
            TextBox_State.Text = emp.State
            TextBox_Country.Text = emp.Country
            TextBox_Phone.Text = emp.Phone
            TextBox_BirthDate.Text = emp.BirthDate
            TextBox_Supervisor.Text = emp.Supervisor
            TextBox_DateHired.Text = emp.DateHired
            TextBox_Designation.Text = emp.Designation


            Text = "Edit " & emp.FirstName
        Else
            Text = "Add New Customer"
        End If

    End Sub




    Private Sub Button_Save_Click(sender As Object, e As EventArgs) Handles Button_Save.Click
        Dim emp As New Employee

        Dim empIdText = TextBox_EmployeeID.Text
        If (String.IsNullOrWhiteSpace(empIdText)) Then
            emp.EmployeeID = 0
        Else
            emp.EmployeeId = Integer.Parse(empIdText)
        End If

        emp.FirstName = TextBox_FirstName.Text
        emp.LastName = TextBox_LastName.Text
        emp.Address = TextBox_Address.Text
        emp.City = TextBox_City.Text
        emp.State = TextBox_State.Text
        emp.Country = TextBox_Country.Text
        emp.Phone = TextBox_Phone.Text
        emp.BirthDate = TextBox_BirthDate.Text
        emp.Supervisor = TextBox_Supervisor.Text
        emp.DateHired = TextBox_DateHired.Text
        emp.Designation = TextBox_Designation.Text

        If (emp.FirstName = "") Then
            MessageBox.Show(Me, "Employee Name is required", "Validation Error")
        ElseIf (emp.Address = "") Then
            MessageBox.Show(Me, "Employee address is required", "Validation Error")
        Else
            _loretto.SaveEmployee(emp)
            _home.RefreshGrid()
            Close()
        End If
    End Sub

    Private Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        Close()
    End Sub
End Class