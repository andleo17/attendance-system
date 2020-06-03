Imports Data

Public Class ContractDA

	Public Shared Function FindActual(Employee As Employee) As Contract
		Using DB = New DBAttendanceEntities
			Try
				Employee = DB.Employee.Find(Employee.CardId)
				Dim Contract = From C In Employee.Contract Where C.State Select C
				Return Contract.Single
			Catch ex As Exception
				Throw ex
			End Try
		End Using
	End Function

	Public Shared Sub Save(Contract As Contract)
		Using DB = New DBAttendanceEntities
			Try
				DB.Contract.Add(Contract)
				DB.SaveChanges()
			Catch ex As Exception
				Throw ex
			End Try
		End Using
	End Sub

End Class
