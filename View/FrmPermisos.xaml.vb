Imports Data
Imports Server

Class FrmPermisos
    Private SelectedPermission As Permission

    Private Function SearchEmployee(Employee As Employee) As Employee
        Try
            Employee.CardId = txtCardID.Text
            Dim E = EmployeeDA.Search(Employee)
            If E IsNot Nothing Then
                Employee = E
                txtEmpleado.Text = E.Name & " " & E.Lastname
            Else
                Employee = Nothing
                MessageBox.Show("DNI inválido o el Emlpleado no existe")
                txtEmpleado.Text = Nothing
                txtCardID.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        Return Employee
    End Function



    Private Function SetPermissionData(Permission As Permission) As Permission
        If Permission.Id = 0 Then
            Permission.PresentationDate = Date.Now.Date
        End If
        Permission.Date = calDate.SelectedDate
        Permission.Motive = txtMotive.Text
        Permission.State = chkActive.IsChecked
        Permission.EmployeeCardId = SearchEmployee(New Employee).CardId
        Return Permission
    End Function

    Private Function SavePermission() As Boolean
        Try
            If SelectedPermission Is Nothing Then
                If ValidateSave() Then
                    Dim Permission = SetPermissionData(New Permission)
                    PermissionDA.Save(Permission)
                    MessageBox.Show("Permiso agregado correctamente")
                    ShowPermissionList()
                    ClearInputs()
                    Return True
                Else
                    Return False
                End If
            Else
                MessageBox.Show("Operación no disponible")
                Return True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function

    Private Sub UpdatePermission()
        If SelectedPermission IsNot Nothing Then
            If ValidateUpdate() Then
                SelectedPermission = SetPermissionData(SelectedPermission)
                PermissionDA.Update(SelectedPermission)
                MessageBox.Show("Permiso actualizado")
                ShowPermissionList()
                ClearInputs()
            End If
        Else
            MessageBox.Show("Seleccione un permiso")
        End If

    End Sub

    Private Sub DeletePermission()
        If SelectedPermission IsNot Nothing Then
            Dim Msg, Style, Title, Response
            Msg = "¿Seguro que desea eliminar el permiso"
            Style = vbYesNo + vbCritical + vbDefaultButton2
            Title = ".:SISTEMA DE ASISTENCIA:."
            Response = MsgBox(Msg, Style, Title)
            If Response = vbYes Then
                SelectedPermission = SetPermissionData(SelectedPermission)
                PermissionDA.Delete(SelectedPermission)
                MessageBox.Show("Permiso eliminado")
                ShowPermissionList()
                ClearInputs()
            End If
        Else
            MessageBox.Show("Seleccione un permiso")
        End If
    End Sub

    Private Sub ShowPermissionList()
        Try
            PermissionList.ItemsSource = PermissionDA.List()
            ClearInputs()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ClearInputs()
        SelectedPermission = Nothing
        For Each C In Form.Children
            If TypeOf C Is TextBox Then
                C.Text = Nothing
            ElseIf TypeOf C Is CheckBox Then
                C.IsChecked = False
            End If
        Next
        txtCardID.Text = Nothing
        txtEmpleado.Text = Nothing
        calDate.SelectedDate = Nothing
        TxtId.IsEnabled = True
        btnSave.Content = "NUEVO"
    End Sub

    Private Function ValidateSave() As Boolean
        If SearchEmployee(New Employee) Is Nothing Then
            Return False
        Else
            If txtMotive.Text.Length <= 0 Then
                MessageBox.Show("Ingrese el motivo")
                Return False
            Else
                If calDate.SelectedDate Is Nothing Or calDate.SelectedDate <= Date.Now.Date Then
                    MessageBox.Show("Ingrese una fecha válida")
                    Return False
                Else
                    Return True
                End If
            End If
        End If
    End Function

    Private Function ValidateUpdate() As Boolean
        If SearchEmployee(New Employee) Is Nothing Then
            Return False
        Else
            If txtMotive.Text.Length <= 0 Then
                MessageBox.Show("Ingrese el motivo")
                Return False
            Else
                If calDate.SelectedDate Is Nothing Or calDate.SelectedDate < SelectedPermission.Date Then
                    MessageBox.Show("Ingrese una fecha válida")
                    Return False
                Else
                    Return True
                End If
            End If
        End If
    End Function

    Private Sub ShowPermission()
        Try
            If PermissionList.SelectedValue IsNot Nothing Then
                SelectedPermission = PermissionList.SelectedValue
            End If
            txtCardID.Text = SelectedPermission.EmployeeCardId
            TxtId.Text = SelectedPermission.Id
            txtMotive.Text = SelectedPermission.Motive
            calDate.SelectedDate = SelectedPermission.Date
            chkActive.IsChecked = SelectedPermission.State
            btnSearchEm_Click(Nothing, Nothing)
            PermissionList.SelectedValue = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SearchPermission(Permission As Permission)
        Try
            Permission.Id = TxtId.Text
            Dim P = PermissionDA.Search(Permission)
            If P IsNot Nothing Then
                Permission = P
                SelectedPermission = Permission
                ShowPermission()
                TxtId.IsEnabled = False
            Else
                MessageBox.Show("No se encontraron resultados")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        txtEmpleado.IsEnabled = False
        ShowPermissionList()
    End Sub

    Private Sub PermissionList_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles PermissionList.MouseDoubleClick
        ClearInputs()
        ShowPermission()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click
        If TxtId.Text.Length > 0 Then
            SearchPermission(New Permission)
        Else
            MessageBox.Show("Ingrese código de permiso")
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        If btnSave.Content = "REGISTRAR" Then
            If SavePermission() Then
                ClearInputs()
                btnSave.Content = "NUEVO"
            End If
        Else
            ClearInputs()
            btnSave.Content = "REGISTRAR"
        End If

    End Sub

    Private Sub btnSearchEm_Click(sender As Object, e As RoutedEventArgs) Handles btnSearchEm.Click
        If txtCardID.Text.Length = 8 Then
            SearchEmployee(New Employee)
        Else
            MessageBox.Show("Ingrese DNI")
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
        UpdatePermission()
    End Sub

    Private Sub btnClean_Click(sender As Object, e As RoutedEventArgs) Handles btnClean.Click
        ClearInputs()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As RoutedEventArgs) Handles btnDelete.Click
        DeletePermission()
    End Sub

    Private Sub txtCardID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCardID.KeyDown
        If e.Key.Equals(Key.Enter) Then
            btnSearchEm_Click(Nothing, Nothing)
        End If
    End Sub
End Class
