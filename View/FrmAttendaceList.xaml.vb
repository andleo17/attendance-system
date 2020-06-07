Imports Data
Imports Server

Class FrmAttendaceList
	Private Sub FilterEmployeeList()
		CollectionViewSource.GetDefaultView(EmployeeList.ItemsSource).Refresh()
	End Sub

	Private Sub BtnAdd_Click(sender As Object, e As RoutedEventArgs) Handles BtnAdd.Click
		NavigationService.GetNavigationService(Me).Content = New FrmLicencia
	End Sub

	Private Sub TxtFilter_TextChanged(sender As Object, e As RoutedEventArgs) Handles TxtFilter.TextChanged
		FilterEmployeeList()
	End Sub

	Private Sub Page_Initialized(sender As Object, e As EventArgs)
		Dim EList = (From S In AttendanceDA.List(Today)
					 Order By S.Schedule.Employee.Lastname Descending, S.Schedule.Employee.Name Descending
					 Select S).ToList
		EmployeeList.ItemsSource = EList
		Dim v = CollectionViewSource.GetDefaultView(EList)
		v.Filter = Function(S As ScheduleDetail)
					   Return S.Schedule.Employee.Name.ToLower.Contains(TxtFilter.Text.ToLower) Or
					   S.Schedule.Employee.Lastname.ToLower.Contains(TxtFilter.Text.ToLower)
				   End Function
	End Sub
End Class
