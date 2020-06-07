Imports Data
Imports Server

Class FrmContrato
	Public Property CurrentContract As Contract
	Public Property Employee As Employee
	Public Property Mode As Integer
	Private SelectedContract As Contract

	Private Sub Back()
		NavigationService.GetNavigationService(Me).GoBack()
	End Sub

	Private Sub DisableButtons()
		btnSave.Visibility = Visibility.Collapsed
		btnUpdate.Visibility = Visibility.Collapsed
		btnDown.Visibility = Visibility.Collapsed
	End Sub

	Private Sub ShowContractData(Contract As Contract)
		TxtCardId.Text = Contract.EmployeeCardId
		ChkExtraHours.IsChecked = Contract.ExtraHours
		TxtMount.Text = Contract.Mount
		DpkStartDate.SelectedDate = Contract.StartDate
		DpkFinishDate.SelectedDate = Contract.FinishDate
		ChkState.IsChecked = Contract.State
	End Sub

	Private Function SetContractData(Contract As Contract) As Contract
		Contract.EmployeeCardId = TxtCardId.Text
		Contract.ExtraHours = ChkExtraHours.IsChecked
		Contract.FinishDate = DpkFinishDate.SelectedDate
		Contract.Mount = TxtMount.Text
		Contract.StartDate = DpkStartDate.SelectedDate
		Return Contract
	End Function

	Private Sub ListContracts()
		Try
			Dim CList As List(Of Contract)
			If Employee Is Nothing Then
				CList = ContractDA.List()
			Else
				CList = Employee.Contract.ToList
				TxtCardId.IsEnabled = False
			End If

			ListaContratos.ItemsSource = CList
			Dim V = CollectionViewSource.GetDefaultView(CList)
			V.Filter = Function(C As Contract) As Boolean
						   Dim FMount = TxtMount.Text.Length = 0 OrElse C.Mount >= Decimal.Parse(TxtMount.Text)
						   Dim FIDate = DpkStartDate.SelectedDate Is Nothing OrElse C.StartDate >= DpkStartDate.SelectedDate
						   Dim FFDate = DpkFinishDate.SelectedDate Is Nothing OrElse C.FinishDate <= DpkFinishDate.SelectedDate
						   Return FMount And FIDate And FFDate
					   End Function
		Catch ex As Exception
			MessageBox.Show("Error al listar los contratos.")
		End Try
	End Sub

	Private Sub FilterList()
		CollectionViewSource.GetDefaultView(ListaContratos.ItemsSource).Refresh()
	End Sub

	Private Sub ClearInputs()
		TxtMount.Text = Nothing
		DpkFinishDate.SelectedDate = Nothing
		DpkStartDate.SelectedDate = Nothing
		ChkExtraHours.IsChecked = False
		ChkState.IsChecked = False
		btnSave.Content = "NUEVO"
		If Mode = 0 Then
			If btnSearch.Content = "BUSCAR" Or Employee Is Nothing Then
				TxtCardId.Text = Nothing
				TxtCardId.IsEnabled = True
				Employee = Nothing
			End If
		End If
		btnSearch.Content = "BUSCAR"
		ListContracts()
	End Sub

	Private Sub Save()
		Try
			Dim Contract = SetContractData(New Contract)
			If Mode = 0 Then
				ContractDA.Save(Contract)
				MessageBox.Show("Contrato añadido correctamente")
				Employee = EmployeeDA.Search(Employee)
				ListContracts()
			ElseIf Mode = 1 Then
				Employee.Contract.Add(Contract)
				MessageBox.Show("Contrato añadido correctamente")
				Back()
			End If
		Catch ex As Exception
			MessageBox.Show("Error al registrar el nuevo contrato")
		End Try
	End Sub

	Private Sub Update()
		Try
			If SelectedContract IsNot Nothing Then
				SelectedContract = SetContractData(SelectedContract)
				If Mode = 0 Then
					ContractDA.Update(SelectedContract)
					MessageBox.Show("Contrato actualizado correctamente")
					ListContracts()
				ElseIf Mode = 1 Then
					MessageBox.Show("Contrato actualizado correctamente")
					ListContracts()
				End If
			Else
				MessageBox.Show("Por favor seleccione un contrato")
			End If
		Catch ex As Exception
			MessageBox.Show("Error al actualizar el contrato")
		End Try
	End Sub

	Private Sub Search()
		Employee = EmployeeDA.Search(TxtCardId.Text)
		If Employee IsNot Nothing Then
			ListContracts()
		Else
			MessageBox.Show("Empleado no encontrado.")
		End If
	End Sub

	Private Sub Down()
		Try
			If Mode = 0 Then
				ContractDA.Down(SelectedContract)
				MessageBox.Show("Contracto dado de baja.")
			ElseIf Mode = 1 Then
				SelectedContract.State = False
				MessageBox.Show("Contrato dado de baja.")
			End If
		Catch ex As Exception
			MessageBox.Show("Error al actualizar el contrato.")
		End Try
	End Sub

	Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
		If Mode = 0 Then
			ListContracts()
		ElseIf Mode = 1 Then
			SelectedContract = CurrentContract
			ListContracts()
			ShowContractData(CurrentContract)
		ElseIf Mode = 2 Then
			ListContracts()
			ShowContractData(CurrentContract)
			DisableButtons()
		End If
	End Sub

	Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
		If btnSave.Content = "REGISTRAR" Then
			Save()
			btnSave.Content = "NUEVO"
		Else
			ClearInputs()
			btnSave.Content = "REGISTRAR"
		End If
	End Sub

	Private Sub TxtCardId_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtCardId.KeyUp
		If e.Key = Key.Enter Then
			If TxtCardId.Text.Length = 8 Then
				Search()
			Else
				MessageBox.Show("Por favor ingrese un DNI válido.")
			End If
		End If
	End Sub

	Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click
		If btnSearch.Content = "BUSCAR" Then
			btnSearch.Content = "NUEVA BÚSQUEDA"
		Else
			ClearInputs()
		End If
		FilterList()
	End Sub

	Private Sub ListaContratos_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles ListaContratos.MouseDoubleClick
		SelectedContract = ListaContratos.SelectedItem
		ShowContractData(SelectedContract)
	End Sub

	Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
		Update()
	End Sub

	Private Sub btnDown_Click(sender As Object, e As RoutedEventArgs) Handles btnDown.Click
		Down()
	End Sub

	Private Sub BtnClear_Click(sender As Object, e As RoutedEventArgs) Handles BtnClear.Click
		ClearInputs()
	End Sub
End Class
