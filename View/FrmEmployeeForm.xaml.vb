Imports Data
Imports Server

Class FrmEmployeeForm
    Public Property SelectedEmployee As Employee
    Public Property Mode As Integer

    Private Sub Back()
        NavigationService.GetNavigationService(Me).GoBack()
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
        Employee.State = ChkState.IsChecked
        Return Employee
    End Function

    Private Sub ShowEmployee()
        TxtAddress.Text = SelectedEmployee.Address
        TxtCardId.Text = SelectedEmployee.CardId
        TxtEmail.Text = SelectedEmployee.Email
        TxtLastname.Text = SelectedEmployee.Lastname
        TxtName.Text = SelectedEmployee.Name
        TxtPhone.Text = SelectedEmployee.Phone
        CboGenre.SelectedValue = SelectedEmployee.Genre
        ChkState.IsChecked = SelectedEmployee.State
        DisableFields()
    End Sub

    Private Sub SaveEmployee()
        Try
            Dim Employee = SetEmployeeData(New Employee)
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
        ElseIf Mode.Equals(1) Then
            Button.Content = "GUARDAR"
            ShowEmployee()
        ElseIf Mode.Equals(2) Then
            Button.Content = "VOLVER"
            ShowEmployee()
        End If
    End Sub
End Class
