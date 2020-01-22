Imports MySql.Data.MySqlClient

Public Class WebForm2


    Inherits System.Web.UI.Page
    Dim texto As String = ""
    Dim capacidad As String = ""
    Dim tipo As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString
        rellenoGrid(texto, capacidad, tipo)

        If Not Page.IsPostBack Then
            Llenar_DropDownList1()
            Llenar_DropDownList2()
        End If

    End Sub

    Protected Sub rellenoGrid(texto, capacidad, tipo)
        Try
            Dim SQLsentence As String

            If capacidad.Equals("") Then
                SQLsentence = "select * from alojamiento where localidad like('" + texto + "') And tipo = '" + tipo + "'"
            ElseIf Not capacidad.Equals("") Then
                SQLsentence = "select * from alojamiento where  localidad like ('" + texto + "') And tipo = '" + tipo + "'" + " AND capacidad >=" + capacidad
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
            Dim da As New MySqlDataAdapter("select distinct localidad from alojamiento order by localidad asc", cnn)
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
            Dim da As New MySqlDataAdapter("select distinct tipo from alojamiento order by tipo asc", cnn)
            da.Fill(ds, "tipo")

            DropDownList2.DataSource = ds.Tables("tipo")

            Me.DropDownList2.DataTextField = "tipo" 'valor a mostrar
            DropDownList2.DataValueField = "tipo"
            Me.DropDownList2.DataBind()
            cnn.Close()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim valor As String = Request.Params("parametro")
        Response.Redirect("WebForm1.aspx?parametro=" + valor)
    End Sub



    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        texto = DropDownList1.SelectedValue
        tipo = DropDownList2.SelectedValue
        If TextBox1.Text.Equals("") Then
            capacidad = ""
        Else
            capacidad = CInt(TextBox1.Text)
        End If

        Label3.Text = capacidad
        Label2.Text = texto
        rellenoGrid(texto, capacidad, tipo)
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim Sqlsentence As String = "select * from alojamiento"
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter(Sqlsentence, cnn)
            da.Fill(ds, "cliente")
            GridView1.DataSource = ds.Tables("cliente")
            GridView1.DataBind()
            cnn.Close()
            Label1.Text = "SE HA CONECTADO"
        Catch ex As Exception
            Label1.Text = "NOOOOOOOOO"
        End Try
    End Sub
End Class