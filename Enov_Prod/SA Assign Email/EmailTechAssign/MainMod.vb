Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports FGSU_TOOLS

Module MainMod
Dim objCmd As SqlCommand
Dim objDR As SqlDataReader

Dim strEmailBody As String = String.Empty

Public Sub Main()

    Try

        'Determine the service call for which e-mail needed to be sent, i.e status = 200 sa assigned
        'insert those call in a custom table with email sent to false
        Call FindServiceCall_SA_Assign()

        'Get the service call for which email need to be sent
        Call GetServiceCall()

    Catch ex As Exception

            CommonFunctions.SendEmail("ftss@fujifilmgs.com", "Fujifilm Technical Services", "mwinn@fujifilm.com", "", ex.ToString(), "Error in program: send email service agent assigned")

  End Try

End Sub


Private Sub FindServiceCall_SA_Assign()
     Try
        Dim strSQL As String = String.Empty
        strSQL = "insert into cust_service_agent_assign (order_id, prv_order_stat_id, order_stat_uniq_id,last_action_dt,service_level_id, sa_person_id,sa_name) " & _
                 "select order_line.order_id, order_line.prv_order_stat_id, order_line.order_stat_uniq_id,order_line.last_action_dt,  order_line.service_level_id, service_call.sa_person_id,search_name " & _
                 "from order_line " & _
                 "inner join service_call on order_line.order_id=service_call.order_id " & _
                 "inner join person on person.person_id = service_call.sa_person_id " & _
                 "where order_type_id = 'service_order' and order_line.order_stat_uniq_id = 200 and order_line.last_action_dt >= dateadd(mi,-15,getdate()) " & _
                 " and not exists (select * from cust_service_agent_assign as t where t.order_id  = service_call.order_id and t.sa_name = search_name) " & _
                 " order by order_line.last_action_Dt desc "

        objCmd = New SqlCommand()
        With objCmd
            .CommandText = strSQL
            .CommandType = CommandType.Text
        End With
        Dim objDataAccess As New FGSU_TOOLS.DataService
        Dim i As Integer = objDataAccess.ExecuteNonQuery(objCmd)


    Catch ex As Exception
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
        strSQL = "SELECT order_id FROM cust_service_agent_assign WHERE email_sent = 0"
        objCmd = New SqlCommand()
        With objCmd
            .CommandText = strSQL
            .CommandType = CommandType.Text
        End With
        Dim objDataAccess As New FGSU_TOOLS.DataService
        objDR = objDataAccess.GetDataReader(objCmd)

        If Not IsNothing(objDR) AndAlso objDR.HasRows = True Then
            While objDR.Read()
                 Dim svc_call_id As String = Convert.ToString(objDR.Item("order_id"))
                 SendEmailWithCallDetail(svc_call_id)
                 UpdateEmailSent(svc_call_id)
            End While
        End If

    Catch ex As Exception
        Throw New Exception(ex.ToString)
    End Try

End Sub



