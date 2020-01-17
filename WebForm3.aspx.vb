Public Class WebForm3
    Inherits System.Web.UI.Page
    Public nomUsuario As String
    Private MinDate As Date = Date.MinValue
    Private MaxDate As Date = Date.MaxValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString

        If Not Page.IsPostBack Then
            Calendar2.SelectedDate = Today
            rellenarAlojamientos()
            Label1.Text = Request.Params("parametro")
            MinDate = Date.Today
            MaxDate = Calendar1.SelectedDate

        End If

    End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged

        Label2.Text = Calendar1.SelectedDate.ToLongDateString

    End Sub
    Protected Sub Calendar2_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar2.SelectionChanged
        Label3.Text = Calendar2.SelectedDate.ToLongDateString
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim row As GridViewRow = GridView1.SelectedRow

        ''Label4.Text = +"    -----------  " + row.Cells(2).Text + "  -----------     " + row.Cells(3).Text + "  -----------     " + row.Cells(4).Text + "  -----------     " + row.Cells(5).Text
        Label5.Text = row.Cells(1).Text
        Label6.Text = row.Cells(2).Text
        Label7.Text = row.Cells(3).Text
        Label8.Text = row.Cells(4).Text
        Label9.Text = row.Cells(5).Text
        Session("nombre") = row.Cells(5).Text
    End Sub
    Protected Sub rellenarAlojamientos()
        Try
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter("select tipo,localidad,direccion,capacidad,nombre from alojamiento ", cnn)
            ' nombre  = SELECT `nombre` from alojamiento where idAloj = (select idAloj from reserva )
            'username = coge de otro webforms
            ' insert into reservas values (idRes,fechaIni,fechaFin,idAloj = select idAloj from alojamiento where nombre = nombre seleccionado en el gridView,idUsr = select idUsr from usuario  where username = me viene de otro forms)
            '
            '
            '
            '
            da.Fill(ds, "alojamiento")
            GridView1.DataSource = ds.Tables("alojamiento")
            GridView1.DataBind()

        Catch ex As Exception
            MsgBox("Error")
        End Try

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim nom, username As String
        nom = Session("nombre")
        MsgBox(nom)
        username = Request.Params("parametro")
        MsgBox(username)
        Dim idAloj, idUsr, idRes As Integer
        Try
            Dim connString As String = "server= 192.168.101.35; database=alojamientos ; user id=lajs; password=lajs"

            Dim sqlQuery As String = "select idUsr from usuario where username = @username"
            Dim sqlQuery2 As String = "select idAloj from alojamiento where nombre= @name"
            Dim sqlQuery3 As String = "select max(idRes) from reserva"

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
                MsgBox(idUsr)
                Using sqlComm2 As New MySqlCommand() 'hay que usar un comando por cada select
                    With sqlComm2
                        .Connection = sqlConn
                        .CommandText = sqlQuery2
                        .CommandType = CommandType.Text
                        .Parameters.AddWithValue("@name", nom)
                    End With
                    Try

                        Dim sqlReader2 As MySqlDataReader = sqlComm2.ExecuteReader()
                        While sqlReader2.Read()
                            idAloj = sqlReader2("idAloj")
                            'MsgBox(idAloj)
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

                            MsgBox(idRes)
                        End While

                    Catch ex As MySqlException
                        MsgBox("Error al sacar el maximo id de reservas")
                    End Try
                End Using
            End Using
        Catch ex As MySqlException
            MsgBox("Error Al Conectar la base de datos")
        End Try
        idRes += 1
        If idUsr <> 0 And idAloj <> 0 And username <> "" Then
            InsertarReservas(idRes, idAloj, idUsr)
        End If
    End Sub

    Protected Sub InsertarReservas(ByRef idRes As Integer, ByRef idAloj As Integer, ByRef idUsr As Integer)
        Dim cn As MySqlConnection = New MySqlConnection("server = 192.168.101.35;Database=alojamientos;User ID=lajs; Password=lajs")
        Dim cm As MySqlCommand
        Dim fechIni As Date = Calendar2.SelectedDate.ToShortDateString
        Dim fechaFin As Date = Calendar1.SelectedDate.ToShortDateString
        Dim fechaInicio, fechaFinal As String

        fechaInicio = Format(fechIni, "yyyy-MM-dd")
        fechaFinal = Format(fechaFin, "yyyy-MM-dd")


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

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim valor As String = Request.Params("parametro")
        MsgBox(valor)
        Response.Redirect("WebForm1.aspx?parametro=" + valor)
    End Sub

    Protected Sub Calendar2_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar2.DayRender
        MinDate = Date.Today
        MaxDate = Calendar1.SelectedDate
        If e.Day.Date < MinDate OrElse e.Day.Date > MaxDate Then
            e.Day.IsSelectable = False
        End If

    End Sub
    Protected Sub Calendar1_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar1.DayRender

        MinDate = Calendar2.SelectedDate
        If e.Day.Date < MinDate Then
            e.Day.IsSelectable = False
        End If

    End Sub
End Class