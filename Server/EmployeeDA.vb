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
                Dim EContract = From C In Employee.Contract Where C.State
                Dim ESchedule = From S In Employee.Schedule Where S.State
                Dim EUser = From U In Employee.User Where U.State

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

    Public Shared Sub Delete(Employee As Employee)
        Using DB = New DBAttendanceEntities()
            Try
                Employee = DB.Employee.Find(Employee.CardId)
                For Each A In Employee.Attendance.ToList
                    For Each J In A.Justification
                        DB.Justification.Remove(J)
                    Next
                    DB.Attendance.Remove(A)
                Next
                For Each C In Employee.Contract.ToList
                    DB.Contract.Remove(C)
                Next
                For Each L In Employee.License.ToList
                    DB.License.Remove(L)
                Next
                For Each P In Employee.Permission.ToList
                    DB.Permission.Remove(P)
                Next
                For Each S In Employee.Schedule.ToList
                    For Each SD In S.ScheduleDetail.ToList
                        DB.ScheduleDetail.Remove(SD)
                    Next
                    DB.Schedule.Remove(S)
                Next
                For Each U In Employee.User.ToList
                    DB.User.Remove(U)
                Next
                DB.Employee.Remove(Employee)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub
End Class
