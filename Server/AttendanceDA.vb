Imports Server
Imports Data

Public Class AttendanceDA

    Public Shared Function SearchDate(InitialDate As Date?, FinalDate As Date?) As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Attendance = From A In DB.Attendance.Where(Function(A) A.Date > InitialDate And A.Date < FinalDate) Select A
            Return Attendance.ToList()
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Shared Function SearchDate(CardId As String) As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Attendance = From A In DB.Attendance.Where(Function(A) A.EmployeeCardId = CardId) Select A
            Return Attendance.ToList()
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Shared Function SearchDate(InitialDate As Date?) As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Attendance = From A In DB.Attendance.Where(Function(A) A.Date = InitialDate) Select A
            Return Attendance.ToList()
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Shared Function SearchDate(InitialDate As Date?, CardId As String) As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Attendance = From A In DB.Attendance.Where(Function(A) A.Date = InitialDate And A.EmployeeCardId = CardId) Select A
            Return Attendance.ToList()
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Shared Function SearchAttendanceDate(InitialDate As Date, CardId As String) As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Attendance = DB.Database.SqlQuery(Of Attendance)("select *
                from Attendance inner join Schedule on Attendance.EmployeeCardId = Schedule.EmployeeCardId 
                inner join ScheduleDetail on Schedule.Id=ScheduleDetail.ScheduleId 
                left join Justification on Attendance.Id=Justification.AttendanceId
                where Attendance.InHour>ScheduleDetail.InHour and  DATEPART(dw,Attendance.Date)-1= ScheduleDetail.Day
                and Justification.Id is null and Attendance.EmployeeCardId=@p0 and Attendance.Date=@p1", CardId, InitialDate) 'DB.Attendance.Where(Function(A) A.Date = InitialDate And A.EmployeeCardId = CardId)
            Return Attendance.ToList()
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Shared Function SearchDate(InitialDate As Date?, FinalDate As Date?, CardId As String) As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Attendance = From A In DB.Attendance.Where(Function(A) A.Date > InitialDate And A.Date < FinalDate And A.EmployeeCardId = CardId) Select A
            Return Attendance.ToList()
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Shared Function List() As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Return DB.Attendance.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function List(EmployeeCardId As String) As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Attendance = From A In DB.Attendance Where A.EmployeeCardId Is EmployeeCardId
            Return Attendance.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function List(PDate As Date) As List(Of ScheduleDetail)
        Try
            Dim DB = New DBAttendanceEntities
            Dim ScheduleDate = From S In DB.Schedule Join SD In DB.ScheduleDetail On S Equals SD.Schedule
                               Where S.State AndAlso S.StartDate <= PDate And PDate <= S.FinishDate AndAlso SD.Day = PDate.DayOfWeek Select SD
            Return ScheduleDate.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Search(Employee As Employee) As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Attendance = From A In DB.Attendance.Where(Function(PE) PE.EmployeeCardId.Equals(Employee.CardId)) Select A
            Return Attendance.ToList()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub Save(Attendance As Attendance)
        Using DB = New DBAttendanceEntities()
            Try
                DB.Attendance.Add(Attendance)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using

    End Sub

    Public Shared Sub Update(Attendance As Attendance)
        Using DB = New DBAttendanceEntities()
            Try
                Dim oldAttendance = DB.Attendance.Find(Attendance.Id)
                DB.Entry(oldAttendance).CurrentValues.SetValues(Attendance)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Delete(Attendance As Attendance)
        Using DB = New DBAttendanceEntities()
            Try
                Attendance = DB.Attendance.Find(Attendance.Id)
                DB.Attendance.Remove(Attendance)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

End Class
