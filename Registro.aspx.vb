Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text
Public Class Registro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        insertarDatos()
        Response.Redirect("InicioSession.aspx")
    End Sub
    Protected Sub insertarDatos()
        Dim cn As MySqlConnection
        cn = New MySqlConnection("server = 192.168.101.35;Database=alojamientos;User ID=lajs; Password=lajs")
        Dim cm As MySqlCommand
        Dim mes As Date = Calendar1.SelectedDate.ToShortDateString
        Dim MyStr As String
        Dim dni As String
        Dim numero As Integer
        MyStr = Format(mes, "yyyy-MM-dd")

        Try
            Dim connString As String = "server= 192.168.101.35; database=alojamientos ; user id=lajs; password=lajs"
            Dim sqlQuery As String = "select max(idUsr) from usuario"
            Dim sqlQuery2 As String = "select dni from usuario where dni = @dni"
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
                            If sqlReader.IsDBNull(0) Then
                                numero = 1
                            Else
                                numero = sqlReader("max(idUsr)")
                            End If

                        End While

                    Catch ex As MySqlException
                        MsgBox("Error al sacar el maximo id")
                    End Try
                End Using

                Using sqlComm2 As New MySqlCommand()
                    With sqlComm2
                        .Connection = sqlConn
                        .CommandText = sqlQuery2
                        .CommandType = CommandType.Text
                        .Parameters.AddWithValue("@dni", Me.TextBox6.Text)
                    End With
                    Try
                        'no hace falta "sql.Open()" porque la conexion ya se ha abierto antes
                        Dim sqlReader2 As MySqlDataReader = sqlComm2.ExecuteReader()
                        If Not sqlReader2.HasRows Then
                            dni = TextBox6.Text
                        Else
                            MsgBox("El Dni ya existe ", MsgBoxStyle.OkCancel)
                            TextBox6.Text = ""
                            dni = ""
                        End If

                    Catch ex As MySqlException
                        MsgBox("Error al logearse")
                    End Try
                End Using
            End Using
        Catch ex As MySqlException
            MsgBox("Error Al Conectar la base de datos")
        End Try
        numero += 1
        ''Insertamos datos a la base de datos 
        If Not dni.Equals("") Then
            cn.Open()

            cm = New MySqlCommand("INSERT INTO usuario(idUsr,admin,apellidos,dni,fechaNac,nombre,password,username) VALUES (?idUsr,?admin,?apellidos,?dni,?fechaNac,?nombre,?password,?username)")
            cm.Parameters.Add("?idUsr", MySqlDbType.VarChar)
            cm.Parameters("?idUsr").Value = numero ''numero que se auto incerementa
            cm.Parameters.Add("?admin", MySqlDbType.Int16)
            cm.Parameters("?admin").Value = 0
            cm.Parameters.Add("?apellidos", MySqlDbType.VarChar)
            cm.Parameters("?apellidos").Value = TextBox5.Text
            cm.Parameters.Add("?dni", MySqlDbType.VarChar)
            cm.Parameters("?dni").Value = dni
            cm.Parameters.Add("?fechaNac", MySqlDbType.VarChar)
            cm.Parameters("?fechaNac").Value = MyStr
            cm.Parameters.Add("?nombre", MySqlDbType.VarChar)
            cm.Parameters("?nombre").Value = TextBox4.Text
            cm.Parameters.Add("?password", MySqlDbType.VarChar)
            cm.Parameters("?password").Value = GetHash(TextBox2.Text)
            cm.Parameters.Add("?username", MySqlDbType.VarChar)
            cm.Parameters("?username").Value = TextBox1.Text
            cm.Connection = cn
            cm.ExecuteNonQuery()
            cn.Close()
        End If
    End Sub
    Shared Function GetHash(theInput As String) As String

        Using hasher As MD5 = MD5.Create()    ' create hash object

            ' Convert to byte array and get hash
            Dim dbytes As Byte() = hasher.ComputeHash(Encoding.UTF8.GetBytes(theInput))

            ' sb to create string from bytes
            Dim sBuilder As New StringBuilder()

            ' convert byte data to hex string
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("X2"))
            Next n

            Return sBuilder.ToString()
        End Using

    End Function
End Class