Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Module Main

Public Sub Main()
    
      Dim objConn As SqlConnection = Nothing
      Dim objCmd AS SqlCommand     = Nothing
      Dim dr AS SqlDataReader        

      Try
       
        objConn = New SqlConnection(ConfigurationManager.AppSettings("AsteaDBConnectionString"))
        objConn.Open()
        
        objCmd = New SqlCommand()
        With objCmd
            .CommandText =  "uspSurveyGetEmails"
            .CommandType = CommandType.StoredProcedure
            .Connection = objConn
        End With

        dr = objCmd.ExecuteReader()
        
        IF dr.HasRows Then
            Do While dr.Read()
                
                Dim strSubject As String = dr.Item("email_subject")
                Dim strMessage As String = dr.Item("email_message")
                Dim strServiceCallID As String = dr.Item("service_order_id")
                Dim strEmail As String = dr.Item("email_address")
               
                IF  strEmail.Contains("fujifilmgs.com") = False Then
                     'Send Email 
                     FGSU_TOOLs.CommonFunctions.SendEmail("FTSS@Fujifilmgs.com","",strEmail,String.Empty,strMessage,strSubject,strBCC:="FTSS@Fujifilmgs.com",strType:="HTML")
                
                     'Update Table with Sent Date
                    UpdateEmailSentDate(strEmail) 
                End If

            Loop
           
        End If
    Catch ex As Exception
        FGSU_TOOLS.CommonFunctions.SendEmail("FTSS@Fujifilmgs.com", "", ConfigurationManager.AppSettings("ErrorEmailTo"), String.Empty, ex.ToString(), "Error in Survey Email Program", strBCC:="FTSS@Fujifilmgs.com", strType:="HTML")
    Finally
       objCmd = Nothing
       IF objConn.State = ConnectionState.Open Then
            objConn.Close()      
       End If
       objConn = Nothing
    End Try

End Sub


Private Sub UpdateEmailSentDate(ByVal Email As String)

    Dim objConn As SqlConnection = Nothing
    Dim objCmd AS SqlCommand     = Nothing

    Try
        objConn = New SqlConnection(ConfigurationManager.AppSettings("AsteaDBConnectionString"))
              
        objCmd = New SqlCommand()
        With objCmd
            .CommandText =  "uspSurveyUpdateEmailSent"
            .CommandType = CommandType.StoredProcedure
            .Connection = objConn
        End With     

        objConn.Open()

        Dim SQLParam1 AS New SqlParameter
        With SQLParam1
            .ParameterName = "@Email"
            .SqlDbType     = SqlDbType.VarChar
            .Value         = Email
        End With    
        objCmd.Parameters.Add(SQLParam1)        
        
        Dim iResult As Integer = objCmd.ExecuteNonQuery()
            
    Catch ex As Exception
        Throw New Exception(ex.ToString())
    Finally
       objCmd = Nothing
       IF objConn.State = ConnectionState.Open Then
            objConn.Close()      
       End If
       objConn = Nothing    
    End Try

End Sub


End Module
