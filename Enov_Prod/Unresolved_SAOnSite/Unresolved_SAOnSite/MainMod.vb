Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports FGSU_TOOLS

Module MainMod
    
Dim objCmd As SqlCommand
Dim objDR As SqlDataReader
 
Public Sub Main()
        
    Try

       Call GetUnresolvedSvcCallWithSAOnSite()        
       Call GetServiceCall()

    Catch ex As Exception
        CommonFunctions.SendEmail("FTSS@Fujifilmgs.com" ,"Fujifilmgs Technical Serv" , "cshah@fujifilmgs.com", "MWinn@fujifilmgs.com", ex.ToString(),"Error in Unresolved SA On Site")
    End Try
    
End Sub

''' <summary>
''' Find all the service calls which are pending (i.e. not resolved yet) and current status is SA on site
''' </summary>
''' <remarks></remarks>
Private Sub GetUnresolvedSvcCallWithSAOnSite()
    objCmd = New SqlCommand()
    Try
        With objCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "uspInsertUnresolvedCallsSAOnSite"
        End With
        Dim objDataAccess As New FGSU_TOOLS.DataService
        objDataAccess.ExecuteNonQuery(objCmd)
     Catch ex As SqlException
        Throw New Exception(ex.ToString)
    End Try
End Sub



''' <summary>
''' Get Service Calls for which we need to send email.
''' </summary>
''' <remarks></remarks>
Private Sub GetServiceCall()

    Try
        Dim strSQL As String = String.Empty
        strSQL = "select service_call_id,  sa_arrived_dt FROM cust_unresolved_saonsite WHERE email_sent = 0 " & _
                 " and convert(varchar(10),expected_completion_date,101) not in ('1/1/2010','05/31/2010','07/05/2010'," & _
                 "'09/06/2010', '11/25/2010','11/26/2010','12/24/2010')" 
        objCmd = New SqlCommand()
        With objCmd
            .CommandText  = strSQL
            .CommandType = CommandType.Text
        End With
        Dim objDataAccess As New FGSU_TOOLS.DataService
        objDR = objDataAccess.GetDataReader(objCmd)
        
        If Not IsNothing(objDR) AndAlso objDR.HasRows = True Then
            While objDR.Read()
               
                Dim svc_call_id As String

                svc_call_id  = Convert.ToString(objDR.Item("service_call_id"))
                Call SendDetails(svc_call_id)
                Call UpdateEmailSent(svc_call_id)
            End While
        End If
    
    Catch ex As Exception
        Throw New Exception(ex.ToString)
    End Try

End Sub



