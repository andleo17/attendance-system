Imports Data
Imports Server

Class FrmUser
    Private SelectedUser As User
    Private UsersActiveList As List(Of User)

    Private Sub ShowUsersList()
        Try
            If UsersActiveList Is Nothing Then
                UsersActiveList = UserDA.List()
            End If
            UserList.ItemsSource = UsersActiveList
            CollectionViewSource.GetDefaultView(UserList.ItemsSource).Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ShowEmployeeUsersList(CardId As String)
        Try
            Dim UList = UserDA.List(CardId)
            Dim UEmployee = UList.Single.Employee
            TxtEmployeeName.Text = UEmployee.Lastname & ", " & UEmployee.Name
            UserList.ItemsSource = UList
            CollectionViewSource.GetDefaultView(UserList.ItemsSource).Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ShowUserData(User As User)
        If User IsNot Nothing Then
            TxtCardId.Text = User.EmployeeCardId
            TxtEmployeeName.Text = User.Employee.Name
            TxtName.Text = User.Name
            TxtPassword.Password = User.Password
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
        If TxtPassword.Password = TxtConfirm.Password Then
            User.EmployeeCardId = TxtCardId.Text
            User.Name = TxtName.Text
            User.Password = TxtPassword.Password
            User.State = True
            Return User
        Else
            Return Nothing
        End If
    End Function

    Private Sub SaveUser()
        Try
            Dim User = GetUserData(New User)
            If User IsNot Nothing Then
                UserDA.Save(User)
                MessageBox.Show("Usuario agregado correctamente")
                ShowUsersList()
                ClearInputs()
            Else
                MessageBox.Show("Las contraseñas no coinciden")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DownUser()
        If SelectedUser IsNot Nothing Then
            UserDA.Down(SelectedUser)
            MessageBox.Show("Empleado eliminado correctamente")
            ShowUsersList()
            ClearInputs()
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

    Private Sub EmployeeList_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles UserList.MouseDoubleClick
        ShowUser()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As RoutedEventArgs) Handles BtnDelete.Click
        DownUser()
    End Sub

    Private Sub TxtCardId_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtCardId.KeyUp
        If e.Key = Key.Enter And TxtCardId.Text.Length = 8 Then
            ShowEmployeeUsersList(TxtCardId.Text)
        Else
            ShowUsersList()
        End If
    End Sub

    Private Sub Page_Initialized(sender As Object, e As EventArgs)
        ShowUsersList()
    End Sub
End Class
