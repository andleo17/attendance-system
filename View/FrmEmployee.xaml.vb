Imports Data
Imports Server

Class FrmEmployee

    Private SelectedEmployee As Employee

    Private Sub ShowEmployeesList()
        Try
            EmployeeList.ItemsSource = EmployeeDA.List()
            ClearInputs()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ShowEmployeeData(Employee As Employee)
        If Employee IsNot Nothing Then
            TxtAddress.Text = Employee.Address
            TxtCardId.Text = Employee.CardId
            TxtEmail.Text = Employee.Email
            TxtLastname.Text = Employee.Lastname
            TxtName.Text = Employee.Name
            TxtPhone.Text = Employee.Phone
            CboGenre.SelectedValue = Employee.Genre
            ChkState.IsChecked = Employee.State
        End If
    End Sub

    Private Sub ShowEmployee()
        Try
            SelectedEmployee = EmployeeList.SelectedValue
            ShowEmployeeData(SelectedEmployee)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function GetEmployeeData(Employee As Employee) As Employee
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

    Private Sub SaveEmployee()
        Try
            Dim Employee = GetEmployeeData(New Employee)
            EmployeeDA.Save(Employee)
            MessageBox.Show("Empleado agregado correctamente")
            ShowEmployeesList()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub UpdateEmployee()
        If SelectedEmployee IsNot Nothing Then
            SelectedEmployee = GetEmployeeData(SelectedEmployee)
            EmployeeDA.Update(SelectedEmployee)
            MessageBox.Show("Datos actualizados correctamente")
            ShowEmployeesList()
        Else
            MessageBox.Show("Por favor seleccione un empleado")
        End If
    End Sub

    Private Sub DeleteEmployee()
        If SelectedEmployee IsNot Nothing Then
            EmployeeDA.Delete(SelectedEmployee)
            MessageBox.Show("Empleado eliminado correctamente")
            ShowEmployeesList()
        Else
            MessageBox.Show("Por favor seleccione un empleado")
        End If
    End Sub

    Private Sub ClearInputs()
        SelectedEmployee = Nothing
        For Each C In Form.Children
            If TypeOf C Is TextBox Then
                C.Text = Nothing
            ElseIf TypeOf C Is ComboBox Then
                C.SelectedIndex = -1
            ElseIf TypeOf C Is CheckBox Then
                C.IsChecked = False
            End If
        Next
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As RoutedEventArgs) Handles BtnSave.Click
        SaveEmployee()
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        ShowEmployeesList()
    End Sub

    Private Sub EmployeeList_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles EmployeeList.MouseDoubleClick
        ShowEmployee()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles BtnUpdate.Click
        UpdateEmployee()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As RoutedEventArgs) Handles BtnDelete.Click
        DeleteEmployee()
    End Sub
End Class