'Send Email with detail description of service call, actual service agent arrival date and estimated completion date and customer information
''' <summary>
'''send an email.
''' </summary>
''' <param name="svccall"></param>
''' <remarks>http://msdn.microsoft.com/en-us/library/yf1a7f4f(VS.80).aspx</remarks>
Private Sub SendDetails(ByVal svccall As String)
    
    If ( svccall = String.Empty)  Then
        Exit Sub
    End If

    Dim Cust_Name As String = Nothing
    Dim Cust_City As String = Nothing
    Dim Cust_State As String = Nothing
    Dim node_id As Integer  = 0
    Dim Zip As String   = Nothing
    Dim strEmailBody As String = String.Empty   
    Dim strEmailTo  As String = String.Empty
    Dim strEmailCC As String = ConfigurationManager.AppSettings("Email_CC").ToString()
    Dim strSubject As String= "Unresolved service call - " & svccall & ""

    Dim objDR2 As SqlDataReader = Nothing 
        
    Dim strSQL1 As String = "select coalesce(cust_company_descr, site_company_descr) as company, address.city, address.state_prov_id as state, address.zip, sa_person_id, person.email_id as sa_email, person.search_name, adds.email_id as EmailTo, coalesce(long_text.long_text,order_line.problem_desc) as problem_desc, service_call.bpart_id as model, node.node_id, SUBSTRING(ISNULL(pclass3_id,''),1,2) as prod_class " & _
                            "from order_line  " & _
                            "inner join address on order_line.ship_to_address_id=address.address_id " & _
                            "inner join service_call on order_line.order_id=service_call.order_id " & _
                            "inner join person on person.person_id = service_call.sa_person_id  " & _
                            "inner join node on node.node_id = order_line.node_id " & _
                            "inner join address as adds on adds.address_id = node.address_id " & _
                            "left outer join long_text on long_text.table_key = order_line.order_id and long_text.table_name =  'order_line' " & _
                            "where order_line.order_id = '" & svccall & "'"
    
   
    Dim strSQL2 As String = "select service_call_id, case call_staus when 400 then 'On Site' end as call_status , pcode_id, sa_arrived_dt, expected_completion_date " & _
                            "from cust_unresolved_saonsite " & _
                            "where email_sent = 0 and  service_call_id = '" &  svccall & "'"                                                    

    strEmailBody = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\email.html" ) 
 
    Try

        objCmd = New SqlCommand()
        With objCmd
            .CommandText = strSQL1
            .CommandType = CommandType.Text
        End With

        Dim objDataAccess As New FGSU_TOOLS.DataService
        Using  objDR1 As SqlDataReader  = objDataAccess.GetDataReader(objCmd)
           
            While objDR1.Read()

                ' We no longer use node_id used to be if node id = 401,402,405 then email goes to MWilson
                ' else if node id is 403,404,406 then email goes to TLash
                ''node_id = CType(objDR1("node_id"),Integer)

                'if service_call pclass3_id starts with WF (Wide Format) then copy to Steve Lynn
                If Not String.IsNullOrEmpty(objDR1("prod_class").ToString()) AndAlso (objDR1("prod_class").ToString().ToUpper = "WF") Then
                    strEmailCC = strEmailCC & ";SLynn@fujifilmgs.com"
                End If

                strEmailBody = strEmailBody.Replace("###ServiceCallNum###", svccall)
                strEmailBody = strEmailBody.Replace("###Company###", objDR1("company").ToString())
                strEmailBody = strEmailBody.Replace("###City###", objDR1("City").ToString())
                strEmailBody = strEmailBody.Replace("###State###", objDR1("State").ToString())
                strEmailBody = strEmailBody.Replace("###Zip###", objDR1("Zip").ToString())

                strEmailBody = strEmailBody.Replace("###Model###", objDR1("Model").ToString())
                strEmailBody = strEmailBody.Replace("###ProblemDesc###", objDR1("problem_desc").ToString())
                strEmailBody = strEmailBody.Replace("###AssignedTo###", objDR1("search_name").ToString())


                'email addresses of techs to whom we need to send e-mails
                strEmailTo = objDR1("EmailTo").ToString()

                Dim objCmd2 As New SqlCommand()
                With objCmd2
                    .CommandType = CommandType.Text
                    .CommandText = strSQL2
                End With

                objDR2 = objDataAccess.GetDataReader(objCmd2)

                If objDR2.HasRows = True Then
                    objDR2.Read()
                    strEmailBody = strEmailBody.Replace("###Call Status###", objDR2.Item("call_status").ToString())
                    strEmailBody = strEmailBody.Replace("###Arrival Date###", objDR2.Item("sa_arrived_dt").ToString())
                    strEmailBody = strEmailBody.Replace("###Completion Date###", objDR2.Item("expected_completion_date").ToString())
                End If

            End While

        End Using

        If strEmailTo <> String.Empty Then
        
            If ConfigurationManager.AppSettings("Email_BCC") <> String.Empty Then
                CommonFunctions.SendEmail("FTSS@Fujifilmgs.com" ,"Fujifilmgs Technical Serv" , strEmailTo,strEmailCC,strEmailBody, strSubject, strType:="html", strBCC:=ConfigurationManager.AppSettings("Email_BCC").ToString(),strPriority:="HIGH")
            Else
               CommonFunctions.SendEmail("FTSS@Fujifilmgs.com" ,"Fujifilmgs Technical Serv" , strEmailTo,strEmailCC,strEmailBody,"Review parts allocated for service call " & svccall , strType:="html")
            End If

        End If
            
    Catch ex As Exception
        Throw New Exception(ex.ToString)
    End Try

End Sub


''' <summary>
''' Update table to confirm that email has been sent. This will prevent any subsequent e-mails being sent for the same service call.
''' </summary>
''' <param name="svccall"></param>
''' <remarks></remarks>
Private Sub UpdateEmailSent(ByVal svccall As String )
    
    Dim strSQL As String = Nothing
    strSQL = "UPDATE cust_unresolved_saonsite " & _
              "SET email_sent = 1 " & _
              "WHERE service_call_id ='" &  svccall & "'"  
       
    objCmd = New SqlCommand()
    Try
        With objCmd
            .CommandType = CommandType.Text
            .CommandText = strSQL
        End With
        Dim objDataAccess As New FGSU_TOOLS.DataService
        objDataAccess.ExecuteNonQuery(objCmd)
    Catch ex As Exception
        Throw New Exception(ex.ToString())
    End Try

End Sub 


End Module
