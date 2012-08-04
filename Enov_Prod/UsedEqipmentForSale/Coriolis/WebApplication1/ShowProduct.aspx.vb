Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Partial Public Class ShowProduct
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Not IsNothing(Request.QueryString("ProductID")) AndAlso Request.QueryString("ProductID") <> String.Empty Then
                    If IsNumeric(Request.QueryString("ProductID")) = True Then

                         LoadProductData()
                    End If

                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString())
        End Try
    End Sub

    Private Sub LoadProductData()

        Dim objDR As SqlDataReader = Nothing
        Dim objCmd As New SqlCommand
        Dim objDataAccess As New FGSU_TOOLS.DataService
        Try

            Me.LTRL_ProductID.Text = Request.QueryString("ProductID").ToString()

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "uspGetProductData"

            objCmd.Parameters.AddWithValue("@ProductID", Me.LTRL_ProductID.Text)
            objDR = objDataAccess.GetDataReader(objCmd)
            If Not IsNothing(objDR) AndAlso objDR.HasRows = True Then
                objDR.Read()
                ImgProduct.ImageUrl = "ShowImage.aspx?ProductID=" & Me.LTRL_ProductID.Text
                DivImgPreview.Visible = True

                Me.lblSerialNumber.Text = objDR.Item("SerialNumber").ToString()
                Me.lblDescription.Text = objDR.Item("Description").ToString()
                Me.lblModel.Text = objDR.Item("Model").ToString()
                Me.lblModelHeader.Text = objDR.Item("Model").ToString()
                Me.lblYearBuilt.Text = objDR.Item("YearBuilt").ToString()
                Me.lblListPrice.Text = String.Format("{0:c}", objDR.Item("ListPrice").ToString())
                Me.lblProductType.Text = objDR.Item("ProductType").ToString()
                If Not (objDR.IsDBNull(objDR.GetOrdinal("PowerSupply"))) Then
                    Me.lblPower.Text = objDR.Item("PowerSupply").ToString()
                End If
                If Not (objDR.IsDBNull(objDR.GetOrdinal("Usage"))) Then
                    Me.lblUsage.Text = objDR.Item("Usage").ToString()
                End If
                If Not (objDR.IsDBNull(objDR.GetOrdinal("CurrentStatus"))) Then
                    Me.lblCurrentStatus.Text = objDR.Item("CurrentStatus").ToString()

                End If
                If Not (objDR.IsDBNull(objDR.GetOrdinal("PrintSize"))) Then
                    Me.lblPrintSize.Text = objDR.Item("PrintSize").ToString()
                Else

                End If

                If Not (objDR.IsDBNull(objDR.GetOrdinal("ProductSOCPath"))) Then
                    Me.lnkWarranty.NavigateUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, System.Web.HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + objDR.Item("ProductSOCPath").ToString()
                    Me.lnkWarranty.Target = "_blank"
                    Me.lnkWarranty.Text = "Warranty Detail"
                End If

                If Not (objDR.IsDBNull(objDR.GetOrdinal("PhysicalDimension"))) Then
                    Me.lblPhysicalDimension.Text = objDR.Item("PhysicalDimension").ToString()
                End If

                If Not (objDR.IsDBNull(objDR.GetOrdinal("CurrentLocation"))) Then
                    Me.lblCurrLocation.Text = objDR.Item("CurrentLocation").ToString()
                End If

                If Not (objDR.IsDBNull(objDR.GetOrdinal("DateAvailable"))) Then
                    Me.lblDateAvailable.Text = objDR.Item("DateAvailable").ToString()
                End If

                If Not (objDR.IsDBNull(objDR.GetOrdinal("ProductSpecURI"))) Then
                    Me.lnkDetailSpec.NavigateUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, System.Web.HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + objDR.Item("ProductSpecURI").ToString()
                    Me.lnkDetailSpec.Target = "_blank"
                    Me.lnkDetailSpec.Text = "Detail Spec"

                End If

                If objDR.Item("Notes").ToString() <> String.Empty Then
                     Me.lblNotes.Text = "<b>Notes:</b> " & objDR.Item("Notes").ToString()
                Else

                End If


                'This code binds the thumnail images of the product
                PopulateRepeater()
            End If

        Catch ex As Exception
             Throw New Exception(ex.ToString())
        End Try
    End Sub


    Private Sub PopulateRepeater()

        If My.Computer.FileSystem.DirectoryExists(Me.Server.MapPath("~/images/") & Me.LTRL_ProductID.Text) Then
            Dim files As IList(Of FileInfo) = New List(Of FileInfo)()
            Dim filters As String = "*.jpg;*.png;*.gif"

            For Each filter As String In filters.Split(";"c)
                Dim fit As FileInfo() = New DirectoryInfo(Me.Server.MapPath("~/images/" & Me.LTRL_ProductID.Text & "/")).GetFiles(filter)
                For Each fi As FileInfo In fit
                    files.Add(fi)
                Next
            Next

            Me.Repeater1.DataSource = files
            Me.Repeater1.DataBind()

        End If
        
    End Sub

End Class