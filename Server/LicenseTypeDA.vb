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



End Class
