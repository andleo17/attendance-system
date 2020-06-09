Imports System.IO
Imports Data
Imports Server

Class AttendaceControl
	Dim AttendanceToday As Attendance
	Dim SD As ScheduleDetail
	Public Flag = True

	Private Function Format(Hour As TimeSpan) As String
		Dim D = Today.Add(Hour)
		Return D.ToString("T")
	End Function

	Private Function ShowPhoto(Employee As Employee) As BitmapImage
		Dim Path = AppDomain.CurrentDomain.BaseDirectory
		Dim Folder = Path & "/temp/"
		Dim FullPath = Folder & Employee.PhotoName

		If Not Directory.Exists(Folder) Then
			Directory.CreateDirectory(Folder)
		End If

		If Not File.Exists(FullPath) Then
			File.WriteAllBytes(FullPath, Employee.Photo)
		End If

		Dim ResourceUri = New Uri(FullPath, UriKind.Absolute)
		Return New BitmapImage(ResourceUri)
	End Function

	Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
		SD = DataContext
		Dim Tolerance = TimeSpan.FromMinutes(10)
		If SD.Schedule.Employee.Photo IsNot Nothing Then
			ImgEmployee.Background = New ImageBrush(ShowPhoto(SD.Schedule.Employee)) With {
				.Stretch = Stretch.UniformToFill
			}
		End If
		TxtInHour.Content = Format(SD.InHour)
		TxtOutHour.Content = Format(SD.OutHour)
		Try
			AttendanceToday = SD.Schedule.Employee.Attendance.First(Function(A) A.Date = Today)
			TxtInHour.Content = Format(AttendanceToday.InHour)
			If AttendanceToday.OutHour IsNot Nothing Then
				TxtOutHour.Content = Format(AttendanceToday.OutHour)
			End If
		Catch ex As Exception
			AttendanceToday = Nothing
			Flag = False
		End Try
		If AttendanceToday Is Nothing Then
			TxtInHour.Foreground = New SolidColorBrush(Colors.Gray)
			TxtOutHour.Foreground = New SolidColorBrush(Colors.Gray)
		Else
			If AttendanceToday.InHour > SD.InHour.Add(Tolerance) Then
				TxtInHour.Foreground = New SolidColorBrush(Colors.Crimson)
			End If
			If AttendanceToday.OutHour IsNot Nothing Then
				If AttendanceToday.OutHour < SD.OutHour.Subtract(Tolerance) Then
					TxtOutHour.Foreground = New SolidColorBrush(Colors.Crimson)
				End If
			End If
		End If
	End Sub

	Private Sub BtnShow_Click(sender As Object, e As RoutedEventArgs) Handles BtnShow.Click
		'Agregar asistencia
		' Comparar si llegó tarde
		Dim Ahorita = Now.TimeOfDay
		Dim Tolerance = TimeSpan.FromMinutes(10)
		If SD.OutHour > Ahorita Then
			If AttendanceToday Is Nothing Then
				If Ahorita >= SD.InHour.Subtract(Tolerance) Then
					TxtInHour.Content = Format(Ahorita)
					If Ahorita <= SD.InHour.Add(Tolerance) Then
						TxtInHour.Foreground = New SolidColorBrush(Colors.White)
						Try
							Dim A = New Attendance
							A.InHour = Ahorita
							A.EmployeeCardId = TxtDni.Content
							A.Date = Date.Now.Date
							AttendanceDA.Save(A)
							AttendanceToday = A
							Flag = True
						Catch ex As Exception
						End Try
					Else
						TxtInHour.Foreground = New SolidColorBrush(Colors.Crimson)
						Try
							Dim A = New Attendance
							A.InHour = Ahorita
							A.EmployeeCardId = TxtDni.Content
							A.Date = Date.Now.Date
							AttendanceDA.Save(A)
							AttendanceToday = A
							Flag = True
						Catch ex As Exception
						End Try
					End If
				End If
			End If
		End If
	End Sub

	Private Sub BtnModify_Click(sender As Object, e As RoutedEventArgs) Handles BtnModify.Click
		'Actualizar asistencia
		If Flag Then
			If AttendanceToday.OutHour Is Nothing Then
				Dim Ahorita = Now.TimeOfDay
				Dim Tolerance = TimeSpan.FromMinutes(10)
				TxtOutHour.Foreground = New SolidColorBrush(Colors.White)
				If Ahorita >= SD.OutHour.Add(Tolerance) Then
					TxtOutHour.FontFamily = New FontFamily("Segoe UI Semibold")
				ElseIf Ahorita <= SD.OutHour.Subtract(Tolerance) Then
					TxtOutHour.Foreground = New SolidColorBrush(Colors.Crimson)
				End If
				TxtOutHour.Content = Format(Ahorita)
				AttendanceToday.OutHour = Ahorita
				AttendanceDA.Update(AttendanceToday)
				Flag = False
			End If
		End If

	End Sub
End Class
