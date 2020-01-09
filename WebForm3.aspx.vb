Public Class WebForm3
    Inherits System.Web.UI.Page
    Dim fecmin As Date = Date.MinValue
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fecmin = Date.Today
    End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged

        Label2.Text = Calendar1.SelectedDate.ToLongDateString

    End Sub
    Protected Sub Calendar2_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar2.SelectionChanged
        fecmin = Calendar1.SelectedDate
        Label3.Text = Calendar2.SelectedDate.ToLongDateString
    End Sub
End Class