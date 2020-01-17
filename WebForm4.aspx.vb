Imports MySql.Data.MySqlClient

Public Class WebForm4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString

        RellenarAlojamientos()
        llamodatos()
    End Sub

    Protected Sub RellenarAlojamientos()
        Try
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter("SELECT * FROM `reserva` ", cnn)
            da.Fill(ds, "reserva")
            GridView3.DataSource = ds.Tables("reserva")
            GridView3.DataBind()
            Label1.Text = "SE HA CONECTADO"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Protected Sub llamodatos()
        Try
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter("select * from usuario ", cnn)
            da.Fill(ds, "usuario")
            GridView1.DataSource = ds.Tables("usuario")
            GridView1.DataBind()
            Label1.Text = "SE HA CONECTADO"

        Catch ex As Exception
            Label1.Text = "NOOOOOOOOO"
        End Try

    End Sub
End Class