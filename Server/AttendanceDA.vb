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

    Public Shared Function SearchDate(InitialDate As Date) As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Attendance = From A In DB.Attendance.Where(Function(A) A.Date = InitialDate) Select A
            Return Attendance.ToList()
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Shared Function SearchDate(InitialDate As Date, CardId As String) As List(Of Attendance)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Attendance = From A In DB.Attendance.Where(Function(A) A.Date = InitialDate And A.EmployeeCardId = CardId) Select A
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
