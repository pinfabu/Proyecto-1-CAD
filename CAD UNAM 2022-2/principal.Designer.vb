<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class principal
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ConexionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CAD2022ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EjemplosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResolviendoUnaCompuertaANDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AsociarSenalAUnaEntradaDirectamenteSinAtributosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrepararElDiccionarioDeUnaCompuertaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResolverCompuertaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dwgActual = New System.Windows.Forms.Label()
        Me.AsociarSenalAUnaSalidaDirectamenteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConexionToolStripMenuItem, Me.EjemplosToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ConexionToolStripMenuItem
        '
        Me.ConexionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CAD2022ToolStripMenuItem})
        Me.ConexionToolStripMenuItem.Name = "ConexionToolStripMenuItem"
        Me.ConexionToolStripMenuItem.Size = New System.Drawing.Size(70, 20)
        Me.ConexionToolStripMenuItem.Text = "Conexion"
        '
        'CAD2022ToolStripMenuItem
        '
        Me.CAD2022ToolStripMenuItem.Name = "CAD2022ToolStripMenuItem"
        Me.CAD2022ToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.CAD2022ToolStripMenuItem.Text = "CAD 2022"
        '
        'EjemplosToolStripMenuItem
        '
        Me.EjemplosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ResolviendoUnaCompuertaANDToolStripMenuItem})
        Me.EjemplosToolStripMenuItem.Name = "EjemplosToolStripMenuItem"
        Me.EjemplosToolStripMenuItem.Size = New System.Drawing.Size(128, 20)
        Me.EjemplosToolStripMenuItem.Text = "Resolviendo Circuito"
        Me.EjemplosToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        '
        'ResolviendoUnaCompuertaANDToolStripMenuItem
        '
        Me.ResolviendoUnaCompuertaANDToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AsociarSenalAUnaEntradaDirectamenteSinAtributosToolStripMenuItem, Me.AsociarSenalAUnaSalidaDirectamenteToolStripMenuItem, Me.PrepararElDiccionarioDeUnaCompuertaToolStripMenuItem, Me.ResolverCompuertaToolStripMenuItem})
        Me.ResolviendoUnaCompuertaANDToolStripMenuItem.Name = "ResolviendoUnaCompuertaANDToolStripMenuItem"
        Me.ResolviendoUnaCompuertaANDToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ResolviendoUnaCompuertaANDToolStripMenuItem.Text = "Resolver un circuito"
        '
        'AsociarSenalAUnaEntradaDirectamenteSinAtributosToolStripMenuItem
        '
        Me.AsociarSenalAUnaEntradaDirectamenteSinAtributosToolStripMenuItem.Name = "AsociarSenalAUnaEntradaDirectamenteSinAtributosToolStripMenuItem"
        Me.AsociarSenalAUnaEntradaDirectamenteSinAtributosToolStripMenuItem.Size = New System.Drawing.Size(359, 22)
        Me.AsociarSenalAUnaEntradaDirectamenteSinAtributosToolStripMenuItem.Text = "Asociar senal a una entrada directamente sin atributos"
        '
        'PrepararElDiccionarioDeUnaCompuertaToolStripMenuItem
        '
        Me.PrepararElDiccionarioDeUnaCompuertaToolStripMenuItem.Name = "PrepararElDiccionarioDeUnaCompuertaToolStripMenuItem"
        Me.PrepararElDiccionarioDeUnaCompuertaToolStripMenuItem.Size = New System.Drawing.Size(359, 22)
        Me.PrepararElDiccionarioDeUnaCompuertaToolStripMenuItem.Text = "Preparar el diccionario de una compuerta"
        '
        'ResolverCompuertaToolStripMenuItem
        '
        Me.ResolverCompuertaToolStripMenuItem.Name = "ResolverCompuertaToolStripMenuItem"
        Me.ResolverCompuertaToolStripMenuItem.Size = New System.Drawing.Size(359, 22)
        Me.ResolverCompuertaToolStripMenuItem.Text = "Resolver Compuerta"
        '
        'dwgActual
        '
        Me.dwgActual.AutoSize = True
        Me.dwgActual.Location = New System.Drawing.Point(12, 78)
        Me.dwgActual.Name = "dwgActual"
        Me.dwgActual.Size = New System.Drawing.Size(68, 13)
        Me.dwgActual.TabIndex = 1
        Me.dwgActual.Text = "Sin conexion"
        '
        'AsociarSenalAUnaSalidaDirectamenteToolStripMenuItem
        '
        Me.AsociarSenalAUnaSalidaDirectamenteToolStripMenuItem.Name = "AsociarSenalAUnaSalidaDirectamenteToolStripMenuItem"
        Me.AsociarSenalAUnaSalidaDirectamenteToolStripMenuItem.Size = New System.Drawing.Size(359, 22)
        Me.AsociarSenalAUnaSalidaDirectamenteToolStripMenuItem.Text = "Asociar senal a una salida directamente"
        '
        'principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.dwgActual)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "principal"
        Me.Text = "Curso de CAD"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ConexionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents dwgActual As Label
    Friend WithEvents CAD2022ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EjemplosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ResolviendoUnaCompuertaANDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AsociarSenalAUnaEntradaDirectamenteSinAtributosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrepararElDiccionarioDeUnaCompuertaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ResolverCompuertaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AsociarSenalAUnaSalidaDirectamenteToolStripMenuItem As ToolStripMenuItem
End Class
