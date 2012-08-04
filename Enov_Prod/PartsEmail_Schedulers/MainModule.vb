Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports FGSU_TOOLS

Module MainModule

Dim objCmd As SqlCommand
Dim objDR As SqlDataReader

Public Sub Main()
        
    Try
        Call MergeSvcCallWithParts()
    
        Call GetServiceCall()
    Catch ex As Exception
            CommonFunctions.SendEmail("ftss@fujifilmgs.com", "Fujifilm Technical Services", "mwinn@fujifilm.com", "", ex.ToString(), "Error in no contracts parts service call")
    End Try
    
End Sub


''' <summary>
''' This method insert into or update cust_svccallpartstotal table 
''' if total cost of parts in a service call exceeds 3000 dollars
''' </summary>
''' <remarks></remarks>
Private Sub MergeSvcCallWithParts()

    objCmd = New SqlCommand()
    Try
        With objCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "uspMergePartsTotalSchedulars"
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
        strSQL = "SELECT service_call_id, total_cost FROM cust_partstotalschedulers WHERE email_sent = 0"
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
                Dim total_parts As Decimal
                svc_call_id  = Convert.ToString(objDR.Item("service_call_id"))
                total_parts =  Convert.ToDecimal(objDR.Item("total_cost"))
                Call SendDetails(svc_call_id,total_parts)
                Call UpdateEmailSent(svc_call_id)
            End While
        End If
    
    Catch ex As Exception
        Throw New Exception(ex.ToString)
    End Try

End Sub



'Send Email with detail description of service call, parts and customer information
''' <summary>
''' For passed service call send an email.
''' </summary>
''' <param name="svccall"></param>
''' <param name="totalparts"></param>
''' <remarks>http://msdn.microsoft.com/en-us/library/yf1a7f4f(VS.80).aspx</remarks>
Private Sub SendDetails(ByVal svccall As String, Byval totalparts As Decimal)
    
    If (totalparts = 0) OR (svccall = String.Empty)  Then
        Exit Sub
    End If

    Dim Cust_Name As String = Nothing
    Dim Cust_City As String = Nothing
    Dim Cust_State As String = Nothing
    Dim Zip As String   = Nothing
    Dim strEmailBody As String = String.Empty   
    Dim row As String  = String.Empty
    Dim strEmailTo  As String = String.Empty

    Dim objDR2 As SqlDataReader = Nothing 
        
         
    Dim strSQL1 As String = "select coalesce(cust_company_descr, site_company_descr) as company, address.city, address.state_prov_id as state, address.zip, sa_person_id, person.email_id as sa_email, person.search_name, adds.email_id as EmailTo, coalesce(long_text.long_text,order_line.problem_desc) as problem_desc, service_call.bpart_id as model " & _
                            "from order_line  " & _
                            "inner join address on order_line.ship_to_address_id=address.address_id " & _
                            "inner join service_call on order_line.order_id=service_call.order_id " & _
                            "inner join person on person.person_id = service_call.sa_person_id  " & _
                            "inner join node on node.node_id = order_line.node_id " & _
                            "inner join address as adds on adds.address_id = node.address_id " & _
                            "left outer join long_text on long_text.table_key = order_line.order_id and long_text.table_name =  'order_line' " & _
                            "where order_line.order_id = '" & svccall & "'"
    
    Dim strSQL2 As String = " select t.service_call_id, bpart.bpart_id as partnumber, bpart.descr as partname, cast(price as decimal(10,2)) as cost " & _
                            "from ( select  service_call.order_id as service_call_id " & _
                            "from order_line " & _
                            "inner join service_call on order_line.order_id=service_call.order_id " & _
                            "left outer join demand on demand.order_id = service_call.order_id " & _
                            "left outer join bpart on bpart.bpart_id = demand.bpart_id " & _
                            "where demand.bpart_gcl_id = 'Goods' and order_line.order_stat_uniq_id < 450 " & _
                            "and (order_line.service_level_id is null and service_call.warr_type_id is null ) and (service_call.pcode_id <> 'I' OR service_call.pcode_id IS NULL) " & _
                            "group by service_call.order_id  " & _
                            "having SUM(bpart.price) >= 750 " & _
                            ") as t inner join demand on. t.service_call_id  = demand.order_id and demand.bpart_gcl_id = 'Goods' " & _
                            " inner join  bpart on bpart.bpart_id = demand.bpart_id  " & _
                            "where t.service_call_id = '" & svccall & "'"
                            

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

                Cust_State = objDR1("State").ToString()
                strEmailBody = strEmailBody.Replace("###ServiceCallNum###", svccall)
                strEmailBody = strEmailBody.Replace("###Company###", objDR1("company").ToString())
                strEmailBody = strEmailBody.Replace("###City###",  objDR1("City").ToString())
                strEmailBody = strEmailBody.Replace("###State###",  objDR1("State").ToString())
                strEmailBody = strEmailBody.Replace("###Zip###",  objDR1("Zip").ToString())
                    
                strEmailBody = strEmailBody.Replace("###Model###",  objDR1("Model").ToString())
                strEmailBody = strEmailBody.Replace("###ProblemDesc###",  objDR1("problem_desc").ToString())
                strEmailBody = strEmailBody.Replace("###AssignedTo###",  objDR1("search_name").ToString())

               
                'email addresses of the techs to whom we need to send e-mails
                strEmailTo = ConfigurationManager.AppSettings("Email_To").ToString()
                
                Dim objCmd2 As New SqlCommand()
                With objCmd2
                    .CommandType = CommandType.Text
                    .CommandText = strSQL2
                End With
            
                objDR2 = objDataAccess.GetDataReader(objCmd2)
                If objDR.HasRows = True Then
                While objDR2.Read()
                      row = row & "<tr><td style='font-family:Courier New;font-size: small;font-weight: normal;'>" & objDR2.Item("partnumber").ToString() & "</td><td style='font-family:Courier New;font-size: small;font-weight: normal;'>" & objDR2.Item("partname").ToString() & "</td><td style='font-family:Courier New;font-size: small;font-weight: normal;'>" & objDR2.Item("cost").ToString() & "</td></tr>"
                End While
                Else
                    Exit Sub
                End IF

                strEmailBody = strEmailBody.Replace("###rows###", row)
                                                                  
                strEmailBody = strEmailBody.Replace("###TotalCost###", Convert.ToString(totalparts))       
    
            End While


        End Using

        If strEmailTo <> String.Empty Then

            Dim strCC As String = String.Empty
            If Not IsNothing(ConfigurationManager.AppSettings("Email_CC")) = True Then
                strCC = ConfigurationManager.AppSettings("Email_CC").ToString()
            End If

            If ConfigurationManager.AppSettings("Email_BCC") <> String.Empty Then


                CommonFunctions.SendEmail("ftss@fujifilmgs.com", "Fujifilm Technical Services", strEmailTo, strCC, strEmailBody, "Review parts allocated for " & svccall & "- " & Cust_State, strType:="html", strBCC:=ConfigurationManager.AppSettings("Email_BCC").ToString())
            Else
                CommonFunctions.SendEmail("ftss@fujifilmgs.com", "Fujifilm Technical Services", strEmailTo, strCC, strEmailBody, "Review parts allocated for " & svccall & "- " & Cust_State, strType:="html")
            End If
        End If
            
           
    Catch ex As Exception
        Throw New Exception(ex.ToString)
    End Try

End Sub



''' <summary>
''' 
''' </summary>
''' <param name="svccall"></param>
''' <remarks></remarks>
Private Sub UpdateEmailSent(ByVal svccall As String )
    
    Dim strSQL As String = Nothing
    strSQL = "UPDATE cust_partstotalschedulers " & _
              "SET email_sent = 1, emailsent_dt = GETDATE() " & _
              "WHERE service_call_id ='" & svccall & "'"
                
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
