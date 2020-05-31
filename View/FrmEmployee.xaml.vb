Imports Data
Imports Server

Class FrmEmployee
    Public Shared StaticFrmContent As Frame

    Public Shared Sub SetMode(Employee As Employee, Mode As Integer)
        Dim Frm = New FrmEmployeeForm With {
            .SelectedEmployee = Employee,
            .Mode = Mode
        }
        StaticFrmContent.Content = Frm
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        StaticFrmContent = FrmContent
    End Sub
End Class
