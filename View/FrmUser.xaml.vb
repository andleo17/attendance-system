Imports Data
Imports Server

Class FrmUser
    Private SelectedUser As User

    Private Sub ShowUsersList()
        Try
            UserList.ItemsSource = UserDA.List()
            ClearInputs()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ShowUserData(User As User)
        If User IsNot Nothing Then
            TxtCardId.Text = User.EmployeeCardId
            TxtEmployeeName.Text = User.Employee.Name
            TxtId.Text = User.Id
            TxtName.Text = User.Name
            TxtPassword.Text = User.Password
        End If
    End Sub

    Private Sub ShowUser()
        Try
            SelectedUser = UserList.SelectedValue
            ShowUserData(SelectedUser)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function GetUserData(User As User) As User
        User.EmployeeCardId = TxtCardId.Text
        User.Id = TxtId.Text
        User.Name = TxtName.Text
        User.Password = TxtPassword.Text
        Return User
    End Function

    Private Sub SaveUser()
        Try
            Dim User = GetUserData(New User)
            UserDA.Save(User)
            MessageBox.Show("Usuario agregado correctamente")
            ShowUsersList()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub UpdateUser()
        If SelectedUser IsNot Nothing Then
            SelectedUser = GetUserData(SelectedUser)
            UserDA.Update(SelectedUser)
            MessageBox.Show("Datos actualizados correctamente")
            ShowUsersList()
        Else
            MessageBox.Show("Por favor seleccione un empleado")
        End If
    End Sub

    Private Sub DeleteUser()
        If SelectedUser IsNot Nothing Then
            UserDA.Delete(SelectedUser)
            MessageBox.Show("Empleado eliminado correctamente")
            ShowUsersList()
        Else
            MessageBox.Show("Por favor seleccione un empleado")
        End If
    End Sub

    Private Sub ClearInputs()
        SelectedUser = Nothing
        For Each C In Form.Children
            If TypeOf C Is TextBox Then
                C.Text = Nothing
            ElseIf TypeOf C Is CheckBox Then
                C.IsChecked = False
            End If
        Next
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As RoutedEventArgs) Handles BtnSave.Click
        SaveUser()
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        ShowUsersList()
    End Sub

    Private Sub EmployeeList_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles UserList.MouseDoubleClick
        ShowUser()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles BtnUpdate.Click
        UpdateUser()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As RoutedEventArgs) Handles BtnDelete.Click
        DeleteUser()
    End Sub


End Class
