'------------------------------------------------------------------------------
' <auto-generated>
'     Este código se generó a partir de una plantilla.
'
'     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
'     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class Contract
    Public Property Id As Integer
    Public Property StartDate As Date
    Public Property FinishDate As Date
    Public Property Mount As Decimal
    Public Property State As Boolean = true
    Public Property ExtraHours As Boolean
    Public Property EmployeeCardId As String

    Public Overridable Property Employee As Employee

End Class
