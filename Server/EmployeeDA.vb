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

    Public Shared Sub Delete(Employee As Employee)
        Using DB = New DBAttendanceEntities()
            Try
                Employee = DB.Employee.Find(Employee.CardId)
                DB.Employee.Remove(Employee)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub
End Class
