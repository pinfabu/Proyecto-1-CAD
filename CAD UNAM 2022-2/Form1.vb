Public Class principal
    Private Sub AutoCAD2022ToolStripMenuItem_Click(sender As Object, e As EventArgs)
        inicializa_conexion("2022")
        If IsNothing(DOCUMENTO) Then
            dwgActual.Text = "Sin Conexion"
        Else
            dwgActual.Text = DOCUMENTO.Name
        End If
    End Sub
End Class
