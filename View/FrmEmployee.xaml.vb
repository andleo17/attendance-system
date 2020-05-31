Imports Data
Imports Server

Class FrmEmployee

    Private SelectedEmployee As Employee
    Public Shared StaticFrmContent As Frame

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        StaticFrmContent = FrmContent
    End Sub
End Class
