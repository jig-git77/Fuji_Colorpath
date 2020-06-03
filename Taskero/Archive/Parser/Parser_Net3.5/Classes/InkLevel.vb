Imports System.Data
Imports System.Data.SqlClient

Public Class InkLevel
 
''' <summary>
''' GUID of the measurement for which this object belongs
''' </summary>
Private _MeasurementGuid As Guid
Public Property Measurement_GUID() As Guid
    Set(ByVal value As Guid)
        _MeasurementGuid = value
    End Set
    Get
        Return  _MeasurementGuid
    End Get
End Property

''' <summary>
''' Position of Ink Cartridge 
''' </summary>
Private _InkPos As String
Public WriteOnly Property InkPos() As String
    Set(ByVal value as String)
        _InkPos = value
    End Set
End Property   


'Ink Name
Private _InkName As String
Public WriteOnly Property InkName() As String
    Set(ByVal value as String)
        _InkName = value
    End Set
End Property

'Ink Level
Private _CurrLevel As Decimal
Public WriteOnly Property CurrLevel As Decimal
    Set(ByVal value As Decimal)
        _CurrLevel = value
    End Set
End Property


'Ink Max Level
Private _MaxLevel As Nullable(Of Decimal)
Public WriteOnly Property MaxLevel As Nullable(Of Decimal)
    Set(ByVal value As Nullable(Of Decimal))
        _MaxLevel = value
    End Set
End Property


''' <summary>
''' Save Ink Levels
''' </summary>
''' <remarks></remarks>
Public Sub Save()
    Dim objCmd As New SqlCommand()
    Try
        With objCmd
            .CommandText = "dbo.uspInsertInkLevel"
            .CommandType = CommandType.StoredProcedure
        End With
        
        'MeasurementGUID
        objCmd.Parameters.AddWithValue("@CPMeasurementGUID",Me.Measurement_GUID)
    
        'InkName
        objCmd.Parameters.AddWithValue("@InkName", Me._InkName)
        
        'CurrInkLevel
        objCmd.Parameters.AddWithValue("@CurrLevel", Me._CurrLevel)
        
        Dim objDataAccess As New DataAccess
        objDataAccess.ExecuteNonQuery(objCmd)
        
    Catch ex As SqlException 
        Throw New Exception ("Insert Error in Save Ink Lavel: " & ex.ToString())   

    Catch ex As Exception
        Throw New Exception("Error Saving Ink Levels... " & ex.ToString )
    End Try
End Sub

End Class