Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports WJS

Imports Koolwired.Imap
'used Koolwired for accessing IMAP using .NET
'Copyright (c) 2007, Koolwired Solutions, LLC.
'All rights reserved.
'Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
'* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
'* Neither the name of Koolwired Solutions, LLC. nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
'THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 


Module MainMod

Dim ServiceCallNum As String 
Dim EmailReceivedDate As Date
Dim TZone As String
Dim TZone_ID As String
Dim CustomerDT As Date = DateTime.MinValue

Public Sub Main()
    

    Dim IMAPConnect as New ImapConnect(ConfigurationManager.AppSettings("SMTPServer"))
    Dim IMAPCommand AS New ImapCommand(IMAPConnect)
        
    Try

      
        Dim IMAPAuthenticate As New Koolwired.Imap.ImapAuthenticate(IMAPConnect, "ftss@fujifilmgs.com", "P0cketPC")
        IMAPConnect.Open()
        IMAPAuthenticate.Login()
      
        Dim MailBox AS ImapMailbox = IMAPCommand.Select("Inbox")
        MailBox =  IMAPCommand.Fetch(MailBox)
        
        If MailBox IsNot Nothing AndAlso MailBox.Messages IsNot Nothing Then 

            For Each Message As ImapMailboxMessage IN MailBox.Messages 
        
                If (Message.Subject.IndexOf("4") > -1) AND (Message.Subject.IndexOf("(") > -1) Then
              
                    If message.Flags.Deleted = True Then 
                        Continue For 
                    End If 
                                       
                    'service call number 
                    ServiceCallNum = Message.Subject.Substring(Message.Subject.IndexOf("4") ,8)

                    ServiceCallNum = ServiceCallNum & "@@1"

                    'email sent date                    
                    EmailReceivedDate  = Message.Received

                    GetCustomerLocalTime()
                                                      
                   'UPDATE Enov_PROD Database
                    Update_Tech_ArrivedDT()   
            
                    'Handle if any exception thrown
                    Try
                        IMAPCommand.Copy("Confirmation",Message.ID,Message.ID)
                                   
                        'Set the flag 
                        IMAPCommand.SetDeleted(message.ID, True) 
                    Catch
                        'Do nothing

                    End Try        
                   
                    'really delete the message 
                    IMAPCommand.Expunge()
 
                End If
                
            Next 

        End If 
    
        IMAPAuthenticate.Logout()
        IMAPConnect.Close()

    Catch ex As Exception
        
        'clean up
        If IMAPConnect.State = Koolwired.Imap.ConnectionState.Open
            IMAPConnect.Close()
        End If
        FGSU_TOOLS.CommonFunctions.SendEmail("FTSS@Fujifilmgs.com" ,"Fujifilmgs Technical Serv" , "cshah@fujifilmgs.com", "MWinn@fujifilmgs.com", ex.ToString(),"Onsite update error -- tech responded by email")
    End Try

End Sub




Private Sub Update_Tech_ArrivedDT()   
    Dim objCmd As New SqlCommand()
    
    Try
                
        With objCmd
            .CommandText = "dbo.uspUpdateSAOnSiteStatus"
            .CommandType = CommandType.StoredProcedure
        End With

        If IsDate(CustomerDT) = True And CustomerDT <> DateTime.MinValue Then

            'service call number
            objCmd.Parameters.AddWithValue("@svc_call_id", ServiceCallNum)

            'arrive_DT
            objCmd.Parameters.AddWithValue("@arrive_dt", CustomerDT.ToShortDateString())

            'arrive_tm
            objCmd.Parameters.AddWithValue("@arrive_tm", CustomerDT.ToShortTimeString())


            Dim objDataAccess As New FGSU_TOOLS.DataService
            objDataAccess.ExecuteNonQuery(objCmd)
        End If

        Catch ex As Exception
            If Not objCmd Is Nothing Then
                objCmd = Nothing
            End If
            Throw New Exception("Error updating service call number " & ServiceCallNum & " " & ex.ToString)
        End Try
End Sub


Private Sub GetCustomerLocalTime()
    Dim objCmd As New SqlCommand()
    Dim State As String  = Nothing  
    Try
        
        With objCmd
            .CommandText = "select state_prov_id from order_line inner join address on order_line.ship_to_address_id = address.address_id where order_id = '" & ServiceCallNum & "'" 
            .CommandType = CommandType.Text
        End With

        Dim objDataAccess As New FGSU_TOOLS.DataService
        Dim objReader As SqlDataReader = objDataAccess.GetDataReader(objCmd)
    
            If objReader.HasRows = True Then

                objReader.Read()
                State = objReader.GetValue(0).ToString()
            End If
        
        If Not IsNothing(State) = True Then
            Dim SourceTimeZone As String = "Eastern Standard Time"  'Server's Time Zone Always Easter Time
        
            Dim CustomerTimeZone As String = String.Empty
       
            Select Case State.ToUpper()
                Case "CA","OR","WA", "NV"
                    CustomerTimeZone= "Pacific Standard Time"       'Changes based on the stagte
                Case "MT", "ID","WY","UT","CO","AZ", "NM"
                    CustomerTimeZone = "Mountain Standard Time"       'Changes based on the state
                Case "TX", "LA", "AL", "MS","AR","OK","IL","MO","KS","NE", "IA", "WI","ND","SD","MN"
                    CustomerTimeZone = "Central Standard Time"       'Changes based on the state
                Case "HI" 
                    CustomerTimeZone = "Hawaiian Standard Time"
                Case "AK"
                     CustomerTimeZone = "Alaskan Standard Time"
                Case Else
                    CustomerTimeZone = "Eastern Standard Time"  
            End Select
            
        
            CustomerDT = Utils.TimeZoneInformation.ToLocalTime(SourceTimeZone,EmailReceivedDate,CustomerTimeZone)
        End If
       
    Catch ex As Exception
         If Not objCmd Is Nothing Then
            objCmd = Nothing
         End If
        Throw New Exception(ex.ToString)
    End Try

End Sub




End Module
