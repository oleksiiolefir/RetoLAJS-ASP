Imports MySql.Data.MySqlClient

Public Class WebForm4
    Inherits System.Web.UI.Page

    Dim idRes, username As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Conectar") = System.Web.Configuration.WebConfigurationManager.AppSettings("ConectarMySQL").ToString

        RellenarReservas()
        llamodatos()
    End Sub

    Protected Sub RellenarReservas()
        Try
            username = Request.Params("parametro")
            Dim cnn As New MySqlConnection()
            cnn.ConnectionString = Session("Conectar")
            Dim ds As New DataSet
            Dim da As New MySqlDataAdapter("SELECT * FROM `reserva` where idUsr = (select idUsr from usuario where username ='" + username + "') order by idRes asc", cnn)
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
        Session("idRes") = row.Cells(1).Text
        MsgBox("Ha seleccionado la fila")
    End Sub

    Protected Sub GridView3_RowDeleting1(sender As Object, e As GridViewDeleteEventArgs) Handles GridView3.RowDeleting
        idRes = Session("idRes")
        MsgBox(idRes)
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

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim valor As String = Request.Params("parametro")
        Response.Redirect("WebForm1.aspx?parametro=" + valor)
    End Sub
    Protected Sub actualizarReserva()
        idRes = Session("idRes")
        MsgBox(idRes)
        Try
            Dim connString As String = "server= 192.168.101.35; database=alojamientos ; user id=lajs; password=lajs"
            Dim sqlsentence As String = "update reserva set fechaEntrada =" + fechaEntrada + ", fechaSalida=" + fechaSalida + " where idRes = " + idRes
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

    End Sub
End Class