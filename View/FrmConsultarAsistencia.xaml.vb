Imports Server
Imports Data

Class FrmConsultarAsistencia
    Private Sub List()
        Dim A = AttendanceDA.List()
        If A.Count > 0 Then
            AttendanceList.ItemsSource = A
        Else
            MessageBox.Show("Sin registros")
        End If
    End Sub

    Private Sub SearchDate()
        Try
            Dim Flag = True
            Dim Results = New List(Of Attendance)
            If ChkInitial.IsChecked And ChkFinal.IsChecked And ChkCardId.IsChecked Then
                Results = AttendanceDA.SearchDate(InitialDate.SelectedDate, FinalDate.SelectedDate, TxtCardId.Text)
            Else
                If ChkInitial.IsChecked And ChkFinal.IsChecked Then
                    Results = AttendanceDA.SearchDate(InitialDate.SelectedDate, FinalDate.SelectedDate)
                Else
                    If ChkInitial.IsChecked And ChkCardId.IsChecked Then
                        Results = AttendanceDA.SearchDate(InitialDate.SelectedDate, TxtCardId.Text)
                    Else
                        If ChkInitial.IsChecked Then
                            Results = AttendanceDA.SearchDate(InitialDate.SelectedDate)
                        Else
                            If ChkCardId.IsChecked Then
                                Results = AttendanceDA.SearchDate(TxtCardId.Text)
                            Else
                                MessageBox.Show("Seleccione un criterio de búsqueda")
                                flag = False
                            End If
                        End If
                    End If
                End If
            End If

            If Results.Count <= 0 And Flag Then
                MessageBox.Show("Los datos ingresados no devuelven resultados")
            Else
                AttendanceList.ItemsSource = Results
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Function Validate(Selection As Integer) As Boolean
    '    If Selection = 1 Then
    '        If (InitialDate.SelectedDate Is Nothing Or FinalDate.SelectedDate Is Nothing) Or (InitialDate.SelectedDate >= FinalDate.SelectedDate) Or TxtCardId.Text.Length <> 8 Then
    '            MessageBox.Show("Los datos ingesados son incorrectos")
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    Else
    '        If Selection = 2 Then
    '            If (InitialDate.SelectedDate Is Nothing Or FinalDate.SelectedDate Is Nothing) Or (InitialDate.SelectedDate >= FinalDate.SelectedDate) Then
    '                MessageBox.Show("Los datos ingesados son incorrectos")
    '                Return False
    '            Else
    '                Return True
    '            End If
    '        Else
    '            If Selection = 3 Then
    '                If InitialDate.SelectedDate Is Nothing Or TxtCardId.Text.Length <> 8 Then
    '                    MessageBox.Show("Los datos ingesados son incorrectos")
    '                    Return False
    '                Else
    '                    Return True
    '                End If
    '            Else
    '                If Selection = 4 Then
    '                    If InitialDate.SelectedDate Is Nothing Then
    '                        MessageBox.Show("Los datos ingesados son incorrectos")
    '                        Return False
    '                    Else
    '                        Return True
    '                    End If
    '                Else
    '                    If TxtCardId.Text.Length <> 8 Then
    '                        MessageBox.Show("Los datos ingesados son incorrectos")
    '                        Return False
    '                    Else
    '                        Return True
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If
    'End Function

    Private Sub ClearInputs()
        TxtCardId.Text = Nothing
        InitialDate.SelectedDate = Nothing
        FinalDate.SelectedDate = Nothing
    End Sub
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        SearchDate()
    End Sub

    Private Sub Grid_Loaded(sender As Object, e As RoutedEventArgs)
        ChkFinal.IsEnabled = False
    End Sub

    Private Sub ChkInitial_Click(sender As Object, e As RoutedEventArgs) Handles ChkInitial.Click
        If ChkInitial.IsChecked Then
            ChkFinal.IsEnabled = True
        Else
            ChkFinal.IsChecked = False
            ChkFinal.IsEnabled = False
        End If
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        List()
    End Sub
    
    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        ClearInputs()
    End Sub
End Class
