Imports MySql.Data.MySqlClient
Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim valor As String = Request.Params("parametro")
        Response.Redirect("WebForm2.aspx?parametro=" + valor)

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim valor As String = Request.Params("parametro")
        Response.Redirect("WebForm3.aspx?parametro=" + valor)
        MsgBox(valor)
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim valor As String = Request.Params("parametro")
        Response.Redirect("WebForm4.aspx?parametro=" + valor)
    End Sub
End Class