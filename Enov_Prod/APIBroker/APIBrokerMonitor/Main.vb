Imports SEALAPI
Imports System.Configuration

Module Main

Public Sub Main()

    Dim session As SEALAPI.Broker    
    Dim iSession As Integer
    
    Try
        
        IF CheckAPIBroker = True Then
            
             session = New SEALAPI.Broker()
             iSession = session.OpenSession("default",ConfigurationManager.AppSettings("UserID"),ConfigurationManager.AppSettings("UserPass"))
                 
             IF (iSession <1) Then
                KillAPIBroker() 
                StartAPIBroker()
             ElseIF (iSession >= 1 ) Then
                session.CloseSession(iSession) 
             End If
        Else
            KillAPIBroker()
            StartAPIBroker()
        End If                   

    Catch ex As Exception
        FGSU_TOOLS.ExceptionHandling.LogExceptionToWinEvengLog("APIBrokerMonitor",ex.ToString,EventLogEntryType.Error)
    End Try


End Sub


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


Private Sub StartAPIBroker()
    Try

        Dim strCmdLineParms As String = "-a Seal_Server " & System.Configuration.ConfigurationManager.AppSettings("profile") & " " & System.Configuration.ConfigurationManager.AppSettings("uid") & " " & System.Configuration.ConfigurationManager.AppSettings("pwd")
        Dim objStartInfo As New ProcessStartInfo(System.Configuration.ConfigurationManager.AppSettings("ServiceAlliancePath"))
      
        objStartInfo.Arguments = strCmdLineParms
        System.Diagnostics.Process.Start(objStartInfo)

        FGSU_TOOLS.ExceptionHandling.LogExceptionToWinEvengLog("APIBrokerMonitor","API Broker Process Was Started",EventLogEntryType.Warning)
    Catch ex As Exception
        Throw New Exception(ex.ToString)
    End Try

End Sub


Private Sub KillAPIBroker()
    Try
        Dim pProcesses As System.Diagnostics.Process() = Process.GetProcessesByName("seal5") 

        For i As Integer = 0 To pProcesses.Length - 1 
           IF Not pProcesses(i).HasExited Then
                pProcesses(i).Kill()
                pProcesses(i).WaitForExit()
                FGSU_TOOLS.ExceptionHandling.LogExceptionToWinEvengLog("APIBrokerMonitor","API Broker Process Was Terminated",EventLogEntryType.Warning)
           End If
                  

        Next

        ''* Iterate through all running processes to locate the specified process and kill it.
        'For Each pProcess  In Process.GetProcessesByName("seal5")
            
        '    '* Kill the process
        '    If Not pProcess.HasExited Then Call pProcess.Kill()
            
        '     '* Wait while the process completes it's exit.
        '    pProcess.WaitForExit()
            
           
        '    'Do While Not pProcess.HasExited
        '    '    Call System.Threading.Thread.Sleep(1000)
        '    'Loop
        'Next pProcess 
    Catch ex As Exception

    End Try
  
End Sub


End Module
