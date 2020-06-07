Imports Data

Public Class ContractDA

	Public Shared Function FindActual(Employee As Employee) As Contract
		Using DB = New DBAttendanceEntities
			Try
				Employee = DB.Employee.Find(Employee.CardId)
				Dim Contract = From C In Employee.Contract Where C.State = True Select C
				Return Contract.Single
			Catch ex As Exception
				Throw ex
			End Try
		End Using
	End Function

	Public Shared Function List() As List(Of Contract)
		Try
			Dim DB = New DBAttendanceEntities
			Dim ContractList = From C In DB.Contract Where C.State Select C
			Return ContractList.ToList
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Public Shared Function List(EmployeeCardId As String) As List(Of Contract)
		Using DB = New DBAttendanceEntities
			Try
				Dim ContractList = From C In DB.Contract Where C.EmployeeCardId Is EmployeeCardId Order By C.State Descending
				Return ContractList.ToList
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

	Public Shared Sub Update(Contract As Contract)
		Using DB = New DBAttendanceEntities()
			Try
				Dim oldContract = DB.Contract.Find(Contract.Id)
				DB.Entry(oldContract).CurrentValues.SetValues(Contract)
				DB.SaveChanges()
			Catch ex As Exception
				Throw ex
			End Try
		End Using
	End Sub

	Public Shared Sub Down(Contract As Contract)
		Try
			Contract.State = False
			Dim DB = DBContextDA.GetContext(Contract)
			DB.SaveChanges()
		Catch ex As Exception
			Throw ex
		End Try
	End Sub
End Class
