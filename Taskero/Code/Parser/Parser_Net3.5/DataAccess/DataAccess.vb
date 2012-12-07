Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class DataAccess


Private ConnString As String = ConfigurationManager.ConnectionStrings("DBConnString").ConnectionString
Public ReadOnly Property DBConn() As String 
    Get
        Return ConnString
    End Get
End Property



Public Function ExecuteNonQuery(ByVal objCmd As SqlCommand) As Integer

    Dim objConn As New SqlConnection(DBConn)
    Dim iResult As Integer = 0
    Try
        With objCmd
            .Connection = objConn
        End With

        'Open Connection        
        objConn.Open()
        iResult = objCmd.ExecuteNonQuery()
    Catch ex As SqlException
        Throw New Exception(ex.ToString)
        Return -1
    Finally
        If objConn.State = ConnectionState.Open Then
            objConn.Close()
        End If
        objConn = Nothing
    End Try

End Function


Public Function GetDataReader(ByVal objCmd As SqlCommand) As SqlDataReader
    Dim objConn As New SqlConnection(DBConn)
    Dim objDR As SqlDataReader
    Try
        With objCmd
            .Connection = objConn
        End With

        'Open Connection        
        objConn.Open()

        objDR = objCmd.ExecuteReader(CommandBehavior.CloseConnection)

    Catch ex As Exception
        Throw New Exception(ex.ToString)
        
    End Try
    Return objDR
End Function


End Class
