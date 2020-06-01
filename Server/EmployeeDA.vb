Imports Data

Public Class EmployeeDA
    Public Shared Function List() As List(Of Employee)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Employees = From E In DB.Employee Select E
            Return Employees.ToList()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Search(Employee As Employee) As Employee
        Try
            Dim DB = New DBAttendanceEntities()
            Return DB.Employee.Find(Employee.CardId)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub Save(Employee As Employee)
        Using DB = New DBAttendanceEntities()
            Try
                DB.Employee.Add(Employee)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Update(Employee As Employee)
        Using DB = New DBAttendanceEntities()
            Try
                Dim OldEmployee = DB.Employee.Find(Employee.CardId)
                DB.Entry(OldEmployee).CurrentValues.SetValues(Employee)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Down(Employee As Employee)
        Using DB = New DBAttendanceEntities()
            Try
                Employee = DB.Employee.Find(Employee.CardId)
                Employee.State = False
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Delete(Employee As Employee)
        Using DB = New DBAttendanceEntities()
            Try
                Employee = DB.Employee.Find(Employee.CardId)
                Dim AttendancesList = From A In DB.Attendance Where A.Employee.CardId Is Employee.CardId Select A
                If AttendancesList.Count > 0 Then
                    Employee.State = False
                Else
                    DB.Employee.Remove(Employee)
                End If
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub
End Class