''' <summary>
''' Get detailed information for the service call
''' </summary>
''' <param name="svc_call_id"></param>
''' <remarks></remarks>
Private Sub SendEmailWithCallDetail(ByVal svc_call_id As String)


    Dim strEmailTo As String = String.Empty
    Dim objDR2 As SqlDataReader = Nothing

    Dim objDR3 As SqlDataReader = Nothing

   Dim strSubject As String = String.Empty

    Dim row As String = String.Empty
    Dim phone_acitvity As String = String.Empty

    Dim strSQL1 As String = "select coalesce(site_company_descr,cust_company_descr) as company, address.address_1 as street_addres, address.city, address.state_prov_id as state, ISNULL(convert(varchar(20), eta_date, 101), '') as eta_date , " & _
                            "address.zip, order_line.order_id as servicecall, service_level.descr as contract, warr_type.descr as warranty, service_call.sa_person_id,  search_name, person.email_id, " & _
                            "order_line.caller_name, order_line.phone_id as caller_phone , order_line.problem_desc, service_call.bpart_id as model, service_call.serial_no, priority_class_id,  ISNULL(order_env.opsys_desc, 'None') as SchedulingNotes " & _
                            " from order_line inner join service_call on order_line.order_id=service_call.order_id " & _
                            " inner join service_call_env on service_call_env.order_id = order_line.order_id " & _
                            " inner join person on person.person_id = service_call.sa_person_id " & _
                            " inner join address on order_line.ship_to_address_id=address.address_id " & _
                            " inner join priority_class on order_line.priority = priority_class.fr_priority " & _
                            "  inner join order_env on order_env.order_id = order_line.order_id " & _
                            " left join service_level  on service_level.service_level_id= order_line.service_level_id " & _
                            " left join warr_type on service_call.warr_type_id =  warr_type.warr_type_id " & _
                            " where order_type_id = 'service_order' and order_stat_uniq_id = 200 " & _
                            " and order_line.order_id = '" & svc_call_id & "' order by last_action_Dt desc "

    'parts orderd for the call
    Dim strSQL2 As String = "select order_line.order_id, bpart.bpart_id as partnumber, bpart.descr as partname, demand.qty, cast(price as decimal(10,2)) as ListPrice " & _
                             "from  order_line inner join demand on order_line.order_id  = demand.order_id and demand.bpart_gcl_id = 'Goods' " & _
                             "inner join  bpart on bpart.bpart_id = demand.bpart_id " & _
                             "where order_line.order_id = '" & svc_call_id & "'"


    'phone activity
    'Dim strSQL3 As String = "select comment_text from demand where order_id = '" & svc_call_id & "' and bpart_id = 'phone_act' and comment_text is not null order by demand_id"
    Dim strSQL3 As String = "select coalesce((select cast(long_text as varchar(8000)) " & _
                            "from long_text where  long_text.table_name = 'demand' and long_text.table_key = demand.demand_id), comment_text) as comment_text  " & _
                             "from demand where order_id = '" & svc_call_id & "' and bpart_id = 'phone_act' and comment_text is not null " & _
                             "order by demand_id "

    strEmailBody = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\email.html")

    Try

        objCmd = New SqlCommand()
        With objCmd
            .CommandText = strSQL1
            .CommandType = CommandType.Text
        End With

        Dim objDataAccess As New FGSU_TOOLS.DataService
        Using objDR1 As SqlDataReader = objDataAccess.GetDataReader(objCmd)

            While objDR1.Read()

                strEmailBody = strEmailBody.Replace("###ServiceCallNum###", svc_call_id)
                strEmailBody = strEmailBody.Replace("###Company###", objDR1("company").ToString())

                strEmailBody = strEmailBody.Replace("###Address###", objDR1("street_addres").ToString())
                strEmailBody = strEmailBody.Replace("###City###", objDR1("City").ToString())
                strEmailBody = strEmailBody.Replace("###State###", objDR1("State").ToString())
                strEmailBody = strEmailBody.Replace("###Zip###", objDR1("Zip").ToString())

                strEmailBody = strEmailBody.Replace("###Model###", objDR1("Model").ToString())
                strEmailBody = strEmailBody.Replace("###ProblemDesc###", objDR1("problem_desc").ToString())

                If objDR1("serial_no").ToString() <> String.Empty Then
                    strEmailBody = strEmailBody.Replace("###Serial Num###", objDR1("serial_no").ToString())
                Else
                    strEmailBody = strEmailBody.Replace("###Serial Num###", "--NA--")
                End If

                If objDR1("Warranty").ToString() <> String.Empty Then
                strEmailBody = strEmailBody.Replace("###Warranty###", objDR1("Warranty").ToString())
                Else
                 strEmailBody = strEmailBody.Replace("###Warranty###", "--NA--")
                End If

                strEmailBody = strEmailBody.Replace("###Contract###", objDR1("contract").ToString())

                If objDR1("priority_class_id").ToString() = "COD" Then
                     strEmailBody = strEmailBody.Replace("###Priority###", " ***COD***. You must collect payment prior to delivering service.")
                Else
                     strEmailBody = strEmailBody.Replace("###Priority###", objDR1("priority_class_id").ToString())
                End If

                strEmailBody = strEmailBody.Replace("###AssignedTo###", objDR1("search_name").ToString())

                strEmailBody = strEmailBody.Replace("###Caller###", objDR1("caller_name").ToString())
                strEmailBody = strEmailBody.Replace("###CallerPhone###", objDR1("caller_phone").ToString())
                strEmailBody = strEmailBody.Replace("###SchedulingNotes###", objDR1("SchedulingNotes").ToString())

                ''email addresses of the techs to whom we need to send e-mails
                If objDR1("email_id").ToString() <> String.Empty Then
                    strEmailTo = objDR1("email_id").ToString()
                End If

                Dim objCmd2 As New SqlCommand()
                With objCmd2
                    .CommandType = CommandType.Text
                    .CommandText = strSQL2
                End With

                objDR2 = objDataAccess.GetDataReader(objCmd2)
                If objDR2.HasRows = True Then
                    strEmailBody = strEmailBody.Replace("###Visible###", "true")
                    While objDR2.Read()
                      row = row & "<tr><td style='font-family:Courier New;font-size: small;font-weight: normal;'>" & objDR2.Item("partnumber").ToString() & "</td><td style='font-family:Courier New;font-size: small;font-weight: normal;'>" & objDR2.Item("partname").ToString() & "</td><td style='font-family:Courier New;font-size: small;font-weight: normal;'>" & objDR2.Item("qty").ToString() & "</td><td style='font-family:Courier New;font-size: small;font-weight: normal;'>" & objDR2.Item("ListPrice").ToString() & "</td></tr>"
                    End While
                Else
                    strEmailBody = strEmailBody.Replace("###Visible###", "false")
                     strEmailBody = strEmailBody.Replace("Part Number", String.Empty)
                    strEmailBody = strEmailBody.Replace("Parts", String.Empty)
                    strEmailBody = strEmailBody.Replace("Qty", String.Empty)
                    strEmailBody = strEmailBody.Replace("List Price", String.Empty)
                End If

                strEmailBody = strEmailBody.Replace("###rows###", row)

                'Phone support activity
                Dim objCmd3 As New SqlCommand()
                With objCmd3
                    .CommandType = CommandType.Text
                    .CommandText = strSQL3
                End With
                objDR3 = objDataAccess.GetDataReader(objCmd3)
                If objDR3.HasRows = True Then
                    Dim idx As Integer = 1
                    While objDR3.Read()
                         If objDR3("comment_text").ToString().Trim() <> String.Empty Then
                                phone_acitvity = phone_acitvity & idx.ToString() & ") " & objDR3("comment_text").ToString() & " " & ControlChars.CrLf
                                idx = idx + 1
                         End If
                    End While
                Else
                     strEmailBody = strEmailBody.Replace("###display_trc###", "none")
                End If

                strEmailBody = strEmailBody.Replace("###PhoneActivity###", phone_acitvity)


                strSubject = "Call Assignment (" & svc_call_id.Substring(0, Len(svc_call_id) - 3) & " ) for " & objDR1("company").ToString() & " ETA: " & objDR1("eta_date").ToString()

            End While

            If strEmailTo <> String.Empty Then

                If ConfigurationManager.AppSettings("Email_BCC") <> String.Empty Then
                    CommonFunctions.SendEmail("ftss@fujifilmgs.com", "Fujifilm Technical Services", strEmailTo, "", strEmailBody, strSubject, strType:="html", strBCC:=ConfigurationManager.AppSettings("Email_BCC").ToString())
                Else
                    CommonFunctions.SendEmail("ftss@fujifilmgs.com", "Fujifilm Technical Services", strEmailTo, "", strEmailBody, strSubject, strType:="html")
                End If
            End If

        End Using


    Catch ex As Exception
        Throw New Exception("Service Call " & svc_call_id & ex.ToString())
    End Try
End Sub



Private Sub UpdateEmailSent(ByVal svc_call_id As String)
     Try

       Dim strSQL As String = String.Empty
       strSQL = "UPDATE cust_service_agent_assign SET email_sent = 1 WHERE  order_id = '" & svc_call_id & "'"

        objCmd = New SqlCommand()
        With objCmd
            .CommandText = strSQL
            .CommandType = CommandType.Text
        End With
        Dim objDataAccess As New FGSU_TOOLS.DataService
        Dim i As Integer = objDataAccess.ExecuteNonQuery(objCmd)

    Catch ex As Exception
        Throw New Exception(ex.ToString)
    End Try
End Sub

End Module
