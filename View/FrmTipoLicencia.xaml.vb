Imports Server
Imports Data
Class FrmTipoLicencia
    Private SelectedLicenseType As LicenseType
    Private Sub listLicenseType()
        Try
            ListaTipoLicencia.ItemsSource = LicenseTypeDA.listarTiposLicencia

        Catch ex As Exception
            MessageBox.Show("Error al listar", "Sistema asistencia", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub Page_Initialized(sender As Object, e As EventArgs)
        listLicenseType()
    End Sub


    Private Sub saveLicenseType()
        Try
            Dim LicenseType = SetLicenseTypeDa(New LicenseType)
            LicenseTypeDA.save(LicenseType)
            MessageBox.Show("Tipo de licencia agregada de manera exitosa")
            listLicenseType()
        Catch ex As Exception
            MessageBox.Show("Error al registrar", MessageBoxImage.Error)
        End Try
    End Sub


    Private Function SetLicenseTypeDa(LicenseType As LicenseType) As LicenseType
        LicenseType.Description = txtDescripcion.Text
        LicenseType.MaximumDays = txtDias.Text
        Return LicenseType
    End Function



    Private Function ShowLicenseType()
        Try
            If ListaTipoLicencia.SelectedValue IsNot Nothing Then
                SelectedLicenseType = ListaTipoLicencia.SelectedValue
            End If
            txtCodigo.Text = SelectedLicenseType.Id
            txtDescripcion.Text = SelectedLicenseType.Description
            txtDias.Text = SelectedLicenseType.MaximumDays
            ListaTipoLicencia.SelectedValue = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function UpdateLicenseType()
        If SelectedLicenseType IsNot Nothing Then
            SelectedLicenseType = SetLicenseTypeDa(SelectedLicenseType)
            LicenseTypeDA.Update(SelectedLicenseType)
            MessageBox.Show("Tipo de licencia actualizado")
            ShowLicenseType()

        End If
        MessageBox.Show("Seleccione tipo de licencia")
    End Function

    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        saveLicenseType()
    End Sub

    Private Sub ListaTipoLicencia_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles ListaTipoLicencia.MouseDoubleClick
        ShowLicenseType()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
        UpdateLicenseType()
    End Sub
End Class
