Imports Server
Imports Data
Class FrmTipoLicencia

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
        LicenseType.Id = txtCodigo.Text
        LicenseType.Description = txtDescripcion.Text
        LicenseType.MaximumDays = txtDias.Text
        Return LicenseType
    End Function

    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        saveLicenseType()
    End Sub
End Class
