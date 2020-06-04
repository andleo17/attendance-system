Imports Server
Imports Data

Class FrmConsultarAsistencia

    Private Sub SearchDate()
        Try
            AttendanceList.ItemsSource = AttendanceDA.SearchDate(InitialDate.SelectedDate, FinalDate.SelectedDate)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        SearchDate()
    End Sub
End Class
