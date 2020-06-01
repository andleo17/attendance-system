Imports Data
Imports Server

Class FrmPermisos
    Public Property SelectedPermission As Permission

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If txtCardID.Text IsNot "" Then
            SearchEmployee(New Employee)
            'MessageBox.Show(cldPresentacion.SelectedDate.ToString)
            'MessageBox.Show(Date.Now.Date.ToString)
        Else
            MessageBox.Show("Ingrese DNI")
        End If
    End Sub

    Private Function SearchEmployee(Employee As Employee) As Employee
        Try
            Employee.CardId = txtCardID.Text
            lblName.Content = EmployeeDA.Search(Employee).Name & " " & EmployeeDA.Search(Employee).Lastname
        Catch ex As Exception
            MessageBox.Show("DNI inválido o el Emlpleado no existe")
        End Try
        Return Employee
    End Function

    Private Function SetPermissionData(Permission As Permission) As Permission
        Permission.PresentationDate = Date.Now.Date
        Permission.Date = cldPresentacion.SelectedDate
        Permission.Motive = "Algo"
        Permission.State = 1
        Permission.EmployeeCardId = SearchEmployee(New Employee).CardId
        Return Permission
    End Function

    Private Sub SavePermission()
        Try
            Dim Permission = SetPermissionData(New Permission)
            PermissionDA.Save(Permission)
            MessageBox.Show("Empleado agregado correctamente")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        SavePermission()
    End Sub

    Private Sub UpdatePermission()
        SelectedPermission = SetPermissionData(SelectedPermission)
        PermissionDA.Update(SelectedPermission)
        MessageBox.Show("Permiso actualizado")


    End Sub
End Class
