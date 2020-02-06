Public Class RealizarReservaB
    Inherits System.Web.UI.Page

    Public nomUsuario As String
    Dim localidad As String = ""
    Dim capacidad As String = ""
    Dim nombreAloj As String = ""
    Dim tipo As String
    Private MinDate As Date = Date.MinValue
    Private MaxDate As Date = Date.MaxValue


    Dim fechaInicio, fechaFinal As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString

        If Not Page.IsPostBack Then
            Calendar2.SelectedDate = Today
            Calendar1.SelectedDate = (Date.Today).AddDays(1)
            Llenar_DropDownList1()
            Llenar_DropDownList2()
            rellenoGrid(localidad, capacidad, tipo, nombreAloj)

            'Request.Params("parametro")
            Dim par As String
            par = (Context.Session("parametro"))
            MinDate = Date.Today
            MaxDate = Calendar1.SelectedDate

        End If

    End Sub
    Private Sub Llenar_DropDownList1()
        Try
            'abrimos la conexion hacia sql
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("conectar")
            Dim ds As New DataSet
            'recuperamos los datos desde sql
            Dim da As New MySqlDataAdapter("select distinct localidad from alojamiento  order by localidad asc", cnn)
            da.Fill(ds, "localidad")
            DropDownList1.DataSource = ds.Tables("localidad")

            Me.DropDownList1.DataTextField = "localidad" 'valor a mostrar
            DropDownList1.DataValueField = "localidad"
            Me.DropDownList1.DataBind()
            cnn.Close()
        Catch ex As Exception
            MsgBox("Error al rellenar el droplist de localidad")
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
            MsgBox("Error al rellenar el droplist de tipo")
        End Try
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim row As GridViewRow = GridView1.SelectedRow

        VerLabels()
        Label5.Text = row.Cells(1).Text
        Label6.Text = row.Cells(2).Text
        Label7.Text = row.Cells(3).Text
        Label8.Text = row.Cells(4).Text
        Label9.Text = row.Cells(5).Text
        Session("nombre") = row.Cells(1).Text
    End Sub
    Protected Sub VerLabels()
        Label5.Visible = True
        Label6.Visible = True
        Label7.Visible = True
        Label8.Visible = True
        Label9.Visible = True
    End Sub
    Protected Sub rellenarAlojamientos()
        Try
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter("select tipo,localidad,direccion,capacidad,nombre from alojamiento order by nombre asc", cnn)
            da.Fill(ds, "alojamiento")
            GridView1.DataSource = ds.Tables("alojamiento")
            GridView1.DataBind()
        Catch ex As Exception
            MsgBox("Error")
        End Try

    End Sub
    Protected Sub rellenoGrid(texto, capacidad, tipo, nombreAloj)
        Try
            Dim SQLsentence As String = ""

            'If capacidad.Equals("") Then
            '    SQLsentence = "select tipo,localidad,direccion,capacidad,nombre from alojamiento where localidad like('" + texto + "') And tipo = '" + tipo + "'"
            'ElseIf Not capacidad.Equals("") Then
            '    SQLsentence = "select tipo,localidad,direccion,capacidad,nombre from alojamiento where  localidad like ('" + texto + "') And tipo = '" + tipo + "'" + " AND capacidad >=" + capacidad
            'End If

            'If texto.Equals("") And capacidad.Equals("") Then
            '    SQLsentence = "select tipo,localidad,direccion,capacidad,nombre from alojamiento order by nombre asc"
            'End If

            If capacidad.Equals("") And nombreAloj.Equals("") Then
                SQLsentence = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono from alojamiento where localidad like('" + texto + "') And tipo = '" + tipo + "' order by nombre asc"
            ElseIf Not nombreAloj.Equals("") And Not capacidad.Equals("") Then
                SQLsentence = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono from alojamiento where  localidad like ('" + texto + "') And tipo = '" + tipo + "' AND capacidad >= " + capacidad + " AND nombre like('%" + nombreAloj + "%') order by nombre asc"
            ElseIf Not capacidad.Equals("") Then
                SQLsentence = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono from alojamiento where  localidad like ('" + texto + "') And tipo = '" + tipo + "' AND capacidad >= " + capacidad + " order by nombre asc"
            ElseIf Not nombreAloj.Equals("") Then
                SQLsentence = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono from alojamiento where  localidad like ('" + texto + "') And tipo = '" + tipo + "' AND nombre like('%" + nombreAloj + "%') order by nombre asc"
            End If

            If texto.Equals("") And capacidad.Equals("") And nombreAloj.Equals("") Then
                SQLsentence = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono from alojamiento order by nombre asc"
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim nom, username As String
        nom = Session("nombre")
        username = (Context.Session("parametro"))
        If username.Equals("") Then
            MsgBox("No se puede hacer reservas sin loggearse")
        ElseIf nom = Nothing Then
            MsgBox("Tiene que seleccionar un alojamiento")
        Else
            Dim idAloj, idUsr, idRes As Integer
            Try
                Dim connString As String = "server= 192.168.101.35; database=alojamientos ; user id=lajs; password=lajs"

                Dim sqlQuery As String = "select idUsr from usuario where username = @username"
                Dim sqlQuery2 As String = "select idAloj from alojamiento where nombre = '" + nom + "'"
                Dim sqlQuery3 As String = "select max(idRes) from reserva"
                'SELECT * FROM `reserva` where fechaEntrada >= "2020-01-22" And fechaSalida <="2020-01-31" AND idAloj = 1
                Dim sqlQuery4 As String = "SELECT * FROM `reserva` where idAloj = @idAloj And fechaEntrada BETWEEN @fechaEntrada And @fechaSalida OR fechaSalida BETWEEN @fechaEntrada And @fechaSalida And idUsr = @idUsr And idAloj = @idAloj "

                Using sqlConn As New MySqlConnection(connString)
                    Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select 
                        With sqlComm
                            .Connection = sqlConn
                            .CommandText = sqlQuery
                            .CommandType = CommandType.Text
                            .Parameters.AddWithValue("@username", username)
                        End With
                        Try
                            sqlConn.Open()
                            Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
                            While sqlReader.Read()
                                idUsr = sqlReader("idUsr")
                            End While

                        Catch ex As MySqlException
                            MsgBox("Error no encuentra el id de Usuario")
                        End Try
                    End Using
                    Using sqlComm2 As New MySqlCommand() 'hay que usar un comando por cada select
                        With sqlComm2
                            .Connection = sqlConn
                            .CommandText = sqlQuery2
                            .CommandType = CommandType.Text

                        End With
                        Try
                            Dim sqlReader2 As MySqlDataReader = sqlComm2.ExecuteReader()

                            While sqlReader2.Read()
                                idAloj = sqlReader2("idAloj")

                            End While

                        Catch ex As MySqlException
                            MsgBox("Error no encuentra el id de Alojamiento")
                        End Try
                    End Using
                    Using sqlComm3 As New MySqlCommand() 'hay que usar un comando por cada select
                        With sqlComm3
                            .Connection = sqlConn
                            .CommandText = sqlQuery3
                            .CommandType = CommandType.Text
                        End With
                        Try

                            Dim sqlReader3 As MySqlDataReader = sqlComm3.ExecuteReader()
                            While sqlReader3.Read()
                                If sqlReader3.IsDBNull(0) Then
                                    idRes = 0
                                Else
                                    idRes = sqlReader3("max(idRes)")
                                End If
                            End While

                        Catch ex As MySqlException
                            MsgBox("Error al sacar el maximo id de reservas")
                        End Try
                    End Using
                    Using sqlComm4 As New MySqlCommand() 'hay que usar un comando por cada select
                        Dim fechIni As Date = Calendar2.SelectedDate.ToShortDateString
                        Dim fechaFin As Date = Calendar1.SelectedDate.ToShortDateString
                        fechaInicio = Format(fechIni, "yyyy-MM-dd")
                        fechaFinal = Format(fechaFin, "yyyy-MM-dd")
                        With sqlComm4
                            .Connection = sqlConn
                            .CommandText = sqlQuery4
                            .CommandType = CommandType.Text
                            .Parameters.AddWithValue("@fechaEntrada", fechaInicio)
                            .Parameters.AddWithValue("@fechaSalida", fechaFinal)
                            .Parameters.AddWithValue("@idAloj", idAloj)
                            .Parameters.AddWithValue("@idUsr", idUsr)
                        End With
                        Try
                            Dim sqlReader4 As MySqlDataReader = sqlComm4.ExecuteReader()

                            If Not sqlReader4.HasRows Then
                                MsgBox("Se puede hacer la reserva")
                            Else
                                MsgBox("No se puede hace la reserva entre estas fechas")
                                fechaInicio = ""
                                fechaFinal = ""
                            End If

                        Catch ex As MySqlException
                            MsgBox("Error al sacar el rango de fechas ")
                        End Try
                    End Using
                End Using
            Catch ex As MySqlException
                MsgBox("Error Al Conectar la base de datos")
            End Try

            idRes += 1

            If idUsr <> 0 And idAloj <> 0 And username <> "" And fechaInicio <> "" And fechaFinal <> "" Then
                InsertarReservas(idRes, idAloj, idUsr)
            End If
        End If


    End Sub

    Protected Sub InsertarReservas(ByRef idRes As Integer, ByRef idAloj As Integer, ByRef idUsr As Integer)
        Dim cn As MySqlConnection = New MySqlConnection("server = 192.168.101.35;Database=alojamientos;User ID=lajs; Password=lajs")
        Dim cm As MySqlCommand


        cn.Open()
        cm = New MySqlCommand("INSERT INTO reserva(idRes,FechaEntrada,FechaSalida,idAloj,idUsr) VALUES (?idRes,?FechaEntrada,?FechaSalida,?idAloj,?idUsr)")
        cm.Parameters.Add("?idRes", MySqlDbType.VarChar)
        cm.Parameters("?idRes").Value = idRes ''numero que se auto incerementa
        cm.Parameters.Add("?FechaEntrada", MySqlDbType.Date)
        cm.Parameters("?FechaEntrada").Value = fechaInicio
        cm.Parameters.Add("?FechaSalida", MySqlDbType.Date)
        cm.Parameters("?FechaSalida").Value = fechaFinal
        cm.Parameters.Add("?idAloj", MySqlDbType.Int16)
        cm.Parameters("?idAloj").Value = idAloj
        cm.Parameters.Add("?idUsr", MySqlDbType.Int16)
        cm.Parameters("?idUsr").Value = idUsr

        cm.Connection = cn
        cm.ExecuteNonQuery()
        cn.Close()
    End Sub


    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            capacidad = ""
        Else
            capacidad = CInt(TextBox1.Text)
        End If
        localidad = DropDownList1.SelectedValue
        tipo = DropDownList2.SelectedValue
        nombreAloj = TextBox2.Text.ToUpper
        rellenoGrid(localidad, capacidad, tipo, nombreAloj)
    End Sub

    Protected Sub Calendar2_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar2.DayRender
        MinDate = Date.Today
        MaxDate = Calendar1.SelectedDate
        If e.Day.Date < MinDate OrElse e.Day.Date > MaxDate Then
            e.Day.IsSelectable = False
        End If

    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim param As String
        param = ""
        Session("logeado") = False
        Response.Redirect("InicioSessionB.aspx?param=" + param)
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim Sqlsentence As String = "select nombre,capacidad,tipo,direccion,localidad,provincia,telefono from alojamiento order by nombre  asc"
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

    Protected Sub Calendar1_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar1.DayRender

        MinDate = Calendar2.SelectedDate
        If e.Day.Date < MinDate Then
            e.Day.IsSelectable = False
        End If

    End Sub

End Class