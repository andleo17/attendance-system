Imports Data

Public Class PermissionDA

    Public Shared Function List() As List(Of Permission)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Permissions = From P In DB.Permission Select P
            Return Permissions.ToList()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Search(Permission As Permission) As Permission
        Try
            Dim DB = New DBAttendanceEntities()
            Return DB.Permission.Find(Permission.Id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Search(Employee As Employee) As List(Of Permission)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim Permissions = From P In DB.Permission.Where(Function(PE) PE.EmployeeCardId.Equals(Employee.CardId)) Select P
            Return Permissions.ToList()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub Save(Permission As Permission)
        Using DB = New DBAttendanceEntities()
            Try
                DB.Permission.Add(Permission)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Update(Permission As Permission)
        Using DB = New DBAttendanceEntities()
            Try
                Dim OldPermission = DB.Permission.Find(Permission.Id)
                DB.Entry(OldPermission).CurrentValues.SetValues(Permission)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Delete(Permission As Permission)
        Using DB = New DBAttendanceEntities()
            Try
                Permission = DB.Permission.Find(Permission.Id)
                DB.Permission.Remove(Permission)
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

End Class
