Imports Data

Public Class UserDA
    Private ReadOnly DB As DBAttendanceEntities = New DBAttendanceEntities()

    Public Function Login(name As String, password As String) As User
        Dim user = From u In DB.User Where u.Name Is name And u.Password Is password Select u
        Try
        Return user.Single()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
