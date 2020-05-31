Imports Data
Imports Server

Class FrmEmployeeList
    Private Sub ShowEmployeesList()
        Try
            EmployeeList.ItemsSource = EmployeeDA.List()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub FindEmployee(CardId As String)
        Try
            Dim EList = EmployeeList.Items.Cast(Of Employee)
        Catch ex As Exception
            MessageBox.Show("No se encuentra el empleado")
        End Try
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        ShowEmployeesList()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As RoutedEventArgs) Handles BtnAdd.Click
        FrmEmployee.StaticFrmContent.Content = New FrmEmployeeForm
    End Sub
End Class
