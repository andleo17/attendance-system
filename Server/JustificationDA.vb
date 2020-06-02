Imports Data

Public Class JustificationDA
    Public Shared Function List() As List(Of Justification)
        Try
            Dim DB = New DBAttendanceEntities
            Dim Justifications = From J In DB.Justification Select J
            Return Justifications.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function List(EmployeeCardId As String) As List(Of Justification)
        Try
            Dim DB = New DBAttendanceEntities
            Dim Justifications = From J In DB.Justification Where J.Attendance.EmployeeCardId = EmployeeCardId Select J
            Return Justifications.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub Save(Justification As Justification)
        Using DB = New DBAttendanceEntities
            Try
                DB.Justification.Add(Justification)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub
End Class
