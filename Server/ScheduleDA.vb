Imports Data
Public Class ScheduleDA

	Public Shared Function FindActual(Employee As Employee) As Schedule
		Try
			Dim DB = New DBAttendanceEntities
			Employee = DB.Employee.Find(Employee.CardId)
			Dim Schedule = From S In Employee.Schedule Where S.State Select S
			Return Schedule.Single
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Public Shared Sub Save(Schedule As Schedule)
		Using DB = New DBAttendanceEntities
			Try
				DB.Schedule.Add(Schedule)
				DB.SaveChanges()
			Catch ex As Exception
				Throw ex
			End Try
		End Using
	End Sub

End Class
