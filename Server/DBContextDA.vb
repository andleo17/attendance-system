Public Class DBContextDA
    Public Shared Function GetContext(Entity As Object) As Entity.DbContext
        Dim ObjectContext = GetObjectContextFromEntity(Entity)

        If ObjectContext Is Nothing Or ObjectContext.TransactionHandler Is Nothing Then
            Return Nothing
        End If
        Return ObjectContext.TransactionHandler.DbContext
    End Function

    Private Shared Function GetObjectContextFromEntity(Entity As Object) As Entity.Core.Objects.ObjectContext
        Dim Field = Entity.GetType().GetField("_entityWrapper")

        If Field Is Nothing Then
            Return Nothing
        End If

        Dim Wrapper = Field.GetValue(Entity)
        Dim Prop = Wrapper.GetType().GetProperty("Context")
        Dim Context = Prop.GetValue(Wrapper, Nothing)

        Return Context
    End Function
End Class
