Imports System.Data
Imports System.Data.SqlClient

Partial Class _Default
    Inherits System.Web.UI.Page

Protected Sub Gridview1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)


    Try
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ImgView_New As System.Web.UI.HtmlControls.HtmlImage
            ImgView_New = DirectCast(e.Row.FindControl("ImgView_New"), System.Web.UI.HtmlControls.HtmlImage)

            Dim _MeasurementStr As String = String.Empty
            _MeasurementStr = "ShowProduct.aspx?ProductID="

            Dim _ProductId As String = DataBinder.Eval(e.Row.DataItem, "ProductID")
            _MeasurementStr = _MeasurementStr & _ProductId


            ImgView_New.Attributes.Add("onclick", "OpenProductWindow('" + _MeasurementStr + "', true, true,'" + _ProductId + "');")



        End If
     Catch ex As Exception

    End Try
End Sub

''' <summary>
''' 
''' </summary>
''' <param name="sender"></param>
''' <param name="e"></param>
''' <remarks></remarks>
Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack = True Then

                BindProductTypeDropDown()
                FillProductGrid()

            End If
        Catch ex As Exception

        End Try
 End Sub


Private Sub BindProductTypeDropDown()
        Dim objCmd As New SqlCommand
        Dim objDataAccess As New FGSU_TOOLS.DataService
        Dim ProductTypeDS As DataSet = Nothing
        Try

            Me.ddlProductType.Items.Clear()


            objCmd.CommandText = "uspGetProductType"
            objCmd.CommandType = CommandType.StoredProcedure
            ProductTypeDS = objDataAccess.GetDataSet(objCmd)

            If (ProductTypeDS.Tables.Count > 0) Then
                Me.ddlProductType.DataSource = ProductTypeDS.Tables(0)
                Me.ddlProductType.DataValueField = "ProductTypeID"
                Me.ddlProductType.DataTextField = "ProductType"
                Me.ddlProductType.DataBind()
            End If

            Me.ddlProductType.Items.Insert(0, New ListItem("--- All ---", "0"))

        Catch ex As Exception
            Throw New Exception(ex.ToString())
        End Try
End Sub


Private Sub FillProductGrid()

        Dim objCmd As New SqlCommand
        Dim objDataAccess As New FGSU_TOOLS.DataService
        Dim ProductDS As DataSet
        Try
            objCmd.CommandText = "uspGetProductList"
            objCmd.CommandType = CommandType.StoredProcedure

            ProductDS = objDataAccess.GetDataSet(objCmd)

            If (ProductDS.Tables.Count > 0) Then
                  Me.GridView1.DataSource = ProductDS
                  Me.GridView1.DataBind()
            End If

            System.Web.HttpContext.Current.Session("dsProducts") = ProductDS

        Catch ex As Exception
            Throw New Exception(ex.ToString())
        End Try
End Sub

Protected Sub DDL_ProductType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim objCmd As New SqlCommand
        Dim objDataAccess As New FGSU_TOOLS.DataService
        Dim ProductDS As DataSet = Nothing
        Try
            If (ddlProductType.SelectedIndex <> 0) Then
                  objCmd.CommandText = "uspGetProductList"
                  objCmd.CommandType = CommandType.StoredProcedure

                  objCmd.Parameters.AddWithValue("@ProductTypeID", ddlProductType.SelectedValue.ToString())
                  ProductDS = objDataAccess.GetDataSet(objCmd)
            Else
                ProductDS = System.Web.HttpContext.Current.Session("dsProducts")
            End If

            '====  Re-Bind Grid  ====//
            Me.GridView1.DataSource = ProductDS
            Me.GridView1.DataBind()
        Catch Ex As Exception
            Throw New Exception(Ex.ToString())
        End Try

     End Sub
End Class
