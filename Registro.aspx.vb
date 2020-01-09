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
        MyStr = Format(mes, "yyyy-MM-dd")
        Label1.Text = MyStr
        Dim numero As Integer = 3
        Dim cn As MySqlConnection
        cn = New MySqlConnection("server = 192.168.101.35;Database=alojamientos;User ID=lajs; Password=lajs")
        cn.Open()
        Dim cm As MySqlCommand
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
        numero += 1
    End Sub

End Class