Public Class Form1

Private Sub Button1_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles Button1.Click
    Try
        Dim obj AS New wsTaskero.ermsweb
        dim str as String =        obj.Test_Comm()
        MessageBox.Show(str)
    Catch ex As Exception
        MessageBox.Show(ex.ToString)
    End Try
    
    

End Sub

Private Sub btnTestWCF_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnTestWCF.Click
  Try
        Dim client As Service1Client = New Service1Client("WSHttpBinding_IService1")
        ' Use the 'client' variable to call operations on the service.

        ' Always close the client.
       dim str as String = client.GetData(5)
        
        Debug.Write(str)

  Catch ex As Exception
        MessageBox.Show(ex.ToString)
  End Try
End Sub

Private Sub Button2_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles Button2.Click
    Dim session As SEALAPI.Broker    
    Dim iSession As Integer
    Dim output as string = string.Empty
    Try
        
        session = New SEALAPI.Broker()
        iSession = session.OpenSession("default","929","canoe#") 
        If iSession >= 1 Then
            'If Output = String.Empty Then
               output = session.SendBO(iSession, "send_report_from_appc " & "47013421@@1")
               messagebox.Show(output)
            'End if
            session.CloseSession(iSession) 
        End If
         
   
    Catch ex As Exception
        MessageBox.Show(ex.ToString)
    End Try

End Sub
End Class
