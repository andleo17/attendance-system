Imports Data

Public MustInherit Class UserDA

    Public Shared Function Login(name As String, password As String) As User
        Dim DB = New DBAttendanceEntities()
        Dim user = From u In DB.User Where u.Name Is name And u.Password Is password Select u
        Try
            Return user.Single()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function List() As List(Of User)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Users = From U In DB.User Select U
            Return Users.ToList()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub Save(User As User)
        Using DB = New DBAttendanceEntities()
            Try
                DB.User.Add(User)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Update(User As User)
        Using DB = New DBAttendanceEntities()
            Try
                Dim OldUser = DB.User.Find(User.Id)
                DB.Entry(OldUser).CurrentValues.SetValues(User)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Delete(User As User)
        Using DB = New DBAttendanceEntities()
            Try
                User = DB.User.Find(User.Id)
                DB.User.Remove(User)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub
End Class
