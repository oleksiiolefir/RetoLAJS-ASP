Public Class VerAlojamientosB
    Inherits System.Web.UI.Page

    Dim texto As String = ""
    Dim capacidad As String = ""
    Dim tipo As String
    Dim nombreAloj As String = ""
    Dim ordenacion As String = "ASC"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString
        If Session("logeado") = False Then
            Button4.Visible = False
        Else
            Button4.Visible = True
        End If
        rellenoGrid(texto, capacidad, tipo, nombreAloj, ordenacion)

        If Not Page.IsPostBack Then

            Llenar_DropDownList1()
            Llenar_DropDownList2()
        End If

    End Sub

    Protected Sub rellenoGrid(texto, capacidad, tipo, nombreAloj, ordenacion)
        Try
            ' MsgBox("AAAAA" + ordenacion)
            Dim SQLsentence As String = ""

            If capacidad.Equals("") And nombreAloj.Equals("") Then
                SQLsentence = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono,email,web,descripcion from alojamiento where localidad like('" + texto + "') And tipo = '" + tipo + "' order by nombre " + ordenacion
            ElseIf Not nombreAloj.Equals("") And Not capacidad.Equals("") Then
                SQLsentence = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono,email,web,descripcion from alojamiento where  localidad like ('" + texto + "') And tipo = '" + tipo + "' AND capacidad >= " + capacidad + " AND nombre like('%" + nombreAloj + "%') order by nombre " + ordenacion
            ElseIf Not capacidad.Equals("") Then
                SQLsentence = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono,email,web,descripcion from alojamiento where  localidad like ('" + texto + "') And tipo = '" + tipo + "' AND capacidad >= " + capacidad + " order by nombre " + ordenacion
            ElseIf Not nombreAloj.Equals("") Then
                SQLsentence = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono,email,web,descripcion from alojamiento where  localidad like ('" + texto + "') And tipo = '" + tipo + "' AND nombre like('%" + nombreAloj + "%') order by nombre " + ordenacion
            End If

            If texto.Equals("") And capacidad.Equals("") And nombreAloj.Equals("") Then
                SQLsentence = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono,email,web,descripcion from alojamiento order by nombre " + ordenacion
            End If

            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter(SQLsentence, cnn)
            da.Fill(ds, "cliente")
            GridView1.DataSource = ds.Tables("cliente")
            GridView1.DataBind()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex)
        End Try


    End Sub

    Private Sub Llenar_DropDownList1()
        Try
            'abrimos la conexion hacia sql
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("conectar")
            Dim ds As New DataSet
            'recuperamos los datos desde sql
            Dim da As New MySqlDataAdapter("select distinct localidad from alojamiento order by localidad  ASC", cnn)
            da.Fill(ds, "localidad")
            DropDownList1.DataSource = ds.Tables("localidad")

            Me.DropDownList1.DataTextField = "localidad" 'valor a mostrar
            DropDownList1.DataValueField = "localidad"
            Me.DropDownList1.DataBind()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex)
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
            Dim da As New MySqlDataAdapter("select distinct tipo from alojamiento order by tipo ASC", cnn)
            da.Fill(ds, "tipo")

            DropDownList2.DataSource = ds.Tables("tipo")

            Me.DropDownList2.DataTextField = "tipo" 'valor a mostrar
            DropDownList2.DataValueField = "tipo"
            Me.DropDownList2.DataBind()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CogerDatos()
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If RadioButton1.Checked Then
                ordenacion = "DESC"
            Else
                ordenacion = "ASC"
            End If

            Dim Sqlsentence As String = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono,email,web,descripcion from alojamiento order by nombre " + ordenacion
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter(Sqlsentence, cnn)
            da.Fill(ds, "todos")
            GridView1.DataSource = ds.Tables("todos")
            GridView1.DataBind()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Protected Sub OrdenarPor(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged
        If RadioButton1.Checked Then
            ordenacion = "DESC"
            ' MsgBox(ordenacion + " descendente")
            CogerDatos()
        Else
            ordenacion = "ASC"
            ' MsgBox(ordenacion + " ascendente")
            CogerDatos()
        End If
    End Sub

    Protected Sub CogerDatos()
        If RadioButton1.Checked Then
            ordenacion = "DESC"
        Else
            ordenacion = "ASC"
        End If

        texto = DropDownList1.SelectedValue
        tipo = DropDownList2.SelectedValue
        nombreAloj = TextBox2.Text.ToUpper

        If TextBox2.Text.Equals("") Then
            nombreAloj = ""
        Else
            nombreAloj = TextBox2.Text.ToUpper
        End If
        If TextBox1.Text.Equals("") Then
            capacidad = ""
        Else
            capacidad = CInt(TextBox1.Text)
        End If


        rellenoGrid(texto, capacidad, tipo, nombreAloj, ordenacion)
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Session("logeado") = False
        Response.Redirect("InicioSessionB.aspx")
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Response.Redirect("Mapa.aspx")
    End Sub

End Class