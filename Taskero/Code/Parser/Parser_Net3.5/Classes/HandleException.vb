Imports System.Net
Imports System.Net.Mail

Public Class HandleException

Private Email_From As String
Public Property FromEmail() As String
    Get
        Return Email_From 
    End Get
    Set(ByVal value As String)
        Email_From = value
    End Set
End Property


Public Shared Function SaveExceptionToDB(Byval exc As Exception) As Boolean
    Try

    Catch ex As Exception

    End Try
End Function


    
Public Shared Function SendExceptionByEmail(ByVal exc As Exception, ByVal strFileContent As String) As Boolean

    Static emailExpression As New System.Text.RegularExpressions.Regex("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")
    Dim objMailMessage  As New MailMessage    
    Dim objSMTP As New SmtpClient

    Dim Email_From As String = ConfigurationManager.AppSettings("Email_From")
    Dim Email_From_Name As String = ConfigurationManager.AppSettings("Email_From_Name")
    Dim Email_To As String = ConfigurationManager.AppSettings("Email_To")
    Dim strBCC As String = Nothing
    Dim strCC As String = Nothing
    If IsNothing(ConfigurationManager.AppSettings("Email_CC")) = False Then
     strCC = ConfigurationManager.AppSettings("Email_CC")
    End If
    If IsNothing(ConfigurationManager.AppSettings("Email_BCC")) = False Then
     strBCC = ConfigurationManager.AppSettings("Email_BCC")
    End If

    Dim FileName As String = HttpContext.Current.Server.MapPath("tmpFiles\") & CreateDateTimeStamp(mSeconds:=True) & ".txt"

    Try

        'Save Message into the temp folder
        objMailMessage.From = New MailAddress(Email_From, Email_From_Name)

         'Set the recepient address of the mail message
         For Each Email As String In Email_To.Split(CChar(";"))
            'If emailExpression.IsMatch(Email) = True Then
                objMailMessage.To.Add(New MailAddress(Email))
            'End If
         Next


         ' Check if the bcc value is nothing or an empty string
         If Not strBCC Is Nothing AndAlso strBCC <> String.Empty Then
            For Each Email As String In strBCC.Split(CChar(";"))
                ' If emailExpression.IsMatch(Email) = True Then
                    ' Set the Bcc address of the mail message
                    objMailMessage.Bcc.Add(New MailAddress(Email))
               'End If
            Next
         End If

         'Check if the cc value is nothing or an empty value
         If Not strCC Is Nothing AndAlso strCC <> String.Empty Then
            For Each Email As String In strCC.Split(CChar(";"))
             '   If emailExpression.IsMatch(Email) = True Then
                    ' Set the CC address of the mail message
                    objMailMessage.CC.Add(New MailAddress(Email))
              '  End If
            Next
         End If

        objMailMessage.Subject = "Exception Parsing a measurement file: "

        objMailMessage.Body = exc.ToString()
        My.Computer.FileSystem.WriteAllText(FileName, strFileContent, False)
        Dim Attachment As System.Net.Mail.Attachment = New Attachment(FileName)
        objMailMessage.Attachments.Add(Attachment)

        objSMTP.Send(objMailMessage)

        If Not IsNothing(Attachment) Then
            Attachment.Dispose()
            Attachment = Nothing
        End If

        'Delete the attachement file from the disk
        My.Computer.FileSystem.DeleteFile(FileName)
    Catch ex As Exception

    End Try


End Function

Public Shared Function CreateDateTimeStamp(Optional ByVal mSeconds As Boolean = False) As String

        Dim strMonth As String
        If Now.Month.ToString.Length < 2 Then
            strMonth = "0" & Now.Month.ToString
        Else
            strMonth = Now.Month.ToString
        End If

        Dim strDay As String
        If Now.Day.ToString.Length < 2 Then
            strDay = "0" & Now.Day.ToString
        Else
            strDay = Now.Day.ToString
        End If

        Dim strYear As String
        If Now.Year.ToString.Length < 2 Then
            strYear = "0" & Now.Year.ToString
        Else
            strYear = Now.Year.ToString
        End If


        Dim strHour As String
        If Now.Hour.ToString.Length < 2 Then
            strHour = "0" & Now.Hour.ToString
        Else
            strHour = Now.Hour.ToString
        End If


        Dim strMinute As String
        If Now.Minute.ToString.Length < 2 Then
            strMinute = "0" & Now.Minute.ToString
        Else
            strMinute = Now.Minute.ToString
        End If

        Dim strSecond As String
        If Now.Second.ToString.Length < 2 Then
            strSecond = "0" & Now.Second.ToString
        Else
            strSecond = Now.Second.ToString
        End If

        Dim strMilliSeconds As String
        If Now.Millisecond.ToString.Length < 2 Then
            strMilliSeconds = "0" & Now.Millisecond.ToString
        Else
            strMilliSeconds = Now.Millisecond.ToString
        End If

        If mSeconds = True Then
            Return strMonth & strDay & strYear & strHour & strMinute & strSecond & strMilliSeconds
        Else
            Return strMonth & strDay & strYear & strHour & strMinute & strSecond
        End If

    End Function



End Class
