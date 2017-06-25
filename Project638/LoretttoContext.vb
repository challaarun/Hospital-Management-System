Imports MySql.Data.MySqlClient
Public Class LorettoContext

    Private _connection As MySqlConnection

    Public Sub New()
        _connection = New MySqlConnection("Server=141.209.241.44;Database=loretto;Uid=chall1a;Pwd=challa;")
        _connection.Open()
    End Sub
    Public Function GetEmployees() As List(Of Employee)
        Dim employees As New List(Of Employee)

        Dim command As New MySqlCommand("Select * From employee order by EmployeeID;", _connection)
        Dim reader = command.ExecuteReader()

        While (reader.Read())

            Dim employee As New Employee


            Dim empIndex = reader.GetOrdinal("EmployeeID")
            If (Not reader.IsDBNull(empIndex)) Then
                employee.EmployeeId = reader.GetInt32("EmployeeID")
            End If

            employee.FirstName = reader.GetString("FirstName")
            employee.LastName = reader.GetString("LastName")

            employee.Address = reader.GetString("Address")
            employee.City = reader.GetString("City")

            Dim stateIndex = reader.GetOrdinal("State")
            If (Not reader.IsDBNull(stateIndex)) Then
                employee.State = reader.GetString("State")
            End If
            employee.Country = reader.GetString("Country")
            employee.Phone = reader.GetString("Phone")
            employee.BirthDate = reader.GetString("BirthDate")
            employee.Supervisor = reader.GetString("Supervisor")
            employee.DateHired = reader.GetString("DateHired")
            employee.Designation = reader.GetString("Designation")




            employees.Add(employee)
        End While

        reader.Close()
        reader.Dispose()
        command.Dispose()

        Return employees
    End Function

    Public Function GetEmployee(ByVal EmployeeID As Integer) As Employee
        Dim Employee As New Employee()

        Dim command As New MySqlCommand("SELECT * FROM Employee where EmployeeID = @EmployeeID; ", _connection)
        command.Parameters.AddWithValue("@EmployeeID", EmployeeID)
        Dim reader = command.ExecuteReader()

        If (reader.Read()) Then

            Dim empIndex = reader.GetOrdinal("EmployeeID")
            If (Not reader.IsDBNull(empIndex)) Then
                Employee.EmployeeId = reader.GetInt32("EmployeeID")
            End If

            Employee.FirstName = reader.GetString("FirstName")
            Employee.LastName = reader.GetString("LastName")

            Employee.Address = reader.GetString("Address")
            Employee.City = reader.GetString("City")

            Dim stateIndex = reader.GetOrdinal("State")
            If (Not reader.IsDBNull(stateIndex)) Then
                Employee.State = reader.GetString("State")
            End If
            Employee.Country = reader.GetString("Country")
            Employee.Phone = reader.GetString("Phone")
            Employee.BirthDate = reader.GetString("BirthDate")
            Employee.Supervisor = reader.GetString("Supervisor")
            Employee.DateHired = reader.GetString("DateHired")
            Employee.Designation = reader.GetString("Designation")
        End If

        reader.Close()
        reader.Dispose()
        command.Dispose()

        Return Employee
    End Function
    Public Sub SaveEmployee(ByVal employee As Employee)
        'TODO allow for edit functionality

        If (employee.EmployeeId = 0) Then
            Dim insert = "INSERT INTO Employee (EmployeeID, FirstName, LastName, Address, City, State, Country, Phone, BirthDate, Supervisor, DateHired, Designation) VALUES(@EmployeeID, @FirstName, @LastName, @Address, @City, @State, @Country, @Phone, @BirthDate, @Supervisor, @DateHired, @Designation)"
            Dim command As New MySqlCommand(insert, _connection)
            command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeId)
            command.Parameters.AddWithValue("@FirstName", employee.FirstName)
            command.Parameters.AddWithValue("@LastName", employee.LastName)
            command.Parameters.AddWithValue("@Address", employee.Address)
            command.Parameters.AddWithValue("@City", employee.City)
            command.Parameters.AddWithValue("@State", employee.State)
            command.Parameters.AddWithValue("@Country", employee.Country)
            command.Parameters.AddWithValue("@Phone", employee.Phone)
            command.Parameters.AddWithValue("@BirthDate", employee.BirthDate)
            command.Parameters.AddWithValue("@Supervisor", employee.Supervisor)
            command.Parameters.AddWithValue("@DateHired", employee.DateHired)
            command.Parameters.AddWithValue("@Designation", employee.Designation)
            command.ExecuteNonQuery()

            command.Dispose()
        Else
            Dim update = "UPDATE Employee set FirstName = @FirstName, LastName = @LastName, Address = @Address, City = @city, State = @state, Country = @Country, Phone = @Phone, BirthDate = @BirthDate, Supervisor = @Supervisor, DateHired = @DateHired, Designation = @Designation WHERE EmployeeID = @EmployeeID; "
            Dim command As New MySqlCommand(update, _connection)
            command.Parameters.AddWithValue("@FirstName", employee.FirstName)
            command.Parameters.AddWithValue("@LastName", employee.LastName)
            command.Parameters.AddWithValue("@Address", employee.Address)
            command.Parameters.AddWithValue("@City", employee.City)
            command.Parameters.AddWithValue("@State", employee.State)
            command.Parameters.AddWithValue("@Country", employee.Country)
            command.Parameters.AddWithValue("@Phone", employee.Phone)
            command.Parameters.AddWithValue("@BirthDate", employee.BirthDate)
            command.Parameters.AddWithValue("@Supervisor", employee.Supervisor)
            command.Parameters.AddWithValue("@DateHired", employee.DateHired)
            command.Parameters.AddWithValue("@Designation", employee.Designation)
            command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeId)
            command.ExecuteNonQuery()
            command.Dispose()
        End If

    End Sub
    Public Sub DeleteEmployee(ByVal employeeId As Integer)
        Dim command As New MySqlCommand("DELETE FROM employee WHERE employeeID = @EmployeeID;", _connection)
        command.Parameters.AddWithValue("@EmployeeID", employeeId)
        command.ExecuteNonQuery()
        command.Dispose()
    End Sub

End Class
