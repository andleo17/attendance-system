﻿Imports Data
Imports Server

Class FrmEmployeeForm
    Public Property SelectedEmployee As Employee
    Public Property Mode As Integer

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
        ShowEmployeeData(SelectedEmployee)
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

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs) Handles Button.Click
        If Mode.Equals(0) Then
            SaveEmployee()
        ElseIf Mode.Equals(1) Then
            UpdateEmployee()
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
