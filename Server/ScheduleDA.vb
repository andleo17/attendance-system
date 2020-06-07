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

	Public Shared Function List(EmployeeCardId As String) As List(Of Schedule)
		Try
			Dim DB = New DBAttendanceEntities
			Dim ScheduleList = From S In DB.Schedule Where S.EmployeeCardId Is EmployeeCardId
			Return ScheduleList.ToList
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Public Shared Function List() As List(Of Schedule)
		Try
			Dim DB = New DBAttendanceEntities
			Dim ScheduleList = From S In DB.Schedule Where S.State
			Return ScheduleList.ToList
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

	Public Shared Sub Update(Schedule As Schedule)
		Using DB = New DBAttendanceEntities()
			Try
				Dim oldSchedule = DB.Schedule.Find(Schedule.Id)
				DB.Entry(oldSchedule).CurrentValues.SetValues(Schedule)
				DB.SaveChanges()
			Catch ex As Exception
				Throw ex
			End Try
		End Using
	End Sub
End Class
