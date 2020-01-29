Imports MySql.Data.MySqlClient
Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim valor As String = Request.Params("parametro")
        Response.Redirect("VerAlojamientos.aspx?parametro=" + valor)

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim valor As String = Request.Params("parametro")
        Response.Redirect("RealizarReserva.aspx?parametro=" + valor)
        MsgBox(valor)
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim valor As String = Request.Params("parametro")
        Response.Redirect("ModificarReserva.aspx?parametro=" + valor)
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Response.Redirect("InicioSession.aspx")
    End Sub

End Class