Imports Data

Class AttendaceControl
	Dim AttendanceToday As Attendance
	Dim SD As ScheduleDetail

	Private Function Format(Hour As TimeSpan) As String
		Dim D = Today.Add(Hour)
		Return D.ToString("T")
	End Function

	Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
		SD = DataContext
		TxtInHour.Content = Format(SD.InHour)
		TxtOutHour.Content = Format(SD.OutHour)
		Try
			AttendanceToday = SD.Schedule.Employee.Attendance.First(Function(A) A.Date = Today)
		Catch ex As Exception
			AttendanceToday = Nothing
		End Try
		If AttendanceToday Is Nothing Then
			TxtInHour.Foreground = New SolidColorBrush(Colors.Gray)
			TxtOutHour.Foreground = New SolidColorBrush(Colors.Gray)
		Else
			If AttendanceToday.OutHour Is Nothing Then
				TxtOutHour.Foreground = New SolidColorBrush(Colors.Gray)
			End If
		End If
	End Sub

	Private Sub BtnShow_Click(sender As Object, e As RoutedEventArgs) Handles BtnShow.Click
		'Agregar asistencia
		' Comparar si llegó tarde
		Dim Ahorita = Now.TimeOfDay
		Dim Tolerance = TimeSpan.FromMinutes(10)
		If Ahorita >= SD.InHour.Subtract(Tolerance) Then
			TxtInHour.Content = Format(Ahorita)
			If Ahorita <= SD.InHour.Add(Tolerance) Then
				TxtInHour.Foreground = New SolidColorBrush(Colors.White)
			Else
				TxtInHour.Foreground = New SolidColorBrush(Colors.Crimson)
			End If
		End If
	End Sub

	Private Sub BtnModify_Click(sender As Object, e As RoutedEventArgs) Handles BtnModify.Click
		'Actualizar asistencia
		Dim Ahorita = Now.TimeOfDay
		Dim Tolerance = TimeSpan.FromMinutes(10)
		TxtOutHour.Foreground = New SolidColorBrush(Colors.White)
		If Ahorita >= SD.OutHour.Add(Tolerance) Then
			TxtOutHour.FontFamily = New FontFamily("Segoe UI Semibold")
		ElseIf Ahorita <= SD.OutHour.Subtract(Tolerance) Then
			TxtInHour.Foreground = New SolidColorBrush(Colors.Crimson)
		End If
		TxtOutHour.Content = Format(Ahorita)
	End Sub
End Class
