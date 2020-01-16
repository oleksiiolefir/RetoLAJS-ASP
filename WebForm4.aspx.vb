Imports MySql.Data.MySqlClient

Public Class WebForm4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString
        Label1.Text = "TERCERA PAG"
        llamodatos()
        rellenarAlojamientos()
    End Sub

    Protected Sub rellenarAlojamientos()
        Try
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter("select * from alojamiento ", cnn)
            da.Fill(ds, "alojamiento")
            GridView2.DataSource = ds.Tables("alojamiento")
            GridView2.DataBind()
            Label1.Text = "SE HA CONECTADO"
        Catch ex As Exception
            Label1.Text = "NOOOOOOOOO"
        End Try

    End Sub
    Protected Sub llamodatos()
        Try
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter("select * from usuario ", cnn)
            da.Fill(ds, "reserva")
            GridView1.DataSource = ds.Tables("reserva")
            GridView1.DataBind()
            Label1.Text = "SE HA CONECTADO"
            cnn.Close()
        Catch ex As Exception
            Label1.Text = "NOOOOOOOOO"
        End Try

    End Sub

    Protected Sub GridView2_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView2.RowEditing

    End Sub
End Class