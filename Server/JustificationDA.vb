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

    'Public Shared Sub Save(Justification As Justification)
    '    Using DB = New DBAttendanceEntities
    '        Try
    '            Dim EmployeeSchedule = From SD In DB.ScheduleDetail Where SD.Schedule.EmployeeCardId Is Justification.Attendance.EmployeeCardId Select SD.InHour, SD.Day
    '            For Each ES In EmployeeSchedule
    '                If ES.Day = Justification.Attendance.Date.DayOfWeek Then
    '                    If ES.InHour <= Justification.Attendance.InHour Then
    '                        DB.Justification.Add(Justification)
    '                        DB.SaveChanges()
    '                        Return
    '                    End If
    '                End If
    '            Next
    '            Throw New Exception("Justificación en día no valido.")
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Using
    'End Sub

    Public Shared Sub Save(Justification As Justification)
        Using DB = New DBAttendanceEntities()
            Try
                DB.Justification.Add(Justification)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Delete(Justification As Justification)
        Using DB = New DBAttendanceEntities()
            Try
                Justification = DB.Justification.Find(Justification.Id)
                DB.Justification.Remove(Justification)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Function Search(Justification As Justification) As Justification
        Try
            Dim DB = New DBAttendanceEntities()
            Return DB.Justification.Find(Justification.Id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub Update(Justification As Justification)
        Using DB = New DBAttendanceEntities()
            Try
                Dim OldJustification = DB.Justification.Find(Justification.Id)
                DB.Entry(OldJustification).CurrentValues.SetValues(Justification)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub
End Class
