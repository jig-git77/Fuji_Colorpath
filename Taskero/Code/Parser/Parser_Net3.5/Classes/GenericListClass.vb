Public Class GenericTypedList(Of MyType)
    Inherits CollectionBase

    Default Public Property Item(ByVal index As Integer) As MyType
        Get
            Return CType(InnerList(index), MyType)
        End Get

        Set(ByVal Value As MyType)
            InnerList(index) = Value
        End Set
    End Property

    Public Function Add(ByVal value As MyType) As Integer
        Return InnerList.Add(value)
    End Function

    Public Function IndexOf(ByVal value As MyType) As Integer
        Return InnerList.IndexOf(value)
    End Function

    Public Sub Insert(ByVal index As Integer, ByVal value As MyType)
        InnerList.Insert(index, value)
    End Sub

    Public Sub Remove(ByVal value As MyType)
        InnerList.Remove(value)
    End Sub

    Public Function Contains(ByVal value As MyType) As Boolean
        Return InnerList.Contains(value)
    End Function

End Class


