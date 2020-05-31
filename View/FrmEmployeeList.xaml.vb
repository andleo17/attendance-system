Imports Data
Imports Server

Class FrmEmployeeList
    Private Sub FilterEmployeeList()
        CollectionViewSource.GetDefaultView(EmployeeList.ItemsSource).Refresh()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As RoutedEventArgs) Handles BtnAdd.Click
        NavigationService.GetNavigationService(Me).Content = New FrmEmployeeForm
    End Sub

    Private Sub TxtFilter_TextChanged(sender As Object, e As RoutedEventArgs) Handles TxtFilter.TextChanged
        FilterEmployeeList()
    End Sub

    Private Sub Page_Initialized(sender As Object, e As EventArgs)
        Dim EList = (From Employee In EmployeeDA.List()
                     Order By Employee.Lastname
                     Select Employee).ToList
        EmployeeList.ItemsSource = EList
        Dim v = CollectionViewSource.GetDefaultView(EList)
        v.Filter = Function(Employee As Employee)
                       Return Employee.Name.ToLower.Contains(TxtFilter.Text.ToLower) Or
                       Employee.Lastname.ToLower.Contains(TxtFilter.Text.ToLower)
                   End Function
    End Sub
End Class
