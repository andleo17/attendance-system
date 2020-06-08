Imports Data
Imports Server

Class FrmHorario
	Public Property CurrentSchedule As Schedule
	Public Property Employee As Employee
	Public Property Mode As Integer
	Private SelectedSchedule As Schedule
	Private ScheduleDetails As List(Of ScheduleDetail) = New List(Of ScheduleDetail)

	Private Sub Back()
		NavigationService.GetNavigationService(Me).GoBack()
	End Sub

	Private Sub GenerateSchedule()
		Dim Hours = New List(Of Object)
		For i As Integer = 7 To 21
			Dim SH = New TimeSpan(i, 0, 0)
			Dim FH = SH.Add(TimeSpan.FromHours(1))
			Dim Hour = New With {
				.StartHour = SH,
				.FinishHour = FH,
				.StringConcat = SH.ToString & " - " & FH.ToString
			}
			Hours.Add(Hour)
		Next
		DgdSchedule.ItemsSource = Hours
	End Sub

	Private Sub PaintSchedule(ScheduleDetail As ScheduleDetail)
		Dim HoursInDays = DgdSchedule.Columns.Item(ScheduleDetail.Day)
		For Each H In DgdSchedule.Items
			Dim C As TextBlock = HoursInDays.GetCellContent(H)
			If C IsNot Nothing Then
				If ScheduleDetail.InHour <= H.StartHour And H.FinishHour <= ScheduleDetail.OutHour Then
					C.Background = New SolidColorBrush(Colors.Yellow)
				Else
					C.Background = Nothing
				End If
			End If
		Next
	End Sub

	Private Sub ListSchedules()
		Try
			Dim CList = ScheduleDA.List()
			ListaHorario.ItemsSource = CList
		Catch ex As Exception
			MessageBox.Show("Error al listar los horarios.")
		End Try
	End Sub

	Private Sub ListEmployeeSchedules(Employee As Employee)
		Try
			Dim CList = Employee.Schedule
			ListaHorario.ItemsSource = CList.ToList
			txtDni.IsEnabled = False
			txtEmpleado.IsEnabled = False
		Catch ex As Exception
			MessageBox.Show("Error al listar los horarios del empleado.")
		End Try
	End Sub

	Private Sub ShowScheduleData(Schedule As Schedule)
		txtDni.Text = Schedule.EmployeeCardId
		txtEmpleado.Text = Employee.Name & " " & Employee.Lastname
		InitialDate.SelectedDate = Schedule.StartDate
		FinalDate.SelectedDate = Schedule.FinishDate
		chkState.IsChecked = Schedule.State
		For Each SD In Schedule.ScheduleDetail
			PaintSchedule(SD)
		Next
	End Sub

	Private Function SetScheduleData(Schedule As Schedule) As Schedule
		Schedule.EmployeeCardId = txtDni.Text
		Schedule.FinishDate = FinalDate.SelectedDate
		Schedule.StartDate = InitialDate.SelectedDate
		Schedule.ScheduleDetail = ScheduleDetails
		Return Schedule
	End Function

	Private Sub AddScheduleDetail()
		Dim Cells = DgdSchedule.SelectedCells
		Dim StartHour = Cells.First().Item.StartHour
		Dim FinishHour = Cells.Last().Item.FinishHour
		Dim Day = Cells.First().Column.DisplayIndex
		Dim SDInList = (From SD In ScheduleDetails Where Day = SD.Day Select SD)
		Dim ScheduleDetail As ScheduleDetail
		If SDInList.Count = 0 Then
			ScheduleDetail = New ScheduleDetail With {
				.InHour = StartHour,
				.OutHour = FinishHour,
				.Day = Day
			}
			ScheduleDetails.Add(ScheduleDetail)
		Else
			ScheduleDetail = SDInList.Single
			If ScheduleDetail.InHour > StartHour Then
				ScheduleDetail.InHour = StartHour
			End If
			If ScheduleDetail.OutHour <= FinishHour Then
				ScheduleDetail.OutHour = FinishHour
			End If
		End If
		PaintSchedule(ScheduleDetail)
	End Sub

	Private Sub ModifyScheduleDetail()
		Dim Cells = DgdSchedule.SelectedCells
		Dim StartHour = Cells.First().Item.StartHour
		Dim FinishHour = Cells.Last().Item.FinishHour
		Dim Day = Cells.First().Column.DisplayIndex
		Dim SDInList = (From SD In ScheduleDetails Where Day = SD.Day Select SD)
		Dim ScheduleDetail As ScheduleDetail
		If SDInList.Count = 0 Then
			ScheduleDetail = New ScheduleDetail With {
				.InHour = StartHour,
				.OutHour = FinishHour,
				.Day = Day
			}
			ScheduleDetails.Add(ScheduleDetail)
		Else
			ScheduleDetail = SDInList.Single
			ScheduleDetail.InHour = StartHour
			ScheduleDetail.OutHour = FinishHour
		End If
		PaintSchedule(ScheduleDetail)
	End Sub

	Private Sub DeleteScheduleDetail()
		Dim Cells = DgdSchedule.SelectedCells
		Dim StartHour = Cells.First().Item.StartHour
		Dim FinishHour = Cells.Last().Item.FinishHour
		Dim Day = Cells.First().Column.DisplayIndex
		Dim SDInList = (From SD In ScheduleDetails Where Day = SD.Day Select SD)
		Try
			Dim ScheduleDetail = SDInList.Single
			If StartHour <= ScheduleDetail.OutHour And ScheduleDetail.InHour <= FinishHour Then
				Dim IHour As TimeSpan
				Dim OHour As TimeSpan
				If ScheduleDetail.InHour > StartHour Then
					IHour = FinishHour
				End If
				If ScheduleDetail.OutHour <= FinishHour Then
					OHour = StartHour
				End If
				If OHour - IHour >= TimeSpan.Zero Then
					ScheduleDetail.InHour = IHour
					ScheduleDetail.OutHour = OHour
				Else
					ScheduleDetails.Remove(ScheduleDetail)
				End If
			End If
			PaintSchedule(ScheduleDetail)
		Catch ex As Exception
			Console.WriteLine("Se intentó eliminar un horario que no existe.")
		End Try
	End Sub

	Private Sub DisableButtons()
		btnDelete.Visibility = Visibility.Collapsed
		btnSave.Visibility = Visibility.Collapsed
		btnUpdate.Visibility = Visibility.Collapsed
		btnDown.Visibility = Visibility.Collapsed
	End Sub

	Private Sub DisableFields()
		txtDni.IsEnabled = False
		txtEmpleado.IsEnabled = False
		InitialDate.IsEnabled = False
		FinalDate.IsEnabled = False
		chkState.IsEnabled = False
	End Sub

	Private Sub ClearInputs()
		InitialDate.SelectedDate = Nothing
		FinalDate.SelectedDate = Nothing
		txtEmpleado.Text = Nothing
		chkState.IsChecked = False
		If Mode = 0 Then
			txtDni.Text = Nothing
			txtDni.IsEnabled = True
			ListSchedules()
		ElseIf Mode = 1 Then
			ListEmployeeSchedules(Employee)
		End If
	End Sub

	Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
		If Mode = 0 Then
			ListSchedules()
		ElseIf Mode = 1 Then
			ListEmployeeSchedules(Employee)
			ShowScheduleData(CurrentSchedule)
		ElseIf Mode = 2 Then
			ListEmployeeSchedules(Employee)
			ShowScheduleData(CurrentSchedule)
			DisableButtons()
			DisableFields()
		End If
	End Sub

	Private Sub Page_Initialized(sender As Object, e As EventArgs)
		GenerateSchedule()
	End Sub

	Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click

	End Sub

	Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
		If btnSave.Content = "REGISTRAR" Then
			'Save()
			btnSave.Content = "NUEVO"
		Else
			ClearInputs()
			btnSave.Content = "REGISTRAR"
		End If
	End Sub

	Private Sub btnClean_Click(sender As Object, e As RoutedEventArgs) Handles btnClean.Click
		ClearInputs()
	End Sub
End Class
