Imports Stimulsoft
Imports Stimulsoft.Report

Class FrmConsultaAsistenciaMes
    Private Sub DatePicker_CalendarClosed(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim Report = New StiReport()
        Report.Load("C://Users//USUARIO//Documents//GitHub//attendance-system//View//reports//licence_report.mrt")
        Report.Show()

    End Sub
End Class
