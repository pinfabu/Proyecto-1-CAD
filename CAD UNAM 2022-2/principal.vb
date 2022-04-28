Public Class principal
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles dwgActual.Click

    End Sub

    Private Sub dwgActual_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ConexionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConexionToolStripMenuItem.Click

    End Sub

    Private Sub CAD2022ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CAD2022ToolStripMenuItem.Click
        inicializa_conexion("2022")
        If IsNothing(DOCUMENTO) Then
            dwgActual.Text = "Sin conexion"
        Else
            dwgActual.Text = DOCUMENTO.Name
        End If
    End Sub

    Private Sub SeleccionDeUnSubelementoToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Me.Visible = False
        seleccionDeObjetos("F")
        Me.Visible = True
    End Sub

    Private Sub AsociarSenalAUnaEntradaDirectamenteSinAtributosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsociarSenalAUnaEntradaDirectamenteSinAtributosToolStripMenuItem.Click
        Me.Visible = False
        asociaSenalEntradaDirecto()
        Me.Visible = True
    End Sub

    Private Sub PrepararElDiccionarioDeUnaCompuertaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrepararElDiccionarioDeUnaCompuertaToolStripMenuItem.Click
        Me.Visible = False
        prepararElDiccionarioDeUnaCompuerta()
        Me.Visible = True
    End Sub

    Private Sub ResolverCompuertaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResolverCompuertaToolStripMenuItem.Click
        Me.Visible = False
        resolverCompuertaSinAtributos()
        Me.Visible = True
    End Sub

    Private Sub AsociarSenalAUnaSalidaDirectamenteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsociarSenalAUnaSalidaDirectamenteToolStripMenuItem.Click
        Me.Visible = False
        asociarSenalSalidaDirecto()
        Me.Visible = True
    End Sub

    Private Sub RresolverCircuitoCompletoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RresolverCircuitoCompletoToolStripMenuItem.Click
        Me.Visible = False
        resolverCircuito()
        Me.Visible = True
    End Sub

    Private Sub ReiniciarSalidasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReiniciarSalidasToolStripMenuItem.Click
        Me.Visible = False
        reiniciaSalidas()
        Me.Visible = True
    End Sub
End Class

