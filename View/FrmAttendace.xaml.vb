Imports Data
Imports Server
Class FrmAttendace
    Private SelectedAttendace As User


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
        Attendance.Id = txtDni.Text
        Attendance.Date = AttendaceDate.SelectedDate
        Attendance.InHour = txtStarTime.
        Attendance.OutHour = txtFinalTime.Text
        Attendance.EmployeeCardId = txtDni.Text
        Return License
    End Function





    Private Sub txtDni_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDni.KeyUp
        If e.Key = Key.Enter And txtDni.Text.Length = 8 Then
            SearchEmployee(txtDni.Text)
        Else
            txtEmpleado.Text = Nothing
        End If
    End Sub

    Private Sub Page_Initialized(sender As Object, e As EventArgs)
        listAttendace()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As RoutedEventArgs) Handles btnDelete.Click

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As RoutedEventArgs) Handles btnSearch.Click
        ShowEmployeeAttendaceList(txtDni.Text)
    End Sub
End Class
