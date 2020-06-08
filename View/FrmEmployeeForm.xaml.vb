Imports Data
Imports Server

Class FrmEmployeeForm
	Public Property SelectedEmployee As Employee
	Public Property Mode As Integer
	Private Property Contract As Contract
	Private Property Schedule As Schedule

	Private Sub Back()
		NavigationService.GetNavigationService(Me).GoBack()
	End Sub

	Private Sub ShowDetails(Page As Page)
		NavigationService.GetNavigationService(Me).Content = Page
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

	Private Sub DisableFields()
		TxtCardId.IsEnabled = False
		If Mode.Equals(2) Then
			TxtAddress.IsEnabled = False
			TxtCardId.IsEnabled = False
			TxtEmail.IsEnabled = False
			TxtLastname.IsEnabled = False
			TxtName.IsEnabled = False
			TxtPhone.IsEnabled = False
			CboGenre.IsEnabled = False
			ChkState.IsEnabled = False
			DpkBirthDate.IsEnabled = False

			DpkContractFinalDate.IsEnabled = False
			DpkContractInitialDate.IsEnabled = False
			TxtMount.IsEnabled = False
			ChkExtraHours.IsEnabled = False

			DpkScheduleInitialDate.IsEnabled = False
			DpkScheduleFinalDate.IsEnabled = False
			BtnAddScheduleDetail.Visibility = Visibility.Collapsed
			BtnModifyScheduleDetail.Visibility = Visibility.Collapsed
			BtnDeleteScheduleDetail.Visibility = Visibility.Collapsed
			DgdSchedule.IsEnabled = False
		End If
	End Sub

	Private Function SetEmployeeData(Employee As Employee) As Employee
		Employee.CardId = TxtCardId.Text
		Employee.Name = TxtName.Text
		Employee.Lastname = TxtLastname.Text
		Employee.Genre = CboGenre.SelectedValue
		Employee.Address = TxtAddress.Text
		Employee.Phone = TxtPhone.Text
		Employee.Email = TxtEmail.Text
		Employee.BirthDate = DpkBirthDate.SelectedDate
		Return Employee
	End Function

	Private Sub ShowEmployee()
		DisableFields()
		TxtAddress.Text = SelectedEmployee.Address
		TxtCardId.Text = SelectedEmployee.CardId
		TxtEmail.Text = SelectedEmployee.Email
		TxtLastname.Text = SelectedEmployee.Lastname
		TxtName.Text = SelectedEmployee.Name
		TxtPhone.Text = SelectedEmployee.Phone
		CboGenre.SelectedValue = SelectedEmployee.Genre
		ChkState.IsChecked = SelectedEmployee.State
		DpkBirthDate.SelectedDate = SelectedEmployee.BirthDate
	End Sub

	Private Function SetContractData(Contract As Contract) As Contract
		Contract.EmployeeCardId = TxtCardId.Text
		Contract.ExtraHours = ChkExtraHours.IsChecked
		Contract.FinishDate = DpkContractFinalDate.SelectedDate
		Contract.Mount = Decimal.Parse(TxtMount.Text)
		Contract.StartDate = DpkContractInitialDate.SelectedDate
		Return Contract
	End Function

	Private Sub ShowContract()
		Try
			Contract = SelectedEmployee.Contract.Last
			chkContract.IsChecked = Contract.State
			DpkContractInitialDate.SelectedDate = Contract.StartDate
			DpkContractFinalDate.SelectedDate = Contract.FinishDate
			TxtMount.Text = Contract.Mount
			ChkExtraHours.IsChecked = Contract.ExtraHours
		Catch ex As Exception
			MessageBox.Show("Error al encontrar el contrato")
		End Try
	End Sub

	Private Function SetScheduleData(Schedule As Schedule) As Schedule
		Schedule.EmployeeCardId = TxtCardId.Text
		Schedule.FinishDate = DpkScheduleFinalDate.SelectedDate
		Schedule.StartDate = DpkScheduleInitialDate.SelectedDate
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
		Dim SDInList = (From SD In Schedule.ScheduleDetail Where CS.Day = SD.Day Select SD)
		Dim ScheduleDetail As ScheduleDetail
		If SDInList.Count = 0 Then
			ScheduleDetail = New ScheduleDetail With {
				.InHour = CS.StartHour,
				.OutHour = CS.FinishHour,
				.Day = CS.Day
			}
			Schedule.ScheduleDetail.Add(ScheduleDetail)
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
		Dim SDInList = (From SD In Schedule.ScheduleDetail Where CS.Day = SD.Day Select SD)
		Dim ScheduleDetail As ScheduleDetail
		If SDInList.Count = 0 Then
			ScheduleDetail = New ScheduleDetail With {
				.InHour = CS.StartHour,
				.OutHour = CS.FinishHour,
				.Day = CS.Day
			}
			Schedule.ScheduleDetail.Add(ScheduleDetail)
		Else
			ScheduleDetail = SDInList.Single
			ScheduleDetail.InHour = CS.StartHour
			ScheduleDetail.OutHour = CS.FinishHour
		End If
		PaintSchedule(ScheduleDetail)
	End Sub

	Private Sub DeleteScheduleDetail()
		Dim CS = GetSelectedCells()
		Dim SDInList = (From SD In Schedule.ScheduleDetail Where CS.Day = SD.Day Select SD)
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
					Schedule.ScheduleDetail.Remove(ScheduleDetail)
				End If
				PaintSchedule(ScheduleDetail)
			End If
		Catch ex As Exception
			Console.WriteLine("Se intentó eliminar un horario que no existe.")
		End Try
	End Sub

	Private Sub ShowSchedule()
		Try
			Schedule = SelectedEmployee.Schedule.Last
			ChkSchedule.IsChecked = Schedule.State
			DpkScheduleInitialDate.SelectedDate = Schedule.StartDate
			DpkScheduleFinalDate.SelectedDate = Schedule.FinishDate
			For Each SD In Schedule.ScheduleDetail
				PaintSchedule(SD)
			Next
		Catch ex As Exception
			MessageBox.Show("Error al encontrar el horario")
		End Try
	End Sub

	Private Sub SaveEmployee()
		Try
			Dim Employee = SetEmployeeData(New Employee)
			Dim Contract = SetContractData(New Contract)
			Schedule = SetScheduleData(Schedule)
			Employee.Contract.Add(Contract)
			Employee.Schedule.Add(Schedule)
			EmployeeDA.Save(Employee)
			MessageBox.Show("Empleado agregado correctamente")
			Back()
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub UpdateEmployee()
		SelectedEmployee = SetEmployeeData(SelectedEmployee)
		EmployeeDA.Update(SelectedEmployee)
		MessageBox.Show("Datos actualizados correctamente")
		Back()
	End Sub

	Private Sub Button_Click(sender As Object, e As RoutedEventArgs) Handles Button.Click
		If Mode.Equals(0) Then
			SaveEmployee()
		ElseIf Mode.Equals(1) Then
			UpdateEmployee()
		ElseIf Mode.Equals(2) Then
			Back()
		End If
	End Sub

	Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
		If Mode.Equals(0) Then
			Button.Content = "REGISTRAR"
			Schedule = New Schedule With {
				.ScheduleDetail = New List(Of ScheduleDetail)
			}
			BtnContractDetails.Visibility = Visibility.Collapsed
			BtnScheduleDetails.Visibility = Visibility.Collapsed
		ElseIf Mode.Equals(1) Then
			Button.Content = "GUARDAR"
			ShowEmployee()
			ShowContract()
			ShowSchedule()
		ElseIf Mode.Equals(2) Then
			Button.Content = "VOLVER"
			ShowEmployee()
			ShowContract()
			ShowSchedule()
		End If
	End Sub

	Private Sub DpkContractInitialDate_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles DpkContractInitialDate.SelectedDateChanged
		DpkScheduleInitialDate.SelectedDate = DpkContractInitialDate.SelectedDate
	End Sub

	Private Sub DpkContractFinalDate_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles DpkContractFinalDate.SelectedDateChanged
		DpkScheduleFinalDate.SelectedDate = DpkContractFinalDate.SelectedDate
	End Sub

	Private Sub BtnAddScheduleDetail_Click(sender As Object, e As RoutedEventArgs) Handles BtnAddScheduleDetail.Click
		AddScheduleDetail()
	End Sub

	Private Sub Page_Initialized(sender As Object, e As EventArgs)
		GenerateSchedule()
	End Sub

	Private Sub BtnContractDetails_Click(sender As Object, e As RoutedEventArgs) Handles BtnContractDetails.Click
		Dim Frm = New FrmContrato With {
			.Employee = SelectedEmployee,
			.CurrentContract = Contract,
			.Mode = Mode
		}
		ShowDetails(Frm)
	End Sub

	Private Sub BtnScheduleDetails_Click(sender As Object, e As RoutedEventArgs) Handles BtnScheduleDetails.Click
		Dim Frm = New FrmHorario With {
			.Employee = SelectedEmployee,
			.CurrentSchedule = Schedule,
			.Mode = Mode
		}
		ShowDetails(Frm)
	End Sub

	Private Sub BtnModifyScheduleDetail_Click(sender As Object, e As RoutedEventArgs) Handles BtnModifyScheduleDetail.Click
		ModifyScheduleDetail()
	End Sub

	Private Sub BtnDeleteScheduleDetail_Click(sender As Object, e As RoutedEventArgs) Handles BtnDeleteScheduleDetail.Click
		DeleteScheduleDetail()
	End Sub
End Class
