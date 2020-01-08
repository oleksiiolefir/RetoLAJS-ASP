Imports MySql.Data.MySqlClient

Public Class WebForm2


    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString
        Llenar_Lista()
    End Sub
    Protected Sub llamodatos(texto)
        Try
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter("select * from alojamiento where localidad like('" + texto + "')", cnn)
            da.Fill(ds, "cliente")
            GridView1.DataSource = ds.Tables("cliente")
            GridView1.DataBind()

            Label1.Text = "SE HA CONECTADO"
        Catch ex As Exception
            Label1.Text = "NOOOOOOOOO"
        End Try


    End Sub

    Private Sub Llenar_Lista()
        Try
            'ABRIMOS LA CONEXION HACIA SQL
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            'RECUPERAMOS LOS DATOS DESDE SQL
            Dim da As New MySqlDataAdapter("select distinct localidad from alojamiento ", cnn)
            da.Fill(ds, "localidad")
            DropDownList1.DataSource = ds.Tables("localidad")
            DropDownList1.DataValueField = "localidad"
            Me.DropDownList1.DataTextField = "localidad" 'Valor a Mostrar
            Me.DropDownList1.DataBind()

        Catch ex As Exception


        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("WebForm1.aspx")
    End Sub



    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim texto As String
        texto = DropDownList1.SelectedValue
        Label2.Text = texto
        llamodatos(texto)
    End Sub

End Class