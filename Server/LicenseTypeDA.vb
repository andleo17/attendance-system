Imports Data
Public Class LicenseTypeDA
    Public Shared Function listarTiposLicencia() As List(Of LicenseType)
        Using DB = New DBAttendanceEntities
            Try
                Dim Lista = DB.LicenseType
                'Dim lista = From LT In DB.LicenseType Select LT.id 
                Return Lista.ToList
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function


    Public Shared Function save(LicenseType As LicenseType)
        Using DB = New DBAttendanceEntities
            Try
                DB.LicenseType.Add(LicenseType)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Shared Sub Update(LicenseType As LicenseType)
        Using DB = New DBAttendanceEntities()
            Try
                Dim oldLicenseType = DB.LicenseType.Find(LicenseType.Id)
                DB.Entry(oldLicenseType).CurrentValues.SetValues(LicenseType)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Sub Delete(LicenseType As LicenseType)
        Using DB = New DBAttendanceEntities()
            Try
                LicenseType = DB.LicenseType.Find(LicenseType.Id)
                DB.LicenseType.Remove(LicenseType)
                DB.SaveChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    Public Shared Function Search(LicenseType As LicenseType)
        Try
            Dim DB = New DBAttendanceEntities()
            Return DB.LicenseType.Find(LicenseType.Id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
