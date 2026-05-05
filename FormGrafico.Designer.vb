<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormGrafico
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PanelGrafico = New System.Windows.Forms.Panel()
        Me.PanelControles = New System.Windows.Forms.Panel()
        Me.BtnZoomIn = New System.Windows.Forms.Button()
        Me.BtnZoomOut = New System.Windows.Forms.Button()
        Me.BtnReset = New System.Windows.Forms.Button()
        Me.LabelInfo = New System.Windows.Forms.Label()
        Me.LabelAyuda = New System.Windows.Forms.Label()
        Me.PanelControles.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelGrafico
        '
        Me.PanelGrafico.BackColor = System.Drawing.Color.White
        Me.PanelGrafico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelGrafico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelGrafico.Location = New System.Drawing.Point(0, 80)
        Me.PanelGrafico.Name = "PanelGrafico"
        Me.PanelGrafico.Size = New System.Drawing.Size(800, 520)
        Me.PanelGrafico.TabIndex = 0
        '
        'PanelControles
        '
        Me.PanelControles.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.PanelControles.Controls.Add(Me.LabelAyuda)
        Me.PanelControles.Controls.Add(Me.LabelInfo)
        Me.PanelControles.Controls.Add(Me.BtnReset)
        Me.PanelControles.Controls.Add(Me.BtnZoomOut)
        Me.PanelControles.Controls.Add(Me.BtnZoomIn)
        Me.PanelControles.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControles.Location = New System.Drawing.Point(0, 0)
        Me.PanelControles.Name = "PanelControles"
        Me.PanelControles.Size = New System.Drawing.Size(800, 80)
        Me.PanelControles.TabIndex = 1
        '
        'BtnZoomIn
        '
        Me.BtnZoomIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnZoomIn.Location = New System.Drawing.Point(12, 12)
        Me.BtnZoomIn.Name = "BtnZoomIn"
        Me.BtnZoomIn.Size = New System.Drawing.Size(50, 35)
        Me.BtnZoomIn.TabIndex = 0
        Me.BtnZoomIn.Text = "+"
        Me.BtnZoomIn.UseVisualStyleBackColor = True
        '
        'BtnZoomOut
        '
        Me.BtnZoomOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnZoomOut.Location = New System.Drawing.Point(68, 12)
        Me.BtnZoomOut.Name = "BtnZoomOut"
        Me.BtnZoomOut.Size = New System.Drawing.Size(50, 35)
        Me.BtnZoomOut.TabIndex = 1
        Me.BtnZoomOut.Text = "-"
        Me.BtnZoomOut.UseVisualStyleBackColor = True
        '
        'BtnReset
        '
        Me.BtnReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(124, 12)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(75, 35)
        Me.BtnReset.TabIndex = 2
        Me.BtnReset.Text = "Reset"
        Me.BtnReset.UseVisualStyleBackColor = True
        '
        'LabelInfo
        '
        Me.LabelInfo.AutoSize = True
        Me.LabelInfo.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelInfo.Location = New System.Drawing.Point(12, 55)
        Me.LabelInfo.Name = "LabelInfo"
        Me.LabelInfo.Size = New System.Drawing.Size(280, 15)
        Me.LabelInfo.TabIndex = 3
        Me.LabelInfo.Text = "X: [-10.00, 10.00] | Y: [-10.00, 10.00] | Zoom: 1.00x"
        '
        'LabelAyuda
        '
        Me.LabelAyuda.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelAyuda.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelAyuda.ForeColor = System.Drawing.Color.DimGray
        Me.LabelAyuda.Location = New System.Drawing.Point(450, 12)
        Me.LabelAyuda.Name = "LabelAyuda"
        Me.LabelAyuda.Size = New System.Drawing.Size(338, 58)
        Me.LabelAyuda.TabIndex = 4
        Me.LabelAyuda.Text = "• Usa la rueda del ratón para hacer zoom" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "• Arrastra con el botón izquierdo para desplazarte" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "• Botones +/- para zoom, Reset para volver al origen"
        Me.LabelAyuda.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FormGrafico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.PanelGrafico)
        Me.Controls.Add(Me.PanelControles)
        Me.Name = "FormGrafico"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Gráfico de Función"
        Me.PanelControles.ResumeLayout(False)
        Me.PanelControles.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelGrafico As Panel
    Friend WithEvents PanelControles As Panel
    Friend WithEvents BtnZoomIn As Button
    Friend WithEvents BtnZoomOut As Button
    Friend WithEvents BtnReset As Button
    Friend WithEvents LabelInfo As Label
    Friend WithEvents LabelAyuda As Label
End Class
