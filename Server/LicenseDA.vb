﻿Imports Data
Public Class LicenseDA
    Public Shared Function List() As List(Of License)
        Using DB = New DBAttendanceEntities
            Try
                Dim Lista = DB.License
                Return Lista.ToList
            Catch ex As Exception
                Throw ex
            End Try
        End Using

    End Function


    Public Shared Function Search(License As License) As License
        Try
            Dim DB = New DBAttendanceEntities()
            Return DB.License.Find(License.Id)
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Sub Save(License As License)
        Using DB = New DBAttendanceEntities()
            Try
                DB.License.Add(License)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using

    End Sub

    Public Shared Sub Update(License As License)
        Using DB = New DBAttendanceEntities()
            Try
                Dim oldLicense = DB.License.Find(License.Id)
                DB.Entry(oldLicense).CurrentValues.SetValues(License)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub


    Public Shared Sub Delete(License As License)
        Using DB = New DBAttendanceEntities()
            Try
                License = DB.License.Find(License.Id)
                DB.License.Remove(License)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub


    'Public Shared Function ListLicenseType() As List(Of LicenseType)
    '    Try
    '        Dim DB = New DBAttendanceEntities()
    '        Dim Lista = DB.License
    '        Return
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function



End Class
