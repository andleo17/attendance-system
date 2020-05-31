Imports Data
Imports Server

Class FrmEmployeeForm
    Property SelectedEmployee As Employee

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
            ShowEmployeeData(SelectedEmployee)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SaveEmployee()
        Try
            Dim Employee = GetEmployeeData(New Employee)
            EmployeeDA.Save(Employee)
            MessageBox.Show("Empleado agregado correctamente")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub UpdateEmployee()
        If SelectedEmployee IsNot Nothing Then
            SelectedEmployee = GetEmployeeData(SelectedEmployee)
            EmployeeDA.Update(SelectedEmployee)
            MessageBox.Show("Datos actualizados correctamente")
        Else
            MessageBox.Show("Por favor seleccione un empleado")
        End If
    End Sub

    Private Sub DeleteEmployee()
        If SelectedEmployee IsNot Nothing Then
            EmployeeDA.Delete(SelectedEmployee)
            MessageBox.Show("Empleado eliminado correctamente")
        Else
            MessageBox.Show("Por favor seleccione un empleado")
        End If
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs) Handles Button.Click
        SaveEmployee()
    End Sub
End Class
