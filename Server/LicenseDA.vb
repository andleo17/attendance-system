Imports Data
Public Class LicenseDA

    'lista de licencias
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

    'buscar licencias por empleado
    Public Shared Function Search(Employee As Employee) As List(Of License)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim License = From L In DB.License.Where(Function(PE) PE.EmployeeCardId.Equals(Employee.CardId)) Select L
            Return License.ToList()
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Public Shared Function Search(License As License) As License
    '    Try
    '        Dim DB = New DBAttendanceEntities()
    '        Return DB.License.Find(License.Id)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function


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


    Public Shared Function List(EmployeeCardId As String) As List(Of License)
        Try
            Dim DB = New DBAttendanceEntities()
            Dim License = From L In DB.License Where L.EmployeeCardId Is EmployeeCardId
            Return License.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Public Shared Function ListLicenseType() As List(Of LicenseType)
    '    Try
    '        Dim DB = New DBAttendanceEntities()
    '        Dim Lista = DB.License
    '        Return
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Validate(InitialDate As Date, FinalDate As Date, EmployeeCardId As String) As Integer
        Try
            Dim DB = New DBAttendanceEntities()
            Dim CountDay = DB.Database.SqlQuery(Of DataTable)("
                select * from Date_range(@p0,@p1) As DR inner join ScheduleDetail on ScheduleDetail.Day = DATEPART(dw,DR.Date) -1  
                inner join Schedule on Schedule.Id = ScheduleDetail.ScheduleId where Schedule.State = 1 and Schedule.EmployeeCardId = @p2", InitialDate, FinalDate, EmployeeCardId)
            Return CountDay.ToList().Count
        Catch ex As Exception
            Throw ex
        End Try
    End Function




End Class
