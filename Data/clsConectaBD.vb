'importaciones de espacios de nombre
Imports System.Data 'ADO Net 
Imports System.Data.SqlClient 'Prov SQL Server  
Imports System.Data.Odbc 'Origenes ODBC

Public Class clsConectaBD
    Private cn As SqlConnection
    Private cnODBC As New OdbcConnection

    Sub New()
        cn = New SqlConnection
        'Autenticación Windows
        'cn.ConnectionString = "workstation id=BDAsistencia-Abel.mssql.somee.com;packet size=4096;user id=AbelTc_SQLLogin_1;pwd=5qqsvla79l;data source=BDAsistencia-Abel.mssql.somee.com;persist security info=False;initial catalog=BDAsistencia-Abel;language=spanish"
        cn.ConnectionString = "Data Source=DESKTOP-RU9JBB0;Initial Catalog=DBAttendance;Integrated Security=True;Language=spanish"
        'cn.ConnectionString = "data source=PC10501;Initial catalog=BDSistemas;user id=sa; password="
        'cn.ConnectionString = "data source=CONSUELO-PC;Initial catalog=BDSistemas;user id=sa;password="
        'Autenticación SQL Server
        'cn.ConnectionString = "Data Source=SQL5014.Smarterasp.net;Initial Catalog=DB_A06A9F_BDHotel;User Id=DB_A06A9F_BDHotel_admin;Password=nicolas070507;"

    End Sub

    Sub New(ByVal estado As Boolean)
        cnODBC.ConnectionString = "DSN=DSNPrueba"
    End Sub

    Sub conectar(ByRef sw As Boolean)
        cnODBC.Open()
        If cnODBC.State = ConnectionState.Open Then
            sw = True
        Else
            sw = False
        End If
    End Sub

    Public Sub conectar()
        Try
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If
        Catch ex As Exception
            'mensaje de error
        End Try
    End Sub

    Public Sub desconectar()
        Try
            If cn.State <> ConnectionState.Closed Then
                cn.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public ReadOnly Property estadoCN() As String
        Get
            If cn.State = ConnectionState.Open Then
                Return "BD está abierta."

            Else
                Return "BD está cerrada."
            End If
        End Get
    End Property

    Public ReadOnly Property miConexion() As SqlConnection
        Get
            Return cn
        End Get
    End Property

    Public ReadOnly Property Servidor() As String
        Get
            Return cn.DataSource.ToString
        End Get
    End Property

    Public Sub abrirconexion()
        Try
            'transaccion = False
            If cn.State <> ConnectionState.Open Then ' SI EL ESTADO DE  LA CONEXION ES DIFERENTE DE ABIERTO ENTONCES ABRE LA CONEXION
                'cn.ConnectionString = "Data Source=SQL5014.Smarterasp.net;Initial Catalog=DB_A06A9F_BDHotel;User Id=DB_A06A9F_BDHotel_admin;Password=nicolas070507;"
                cn.ConnectionString = "Data Source=DESKTOP-RU9JBB0;Initial Catalog=DBAttendance;Integrated Security=True;Language=spanish"
                'cn.ConnectionString = "workstation id=BDAsistencia-Abel.mssql.somee.com;packet size=4096;user id=AbelTc_SQLLogin_1;pwd=5qqsvla79l;data source=BDAsistencia-Abel.mssql.somee.com;persist security info=False;initial catalog=BDAsistencia-Abel;language=spanish"
                cn.Open()
            End If
        Catch Ex As Exception
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End Try
    End Sub

    Public Sub cerrarconexion()
        Try
            'transaccion = False
            If cn.State = ConnectionState.Open Then
                cn.Close()
                cn.Dispose()
            End If
        Catch Ex As Exception
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End Try
    End Sub

    Public Sub abrirconexionTrans()
        'Try
        '    If transaccion <> True Then
        '        abrirconexion()
        '        tsql = cn.BeginTransaction()
        '        transaccion = True
        '    End If
        'Catch ex As Exception
        '    Err.Raise(Err.Number, Err.Source, Err.Description)
        'End Try

    End Sub

    Public Sub cerrarconexionTrans()
        'Try
        '    If transaccion = True Then
        '        tsql.Commit()
        '        cerrarconexion()
        '        transaccion = False
        '    End If
        'Catch ex As Exception
        '    Err.Raise(Err.Number, Err.Source, Err.Description)
        'End Try
    End Sub

    Public Sub cancelarconexionTrans()
        'Try
        '    If transaccion = True Then
        '        tsql.Rollback()
        '        cerrarconexion()
        '        transaccion = False
        '    End If
        'Catch ex As Exception
        '    Err.Raise(Err.Number, Err.Source, Err.Description)
        'End Try
    End Sub
End Class
