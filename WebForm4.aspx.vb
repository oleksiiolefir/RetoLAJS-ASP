Imports MySql.Data.MySqlClient

Public Class WebForm4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = "TERCERA PAG"
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString
        llamodatos()
    End Sub
    Protected Sub llamodatos()
        Try
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter("select * from reserva where idAloj = 6915", cnn)
            da.Fill(ds, "reserva")
            GridView1.DataSource = ds.Tables("reserva")
            GridView1.DataBind()
            Label1.Text = "SE HA CONECTADO"
        Catch ex As Exception
            Label1.Text = "NOOOOOOOOO"
        End Try

    End Sub
End Class