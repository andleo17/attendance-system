﻿Imports Data
Imports Server
Class FrmLicencia

    Private SelectedLicense As License

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            CboType.ItemsSource = LicenseTypeDA.listarTiposLicencia()
            listLicense()
            txtEmpleado.IsEnabled = False
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
            txtDoc.Text = SelectedLicense.Document
            InitialDate.SelectedDate = SelectedLicense.StartDate
            FinalDate.SelectedDate = SelectedLicense.FinishDate
            CboType.SelectedItem = SelectedLicense.LicenseTypeId
            chkState.IsChecked = SelectedLicense.State
            ListaLicencia.SelectedValue = Nothing
            btnSearch.IsEnabled = False
            txtDni.IsEnabled = False
            txtId.IsEnabled = False
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
        License.Document = txtDoc.Text
        License.EmployeeCardId = txtDni.Text
        License.LicenseTypeId = CboType.SelectedItem

        Return License
    End Function


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


    Private Sub UpdateLicense()
        Try
            If SelectedLicense IsNot Nothing Then
                Dim Msg, Style, Title, Response
                Msg = "¿Seguro que desea eliminar el permiso"
                Style = vbYesNo + vbCritical + vbDefaultButton2
                Title = ".:SISTEMA DE ASISTENCIA:."
                Response = MsgBox(Msg, Style, Title)
                If Response = vbYes Then
                    SelectedLicense = SetLicense(SelectedLicense)
                    LicenseDA.Update(SelectedLicense)
                    MessageBox.Show("Licencia modificada correctamente", MessageBoxImage.Information)
                    listLicense()
                    ClearInputs()
                End If

            Else
                MessageBox.Show("No se pudo modificar la licencia", MessageBoxImage.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub


    Private Sub DeleteLicense()
        If SelectedLicense IsNot Nothing Then
            SelectedLicense = SetLicense(SelectedLicense)
            LicenseDA.Delete(SelectedLicense)
            MessageBox.Show("Licencia eliminada correctamente", MessageBoxImage.Information)

        End If
    End Sub

    Private Sub ClearInputs()
        SelectedLicense = Nothing
        txtDni.Text = Nothing
        txtDoc.Text = Nothing
        txtEmpleado.Text = Nothing
        txtId.Text = Nothing
        FinalDate.SelectedDate = Nothing
        InitialDate.SelectedDate = Nothing
        txtId.IsEnabled = True
        txtDni.IsEnabled = True
        chkState.IsChecked = False
    End Sub


    Private Sub ListaLicencia_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles ListaLicencia.MouseDoubleClick
        ShowLicense()
    End Sub

    Private Sub txtDni_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDni.KeyUp
        If e.Key.Equals(Key.Enter) Then
            If txtDni.Text.Length = 8 Then
                SearchEmployee(txtDni.Text)
            Else
                MessageBox.Show("Ingrese DNI")
            End If
        End If
    End Sub

    Private Sub btnClean_Click(sender As Object, e As RoutedEventArgs) Handles btnClean.Click
        ClearInputs()
    End Sub

End Class
