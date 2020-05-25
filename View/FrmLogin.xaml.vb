Imports Server

Class FrmLogin
    Private _User As Data.User

    Private Sub BtnLogin_Click(sender As Object, e As RoutedEventArgs) Handles BtnLogin.Click
        Try
            _User = New UserDA().Login(TxtUser.Text, TxtPassword.Password)
            MessageBox.Show("Bienvenido " + _User.Employee.Name)
            Close()
        Catch ex As Exception
            'MessageBox.Show("Credenciales incorrectas")
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Property User() As Data.User
        Get
            Return _User
        End Get
        Set(value As Data.User)
            _User = value
        End Set
    End Property
End Class
