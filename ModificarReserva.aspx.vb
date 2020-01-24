Imports MySql.Data.MySqlClient

Public Class WebForm4
    Inherits System.Web.UI.Page

    Dim idRes, username As String
    Dim fechaEntrada, fechaSalida As String
    Private MinDate As Date = Date.MinValue
    Private MaxDate As Date = Date.MaxValue
    Dim tabla As New DataTable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString
        If Not Page.IsPostBack Then
            Calendar1.SelectedDate = Today
            Calendar2.SelectedDate = (Date.Today).AddDays(1)
            MinDate = Date.Today
            MaxDate = Calendar1.SelectedDate
            RellenarReservas()
            llamodatos()
        End If
    End Sub

    Protected Sub RellenarReservas()
        Try

            username = Request.Params("parametro")
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim sqlsent As String = "SELECT fechaEntrada As 'Fecha De Entrada',fechaSalida As 'Fecha De Salida',nombre As 'Nombre del Alojamiento',idUsr FROM alojamiento,reserva 
                 WHERE alojamiento.idAloj = reserva.idAloj And `idUsr` = (SELECT idUsr from usuario where username = '" + username + "')"
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter(sqlsent, cnn)
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

    Protected Sub GridView3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView3.SelectedIndexChanged
        Dim row As GridViewRow = GridView3.SelectedRow
        Session("idAloj") = row.Cells(3).Text
        Session("idUsr") = row.Cells(4).Text

        MsgBox("Ha seleccionado la fila")
    End Sub

    Sub SacaIdRes()
        Try
            Dim nombre As String = Session("idAloj")
            MsgBox("--------------------" + nombre)
            username = Request.Params("parametro")
            MsgBox("--------------------" + username)
            Dim connString As String = "server= 192.168.101.35; database=alojamientos ; user id=lajs; password=lajs"
            Dim sqlsentence As String = " SELECT idRes FROM `reserva` WHERE idUsr = (select idUsr from usuario where username = @username) And idAloj = (select idAloj from alojamiento where nombre = @nombre)"
            Using sqlConn As New MySqlConnection(connString)
                Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select 
                    With sqlComm
                        .Connection = sqlConn
                        .CommandText = sqlsentence
                        .CommandType = CommandType.Text
                        .Parameters.AddWithValue("@username", username)
                        .Parameters.AddWithValue("@nombre", nombre)

                    End With
                    Try
                        sqlConn.Open()
                        Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
                        While sqlReader.Read()
                            idRes = sqlReader("idRes")
                            MsgBox("AAAAAAAAAAAAAAAAAAAAAA" + idRes)
                        End While
                    Catch ex As MySqlException
                        MsgBox(ex)
                    End Try
                End Using
            End Using

            Session("idRes") = idRes
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Protected Sub GridView3_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView3.RowDeleting
        SacaIdRes()
        idRes = Session("idRes")
        MsgBox(idRes)
        If idRes = "" Then
            MsgBox("Seleccione una reserva")
        Else
            Try
                Dim connString As String = "server= 192.168.101.35; database=alojamientos ; user id=lajs; password=lajs"
                Dim sqlsentence As String = "delete from reserva where idRes = @idRes"
                Using sqlConn As New MySqlConnection(connString)
                    Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select 
                        With sqlComm
                            .Connection = sqlConn
                            .CommandText = sqlsentence
                            .CommandType = CommandType.Text
                            .Parameters.AddWithValue("@idRes", idRes)
                        End With
                        Try
                            sqlConn.Open()
                            Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
                            RellenarReservas()
                        Catch ex As MySqlException
                            MsgBox(ex)
                        End Try
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("ERRRRRRROR")
            End Try
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim valor As String = Request.Params("parametro")
        Response.Redirect("WebForm1.aspx?parametro=" + valor)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CompararFechas()
        If fechaEntrada <> "" And fechaSalida <> "" Then
            actualizarReserva(fechaEntrada, fechaSalida)
        End If
    End Sub

    Protected Sub actualizarReserva(fechaEnt, fechaSal)

        SacaIdRes()
        MsgBox(idRes)
        Try
            Dim connString As String = "server= 192.168.101.35; database=alojamientos ; user id=lajs; password=lajs"
            MsgBox(fechaEnt)
            MsgBox(fechaSal)
            Dim sqlsentence As String = "update reserva set fechaEntrada ='" + fechaEnt + "', fechaSalida='" + fechaSal + "' where idRes = @idRes"
            Using sqlConn As New MySqlConnection(connString)
                Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select 
                    With sqlComm
                        .Connection = sqlConn
                        .CommandText = sqlsentence
                        .CommandType = CommandType.Text
                        .Parameters.AddWithValue("@idRes", idRes)
                    End With
                    Try
                        sqlConn.Open()
                        Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()
                        RellenarReservas()
                        MsgBox("La RESERVA ACTUALIZADA CORRECTAMENTE")
                    Catch ex As MySqlException
                        MsgBox(ex)
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MsgBox("ERRRRRRROR")
        End Try

    End Sub
    Protected Sub CompararFechas()
        Dim connString As String
        Dim sql As String
        Dim idAloj, idUsr As String
        Dim fechIni As Date = Calendar1.SelectedDate.ToShortDateString
        Dim fechaFin As Date = Calendar2.SelectedDate.ToShortDateString
        Try
            idAloj = Session("idAloj")
            idUsr = Session("idUsr")
            fechaEntrada = Format(fechIni, "yyyy-MM-dd")
            MsgBox(fechaEntrada)
            fechaSalida = Format(fechaFin, "yyyy-MM-dd")
            MsgBox(fechaSalida)

            connString = "server= 192.168.101.35; database=alojamientos ; user id=lajs; password=lajs"
            sql = "SELECT * FROM `reserva` where idUsr = @idUsr And idAloj =(select idAloj from alojamiento where nombre =@idAloj ) AND fechaEntrada BETWEEN @fechaEntrada And @fechaSalida AND fechaSalida BETWEEN @fechaEntrada And @fechaSalida   "
            Using sqlConn As New MySqlConnection(connString)
                Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select 
                    With sqlComm
                        .Connection = sqlConn
                        .CommandText = sql
                        .CommandType = CommandType.Text
                        .Parameters.AddWithValue("@idRes", idRes)
                        .Parameters.AddWithValue("@fechaEntrada", fechaEntrada)
                        .Parameters.AddWithValue("@fechaSalida", fechaSalida)
                        .Parameters.AddWithValue("@idAloj", idAloj)
                        .Parameters.AddWithValue("@idUsr", idUsr)
                    End With
                    Try
                        sqlConn.Open()
                        Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()

                        If Not sqlReader.HasRows Then
                            MsgBox("Las fechas son correctas para realizar una reserva")
                        Else
                            MsgBox("No se puede hace la reserva entre estas fechas")
                            fechaEntrada = ""
                            fechaSalida = ""
                        End If
                    Catch ex As MySqlException
                        MsgBox(ex)
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error en hacer la select ")
        End Try
    End Sub
    Protected Sub Calendar1_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar1.DayRender
        MinDate = Date.Today
        MaxDate = Calendar2.SelectedDate
        If e.Day.Date < MinDate OrElse e.Day.Date > MaxDate Then
            e.Day.IsSelectable = False
        End If
    End Sub
    Protected Sub Calendar2_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar2.DayRender
        MinDate = Calendar1.SelectedDate
        If e.Day.Date < MinDate Then
            e.Day.IsSelectable = False
        End If
    End Sub
End Class