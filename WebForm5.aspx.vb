Imports System.Security.Cryptography

Public Class WebForm5
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub



    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim contra As Boolean
        contra = SacaContra()
        If contra = True Then
            Response.Redirect("WebForm1.aspx")
        Else
            MsgBox("Datos erroneos")
            TextBox1.Text = ""
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("Registro.aspx")
    End Sub
    Protected Function SacaContra()
        Dim contrasenia As String
        Dim var As Boolean = False
        Try
            Dim connString As String = "server= 192.168.101.35; database=alojamientos ; user id=lajs; password=lajs"
            Dim sqlQuery As String = "select password from usuario where username = @username"
            Using sqlConn As New MySqlConnection(connString)
                Using sqlComm2 As New MySqlCommand()
                    With sqlComm2
                        .Connection = sqlConn
                        .CommandText = sqlQuery
                        .CommandType = CommandType.Text
                        .Parameters.AddWithValue("@username", TextBox1.Text)
                    End With
                    Try
                        sqlConn.Open()
                        Dim sqlReader2 As MySqlDataReader = sqlComm2.ExecuteReader()
                        While sqlReader2.Read()
                            If Not sqlReader2.HasRows Then
                                contrasenia = ""
                                MsgBox("No se ha encontrado el usuario")
                                var = False
                            Else
                                contrasenia = sqlReader2("password").ToString
                                ''Comparar las contraseñas   del textbox con la contrasenia
                                If contrasenia.Equals(GetHash(TextBox2.Text)) Then
                                    var = True
                                Else
                                    var = False
                                End If
                            End If
                        End While
                    Catch ex As MySqlException
                        MsgBox("Error al logearse")
                    End Try
                End Using
            End Using
        Catch ex As MySqlException
            MsgBox("Error Al Conectar la base de datos")
        End Try

        Return var
    End Function

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