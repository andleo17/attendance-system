Imports Data

Public MustInherit Class UserDA

    Public Shared Function Login(name As String, password As String) As User
        Dim DB = New DBAttendanceEntities()
        Dim user = From u In DB.User Where (u.Name Is name And u.Password Is password) And u.State Select u
        Try
            Return user.Single()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function List() As List(Of User)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Users = From U In DB.User Where U.State = True Select U
            Return Users.ToList()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function List(EmployeeCardId As String) As List(Of User)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Users = From U In DB.User Where U.EmployeeCardId Is EmployeeCardId Order By U.State Select U
            Return Users.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub Save(User As User)
        Using DB = New DBAttendanceEntities()
            Try
                Dim OldUser = From U In DB.User Where U.EmployeeCardId Is User.EmployeeCardId And U.State = True
                If OldUser.Count = 1 Then
                    OldUser.Single().State = False
                End If
                DB.User.Add(User)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Down(User As User)
        Using DB = New DBAttendanceEntities()
            Try
                User = DB.User.Find(User.Id)
                User.State = False
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub
End Class
