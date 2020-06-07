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
		btnDelete.Visibility = Visibility.Collapsed
		btnSave.Visibility = Visibility.Collapsed
		btnUpdate.Visibility = Visibility.Collapsed
		btnDown.Visibility = Visibility.Collapsed
	End Sub

	Private Sub DisableFields()
		ChkExtraHours.IsEnabled = False
		TxtMount.IsEnabled = False
		DpkStartDate.IsEnabled = False
		DpkFinishDate.IsEnabled = False
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

	Private Sub ListEmployeeContracts(Employee As Employee)
		Try
			Dim CList = Employee.Contract
			ListaContratos.ItemsSource = CList.ToList
			TxtCardId.IsEnabled = False
		Catch ex As Exception
			MessageBox.Show("Error al listar los contratos del empleado.")
		End Try
	End Sub

	Private Sub ListContracts()
		Try
			Dim CList = ContractDA.List()
			ListaContratos.ItemsSource = CList
		Catch ex As Exception
			MessageBox.Show("Error al listar los contratos.")
		End Try
	End Sub

	Private Sub ClearInputs()
		TxtMount.Text = Nothing
		DpkFinishDate.SelectedDate = Nothing
		DpkStartDate.SelectedDate = Nothing
		ChkExtraHours.IsChecked = False
		ChkState.IsChecked = False
		If Mode = 0 Then
			TxtCardId.Text = Nothing
			TxtCardId.IsEnabled = True
			ListContracts()
		ElseIf Mode = 1 Then
			ListEmployeeContracts(Employee)
		End If
	End Sub

	Private Sub Save()
		Try
			Dim Contract = SetContractData(New Contract)
			If Mode = 0 Then
				ContractDA.Save(Contract)
				MessageBox.Show("Contrato añadido correctamente")
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
			SelectedContract = SetContractData(SelectedContract)
			If Mode = 0 Then
				ContractDA.Update(SelectedContract)
				MessageBox.Show("Contrato actualizado correctamente")
				ListContracts()
			ElseIf Mode = 1 Then
				MessageBox.Show("Contrato actualizado correctamente")
				ListEmployeeContracts(Employee)
			End If
		Catch ex As Exception
			MessageBox.Show("Error al actualizar el contrato")
		End Try
	End Sub

	Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
		If Mode = 0 Then
			ListContracts()
		ElseIf Mode = 1 Then
			SelectedContract = CurrentContract
			ListEmployeeContracts(Employee)
			ShowContractData(CurrentContract)
		ElseIf Mode = 2 Then
			ListEmployeeContracts(Employee)
			ShowContractData(CurrentContract)
			DisableButtons()
			DisableFields()
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

	Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click
		If btnSearch.Content = "BUSCAR" Then
			Employee = EmployeeDA.Search(TxtCardId.Text)
			ListEmployeeContracts(Employee)
			btnSearch.Content = "NUEVA BÚSQUEDA"
		Else
			ClearInputs()
			btnSearch.Content = "BUSCAR"
		End If
	End Sub

	Private Sub ListaContratos_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles ListaContratos.MouseDoubleClick
		SelectedContract = ListaContratos.SelectedItem
		ShowContractData(SelectedContract)
	End Sub

	Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
		Update()
	End Sub
End Class
