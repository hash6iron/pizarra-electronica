<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.PanelPizarra = New DoubleBufferedPanel()
        Me.MenuPrincipal = New System.Windows.Forms.MenuStrip()
        Me.MenuArchivo = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuAbrir = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuGuardar = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuGuardarComo = New System.Windows.Forms.ToolStripMenuItem()
        Me.SeparadorArchivo = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuSalir = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuEditar = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuVer = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuGraficar = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuOpciones = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuRadianes = New System.Windows.Forms.ToolStripMenuItem()
        Me.SeparadorOpciones = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuTemas = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuTemaClasico = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuTemaOscuroAzul = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuTemaMatriz = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuTemaRetro = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuAprender = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuHorasReloj = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuEstadisticas = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuVerEstadisticas = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuVerHistorial = New System.Windows.Forms.ToolStripMenuItem()
        Me.SeparadorEstadisticas = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuReiniciarEstadisticas = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuAyuda = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuComoUsar = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuAtajosTeclado = New System.Windows.Forms.ToolStripMenuItem()
        Me.SeparadorAyuda = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuAcercaDe = New System.Windows.Forms.ToolStripMenuItem()
        Me.BarraEstado = New System.Windows.Forms.StatusStrip()
        Me.LabelModo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LabelPosicion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuPrincipal.SuspendLayout()
        Me.BarraEstado.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelPizarra
        '
        Me.PanelPizarra.BackColor = System.Drawing.Color.Black
        Me.PanelPizarra.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelPizarra.Location = New System.Drawing.Point(0, 24)
        Me.PanelPizarra.Name = "PanelPizarra"
        Me.PanelPizarra.Size = New System.Drawing.Size(1200, 654)
        Me.PanelPizarra.TabIndex = 0
        '
        'MenuPrincipal
        '
        Me.MenuPrincipal.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuArchivo, Me.MenuEditar, Me.MenuVer, Me.MenuOpciones, Me.MenuAprender, Me.MenuEstadisticas, Me.MenuAyuda})
        Me.MenuPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.MenuPrincipal.Name = "MenuPrincipal"
        Me.MenuPrincipal.Size = New System.Drawing.Size(1200, 24)
        Me.MenuPrincipal.TabIndex = 1
        Me.MenuPrincipal.Text = "MenuStrip1"
        '
        'MenuArchivo
        '
        Me.MenuArchivo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuAbrir, Me.MenuGuardar, Me.MenuGuardarComo, Me.SeparadorArchivo, Me.MenuSalir})
        Me.MenuArchivo.Name = "MenuArchivo"
        Me.MenuArchivo.Size = New System.Drawing.Size(60, 20)
        Me.MenuArchivo.Text = "&Archivo"
        '
        'MenuAbrir
        '
        Me.MenuAbrir.Name = "MenuAbrir"
        Me.MenuAbrir.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.MenuAbrir.Size = New System.Drawing.Size(200, 22)
        Me.MenuAbrir.Text = "&Abrir..."
        '
        'MenuGuardar
        '
        Me.MenuGuardar.Name = "MenuGuardar"
        Me.MenuGuardar.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.MenuGuardar.Size = New System.Drawing.Size(200, 22)
        Me.MenuGuardar.Text = "&Guardar"
        '
        'MenuGuardarComo
        '
        Me.MenuGuardarComo.Name = "MenuGuardarComo"
        Me.MenuGuardarComo.Size = New System.Drawing.Size(200, 22)
        Me.MenuGuardarComo.Text = "Guardar &como..."
        '
        'SeparadorArchivo
        '
        Me.SeparadorArchivo.Name = "SeparadorArchivo"
        Me.SeparadorArchivo.Size = New System.Drawing.Size(197, 6)
        '
        'MenuSalir
        '
        Me.MenuSalir.Name = "MenuSalir"
        Me.MenuSalir.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.MenuSalir.Size = New System.Drawing.Size(200, 22)
        Me.MenuSalir.Text = "&Salir"
        '
        'MenuEditar
        '
        Me.MenuEditar.CheckOnClick = True
        Me.MenuEditar.Name = "MenuEditar"
        Me.MenuEditar.Size = New System.Drawing.Size(49, 20)
        Me.MenuEditar.Text = "&Editar"
        '
        'MenuVer
        '
        Me.MenuVer.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuGraficar})
        Me.MenuVer.Name = "MenuVer"
        Me.MenuVer.Size = New System.Drawing.Size(35, 20)
        Me.MenuVer.Text = "&Ver"
        '
        'MenuGraficar
        '
        Me.MenuGraficar.Name = "MenuGraficar"
        Me.MenuGraficar.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.MenuGraficar.Size = New System.Drawing.Size(200, 22)
        Me.MenuGraficar.Text = "&Graficar Función..."
        '
        'MenuOpciones
        '
        Me.MenuOpciones.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuRadianes, Me.SeparadorOpciones, Me.MenuTemas})
        Me.MenuOpciones.Name = "MenuOpciones"
        Me.MenuOpciones.Size = New System.Drawing.Size(69, 20)
        Me.MenuOpciones.Text = "&Opciones"
        '
        'MenuRadianes
        '
        Me.MenuRadianes.CheckOnClick = True
        Me.MenuRadianes.Name = "MenuRadianes"
        Me.MenuRadianes.Size = New System.Drawing.Size(200, 22)
        Me.MenuRadianes.Text = "Usar &Radianes"
        '
        'SeparadorOpciones
        '
        Me.SeparadorOpciones.Name = "SeparadorOpciones"
        Me.SeparadorOpciones.Size = New System.Drawing.Size(197, 6)
        '
        'MenuTemas
        '
        Me.MenuTemas.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuTemaClasico, Me.MenuTemaOscuroAzul, Me.MenuTemaMatriz, Me.MenuTemaRetro})
        Me.MenuTemas.Name = "MenuTemas"
        Me.MenuTemas.Size = New System.Drawing.Size(200, 22)
        Me.MenuTemas.Text = "&Temas de Color"
        '
        'MenuTemaClasico
        '
        Me.MenuTemaClasico.Checked = True
        Me.MenuTemaClasico.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MenuTemaClasico.Name = "MenuTemaClasico"
        Me.MenuTemaClasico.Size = New System.Drawing.Size(200, 22)
        Me.MenuTemaClasico.Text = "&Clásico"
        '
        'MenuTemaOscuroAzul
        '
        Me.MenuTemaOscuroAzul.Name = "MenuTemaOscuroAzul"
        Me.MenuTemaOscuroAzul.Size = New System.Drawing.Size(200, 22)
        Me.MenuTemaOscuroAzul.Text = "&Oscuro Azul"
        '
        'MenuTemaMatriz
        '
        Me.MenuTemaMatriz.Name = "MenuTemaMatriz"
        Me.MenuTemaMatriz.Size = New System.Drawing.Size(200, 22)
        Me.MenuTemaMatriz.Text = "&Matriz (Verde)"
        '
        'MenuTemaRetro
        '
        Me.MenuTemaRetro.Name = "MenuTemaRetro"
        Me.MenuTemaRetro.Size = New System.Drawing.Size(200, 22)
        Me.MenuTemaRetro.Text = "&Retro"
        '
        'MenuAprender
        '
        Me.MenuAprender.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuHorasReloj})
        Me.MenuAprender.Name = "MenuAprender"
        Me.MenuAprender.Size = New System.Drawing.Size(69, 20)
        Me.MenuAprender.Text = "A&prender"
        '
        'MenuHorasReloj
        '
        Me.MenuHorasReloj.Name = "MenuHorasReloj"
        Me.MenuHorasReloj.Size = New System.Drawing.Size(200, 22)
        Me.MenuHorasReloj.Text = "&Horas del Reloj..."
        '
        'MenuEstadisticas
        '
        Me.MenuEstadisticas.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuVerEstadisticas, Me.MenuVerHistorial, Me.SeparadorEstadisticas, Me.MenuReiniciarEstadisticas})
        Me.MenuEstadisticas.Name = "MenuEstadisticas"
        Me.MenuEstadisticas.Size = New System.Drawing.Size(82, 20)
        Me.MenuEstadisticas.Text = "E&stadísticas"
        '
        'MenuVerEstadisticas
        '
        Me.MenuVerEstadisticas.Name = "MenuVerEstadisticas"
        Me.MenuVerEstadisticas.Size = New System.Drawing.Size(200, 22)
        Me.MenuVerEstadisticas.Text = "&Ver Estadísticas"
        '
        'MenuVerHistorial
        '
        Me.MenuVerHistorial.Name = "MenuVerHistorial"
        Me.MenuVerHistorial.Size = New System.Drawing.Size(200, 22)
        Me.MenuVerHistorial.Text = "Ver &Historial"
        '
        'SeparadorEstadisticas
        '
        Me.SeparadorEstadisticas.Name = "SeparadorEstadisticas"
        Me.SeparadorEstadisticas.Size = New System.Drawing.Size(197, 6)
        '
        'MenuReiniciarEstadisticas
        '
        Me.MenuReiniciarEstadisticas.Name = "MenuReiniciarEstadisticas"
        Me.MenuReiniciarEstadisticas.Size = New System.Drawing.Size(200, 22)
        Me.MenuReiniciarEstadisticas.Text = "&Reiniciar Estadísticas"
        '
        'MenuAyuda
        '
        Me.MenuAyuda.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuComoUsar, Me.MenuAtajosTeclado, Me.SeparadorAyuda, Me.MenuAcercaDe})
        Me.MenuAyuda.Name = "MenuAyuda"
        Me.MenuAyuda.Size = New System.Drawing.Size(53, 20)
        Me.MenuAyuda.Text = "A&yuda"
        '
        'MenuComoUsar
        '
        Me.MenuComoUsar.Name = "MenuComoUsar"
        Me.MenuComoUsar.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.MenuComoUsar.Size = New System.Drawing.Size(200, 22)
        Me.MenuComoUsar.Text = "&Cómo Usar..."
        '
        'MenuAtajosTeclado
        '
        Me.MenuAtajosTeclado.Name = "MenuAtajosTeclado"
        Me.MenuAtajosTeclado.Size = New System.Drawing.Size(200, 22)
        Me.MenuAtajosTeclado.Text = "&Atajos de Teclado"
        '
        'SeparadorAyuda
        '
        Me.SeparadorAyuda.Name = "SeparadorAyuda"
        Me.SeparadorAyuda.Size = New System.Drawing.Size(197, 6)
        '
        'MenuAcercaDe
        '
        Me.MenuAcercaDe.Name = "MenuAcercaDe"
        Me.MenuAcercaDe.Size = New System.Drawing.Size(200, 22)
        Me.MenuAcercaDe.Text = "&Acerca de..."
        '
        'BarraEstado
        '
        Me.BarraEstado.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LabelModo, Me.LabelPosicion})
        Me.BarraEstado.Location = New System.Drawing.Point(0, 678)
        Me.BarraEstado.Name = "BarraEstado"
        Me.BarraEstado.Size = New System.Drawing.Size(1200, 22)
        Me.BarraEstado.TabIndex = 2
        Me.BarraEstado.Text = "StatusStrip1"
        '
        'LabelModo
        '
        Me.LabelModo.Name = "LabelModo"
        Me.LabelModo.Size = New System.Drawing.Size(0, 17)
        '
        'LabelPosicion
        '
        Me.LabelPosicion.Name = "LabelPosicion"
        Me.LabelPosicion.Size = New System.Drawing.Size(1185, 17)
        Me.LabelPosicion.Spring = True
        Me.LabelPosicion.Text = "Línea: 1, Columna: 1"
        Me.LabelPosicion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.PanelPizarra)
        Me.Controls.Add(Me.MenuPrincipal)
        Me.Controls.Add(Me.BarraEstado)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuPrincipal
        Me.Name = "Form1"
        Me.Text = "Pizarra Electrónica - Práctica de Cálculos"
        Me.MenuPrincipal.ResumeLayout(False)
        Me.MenuPrincipal.PerformLayout()
        Me.BarraEstado.ResumeLayout(False)
        Me.BarraEstado.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelPizarra As DoubleBufferedPanel
    Friend WithEvents MenuPrincipal As MenuStrip
    Friend WithEvents MenuArchivo As ToolStripMenuItem
    Friend WithEvents MenuAbrir As ToolStripMenuItem
    Friend WithEvents MenuGuardar As ToolStripMenuItem
    Friend WithEvents MenuGuardarComo As ToolStripMenuItem
    Friend WithEvents SeparadorArchivo As ToolStripSeparator
    Friend WithEvents MenuSalir As ToolStripMenuItem
    Friend WithEvents MenuEditar As ToolStripMenuItem
    Friend WithEvents MenuVer As ToolStripMenuItem
    Friend WithEvents MenuGraficar As ToolStripMenuItem
    Friend WithEvents MenuOpciones As ToolStripMenuItem
    Friend WithEvents MenuRadianes As ToolStripMenuItem
    Friend WithEvents SeparadorOpciones As ToolStripSeparator
    Friend WithEvents MenuTemas As ToolStripMenuItem
    Friend WithEvents MenuTemaClasico As ToolStripMenuItem
    Friend WithEvents MenuTemaOscuroAzul As ToolStripMenuItem
    Friend WithEvents MenuTemaMatriz As ToolStripMenuItem
    Friend WithEvents MenuTemaRetro As ToolStripMenuItem
    Friend WithEvents MenuAprender As ToolStripMenuItem
    Friend WithEvents MenuHorasReloj As ToolStripMenuItem
    Friend WithEvents MenuEstadisticas As ToolStripMenuItem
    Friend WithEvents MenuVerEstadisticas As ToolStripMenuItem
    Friend WithEvents MenuVerHistorial As ToolStripMenuItem
    Friend WithEvents SeparadorEstadisticas As ToolStripSeparator
    Friend WithEvents MenuReiniciarEstadisticas As ToolStripMenuItem
    Friend WithEvents MenuAyuda As ToolStripMenuItem
    Friend WithEvents MenuComoUsar As ToolStripMenuItem
    Friend WithEvents MenuAtajosTeclado As ToolStripMenuItem
    Friend WithEvents SeparadorAyuda As ToolStripSeparator
    Friend WithEvents MenuAcercaDe As ToolStripMenuItem
    Friend WithEvents BarraEstado As StatusStrip
    Friend WithEvents LabelModo As ToolStripStatusLabel
    Friend WithEvents LabelPosicion As ToolStripStatusLabel
End Class
