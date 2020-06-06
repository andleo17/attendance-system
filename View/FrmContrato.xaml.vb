Imports Data
Imports Server

Class FrmContrato
	Public Property CurrentContract As Contract
	Public Property Mode As Integer

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
	End Sub

	Private Function SetContractData(Contract As Contract) As Contract
		Contract.EmployeeCardId = TxtCardId.Text
		Contract.ExtraHours = ChkExtraHours.IsChecked
		Contract.FinishDate = DpkFinishDate.SelectedDate
		Contract.Mount = TxtMount.Text
		Contract.StartDate = DpkStartDate.SelectedDate
		Return Contract
	End Function

	Private Sub ListEmployeeContracts()
		Try
			TxtCardId.IsEnabled = False
			Dim CList = ContractDA.List(CurrentContract.EmployeeCardId)
			ListaContratos.ItemsSource = CList
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
			ListContracts()
		ElseIf Mode = 1 Then
			ListEmployeeContracts()
		End If
	End Sub

	Private Sub Save()
		Try
			Dim Contract = SetContractData(New Contract)
			ContractDA.Save(Contract)
			MessageBox.Show("Contrato añadido correctamente")
			ClearInputs()
		Catch ex As Exception
			MessageBox.Show("Error al registrar el nuevo contrato")
		End Try
	End Sub

	Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
		If Mode = 0 Then
			ListContracts()
		ElseIf Mode = 1 Then
			ListEmployeeContracts()
			ShowContractData(CurrentContract)
		ElseIf Mode = 2 Then
			ListEmployeeContracts()
			ShowContractData(CurrentContract)
			DisableButtons()
			DisableFields()
		End If
	End Sub

	Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
		Save()
	End Sub
End Class
