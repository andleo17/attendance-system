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
                lblName.Content = E.Name & " " & E.Lastname
            Else
                MessageBox.Show("DNI inválido o el Emlpleado no existe")
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
        Permission.Motive = New TextRange(txtMotive.Document.ContentStart, txtMotive.Document.ContentEnd).Text
        Permission.State = chkActive.IsChecked
        Permission.EmployeeCardId = SearchEmployee(New Employee).CardId
        Return Permission
    End Function

    Private Sub SavePermission()
        Try
            If SelectedPermission Is Nothing Then
                If ValidateSave() Then
                    Dim Permission = SetPermissionData(New Permission)
                    PermissionDA.Save(Permission)
                    MessageBox.Show("Permiso agregado correctamente")
                    ShowPermissionList()
                    ClearInputs()
                End If

            Else
                MessageBox.Show("Operación no disponible, limpie para continuar")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub UpdatePermission()
        If SelectedPermission IsNot Nothing Then
            If ValidateSave() Then
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
        txtMotive.Document.Blocks.Clear()
        txtCardID.Text = Nothing
        lblName.Content = "------------------"
        calDate.SelectedDate = Nothing
    End Sub

    Private Function ValidateSave() As Boolean
        If SearchEmployee(New Employee).Name = "" Then
            Return False
        Else
            If New TextRange(txtMotive.Document.ContentStart, txtMotive.Document.ContentEnd).IsEmpty Then
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

    Private Sub btnClean_Click(sender As Object, e As RoutedEventArgs) Handles btnClean.Click
        ClearInputs()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As RoutedEventArgs) Handles btnDelete.Click
        DeletePermission()
    End Sub
End Class
