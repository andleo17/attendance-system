Imports Data
Imports Server

Public Class EmployeeControl
    Private Sub Reload()
        NavigationService.GetNavigationService(Me).Refresh()
    End Sub

    Private Sub OpenDetails(Mode As Integer)
        NavigationService.GetNavigationService(Me).Content = New FrmEmployeeForm With {
            .SelectedEmployee = DataContext,
            .Mode = Mode
        }
    End Sub

    Private Sub DownEmployee()
        Try
            EmployeeDA.Down(DataContext)
            MessageBox.Show("Empleado dado de baja")
            Reload()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtnShow_Click(sender As Object, e As RoutedEventArgs) Handles BtnShow.Click
        OpenDetails(2)
    End Sub

    Private Sub BtnModify_Click(sender As Object, e As RoutedEventArgs) Handles BtnModify.Click
        OpenDetails(1)
    End Sub

    Private Sub BtnDown_Click(sender As Object, e As RoutedEventArgs) Handles BtnDown.Click
        DownEmployee()
    End Sub
End Class
