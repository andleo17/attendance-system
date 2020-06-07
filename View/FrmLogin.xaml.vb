Imports Server

Class FrmLogin
    Public Property User As Data.User

    Private Sub BtnLogin_Click(sender As Object, e As RoutedEventArgs) Handles BtnLogin.Click
        Try
            User = UserDA.Login(TxtUser.Text, TxtPassword.Password)
            If User.State Then
                MessageBox.Show("Bienvenido " + User.Employee.Name)
                Close()
            Else
                MessageBox.Show("Usuario no vigente.")
            End If
        Catch ex As Exception
            MessageBox.Show("Credenciales incorrectas")
        End Try
    End Sub
End Class
