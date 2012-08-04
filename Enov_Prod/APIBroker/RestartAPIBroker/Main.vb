Imports SEALAPI
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration


Module Main

    Dim strEmailSuccess As String = "This e-mail confirms that API Broker process (Seal5.exe) has been successfully restarted on the server."
    Dim strEmailFailure As String = "Please Check API Broker on the server. Process failed to restart it on the server."
    Dim strSubject As String = "API Broker Process on " & ConfigurationManager.AppSettings("Server_Name")

    Public Sub Main()
        Try
            KillAPIBroker()
            StartAPIBroker()
            IF CheckAPIBroker = True Then
                IF ConfigurationManager.AppSettings("Email_TO") <> String.Empty Then
                    FGSU_TOOLS.CommonFunctions.SendEmailUsingSQLXPSMTP("cshah@fujifilmgs.com","FTSS SQL",ConfigurationManager.AppSettings("Email_TO"),ConfigurationManager.AppSettings("Email_CC"),strEmailSuccess,strSubject)        
                   
                End If
                
            Else
                IF ConfigurationManager.AppSettings("Email_TO") <> String.Empty Then
                     FGSU_TOOLS.CommonFunctions.SendEmailUsingSQLXPSMTP("cshah@fujifilmgs.com","FTSS SQL",ConfigurationManager.AppSettings("Email_TO"),ConfigurationManager.AppSettings("Email_CC"),strEmailFailure,strSubject, strPriority:="High")

                End If      
            End If
        Catch ex As Exception
             FGSU_TOOLS.ExceptionHandling.LogExceptionToWinEvengLog("APIBrokerMonitor",ex.Message,EventLogEntryType.Error)
        End Try
    End Sub


    Private Sub StartAPIBroker()
        Try

            Dim strCmdLineParms As String = "-a Seal_Server " & System.Configuration.ConfigurationManager.AppSettings("profile") & " " & System.Configuration.ConfigurationManager.AppSettings("uid") & " " & System.Configuration.ConfigurationManager.AppSettings("pwd")
            Dim objStartInfo As New ProcessStartInfo(System.Configuration.ConfigurationManager.AppSettings("ServiceAlliancePath"))
      
            objStartInfo.Arguments = strCmdLineParms
            System.Diagnostics.Process.Start(objStartInfo)
        
        Catch ex As Exception
            'Send Email 
        
        End Try
   End Sub

   
   ''' <summary>
   ''' Kill API Broker Process Seal5.exe
   ''' </summary>
   ''' <remarks>Author: cshah@fujifilmgs.com</remarks>
   Private Sub KillAPIBroker()
        Try
            Dim pProcesses As System.Diagnostics.Process() = Process.GetProcessesByName("seal5") 

            For i As Integer = 0 To pProcesses.Length - 1 
                IF Not pProcesses(i).HasExited Then
                    pProcesses(i).Kill()
                    pProcesses(i).WaitForExit()
                End If
             Next
     
        Catch ex As Exception

        End Try
  
    End Sub


    ''' <summary>
    ''' Check whether API Broker process has started
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckAPIBroker() As Boolean
        
        Try
             For Each pProcess As Process In Process.GetProcesses
                IF pProcess.ProcessName.Contains("seal5") = True Then
                    Return True
                End If
            Next pProcess
       
            Return False
        Catch ex As Exception
            
        End Try

    End Function


  
End Module
