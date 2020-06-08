Imports Data
Imports Server
Class FrmLicencia

    Private SelectedLicense As License
    Private SelectedEmployee As Employee

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            CboType.ItemsSource = LicenseTypeDA.listarTiposLicencia()
            txtId.IsEnabled = False
            listLicense()
            txtEmpleado.IsEnabled = False
            Paint()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub listLicense()
        Try
            ListaLicencia.ItemsSource = LicenseDA.List()

        Catch ex As Exception
            MessageBox.Show("Error al listar", "Sistema asistencia", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub ShowLicense()
        Try
            If ListaLicencia.SelectedValue IsNot Nothing Then
                SelectedLicense = ListaLicencia.SelectedValue
            End If
            txtId.Text = SelectedLicense.Id
            txtDni.Text = SelectedLicense.EmployeeCardId
            'txtDoc.Text = SelectedLicense.Document
            InitialDate.SelectedDate = SelectedLicense.StartDate
            FinalDate.SelectedDate = SelectedLicense.FinishDate
            CboType.SelectedValue = SelectedLicense.LicenseTypeId
            chkState.IsChecked = SelectedLicense.State
            ListaLicencia.SelectedValue = Nothing
            btnSearch.IsEnabled = False
            txtDni.IsEnabled = False
            txtId.IsEnabled = False

            SearchEmployee(txtDni.Text)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SearchEmployee(EmployeeCardId As String)
        Try
            Dim E = EmployeeDA.Search(EmployeeCardId)
            If E IsNot Nothing Then
                txtEmpleado.Text = E.Name & " " & E.Lastname
            Else
                MessageBox.Show("DNI inválido o el empleado no existe")
                txtEmpleado.Text = " "
                txtDni.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub


    Private Function SetLicense(License As License) As License
        License.PresentationDate = Date.Now.Date
        License.FinishDate = FinalDate.SelectedDate
        License.StartDate = InitialDate.SelectedDate
        License.State = chkState.IsChecked
        License.LicenseTypeId = CboType.SelectedValue
        'License.Document = txtDoc.Text
        License.EmployeeCardId = txtDni.Text
        Return License
    End Function

    Private Sub ShowEmployeeLicenseList(CardId As String)
        Try
            Dim AList = LicenseDA.List(CardId)
            Dim AEmployee = AList.First.Employee
            txtEmpleado.Text = AEmployee.Lastname & ", " & AEmployee.Name
            ListaLicencia.ItemsSource = AList
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub SaveLicense()
        Try
            If SelectedLicense Is Nothing Then
                Dim License = SetLicense(New License)
                LicenseDA.Save(License)
                MessageBox.Show("Licencia registrada correctamente", MessageBoxImage.Information)
                listLicense()
                ClearInputs()
            Else
                MessageBox.Show("No se puede registrar la licencia", MessageBoxImage.Error)

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub


    Private Sub DeleteLicense()
        Try
            If SelectedLicense IsNot Nothing Then
                Dim Msg, Style, Title, Response
                Msg = "¿Seguro que desea eliminar la licencia"
                Style = vbYesNo + vbCritical + vbDefaultButton2
                Title = ".:SISTEMA DE ASISTENCIA:."
                Response = MsgBox(Msg, Style, Title)
                If Response = vbYes Then
                    SelectedLicense = SetLicense(SelectedLicense)
                    LicenseDA.Delete(SelectedLicense)
                    MessageBox.Show("Licencia eliminar correctamente", MessageBoxImage.Information)
                    listLicense()
                    ClearInputs()
                End If

            Else
                MessageBox.Show("No se pudo eliminar la licencia", MessageBoxImage.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub UpdateLicense()
        If SelectedLicense IsNot Nothing Then
            SelectedLicense = SetLicense(SelectedLicense)
            LicenseDA.Update(SelectedLicense)
            MessageBox.Show("Licencia actualizado")
            listLicense()
            ClearInputs()
        Else
            MessageBox.Show("Seleccione un permiso")
        End If

    End Sub


    Private Sub ClearInputs()
        SelectedLicense = Nothing
        txtDni.Text = Nothing
        'txtDoc.Text = Nothing
        txtEmpleado.Text = Nothing
        txtId.Text = Nothing
        FinalDate.SelectedDate = Nothing
        InitialDate.SelectedDate = Nothing
        txtDni.IsEnabled = True
        chkState.IsChecked = False
    End Sub


    Private Sub Paint()

    End Sub

    Private Sub ListaLicencia_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles ListaLicencia.MouseDoubleClick
        ShowLicense()
        Paint()
    End Sub

    Private Sub txtDni_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDni.KeyUp
        If e.Key.Equals(Key.Enter) Then
            If txtDni.Text.Length = 8 Then
                SearchEmployee(txtDni.Text)
                listLicense()
            Else
                MessageBox.Show("Ingrese DNI")
            End If
        End If
    End Sub

    Private Sub btnClean_Click(sender As Object, e As RoutedEventArgs) Handles btnClean.Click
        ClearInputs()
        listLicense()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
        UpdateLicense()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As RoutedEventArgs) Handles btnDelete.Click
        DeleteLicense()
    End Sub

    Private Sub txtId_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtId.TextChanged

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click

        If txtDni.Text.Length = 8 Then
            ShowEmployeeLicenseList(txtDni.Text)
        Else
            MessageBox.Show("Ingrese DNI")
        End If

    End Sub

    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        SaveLicense()
    End Sub

    Private Sub btnOpenFile_Click(sender As Object, e As RoutedEventArgs)
        'txtEditor.Text = FileOpen(btnOpenFile.FindName)
    End Sub
End Class
