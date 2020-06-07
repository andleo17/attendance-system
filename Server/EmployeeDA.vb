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

    Public Shared Function Search(EmployeeCardId As String) As Employee
        Try
            Dim DB = New DBAttendanceEntities
            Return DB.Employee.Find(EmployeeCardId)
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
        Try
            Dim DB = DBContextDA.GetContext(Employee)
            DB.SaveChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub Down(Employee As Employee)
        Using DB = New DBAttendanceEntities()
            Try
                Employee = DB.Employee.Find(Employee.CardId)
                Dim EContract = From C In Employee.Contract Where C.State = True
                Dim ESchedule = From S In Employee.Schedule Where S.State = True
                Dim EUser = From U In Employee.User Where U.State = True

                If EContract.Count = 1 Then
                    EContract.Single.State = False
                End If
                If ESchedule.Count = 1 Then
                    ESchedule.Single.State = False
                End If
                If EUser.Count = 1 Then
                    EUser.Single.State = False
                End If
                Employee.State = False
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub
End Class

