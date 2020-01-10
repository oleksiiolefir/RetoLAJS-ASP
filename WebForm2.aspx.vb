Imports MySql.Data.MySqlClient

Public Class WebForm2


    Inherits System.Web.UI.Page
    Dim texto As String = ""
    Dim capacidad As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString
        rellenoGrid(texto, capacidad)

        If Not Page.IsPostBack Then
            Llenar_DropDownList1()
            '' Llenar_DropDownList2()
        End If

    End Sub

    Protected Sub rellenoGrid(texto, capacidad)
        Try
            Dim SQLsentence As String

            If capacidad.Equals("") Then
                SQLsentence = "select * from alojamiento where localidad like('" + texto + "')"
            ElseIf Not capacidad.Equals("") Then
                SQLsentence = "select * from alojamiento where  localidad like ('" + texto + "')" + "AND capacidad =" + capacidad
            End If

            If texto.Equals("") And capacidad.Equals("") Then
                SQLsentence = "select * from alojamiento"
            End If



            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter(SQLsentence, cnn)
            da.Fill(ds, "cliente")
            GridView1.DataSource = ds.Tables("cliente")
            GridView1.DataBind()
            cnn.Close()
            Label1.Text = "SE HA CONECTADO"
        Catch ex As Exception
            Label1.Text = "NOOOOOOOOO"
        End Try


    End Sub

    Private Sub Llenar_DropDownList1()
        Try
            'abrimos la conexion hacia sql
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("conectar")
            Dim ds As New DataSet
            'recuperamos los datos desde sql
            Dim da As New MySqlDataAdapter("select distinct localidad from alojamiento ", cnn)
            da.Fill(ds, "localidad")
            DropDownList1.DataSource = ds.Tables("localidad")

            Me.DropDownList1.DataTextField = "localidad" 'valor a mostrar
            DropDownList1.DataValueField = "localidad"
            Me.DropDownList1.DataBind()
            cnn.Close()
        Catch ex As Exception


        End Try
    End Sub
    Private Sub Llenar_DropDownList2()
        Try
            'abrimos la conexion hacia sql
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("conectar")
            Dim ds As New DataSet
            Dim ds2 As New DataSet
            'recuperamos los datos desde sql
            Dim da As New MySqlDataAdapter("select distinct provincia from alojamiento ", cnn)
            da.Fill(ds, "provincia")
            Dim da2 As New MySqlDataAdapter("select max(idCli) from cliente ", cnn)
            da2.Fill(ds2, "maxcont")

            DropDownList1.DataSource = ds.Tables("provincia")

            Me.DropDownList1.DataTextField = "provincia" 'valor a mostrar
            DropDownList1.DataValueField = "provincia"
            Me.DropDownList1.DataBind()
            cnn.Close()
        Catch ex As Exception


        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("WebForm1.aspx")
    End Sub



    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        texto = DropDownList1.SelectedValue
        If TextBox1.Text.Equals("") Then
            capacidad = ""
        Else
            capacidad = CInt(TextBox1.Text)
        End If

        Label3.Text = capacidad
        Label2.Text = texto
        rellenoGrid(texto, capacidad)
    End Sub

End Class