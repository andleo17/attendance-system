Imports Stimulsoft
Imports Stimulsoft.Report

Class FrmConsultaAsistenciaMes
    Private Sub DatePicker_CalendarClosed(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim ruta() = Split(My.Computer.FileSystem.CurrentDirectory, "bin\", 2)
        Dim Report = New StiReport()
        Report.Load(ruta(0) & "reports\licence_report.mrt")
        Report.Show()
    End Sub
End Class
