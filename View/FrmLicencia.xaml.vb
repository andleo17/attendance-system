Imports Data
Imports Server
Class FrmLicencia

    Private SelectedLicense As License

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            CboType.ItemsSource = LicenseTypeDA.listarTiposLicencia()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Sub ShowLicenseType()
    '    Try

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub


End Class
