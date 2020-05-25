Imports System.Windows.Threading

Public Class MdiPrincipal
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
        LblTimer.Content = Date.Now.ToString("HH:mm:ss")
    End Sub

    Private Sub ShowLoginForm()
        Dim FrmLogin = New FrmLogin()
        FrmLogin.ShowDialog()
        _User = FrmLogin.User
        If _User IsNot Nothing Then
            ShowUserData()
        Else
            Windows.Application.Current.Shutdown()
        End If
    End Sub

    Private Sub ShowUserData()
        LblEmployee.Content = _User.Employee.Name + " " + _User.Employee.Lastname
    End Sub

    Private Sub ClearUserData()
        LblEmployee.Content = Nothing
    End Sub

    Private Sub Logout()
        ClearUserData()
        ShowLoginForm()
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        InitClock()
        ShowLoginForm()
    End Sub

    Private Sub MnuLogout_Click(sender As Object, e As RoutedEventArgs) Handles MnuLogout.Click
        Logout()
    End Sub
End Class
