Imports System.IO
Imports System.Windows.Threading
Imports Stimulsoft
Imports Stimulsoft.Report
Public Class FrmMain
	Private _User As Data.User
	Private WithEvents Timer As DispatcherTimer

	Public Property User As Data.User
		Get
			Return _User
		End Get
		Set(value As Data.User)
			_User = value
		End Set
	End Property

	Private Sub InitClock()
		Timer = New DispatcherTimer With {
			.Interval = TimeSpan.FromSeconds(1)
		}
		Timer.Start()
	End Sub

	Private Sub TickEvent(sender As Object, e As EventArgs) Handles Timer.Tick
        LblTimer.Content = Date.Now.ToString("dddd, dd \de MMMM \del yyyy HH:mm:ss")
    End Sub

    Private Function ShowPhoto(Employee As Data.Employee) As BitmapImage
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

    Private Sub ShowLoginForm()
        Dim FrmLogin = New FrmLogin()
        FrmLogin.ShowDialog()
        _User = FrmLogin.User
        If _User IsNot Nothing Then
            ShowUserData()
            Show()
        Else
            Windows.Application.Current.Shutdown()
        End If
    End Sub

    Private Sub ShowUserData()
        If _User.Employee.Photo IsNot Nothing Then
            ImgEmployee.Background = New ImageBrush(ShowPhoto(_User.Employee)) With {
                .Stretch = Stretch.UniformToFill
            }
        End If
        LblEmployee.Content = _User.Employee.Name + " " + _User.Employee.Lastname
    End Sub

    Private Sub ClearUserData()
        FrmContent.Content = Nothing
        LblEmployee.Content = Nothing
    End Sub

    Private Sub Logout()
        ClearUserData()
        Hide()
        ShowLoginForm()
    End Sub

    Private Sub ShowFrame(Frame As Page)
        FrmContent.Content = Frame
    End Sub

    Private Sub MnuLogout_Click(sender As Object, e As RoutedEventArgs) Handles MnuLogout.Click
        Logout()
    End Sub

    Private Sub MnuEmployee_Click(sender As Object, e As RoutedEventArgs) Handles MnuEmployee.Click
        ShowFrame(New FrmEmployee)
    End Sub

    Private Sub MnuUsers_Click(sender As Object, e As RoutedEventArgs) Handles MnuUsers.Click
        ShowFrame(New FrmUser)
    End Sub

    Private Sub MnuJustification_Click(sender As Object, e As RoutedEventArgs) Handles MnuJustification.Click
        ShowFrame(New FrmJustification)
    End Sub

    Private Sub FrmContent_Navigated(sender As Object, e As NavigationEventArgs) Handles FrmContent.Navigated

    End Sub

	Private Sub MnuPermisos_Click(sender As Object, e As RoutedEventArgs) Handles MnuPermisos.Click
		ShowFrame(New FrmPermisos)
	End Sub

	Private Sub MnuContratos_Click(sender As Object, e As RoutedEventArgs) Handles MnuContratos.Click
		ShowFrame(New FrmContrato)
	End Sub

	Private Sub MnuTipoLicencia_Click(sender As Object, e As RoutedEventArgs) Handles MnuTipoLicencia.Click
        ShowFrame(New FrmTipoLicencia)
    End Sub

    Private Sub FrmLicencias_Click(sender As Object, e As RoutedEventArgs) Handles FrmLicencias.Click
        ShowFrame(New FrmLicencia)
    End Sub

    Private Sub FrmConsultaAsistencia_Click(sender As Object, e As RoutedEventArgs) Handles FrmConsultaAsistencia.Click
        ShowFrame(New FrmConsultarAsistencia)
    End Sub

    Private Sub Window_Initialized(sender As Object, e As EventArgs)
        InitClock()
        ShowLoginForm()
    End Sub

    Private Sub MenuItem_Click_3(sender As Object, e As RoutedEventArgs)
        ShowFrame(New FrmHorario)
    End Sub

    Private Sub MnuAsistencias_Click(sender As Object, e As RoutedEventArgs) Handles MnuAsistencias.Click
        ShowFrame(New FrmAttendace)
    End Sub

    Private Sub MnuRegistrarAsistencias_Click(sender As Object, e As RoutedEventArgs) Handles MnuRegistrarAsistencias.Click
        ShowFrame(New FrmAttendaceList)
    End Sub

    Private Sub MnuTardanzas_Click(sender As Object, e As RoutedEventArgs) Handles MnuTardanzas.Click
        Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
        Dim Report = New StiReport()
        Report.Load(ruta(0) & "reports\delay_report.mrt")
        Report.Show()
    End Sub

    Private Sub MnuConsolidadoAsistencias_Click(sender As Object, e As RoutedEventArgs) Handles MnuConsolidadoAsistencias.Click
        Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
        Dim Report = New StiReport()
        Report.Load(ruta(0) & "reports\consolidate_attendance_report.mrt")
        Report.Show()
    End Sub

    Private Sub MnuLicencias_Click(sender As Object, e As RoutedEventArgs) Handles MnuLicencias.Click
        Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
        Dim Report = New StiReport()
        Report.Load(ruta(0) & "reports\licence_report.mrt")
        Report.Show()
    End Sub

    Private Sub MnuLicenciasTipo_Click(sender As Object, e As RoutedEventArgs) Handles MnuLicenciasTipo.Click
        Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
        Dim Report = New StiReport()
        Report.Load(ruta(0) & "reports\licence_report_group.mrt")
        Report.Show()
    End Sub

    Private Sub MnuInasistencias_Click(sender As Object, e As RoutedEventArgs) Handles MnuInasistencias.Click
        Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
        Dim Report = New StiReport()
        Report.Load(ruta(0) & "reports\non_attendance_report.mrt")
        Report.Show()
    End Sub

	Private Sub MnuEstadisticasTardanzas_Click(sender As Object, e As RoutedEventArgs) Handles MnuEstadisticasTardanzas.Click
		Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
		Dim Report = New StiReport()
		Report.Load(ruta(0) & "reports\pie_chart_ad_report.mrt")
		Report.Show()
	End Sub

    'Private Sub MnuAsistenciasEmpleado_Click(sender As Object, e As RoutedEventArgs) Handles MnuAsistenciasEmpleado.Click
    '       Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
    '       Dim Report = New StiReport()
    '       Report.Load(ruta(0) & "reports\pie_chart_af_report.mrt")
    '       Report.Show()
    '   End Sub
    Private Sub MnuEstadisticasInasistencias_Click(sender As Object, e As RoutedEventArgs) Handles MnuEstadisticasInasistencias.Click
        Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
        Dim Report = New StiReport()
        Report.Load(ruta(0) & "reports\pie_chart_af_report.mrt")
        Report.Show()
    End Sub

    Private Sub MenuItem_Click_4(sender As Object, e As RoutedEventArgs)
		'ShowFrame(New FrmMain)
	End Sub

    'Private Sub MnuAsistenciaEmp_Click(sender As Object, e As RoutedEventArgs) Handles MnuAsistenciasEmpleado.Click
    '    Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
    '    Dim Report = New StiReport()
    '    Report.Load(ruta(0) & "reports\non_attendance_emp_report.mrt")
    '    Report.Show()
    'End Sub

    Private Sub MnuAsistenciaEmp_Click_1(sender As Object, e As RoutedEventArgs) Handles MnuAsistenciaEmp.Click
        Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
        Dim Report = New StiReport()
        Report.Load(ruta(0) & "reports\non_attendance_emp_report.mrt")
        Report.Show()
    End Sub
End Class
