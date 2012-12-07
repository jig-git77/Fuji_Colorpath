Imports System.Data
Imports System.Data.SqlClient

Public Class MeasuredPatch_Plate
   

Private _MeasurementGuid As Guid
Public WriteOnly Property Measurement_GUID() As Guid
    Set(ByVal value As Guid)
        _MeasurementGuid = value
    End Set
End Property


Private Sample_ID As Integer
Public WriteOnly Property SampleID() As Integer
    Set(ByVal value as Integer)
        Sample_ID = value
    End Set
End Property


Private Sample_Name As String
Public WriteOnly Property SampleName() As String
    Set(ByVal value as String)
        Sample_Name = value
    End Set
End Property


 ''' <summary>
''' Target Dot Area
''' </summary>
''' <remarks>Specfic to Plate Reading Only</remarks>
Private _TargetDotArea As Nullable(Of Decimal)
Public WriteOnly Property TargetDotArea() As Nullable(Of Decimal)
    Set(ByVal value as Nullable(Of Decimal))
        _TargetDotArea = value
    End Set
End Property


''' <summary>
''' Actual Dot Area
''' </summary>
''' <remarks>Specfic to Plate Reading Only</remarks>
Private _ActualDotArea As Nullable(Of Decimal)
Public WriteOnly Property ActualDotArea() As Nullable(Of Decimal)
    Set(ByVal value as Nullable(Of Decimal))
        _ActualDotArea = value
    End Set
End Property


''' <summary>
''' Reference Dot Area
''' </summary>
''' <remarks>Specfic to Plate Reading Only</remarks>
Private _ReferenceDotArea As Nullable(Of Decimal)
Public WriteOnly Property ReferenceDotArea() As Nullable(Of Decimal)
    Set(ByVal value as Nullable(Of Decimal))
        _ReferenceDotArea = value
    End Set
End Property


''' <summary>
''' Deviation
''' </summary>
''' <remarks>Specfic to Plate Reading Only</remarks>
Private _Deviation As Nullable(Of Decimal)
Public WriteOnly Property Deviation() As Nullable(Of Decimal)
    Set(ByVal value as Nullable(Of Decimal))
         _Deviation = value
    End Set
End Property


''' <summary>
''' Tol Strict
''' </summary>
''' <remarks>Individual Patch Tolerance </remarks>
Private _TolStrict As Nullable(Of Decimal)
Public WriteOnly Property TolStrict() As Nullable(Of Decimal)
    Set(ByVal value as Nullable(Of Decimal))
         _TolStrict = value
    End Set
End Property


''' <summary>
''' Save Individual Patch Data into the Database
''' </summary>
''' <remarks></remarks>
Public Sub SavePatchData()

    Dim objCmd As New SqlCommand()
    Try
           With objCmd
                .CommandText = "dbo.uspInsertPatchPlateData"
                .CommandType = CommandType.StoredProcedure
           End With
    
           'MeasurementGUID 
           objCmd.Parameters.AddWithValue("@CPMeasurementGUID", Me._MeasurementGuid)
    
           'Sample ID like 1,2,3
           objCmd.Parameters.AddWithValue("@Sample_ID", Me.Sample_ID)
            
           'Sample Name Like A1, A2, A3
           objCmd.Parameters.AddWithValue("@Sample_Name" , Me.Sample_Name)

           'Target Dot Area
           objCmd.Parameters.AddWithValue("@TargetDotArea", Me._TargetDotArea)

           'Actual Dot Area
           objCmd.Parameters.AddWithValue("@ActualDotArea", Me._ActualDotArea)

           'Reference Dot Area
           objCmd.Parameters.AddWithValue("@ReferenceDotArea", Me._ReferenceDotArea)
         
           If Me._TolStrict.HasValue = True Then
                objCmd.Parameters.AddWithValue("@TolStrict", Me._TolStrict)
           End If
                
           Dim objDataAccess As New DataAccess
           objDataAccess.ExecuteNonQuery(objCmd)

       
    Catch ex As Exception
        Throw New Exception("Exception thrown in MeasuredPatch_Plate SavePatchData: " & ex.ToString)
    End Try
End Sub



End Class
