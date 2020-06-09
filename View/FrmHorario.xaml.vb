Imports Data
Imports Server

Class FrmHorario
	Public Property CurrentSchedule As Schedule
	Public Property Employee As Employee
	Public Property Mode As Integer
	Private Property SelectedSchedule As Schedule

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

	Private Sub UnPaintSchedule()
		For Each C In DgdSchedule.Columns
			For Each D In DgdSchedule.Items
				Dim T As TextBlock = C.GetCellContent(D)
				If T IsNot Nothing Then
					T.Background = Nothing
				End If
			Next
		Next
	End Sub

	Private Sub SearchEmployee(EmployeeCardId As String)
		Try
			Employee = EmployeeDA.Search(EmployeeCardId)
			If Employee IsNot Nothing Then
				txtEmpleado.Text = Employee.Name & " " & Employee.Lastname
				ListSchedules()
			Else
				MessageBox.Show("Empleado no encontrado")
				txtDni.Focus()
			End If
		Catch ex As Exception
			MessageBox.Show(ex.ToString)
		End Try
	End Sub

	Private Sub ListSchedules()
		Try
			Dim CList As List(Of Schedule)
			If Employee Is Nothing Then
				CList = ScheduleDA.List()
			Else
				CList = Employee.Schedule.ToList
				txtDni.IsEnabled = False
			End If
			ListaHorario.ItemsSource = CList
			Dim V = CollectionViewSource.GetDefaultView(CList)
			V.Filter = Function(C As Schedule) As Boolean
						   Dim FIDate = InitialDate.SelectedDate Is Nothing OrElse C.StartDate >= InitialDate.SelectedDate
						   Dim FFDate = FinalDate.SelectedDate Is Nothing OrElse C.FinishDate <= FinalDate.SelectedDate
						   Return FIDate And FFDate
					   End Function
		Catch ex As Exception
			MessageBox.Show("Error al listar los horarios.")
		End Try
	End Sub

	Private Sub FilterList()
		CollectionViewSource.GetDefaultView(ListaHorario.ItemsSource).Refresh()
	End Sub

	Private Sub ShowScheduleData(Schedule As Schedule)
		txtDni.Text = Schedule.EmployeeCardId
		txtId.Text = Schedule.Id
		txtEmpleado.Text = Schedule.Employee.Name & " " & Schedule.Employee.Lastname
		InitialDate.SelectedDate = Schedule.StartDate
		FinalDate.SelectedDate = Schedule.FinishDate
		chkState.IsChecked = Schedule.State
		ShowSchedule()
		For Each SD In Schedule.ScheduleDetail
			PaintSchedule(SD)
		Next
	End Sub

	Private Function SetScheduleData(Schedule As Schedule) As Schedule
		Schedule.EmployeeCardId = txtDni.Text
		Schedule.FinishDate = FinalDate.SelectedDate
		Schedule.StartDate = InitialDate.SelectedDate
		Return Schedule
	End Function

	Private Function GetSelectedCells() As Object
		Dim Cells = DgdSchedule.SelectedCells
		Return New With {
			Cells.First().Item.StartHour,
			Cells.Last().Item.FinishHour,
			.Day = Cells.First().Column.DisplayIndex
		}
	End Function

	Private Sub AddScheduleDetail()
		Dim CS = GetSelectedCells()
		Dim SDInList = (From SD In SelectedSchedule.ScheduleDetail Where CS.Day = SD.Day Select SD)
		Dim ScheduleDetail As ScheduleDetail
		If SDInList.Count = 0 Then
			ScheduleDetail = New ScheduleDetail With {
				.InHour = CS.StartHour,
				.OutHour = CS.FinishHour,
				.Day = CS.Day
			}
			SelectedSchedule.ScheduleDetail.Add(ScheduleDetail)
		Else
			ScheduleDetail = SDInList.Single
			If ScheduleDetail.InHour > CS.StartHour Then
				ScheduleDetail.InHour = CS.StartHour
			End If
			If ScheduleDetail.OutHour <= CS.FinishHour Then
				ScheduleDetail.OutHour = CS.FinishHour
			End If
		End If
		PaintSchedule(ScheduleDetail)
	End Sub

	Private Sub ModifyScheduleDetail()
		Dim CS = GetSelectedCells()
		Dim SDInList = (From SD In SelectedSchedule.ScheduleDetail Where CS.Day = SD.Day Select SD)
		Dim ScheduleDetail As ScheduleDetail
		If SDInList.Count = 0 Then
			ScheduleDetail = New ScheduleDetail With {
				.InHour = CS.StartHour,
				.OutHour = CS.FinishHour,
				.Day = CS.Day
			}
			SelectedSchedule.ScheduleDetail.Add(ScheduleDetail)
		Else
			ScheduleDetail = SDInList.Single
			ScheduleDetail.InHour = CS.StartHour
			ScheduleDetail.OutHour = CS.FinishHour
		End If
		PaintSchedule(ScheduleDetail)
	End Sub

	Private Sub DeleteScheduleDetail()
		Dim CS = GetSelectedCells()
		Dim SDInList = (From SD In SelectedSchedule.ScheduleDetail Where CS.Day = SD.Day Select SD)
		Try
			Dim ScheduleDetail = SDInList.Single
			If CS.StartHour <= ScheduleDetail.OutHour And ScheduleDetail.InHour <= CS.FinishHour Then
				Dim IHour = ScheduleDetail.InHour
				Dim OHour = ScheduleDetail.OutHour
				If IHour >= CS.StartHour Then
					IHour = CS.FinishHour
				End If
				If OHour <= CS.FinishHour Then
					OHour = CS.StartHour
				End If
				If OHour - IHour >= TimeSpan.Zero Then
					ScheduleDetail.InHour = IHour
					ScheduleDetail.OutHour = OHour
				Else
					ScheduleDetail.InHour = Nothing
					ScheduleDetail.OutHour = Nothing
					SelectedSchedule.ScheduleDetail.Remove(ScheduleDetail)
				End If
				PaintSchedule(ScheduleDetail)
			End If
		Catch ex As Exception
			Console.WriteLine("Se intentó eliminar un horario que no existe.")
		End Try
	End Sub

	Private Sub HideDetailButtons()
		BtnModifyDetail.Visibility = Visibility.Collapsed
		BtnAddDetail.Visibility = Visibility.Collapsed
		BtnDeleteDetail.Visibility = Visibility.Collapsed
	End Sub

	Private Sub HideSchedule()
		HideDetailButtons()
		DgdSchedule.Visibility = Visibility.Collapsed
	End Sub

	Private Sub ShowDetailButtons()
		BtnModifyDetail.Visibility = Visibility.Visible
		BtnAddDetail.Visibility = Visibility.Visible
		BtnDeleteDetail.Visibility = Visibility.Visible
	End Sub

	Private Sub ShowSchedule()
		If Mode <> 2 Then
			ShowDetailButtons()
		End If
		DgdSchedule.Visibility = Visibility.Visible
		UnPaintSchedule()
	End Sub

	Private Sub DisableButtons()
		btnSave.Visibility = Visibility.Collapsed
		btnUpdate.Visibility = Visibility.Collapsed
		btnDown.Visibility = Visibility.Collapsed
		HideDetailButtons()
	End Sub

	Private Sub ClearInputs()
		InitialDate.SelectedDate = Nothing
		FinalDate.SelectedDate = Nothing
		chkState.IsChecked = False
		btnSave.Content = "NUEVO"
		HideSchedule()
		If Mode = 0 Then
			If btnSearch.Content = "BUSCAR" Or Employee Is Nothing Then
				txtDni.Text = Nothing
				txtDni.IsEnabled = True
				txtEmpleado.Text = Nothing
				Employee = Nothing
			End If
		End If
		btnSearch.Content = "BUSCAR"
		ListSchedules()
	End Sub

	Private Sub Save()
		Try
			Dim Schedule = SetScheduleData(SelectedSchedule)
			If Mode = 0 Then
				ScheduleDA.Save(Schedule)
				MessageBox.Show("Horario añadido correctamente")
				Employee = EmployeeDA.Search(Employee)
				ListSchedules()
			ElseIf Mode = 1 Then
				Employee.Schedule.Add(Schedule)
				MessageBox.Show("Horario añadido correctamente")
				Back()
			End If
		Catch ex As Exception
			MessageBox.Show("Error al registrar el nuevo horario")
		End Try
	End Sub

	Private Sub Update()
		Try
			If SelectedSchedule IsNot Nothing Then
				SelectedSchedule = SetScheduleData(SelectedSchedule)
				If Mode = 0 Then
					ScheduleDA.Update(SelectedSchedule)
					MessageBox.Show("Horario actualizado correctamente")
					ListSchedules()
				ElseIf Mode = 1 Then
					MessageBox.Show("Horario actualizado correctamente")
					ListSchedules()
				End If
			Else
				MessageBox.Show("Por favor seleccione un horario")
			End If
		Catch ex As Exception
			MessageBox.Show("Error al actualizar el horario")
		End Try
	End Sub

	Private Sub Down()
		Try
			If Mode = 0 Then
				ScheduleDA.Down(SelectedSchedule)
				MessageBox.Show("Horario dado de baja.")
			ElseIf Mode = 1 Then
				SelectedSchedule.State = False
				MessageBox.Show("Horario dado de baja.")
			End If
		Catch ex As Exception
			MessageBox.Show("Error al actualizar el horario.")
		End Try
	End Sub

	Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
		HideSchedule()
		If Mode = 0 Then
			ListSchedules()
		ElseIf Mode = 1 Then
			SelectedSchedule = CurrentSchedule
			ListSchedules()
			ShowScheduleData(CurrentSchedule)
		ElseIf Mode = 2 Then
			ListSchedules()
			ShowScheduleData(CurrentSchedule)
			DisableButtons()
		End If
	End Sub

	Private Sub Page_Initialized(sender As Object, e As EventArgs)
		GenerateSchedule()
	End Sub

	Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
		Update()
		ClearInputs()
	End Sub

	Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
		If btnSave.Content = "REGISTRAR" Then
			Save()
			btnSave.Content = "NUEVO"
		Else
			SelectedSchedule = New Schedule With {
				.ScheduleDetail = New List(Of ScheduleDetail)
			}
			ClearInputs()
			ShowSchedule()
			btnSave.Content = "REGISTRAR"
		End If
	End Sub

	Private Sub btnClean_Click(sender As Object, e As RoutedEventArgs) Handles btnClean.Click
		ClearInputs()
	End Sub

	Private Sub BtnAddDetail_Click(sender As Object, e As RoutedEventArgs) Handles BtnAddDetail.Click
		If DgdSchedule.SelectedCells.Count > 0 Then
			AddScheduleDetail()
		Else
			MessageBox.Show("Por favor seleccione celdas.")
		End If
	End Sub

	Private Sub BtnModifyDetail_Click(sender As Object, e As RoutedEventArgs) Handles BtnModifyDetail.Click
		If DgdSchedule.SelectedCells.Count > 0 Then
			ModifyScheduleDetail()
		Else
			MessageBox.Show("Por favor seleccione celdas.")
		End If
	End Sub

	Private Sub BtnDeleteDetail_Click(sender As Object, e As RoutedEventArgs) Handles BtnDeleteDetail.Click
		If DgdSchedule.SelectedCells.Count > 0 Then
			DeleteScheduleDetail()
		Else
			MessageBox.Show("Por favor seleccione celdas.")
		End If
	End Sub

	Private Sub ListaHorario_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles ListaHorario.MouseDoubleClick
		SelectedSchedule = ListaHorario.SelectedItem
		ShowScheduleData(SelectedSchedule)
	End Sub

	Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click
		If btnSearch.Content = "BUSCAR" Then
			btnSearch.Content = "NUEVA BÚSQUEDA"
		Else
			ClearInputs()
		End If
		FilterList()
	End Sub

	Private Sub btnDown_Click(sender As Object, e As RoutedEventArgs) Handles btnDown.Click
		Down()
		ClearInputs()
	End Sub

	Private Sub txtDni_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDni.KeyDown
		If e.Key = Key.Enter Then
			If txtDni.Text.Length = 8 Then
				SearchEmployee(txtDni.Text)
			Else
				MessageBox.Show("Por favor ingrese un DNI válido.")
			End If
		End If
	End Sub
End Class
