Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration

Partial Public Class TempRH_Default
    Inherits System.Web.UI.Page

Dim objDS As New DataSet
Dim objConn As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

Dim dtCreatedDT As Date             'Created Date and Time
Dim dtCreatedGMT As Date            'Created Universal Time

Dim strTRHDeviceName As String      'Device Name
Dim strTRHDeviceType As String      'Device Type
Dim strTRHDeviceSerialNum As String 'Device Serial Num

Dim dRH As Decimal                  'RH as Decimal
Dim dTemp As Decimal                'Temp as Celcius Decimal

''' <summary>
''' Parse the Posted Temp RH File
''' </summary>
''' <remarks>Author:Chirag Shah</remarks>

Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
        'Declar a Byte Array
        'Reference: http://www.vikramlakhotia.com/Reading_Post_data_in_the_Request_Object.aspx
        If Request.TotalBytes = 0 Then
            Response.Write("No Data")
            Response.End()
        End If

        Dim buffer As Byte() = New Byte(Request.ContentLength - 1) {}
        Request.InputStream.Read(buffer, 0, Request.ContentLength)

        'Convert Byte Array into an Input Stream
        Dim strTHXMLFile As String = System.Text.UTF8Encoding.UTF8.GetString(buffer)

        Dim rdr As System.Xml.XmlReader = System.Xml.XmlReader.Create(New System.IO.StringReader(strTHXMLFile))

        objDS.ReadXml(rdr)

        If objDS.Tables.Contains("cpvTH") Then

           dtCreatedDT = objDS.Tables("cpvTH").Rows(0).Item("created")
           dtCreatedGMT = objDS.Tables("cpvTH").Rows(0).Item("createdgmt")

           strTRHDeviceName = objDS.Tables("cpvTH").Rows(0).Item("thname")
           strTRHDeviceType = objDS.Tables("cpvTH").Rows(0).Item("thtype")
           strTRHDeviceSerialNum = objDS.Tables("cpvTH").Rows(0).Item("thserial")

           dRH = objDS.Tables("cpvTH").Rows(0).Item("rh")
           dTemp = objDS.Tables("cpvTH").Rows(0).Item("tempC")
        Else
            Throw New Exception("No Temp and RH data Posted")
        End If

        If SaveTRHData() Then
            Response.Write("ok")
        End If

    Catch ex As Exception
      
    
    End Try

End Sub


Private Function SaveTRHData() As Boolean
'Open Connection 
'Create A Command Object
'Create SQLParameters Object
'Save Data into Database

    objConn.Open()

    Dim objCmd As New SqlCommand
    Try
        With objCmd
            .CommandText = "uspInsertCPTRHData"
            .CommandType = CommandType.StoredProcedure
            .Connection = objConn
        End With


        'CreatedDate as Parameter
        If dtCreatedDT <> DateTime.MinValue Then
            Dim SQLParam1 As New SqlParameter()
            With SQLParam1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
                .Value = dtCreatedDT
                .ParameterName = "@CreatedDT"
            End With

            objCmd.Parameters.Add(SQLParam1)
        End If

        'CreatedDateGMT as Parameter
        If dtCreatedGMT <> DateTime.MinValue Then
            Dim SQLParam2 As New SqlParameter()
            With SQLParam2
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
                .Value = dtCreatedGMT
                .ParameterName = "@CreatedGMT"
            End With

            objCmd.Parameters.Add(SQLParam2)
        End If

        'TRHDeviceName
        If strTRHDeviceName <> String.Empty Then
            Dim SQLParam3 As New SqlParameter()
            With SQLParam3
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                .Value = strTRHDeviceName
                .ParameterName = "@TRHDeviceName"
            End With

            objCmd.Parameters.Add(SQLParam3)

        End If

        'TRHDeviceType
        If strTRHDeviceType <> String.Empty Then
            Dim SQLParam4 As New SqlParameter()
            With SQLParam4
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                .Value = strTRHDeviceType
                .ParameterName = "@TRHDeviceType"
            End With

            objCmd.Parameters.Add(SQLParam4)

        End If

        'TRHDevice Serial Num
        If strTRHDeviceSerialNum <> String.Empty Then
            Dim SQLParam5 As New SqlParameter()
            With SQLParam5
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                .Value = strTRHDeviceSerialNum
                .ParameterName = "@TRHDeviceSerialNum"
            End With

            objCmd.Parameters.Add(SQLParam5)

        End If

        'Temp Data
        If dTemp <> 0 Then
            Dim SQLParam6 As New SqlParameter()
            With SQLParam6
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Decimal
                .Value = dTemp
                .ParameterName = "@Temp"
            End With

            objCmd.Parameters.Add(SQLParam6)

        End If

        'RH Data
        If dRH <> 0 Then
            Dim SQLParam7 As New SqlParameter()
            With SQLParam7
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Decimal
                .Value = dRH
                .ParameterName = "@RH"
            End With

            objCmd.Parameters.Add(SQLParam7)
        End If

        objCmd.ExecuteNonQuery()

    Catch ex As Exception
    'Perform Error Logging

    Finally
        objCmd = Nothing
        If objConn.State = ConnectionState.Open Then
            objConn.Close()
        End If
        objConn = Nothing
    End Try
    Return True
End Function

End Class