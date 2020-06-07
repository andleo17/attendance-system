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

End Class
