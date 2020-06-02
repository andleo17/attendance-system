Imports Data
Imports Server

Class FrmPermisos
    Private SelectedPermission As Permission

    Private Function SearchEmployee(Employee As Employee) As Employee
        Try
            Employee.CardId = txtCardID.Text
            lblName.Content = EmployeeDA.Search(Employee).Name & " " & EmployeeDA.Search(Employee).Lastname
        Catch ex As Exception
            MessageBox.Show("DNI inválido o el Emlpleado no existe")
        End Try
        Return Employee
    End Function

    Private Function SetPermissionData(Permission As Permission) As Permission
        Permission.PresentationDate = Date.Now.Date
        Permission.Date = calDate.SelectedDate
        Permission.Motive = New TextRange(txtMotive.Document.ContentStart, txtMotive.Document.ContentEnd).Text
        Permission.State = chkActive.IsChecked
        Permission.EmployeeCardId = SearchEmployee(New Employee).CardId
        Return Permission
    End Function

    Private Sub SavePermission()
        Try
            Dim Permission = SetPermissionData(New Permission)
            PermissionDA.Save(Permission)
            MessageBox.Show("Permiso agregado correctamente")
            ShowPermissionList()
        Catch ex As Exception
            MessageBox.Show("Error al registrar")
        End Try
    End Sub

    Private Sub UpdatePermission()
        SelectedPermission = SetPermissionData(SelectedPermission)
        PermissionDA.Update(SelectedPermission)
        MessageBox.Show("Permiso actualizado")
        ShowPermissionList()
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
        txtMotive.Document.Blocks.Clear()
    End Sub

    Private Sub ShowPermission()
        Try
            ClearInputs()
            SelectedPermission = PermissionList.SelectedValue
            txtCardID.Text = SelectedPermission.EmployeeCardId
            TxtId.Text = SelectedPermission.Id
            txtMotive.Document.Blocks.Add(New Paragraph(New Run(SelectedPermission.Motive)))
            calDate.SelectedDate = SelectedPermission.Date
            chkActive.IsChecked = SelectedPermission.State
            btnSearchEm_Click(Nothing, Nothing)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Sub ShowPermissionData(Permission As Permission)
    '    If Permission IsNot Nothing Then
    '        ClearInputs()
    '        txtCardID.Text = Permission.EmployeeCardId
    '        TxtId.Text = Permission.Id
    '        txtMotive.Document.Blocks.Add(New Paragraph(New Run(Permission.Motive)))
    '        calDate.SelectedDate = Permission.Date
    '        chkActive.IsChecked = Permission.State
    '        btnSearchEm_Click(Nothing, Nothing)
    '    End If
    'End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        ShowPermissionList()
    End Sub

    Private Sub PermissionList_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles PermissionList.MouseDoubleClick
        ShowPermission()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click

    End Sub

    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        SavePermission()
    End Sub

    Private Sub btnSearchEm_Click(sender As Object, e As RoutedEventArgs) Handles btnSearchEm.Click
        If txtCardID.Text IsNot "" Then
            SearchEmployee(New Employee)
        Else
            MessageBox.Show("Ingrese DNI")
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
        UpdatePermission()
    End Sub
End Class
