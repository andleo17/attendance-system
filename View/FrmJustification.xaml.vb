Imports Data
Imports Server

Class FrmJustification
	Private SelectedJustification As Justification

	Private Sub SearchAttendance()
		Try
			Dim Attendance = AttendanceDA.SearchAttendanceDate(AttendanceDate.SelectedDate, TxtCardId.Text)
			If Attendance.Count > 0 Then
				For Each A In Attendance
					Dim Emp = New Employee
					Emp.CardId = A.EmployeeCardId
					Emp = EmployeeDA.Search(Emp)
					MessageBox.Show("Empleado: " & Emp.Name & " " & Emp.Lastname)
					TxtAttendanceId.Text = A.Id
				Next
			Else
				MessageBox.Show("No se puede registrar justificación para la tardanza indicada")
			End If
		Catch ex As Exception
			MessageBox.Show(ex.ToString)
		End Try
	End Sub

	Private Function SetJustificationData(Justification As Justification) As Justification
		If Justification.Id = 0 Then
			Justification.Date = Date.Now.Date
		End If
		Justification.Motive = TxtMotive.Text
		Justification.State = ChkState.IsChecked
		Justification.AttendanceId = TxtAttendanceId.Text
		Return Justification
	End Function

	Private Sub JustificationSave()
		Using DB = New DBAttendanceEntities()
			Try
				If SelectedJustification Is Nothing Then
					If TxtAttendanceId.Text.Count <= 0 Then
						MessageBox.Show("Busque una tardanza")
					Else
						If TxtMotive.Text.Count <= 0 Then
							MessageBox.Show("Ingrese el motivo")
						Else
							Dim Justification = SetJustificationData(New Justification)
							JustificationDA.Save(Justification)
							MessageBox.Show("Justificación agregada correctamente")
							ShowJustificationList()
							ClearInputs()
						End If
					End If
				Else
					MessageBox.Show("Operación no disponible, limpie para continuar")
				End If
			Catch ex As Exception
				MessageBox.Show(ex.ToString)
			End Try
		End Using
	End Sub


	Private Sub ShowJustificationList()
		Try
			JustificationList.ItemsSource = JustificationDA.List()
		Catch ex As Exception
			MessageBox.Show(ex.ToString)
		End Try
	End Sub

	Private Sub ShowJustification()
		Try
			If JustificationList.SelectedValue IsNot Nothing Then
				SelectedJustification = JustificationList.SelectedValue
			End If
			TxtId.Text = SelectedJustification.Id
			TxtCardId.Text = SelectedJustification.Attendance.Employee.CardId
			TxtAttendanceId.Text = SelectedJustification.AttendanceId
			AttendanceDate.SelectedDate = SelectedJustification.Attendance.Date
			ChkState.IsChecked = SelectedJustification.State
			TxtMotive.Text = SelectedJustification.Motive
			TxtFecha.Text = SelectedJustification.Date
			JustificationList.SelectedValue = Nothing
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub ClearInputs()
		SelectedJustification = Nothing
		TxtAttendanceId.Text = Nothing
		AttendanceDate.SelectedDate = Nothing
		TxtCardId.Text = Nothing
		TxtFecha.Text = Date.Now.Date
		ChkState.IsChecked = True
		TxtId.Text = Nothing
		TxtMotive.Text = Nothing
		TxtId.IsEnabled = True
	End Sub

	Private Sub DeleteJustification()
		If SelectedJustification IsNot Nothing Then
			Dim Msg, Style, Title, Response
			Msg = "¿Seguro que desea eliminar la justificación"
			Style = vbYesNo + vbCritical + vbDefaultButton2
			Title = ".:SISTEMA DE ASISTENCIA:."
			Response = MsgBox(Msg, Style, Title)
			If Response = vbYes Then
				SelectedJustification = SetJustificationData(SelectedJustification)
				JustificationDA.Delete(SelectedJustification)
				MessageBox.Show("Justificación eliminada")
				ShowJustificationList()
				ClearInputs()
			End If
		Else
			MessageBox.Show("Seleccione na justificación")
		End If
	End Sub

	Private Sub SearchJustification(Justification As Justification)
		Try
			Justification.Id = TxtId.Text
			Dim J = JustificationDA.Search(Justification)
			If J IsNot Nothing Then
				Justification = J
				SelectedJustification = Justification
				ShowJustification()
				TxtId.IsEnabled = False
			End If
		Catch ex As Exception
			Throw ex
		End Try
	End Sub

	Private Sub UpdateJustification()
		If SelectedJustification IsNot Nothing Then
			If TxtMotive.Text.Count > 0 Then
				SelectedJustification = SetJustificationData(SelectedJustification)
				JustificationDA.Update(SelectedJustification)
				MessageBox.Show("Justificación actualizada")
				ShowJustificationList()
				ClearInputs()
			End If
		Else
			MessageBox.Show("Seleccione una justificación")
		End If

	End Sub

	Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
		ShowJustificationList()
		TxtAttendanceId.IsEnabled = False
		TxtFecha.Text = Date.Now.Date
		TxtFecha.IsEnabled = False
		ChkState.IsChecked = True
	End Sub

	Private Sub BtnSave_Click(sender As Object, e As RoutedEventArgs) Handles BtnSave.Click
		JustificationSave()
	End Sub

	Private Sub JustificationList_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles JustificationList.MouseDoubleClick
		ShowJustification()
	End Sub

	Private Sub BtnClear_Click(sender As Object, e As RoutedEventArgs) Handles BtnClear.Click
		ClearInputs()
	End Sub

	Private Sub BtnDelete_Click(sender As Object, e As RoutedEventArgs) Handles BtnDelete.Click
		DeleteJustification()
	End Sub

	Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click
		If TxtId.Text.Length > 0 Then
			SearchJustification(New Justification)
		Else
			MessageBox.Show("Ingrese código de la justificación")
		End If
	End Sub

	Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
		UpdateJustification()
	End Sub
End Class
