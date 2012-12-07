Imports System.Data
Imports System.Data.SqlClient

Public Class MeasuredPatch_Color



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

'Percentage of Cyan
Private _CMYK_C As Decimal
Public WriteOnly Property CMYK_C() As Decimal
    Set(ByVal value as Decimal)
        _CMYK_C = value
    End Set
End Property

'Percentage of Magenta
Private _CMYK_M As Decimal
Public WriteOnly Property CMYK_M() As Decimal
    Set(ByVal value as Decimal)
        _CMYK_M = value
    End Set
End Property

'Percentage of Yellow
Private _CMYK_Y As Decimal
Public WriteOnly Property CMYK_Y() As Decimal
    Set(ByVal value as Decimal)
        _CMYK_Y = value
    End Set
End Property

'Percentage of Key (Black)
Private _CMYK_K As Decimal
Public WriteOnly Property CMYK_K() As Decimal
    Set(ByVal value as Decimal)
        _CMYK_K = value
    End Set
End Property


'CIEL*a*b Target L value
Private _CIELAB_L_Target As Decimal
Public WriteOnly Property CIELAB_L_Target() As Decimal
    Set(ByVal value as Decimal)
        _CIELAB_L_Target = value
    End Set
End Property

'CIEL*a*b Target a* value
Private _CIELAB_a_Target As Decimal
Public WriteOnly Property CIELAB_a_Target () As Decimal
    Set(ByVal value as Decimal)
        _CIELAB_a_Target  = value
    End Set
End Property


'CIEL*a*b Target b* value
Private _CIELAB_b_Target As Decimal
Public WriteOnly Property CIELAB_b_Target() As Decimal
    Set(ByVal value as Decimal)
        _CIELAB_b_Target = value
    End Set
End Property


'CIEL*a*b L* value
Private _CIELAB_L As Decimal
Public WriteOnly Property CIELAB_L() As Decimal
    Set(ByVal value as Decimal)
        _CIELAB_L = value
    End Set
End Property

Private _CIELAB_a As Decimal
Public WriteOnly Property CIELAB_a() As Decimal
    Set(ByVal value as Decimal)
        _CIELAB_a = value
    End Set
End Property

Private _CIELAB_b As Decimal
Public WriteOnly Property CIELAB_b() As Decimal
    Set(ByVal value as Decimal)
        _CIELAB_b = value
    End Set
End Property

'Delta E 
Private _deltaE As Decimal
Public WriteOnly Property deltaE() As Decimal
    Set(ByVal value as Decimal)
        _deltaE= value
    End Set
End Property

Private _dL As Decimal
Public WriteOnly Property dL() As Decimal
    Set(ByVal value as Decimal)
        _dL= value
    End Set
End Property


Private _dA As Decimal
Public WriteOnly Property dA() As Decimal
    Set(ByVal value as Decimal)
        _dA = value
    End Set
End Property


Private _dB As Decimal
Public WriteOnly Property dB() As Decimal
    Set(ByVal value as Decimal)
        _dB = value
    End Set
End Property

'Delta of Hue
Private _dH As Decimal
Public WriteOnly Property dH() As Decimal
    Set(ByVal value as Decimal)
        _dH = value
    End Set
End Property


'Delta of Chroma
Private _dC As Decimal
Public WriteOnly Property dC() As Decimal
    Set(ByVal value as Decimal)
        _dC = value
    End Set
End Property

'Density
Private _Density As Decimal
Public WriteOnly Property Density() As Decimal
    Set(ByVal value As Decimal)
        If (value > 0 And value < 5) Then
              _Density = value
        Else
            Throw New Exception("Invaid Density Values")
        End If
    End Set
End Property

'Individual Patch Tolerance - Caution
Private _TolCaution As  Nullable(Of Decimal)
Public WriteOnly Property TolCaution() As Decimal
    Set(ByVal value as Decimal)
        _TolCaution  = value
    End Set
End Property


'Individual Patch Tolerance - Strict
Private _TolStrict As Nullable(Of Decimal)
Public WriteOnly Property TolStrict() As Nullable(Of Decimal)
    Set(ByVal value as Nullable(Of Decimal))
        _TolStrict   = value
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
                .CommandText = "dbo.uspInsertPatchLabData"
                .CommandType = CommandType.StoredProcedure
            End With
    
            'MeasurementGUID 
            objCmd.Parameters.AddWithValue("@CPMeasurementGUID", Me._MeasurementGuid)
    
            'Sample ID like 1,2,3
            objCmd.Parameters.AddWithValue("@Sample_ID", Me.Sample_ID)
            
            'Sample Name Like A1, A2, A3
            objCmd.Parameters.AddWithValue("@Sample_Name" , Me.Sample_Name)

            'Cyan 
            objCmd.Parameters.AddWithValue("@CMYK_C", Me._CMYK_C)

            'Magenta
            objCmd.Parameters.AddWithValue("@CMYK_M", Me._CMYK_M)

            'Yellow
            objCmd.Parameters.AddWithValue("@CMYK_Y", Me._CMYK_Y)

            'Key (Black)
            objCmd.Parameters.AddWithValue("@CMYK_K", Me._CMYK_K)

            'Target L*
            objCmd.Parameters.AddWithValue("@LAB_L_Target", Me._CIELAB_L_Target)

            'Target a*
            objCmd.Parameters.AddWithValue("@LAB_a_Target", Me._CIELAB_a_Target)

            'Target b*
            objCmd.Parameters.AddWithValue("@LAB_b_Target", Me._CIELAB_b_Target)

            'Measured L*
            objCmd.Parameters.AddWithValue("@LAB_L_ACT", Me._CIELAB_L)

            'Measured a*
            objCmd.Parameters.AddWithValue("@LAB_a_ACT",Me._CIELAB_a)

            'Measured b*
            objCmd.Parameters.AddWithValue("@LAB_b_ACT",Me._CIELAB_b)

            'Delta E
            objCmd.Parameters.AddWithValue("@DE", Me._deltaE)

            'dL
            objCmd.Parameters.AddWithValue("@DL", Me._dL)

            'dA
            objCmd.Parameters.AddWithValue("@DA", Me._dA)

            'dB
            objCmd.Parameters.AddWithValue("@DB", Me._dB)
                
            'DH
            objCmd.Parameters.AddWithValue("@DH", Me._dH)

            'DC
            objCmd.Parameters.AddWithValue("@DC",Me._dC)

            'Density
            objCmd.Parameters.AddWithValue("@DENSITY", Me._Density)

            If Me._TolCaution.HasValue = True Then
                objCmd.Parameters.AddWithValue("@TolCaution", Me._TolCaution)
            End If
        
            If Me._TolStrict.HasValue = True Then
                objCmd.Parameters.AddWithValue("@TolStrict", Me._TolStrict)
            End If
        
            Dim objDataAccess As New DataAccess
            objDataAccess.ExecuteNonQuery(objCmd)

        
    Catch ex As SqlClient.SqlException
        Throw New Exception("SQL Exception thrown in MeasuredPatch_Color SavePatchData: " & ex.ToString)
    Catch ex As Exception
        Throw New Exception("Exception thrown in  MeasuredPatch_Color SavePatchData: " & ex.ToString)
     End Try
End Sub



End Class
