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
            ClearInputs()
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
            ClearInputs()
        End If
        MessageBox.Show("Seleccione tipo de licencia")
    End Function


    Private Function DeleteLicenseType()
        If SelectedLicenseType IsNot Nothing Then
            Dim Msg, Style, Title, Response
            Msg = "¿Seguro que desea eliminar el tipo de licencia"
            Style = vbYesNo + vbCritical + vbDefaultButton2
            Title = ".:SISTEMA DE ASISTENCIA:."
            Response = MsgBox(Msg, Style, Title)
            If Response = vbYes Then
                SelectedLicenseType = SetLicenseTypeDa(SelectedLicenseType)
                LicenseTypeDA.Delete(SelectedLicenseType)
                MessageBox.Show("Tipo de licencia eliminada")
                listLicenseType()
                ClearInputs()
            End If
        Else
            MessageBox.Show("Seleccione un tipo de licencia")
        End If
    End Function

    Private Sub ClearInputs()
        SelectedLicenseType = Nothing
        txtCodigo.Text = Nothing
        txtDescripcion.Text = Nothing
        txtDias.Text = Nothing
    End Sub

    Private Sub SearchLicenseType(LicenseType As LicenseType)
        Try
            LicenseType.Id = txtCodigo.Text
            Dim LT = LicenseTypeDA.Search(LicenseType)
            If LT IsNot Nothing Then
                LicenseType = LT
                SelectedLicenseType = LicenseType
                ShowLicenseType()
                txtCodigo.IsEnabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        saveLicenseType()
    End Sub

    Private Sub ListaTipoLicencia_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles ListaTipoLicencia.MouseDoubleClick
        ShowLicenseType()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
        UpdateLicenseType()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As RoutedEventArgs) Handles btnDelete.Click
        DeleteLicenseType()
    End Sub

    Private Sub btnClean_Click(sender As Object, e As RoutedEventArgs) Handles btnClean.Click
        ClearInputs()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click
        If txtCodigo.Text.Length <> 0 Then
            SearchLicenseType(New LicenseType)
        Else
            MessageBox.Show("Ingrese código de tipo de licensia")
        End If
    End Sub





End Class
