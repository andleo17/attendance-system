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

Partial Public Class Employee
    Public Property CardId As String
    Public Property Name As String
    Public Property Lastname As String
    Public Property Genre As String
    Public Property BirthDate As Date
    Public Property Address As String
    Public Property Phone As String
    Public Property Email As String
    Public Property State As Boolean = true
    Public Property Photo As Byte()
    Public Property PhotoName As String

    Public Overridable Property Attendance As ICollection(Of Attendance) = New HashSet(Of Attendance)
    Public Overridable Property Contract As ICollection(Of Contract) = New HashSet(Of Contract)
    Public Overridable Property License As ICollection(Of License) = New HashSet(Of License)
    Public Overridable Property Permission As ICollection(Of Permission) = New HashSet(Of Permission)
    Public Overridable Property Schedule As ICollection(Of Schedule) = New HashSet(Of Schedule)
    Public Overridable Property User As ICollection(Of User) = New HashSet(Of User)

End Class
