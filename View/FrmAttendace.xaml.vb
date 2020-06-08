Imports Data
Imports Server
Class FrmAttendace
    Private SelectedAttendace As Attendance


    Private Sub listAttendace()
        Try
            ListaAttendace.ItemsSource = AttendanceDA.List

        Catch ex As Exception
            MessageBox.Show("Error al listar", "Sistema asistencia", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub ShowEmployeeAttendaceList(CardId As String)
        Try
            Dim AList = AttendanceDA.List(CardId)
            Dim AEmployee = AList.First.Employee
            txtEmpleado.Text = AEmployee.Lastname & ", " & AEmployee.Name
            ListaAttendace.ItemsSource = AList
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

    Private Function SetAttendace(Attendance As Attendance) As Attendance
        Attendance.Date = AttendaceDate.SelectedDate
        Attendance.InHour = TimeSpan.Parse(TPInHour.Text)
        Attendance.OutHour = TimeSpan.Parse(TPOutHour.Text)
        Attendance.EmployeeCardId = txtDni.Text
        Return Attendance
    End Function


    Private Sub ShowAttendace()
        Try
            If ListaAttendace.SelectedValue IsNot Nothing Then
                SelectedAttendace = ListaAttendace.SelectedValue
            End If
            txtId.Text = SelectedAttendace.Id
            txtDni.Text = SelectedAttendace.EmployeeCardId
            TPInHour.Value = SelectedAttendace.InHour.ToString
            TPOutHour.Value = SelectedAttendace.OutHour.ToString
            AttendaceDate.SelectedDate = SelectedAttendace.Date
            ListaAttendace.SelectedValue = Nothing
            btnSearch.IsEnabled = False
            txtDni.IsEnabled = False
            txtId.IsEnabled = False

            SearchEmployee(txtDni.Text)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SaveAttendace()
        Try
            If SelectedAttendace Is Nothing Then
                Dim Attendance = SetAttendace(New Attendance)
                AttendanceDA.Save(Attendance)
                MessageBox.Show("Licencia registrada correctamente", MessageBoxImage.Information)
                listAttendace()
                ClearInputs()
            Else
                MessageBox.Show("No se puede registrar la licencia", MessageBoxImage.Error)

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub UpdateAttendace()
        If SelectedAttendace IsNot Nothing Then
            SelectedAttendace = SetAttendace(SelectedAttendace)
            AttendanceDA.Update(SelectedAttendace)
            MessageBox.Show("Asistencia actualizada")
            listAttendace()
            ClearInputs()
        Else
            MessageBox.Show("Seleccione una asistencia")
        End If

    End Sub

    Private Sub DeleteAttendace()
        Try
            If SelectedAttendace IsNot Nothing Then
                Dim Msg, Style, Title, Response
                Msg = "¿Seguro que desea eliminar la asistencia"
                Style = vbYesNo + vbCritical + vbDefaultButton2
                Title = ".:SISTEMA DE ASISTENCIA:."
                Response = MsgBox(Msg, Style, Title)
                If Response = vbYes Then
                    SelectedAttendace = SetAttendace(SelectedAttendace)
                    AttendanceDA.Delete(SelectedAttendace)
                    MessageBox.Show("Asistencia eliminada correctamente", MessageBoxImage.Information)
                    listAttendace()
                    ClearInputs()
                    btnSearch.IsEnabled = True
                End If

            Else
                MessageBox.Show("No se pudo eliminar la asistencia", MessageBoxImage.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub ClearInputs()
        SelectedAttendace = Nothing
        txtDni.Text = Nothing
        txtEmpleado.Text = Nothing
        txtId.Text = Nothing
        AttendaceDate.SelectedDate = Nothing
        TPInHour.Value = Nothing
        TPOutHour.Value = Nothing
        txtDni.IsEnabled = True
        btnSearch.IsEnabled = True
        listAttendace()
    End Sub

    Private Sub ListaAttendace_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles ListaAttendace.MouseDoubleClick
        ShowAttendace()
    End Sub

    Private Sub txtDni_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDni.KeyUp
        If e.Key = Key.Enter And txtDni.Text.Length = 8 Then
            SearchEmployee(txtDni.Text)
        Else
            txtEmpleado.Text = Nothing
        End If
    End Sub

    Private Sub Page_Initialized(sender As Object, e As EventArgs)
        listAttendace()
        txtId.IsEnabled = False
        txtEmpleado.IsEnabled = False
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As RoutedEventArgs) Handles btnDelete.Click
        DeleteAttendace()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click
        ShowEmployeeAttendaceList(txtDni.Text)
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
        UpdateAttendace()
    End Sub

    Private Sub btnClean_Click(sender As Object, e As RoutedEventArgs) Handles btnClean.Click
        ClearInputs()
    End Sub
End Class
