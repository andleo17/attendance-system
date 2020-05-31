Public Class EmployeeControl
    Private Sub BtnShow_Click(sender As Object, e As RoutedEventArgs) Handles BtnShow.Click
        Dim f = sender.ToString
        Console.WriteLine("Mostrar " + f)
    End Sub

    Private Sub BtnModify_Click(sender As Object, e As RoutedEventArgs) Handles BtnModify.Click
        Dim f = sender.ToString
        Console.WriteLine("Modificar " + f)
    End Sub

    Private Sub BtnDown_Click(sender As Object, e As RoutedEventArgs) Handles BtnDown.Click
        Dim f = sender.ToString
        Console.WriteLine("Dar de baja " + f)
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As RoutedEventArgs) Handles BtnDelete.Click
        Dim f = e.Source
        Console.WriteLine("Borrar " + f)
    End Sub
End Class
