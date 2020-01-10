Imports MySql.Data.MySqlClient

Public Class WebForm6
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        insertarDatos()
        '' Response.Redirect("WebForm5.aspx")
    End Sub
    Protected Sub insertarDatos()
        Dim mes As Date = Calendar1.SelectedDate.ToShortDateString
        Dim MyStr As String
        Dim numero As Integer-*
        MyStr = Format(mes, "yyyy-MM-dd")
        Label1.Text = MyStr


        Try
            Dim connString As String = "server = 192.168.101.35;Database=alojamientos;User ID=lajs; Password=lajs"
            Dim sqlQuery As String = "select max(idCli) from cliente "

            Using sqlConn As New MySqlConnection(connString)
                Using sqlComm As New MySqlCommand() 'hay que usar un comando por cada select
                    With sqlComm
                        .Connection = sqlConn
                        .CommandText = sqlQuery
                        .CommandType = CommandType.Text
                    End With
                    Try
                        sqlConn.Open()
                        Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()

                        While sqlReader.Read()

                            numero = sqlReader("idCli").ToString

                        End While
                        Label2.Text = numero
                    Catch ex As MySqlException

                    End Try
                End Using
            End Using
        Catch

        End Try



        'recuperamos los datos desde sql



        cm = New MySqlCommand("INSERT INTO cliente(idCli,apellidos,fechaNac,nombre,password,username) VALUES (?idCli,?apellidos,?fechaNac,?nombre,?password,?username)")
        cm.Parameters.Add("?idCli", MySqlDbType.VarChar)
        cm.Parameters("?idCli").Value = numero ''numero que se auto incerementa
        cm.Parameters.Add("?apellidos", MySqlDbType.VarChar)
        cm.Parameters("?apellidos").Value = TextBox5.Text

        cm.Parameters.Add("?fechaNac", MySqlDbType.VarChar)
        cm.Parameters("?fechaNac").Value = MyStr
        cm.Parameters.Add("?nombre", MySqlDbType.VarChar)
        cm.Parameters("?nombre").Value = TextBox4.Text
        cm.Parameters.Add("?password", MySqlDbType.VarChar)
        cm.Parameters("?password").Value = TextBox2.Text
        cm.Parameters.Add("?username", MySqlDbType.VarChar)
        cm.Parameters("?username").Value = TextBox1.Text
        cm.Connection = cn
        cm.ExecuteNonQuery()
        cn.Close()

    End Sub

End Class