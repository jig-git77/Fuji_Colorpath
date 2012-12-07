Imports System.Data
Imports System.Data.SqlClient

Partial Public Class server_check
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim intLen As Integer = 0
        Try
            If Not Page.IsPostBack Then

                intLen = Request.TotalBytes
                Dim strbyte() As Byte = Request.BinaryRead(intLen)

                'Convert a byte array to a string:
                Dim objEncoding As New System.Text.ASCIIEncoding

                If objEncoding.GetString(strbyte) = "Hello Taskero" Then

                    'Check Database
                    Dim strDBStatus As String = CheckSQLDB()
                    If strDBStatus <> "OK" Then
                       Response.Write("Error connectiong to taskero database: " & strDBStatus)
                       HttpContext.Current.ApplicationInstance.CompleteRequest()
                       Exit Sub
                    Else
                        Response.Write("OK")
                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                        Exit Sub
                    End If


                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Function CheckSQLDB() As String
        Dim objCmd As New SqlCommand()
        Dim strReturn As String = Nothing
        Try
            With objCmd
                .CommandText = "SELECT TOP 1 CPPatchDataLabID FROM dbo.CPPatchDataLab ORDER BY CPPatchDataLabID DESC"
                .CommandType = CommandType.Text
            End With

            Dim objDataAccess As New DataAccess
            Dim objDR As SqlDataReader = objDataAccess.GetDataReader(objCmd)
            If Not IsNothing(objDR) And objDR.HasRows = True Then
                strReturn = "OK"
            End If
        Catch ex As Exception
            strReturn = (ex.Message)
        End Try
            Return strReturn
    End Function


End Class