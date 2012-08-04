Imports System.Data
Imports System.Data.SqlClient

Partial Public Class ShowImage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            DisplayImage()
        End If

    End Sub


    Private Sub DisplayImage()
        Dim objDR As SqlDataReader = Nothing
        Dim objCmd As New SqlCommand
        Dim objDataAccess As New FGSU_TOOLS.DataService
        Try
             objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "uspGetProductData"

            objCmd.Parameters.AddWithValue("@ProductID", Request.QueryString("ProductID"))
            objDR = objDataAccess.GetDataReader(objCmd)
            If Not IsNothing(objDR) AndAlso objDR.HasRows = True Then
                objDR.Read()

                Dim imgbyte As Byte() = DirectCast(objDR.Item("Photo"), Byte())

                Response.Buffer = True
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = ""
                Response.AddHeader("content-disposition", "attachment;filename=photo.jpg")
                Response.BinaryWrite(imgbyte)
                Response.Flush()
                HttpContext.Current.ApplicationInstance.CompleteRequest()


            End If
        Catch ex As Exception
              Throw New Exception(ex.ToString())
        End Try
    End Sub

End Class