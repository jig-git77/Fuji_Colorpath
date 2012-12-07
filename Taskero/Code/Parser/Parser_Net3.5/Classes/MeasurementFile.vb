Imports System.Data
Imports System.Data.SqlClient

Public Class MeasurementFile

    'MeasurementGuid
    Private _MeasurementGUID As Guid
      Public WriteOnly Property MeasurementGUID() As Guid
        Set(ByVal value As Guid)
           _MeasurementGUID = value
        End Set
    End Property    
    
    'MeasurementFile
    Private _MeasurementFile As String
    Public WriteOnly Property MeasurementFile() As String
        Set(ByVal value As String)
           _MeasurementFile = value
        End Set
    End Property
    
    ''' <summary>
    ''' Save Measurement File
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Save()
        Dim objCmd As New SqlCommand()        
        Try
            With objCmd
                .CommandType = CommandType.StoredProcedure
                .CommandText = "dbo.uspInsertMeasurementFile"
            End With
            
            objCmd.Parameters.AddWithValue("@CPMeasurementGUID ",Me._MeasurementGUID)
            objCmd.Parameters.AddWithValue("@MeasurementFile", Me._MeasurementFile)   

            Dim objDataAccess As New DataAccess
            objDataAccess.ExecuteNonQuery(objCmd) 
                
        Catch ex As SqlException
            Throw New Exception("Exception thrown in MeasurementFile Save: " & ex.ToString)   
        End Try
    End Sub

End Class
