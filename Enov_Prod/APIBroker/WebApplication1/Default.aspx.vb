Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    MailHelper.SendMailMessage("cshah@fujifilmgs.com","chiragiit@yahoo.com","","shahchi1@iit.edu","Test", "test")
End Sub
End Class