<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormHorasReloj
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
        Me.PanelReloj = New System.Windows.Forms.Panel()
        Me.LabelTitulo = New System.Windows.Forms.Label()
        Me.LabelPeriodo = New System.Windows.Forms.Label()
        Me.LabelInstrucciones = New System.Windows.Forms.Label()
        Me.LabelHora = New System.Windows.Forms.Label()
        Me.TextBoxHora = New System.Windows.Forms.TextBox()
        Me.LabelDosPuntos = New System.Windows.Forms.Label()
        Me.TextBoxMinutos = New System.Windows.Forms.TextBox()
        Me.RadioButtonOpcion1 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonOpcion2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonOpcion3 = New System.Windows.Forms.RadioButton()
        Me.ButtonComprobar = New System.Windows.Forms.Button()
        Me.ButtonSiguiente = New System.Windows.Forms.Button()
        Me.LabelEstadisticas = New System.Windows.Forms.Label()
        Me.CheckBoxModoTexto = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'PanelReloj
        '
        Me.PanelReloj.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.PanelReloj.Location = New System.Drawing.Point(50, 90)
        Me.PanelReloj.Name = "PanelReloj"
        Me.PanelReloj.Size = New System.Drawing.Size(400, 400)
        Me.PanelReloj.TabIndex = 0
        '
        'LabelTitulo
        '
        Me.LabelTitulo.AutoSize = True
        Me.LabelTitulo.Font = New System.Drawing.Font("Arial", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTitulo.ForeColor = System.Drawing.Color.DarkBlue
        Me.LabelTitulo.Location = New System.Drawing.Point(120, 20)
        Me.LabelTitulo.Name = "LabelTitulo"
        Me.LabelTitulo.Size = New System.Drawing.Size(260, 32)
        Me.LabelTitulo.TabIndex = 1
        Me.LabelTitulo.Text = "Aprender las Horas"
        '
        'LabelPeriodo
        '
        Me.LabelPeriodo.AutoSize = True
        Me.LabelPeriodo.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPeriodo.ForeColor = System.Drawing.Color.Orange
        Me.LabelPeriodo.Location = New System.Drawing.Point(530, 120)
        Me.LabelPeriodo.Name = "LabelPeriodo"
        Me.LabelPeriodo.Size = New System.Drawing.Size(142, 37)
        Me.LabelPeriodo.TabIndex = 2
        Me.LabelPeriodo.Text = "Mañana"
        '
        'LabelInstrucciones
        '
        Me.LabelInstrucciones.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelInstrucciones.ForeColor = System.Drawing.Color.Black
        Me.LabelInstrucciones.Location = New System.Drawing.Point(500, 180)
        Me.LabelInstrucciones.Name = "LabelInstrucciones"
        Me.LabelInstrucciones.Size = New System.Drawing.Size(250, 60)
        Me.LabelInstrucciones.TabIndex = 3
        Me.LabelInstrucciones.Text = "¿Qué hora marca el reloj?" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Escribe la hora en formato 24h"
        '
        'LabelHora
        '
        Me.LabelHora.AutoSize = True
        Me.LabelHora.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelHora.Location = New System.Drawing.Point(500, 260)
        Me.LabelHora.Name = "LabelHora"
        Me.LabelHora.Size = New System.Drawing.Size(51, 19)
        Me.LabelHora.TabIndex = 4
        Me.LabelHora.Text = "Hora:"
        '
        'TextBoxHora
        '
        Me.TextBoxHora.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxHora.Location = New System.Drawing.Point(560, 255)
        Me.TextBoxHora.MaxLength = 2
        Me.TextBoxHora.Name = "TextBoxHora"
        Me.TextBoxHora.Size = New System.Drawing.Size(50, 35)
        Me.TextBoxHora.TabIndex = 5
        Me.TextBoxHora.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LabelDosPuntos
        '
        Me.LabelDosPuntos.AutoSize = True
        Me.LabelDosPuntos.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDosPuntos.Location = New System.Drawing.Point(610, 258)
        Me.LabelDosPuntos.Name = "LabelDosPuntos"
        Me.LabelDosPuntos.Size = New System.Drawing.Size(17, 29)
        Me.LabelDosPuntos.TabIndex = 6
        Me.LabelDosPuntos.Text = ":"
        '
        'TextBoxMinutos
        '
        Me.TextBoxMinutos.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxMinutos.Location = New System.Drawing.Point(627, 255)
        Me.TextBoxMinutos.MaxLength = 2
        Me.TextBoxMinutos.Name = "TextBoxMinutos"
        Me.TextBoxMinutos.Size = New System.Drawing.Size(50, 35)
        Me.TextBoxMinutos.TabIndex = 7
        Me.TextBoxMinutos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RadioButtonOpcion1
        '
        Me.RadioButtonOpcion1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButtonOpcion1.Location = New System.Drawing.Point(500, 260)
        Me.RadioButtonOpcion1.Name = "RadioButtonOpcion1"
        Me.RadioButtonOpcion1.Size = New System.Drawing.Size(250, 30)
        Me.RadioButtonOpcion1.TabIndex = 11
        Me.RadioButtonOpcion1.Text = "Opción 1"
        Me.RadioButtonOpcion1.UseVisualStyleBackColor = True
        Me.RadioButtonOpcion1.Visible = False
        '
        'RadioButtonOpcion2
        '
        Me.RadioButtonOpcion2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButtonOpcion2.Location = New System.Drawing.Point(500, 300)
        Me.RadioButtonOpcion2.Name = "RadioButtonOpcion2"
        Me.RadioButtonOpcion2.Size = New System.Drawing.Size(250, 30)
        Me.RadioButtonOpcion2.TabIndex = 12
        Me.RadioButtonOpcion2.Text = "Opción 2"
        Me.RadioButtonOpcion2.UseVisualStyleBackColor = True
        Me.RadioButtonOpcion2.Visible = False
        '
        'RadioButtonOpcion3
        '
        Me.RadioButtonOpcion3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButtonOpcion3.Location = New System.Drawing.Point(500, 340)
        Me.RadioButtonOpcion3.Name = "RadioButtonOpcion3"
        Me.RadioButtonOpcion3.Size = New System.Drawing.Size(250, 30)
        Me.RadioButtonOpcion3.TabIndex = 13
        Me.RadioButtonOpcion3.Text = "Opción 3"
        Me.RadioButtonOpcion3.UseVisualStyleBackColor = True
        Me.RadioButtonOpcion3.Visible = False
        '
        'ButtonComprobar
        '
        Me.ButtonComprobar.BackColor = System.Drawing.Color.LimeGreen
        Me.ButtonComprobar.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonComprobar.ForeColor = System.Drawing.Color.White
        Me.ButtonComprobar.Location = New System.Drawing.Point(50, 510)
        Me.ButtonComprobar.Name = "ButtonComprobar"
        Me.ButtonComprobar.Size = New System.Drawing.Size(130, 40)
        Me.ButtonComprobar.TabIndex = 8
        Me.ButtonComprobar.Text = "Comprobar"
        Me.ButtonComprobar.UseVisualStyleBackColor = False
        '
        'ButtonSiguiente
        '
        Me.ButtonSiguiente.BackColor = System.Drawing.Color.DodgerBlue
        Me.ButtonSiguiente.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSiguiente.ForeColor = System.Drawing.Color.White
        Me.ButtonSiguiente.Location = New System.Drawing.Point(190, 510)
        Me.ButtonSiguiente.Name = "ButtonSiguiente"
        Me.ButtonSiguiente.Size = New System.Drawing.Size(130, 40)
        Me.ButtonSiguiente.TabIndex = 9
        Me.ButtonSiguiente.Text = "Siguiente"
        Me.ButtonSiguiente.UseVisualStyleBackColor = False
        '
        'LabelEstadisticas
        '
        Me.LabelEstadisticas.AutoSize = True
        Me.LabelEstadisticas.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelEstadisticas.Location = New System.Drawing.Point(400, 522)
        Me.LabelEstadisticas.Name = "LabelEstadisticas"
        Me.LabelEstadisticas.Size = New System.Drawing.Size(284, 16)
        Me.LabelEstadisticas.TabIndex = 10
        Me.LabelEstadisticas.Text = "Ejercicios: 0 | Correctos: 0 | Porcentaje: 0%"
        '
        'CheckBoxModoTexto
        '
        Me.CheckBoxModoTexto.AutoSize = True
        Me.CheckBoxModoTexto.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxModoTexto.Location = New System.Drawing.Point(500, 226)
        Me.CheckBoxModoTexto.Name = "CheckBoxModoTexto"
        Me.CheckBoxModoTexto.Size = New System.Drawing.Size(181, 21)
        Me.CheckBoxModoTexto.TabIndex = 14
        Me.CheckBoxModoTexto.Text = "Modo Texto (menos 10)"
        Me.CheckBoxModoTexto.UseVisualStyleBackColor = True
        '
        'FormHorasReloj
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(784, 570)
        Me.Controls.Add(Me.CheckBoxModoTexto)
        Me.Controls.Add(Me.RadioButtonOpcion3)
        Me.Controls.Add(Me.RadioButtonOpcion2)
        Me.Controls.Add(Me.RadioButtonOpcion1)
        Me.Controls.Add(Me.LabelEstadisticas)
        Me.Controls.Add(Me.ButtonSiguiente)
        Me.Controls.Add(Me.ButtonComprobar)
        Me.Controls.Add(Me.TextBoxMinutos)
        Me.Controls.Add(Me.LabelDosPuntos)
        Me.Controls.Add(Me.TextBoxHora)
        Me.Controls.Add(Me.LabelHora)
        Me.Controls.Add(Me.LabelInstrucciones)
        Me.Controls.Add(Me.LabelPeriodo)
        Me.Controls.Add(Me.LabelTitulo)
        Me.Controls.Add(Me.PanelReloj)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormHorasReloj"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Aprender Horas del Reloj"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelReloj As Panel
    Friend WithEvents LabelTitulo As Label
    Friend WithEvents LabelPeriodo As Label
    Friend WithEvents LabelInstrucciones As Label
    Friend WithEvents LabelHora As Label
    Friend WithEvents TextBoxHora As TextBox
    Friend WithEvents LabelDosPuntos As Label
    Friend WithEvents TextBoxMinutos As TextBox
    Friend WithEvents RadioButtonOpcion1 As RadioButton
    Friend WithEvents RadioButtonOpcion2 As RadioButton
    Friend WithEvents RadioButtonOpcion3 As RadioButton
    Friend WithEvents ButtonComprobar As Button
    Friend WithEvents ButtonSiguiente As Button
    Friend WithEvents LabelEstadisticas As Label
    Friend WithEvents CheckBoxModoTexto As CheckBox
End Class
