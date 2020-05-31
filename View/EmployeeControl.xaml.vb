Imports Data
Imports Server

Public Class EmployeeControl
    Private Sub DownEmployee()
        Try
            EmployeeDA.Down(DataContext)
            MessageBox.Show("Empleado dado de baja")
            FrmEmployee.StaticFrmContent.NavigationService.Content = New FrmEmployeeList
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DeleteEmployee()
        Try
            EmployeeDA.Delete(DataContext)
            MessageBox.Show("Empleado eliminado correctamente")
            FrmEmployee.StaticFrmContent.NavigationService.Content = New FrmEmployeeList
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtnShow_Click(sender As Object, e As RoutedEventArgs) Handles BtnShow.Click
        FrmEmployee.SetMode(DataContext, 2)
    End Sub

    Private Sub BtnModify_Click(sender As Object, e As RoutedEventArgs) Handles BtnModify.Click
        FrmEmployee.SetMode(DataContext, 1)
    End Sub

    Private Sub BtnDown_Click(sender As Object, e As RoutedEventArgs) Handles BtnDown.Click
        DownEmployee()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As RoutedEventArgs) Handles BtnDelete.Click
        DeleteEmployee()
    End Sub
End Class
