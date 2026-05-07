<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormGenerarEjercicios
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
        Me.GroupBoxTipo = New System.Windows.Forms.GroupBox()
        Me.ComboTipoOperacion = New System.Windows.Forms.ComboBox()
        Me.LabelTipo = New System.Windows.Forms.Label()
        Me.GroupBoxCifras = New System.Windows.Forms.GroupBox()
        Me.ComboNumeroCifras = New System.Windows.Forms.ComboBox()
        Me.LabelCifras = New System.Windows.Forms.Label()
        Me.GroupBoxCantidad = New System.Windows.Forms.GroupBox()
        Me.NumericCantidad = New System.Windows.Forms.NumericUpDown()
        Me.LabelCantidad = New System.Windows.Forms.Label()
        Me.GroupBoxFormato = New System.Windows.Forms.GroupBox()
        Me.RadioVertical = New System.Windows.Forms.RadioButton()
        Me.RadioHorizontal = New System.Windows.Forms.RadioButton()
        Me.BtnGenerar = New System.Windows.Forms.Button()
        Me.BtnCancelar = New System.Windows.Forms.Button()
        Me.GroupBoxTipo.SuspendLayout()
        Me.GroupBoxCifras.SuspendLayout()
        Me.GroupBoxCantidad.SuspendLayout()
        CType(Me.NumericCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxFormato.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxTipo
        '
        Me.GroupBoxTipo.Controls.Add(Me.ComboTipoOperacion)
        Me.GroupBoxTipo.Controls.Add(Me.LabelTipo)
        Me.GroupBoxTipo.Location = New System.Drawing.Point(12, 12)
        Me.GroupBoxTipo.Name = "GroupBoxTipo"
        Me.GroupBoxTipo.Size = New System.Drawing.Size(360, 70)
        Me.GroupBoxTipo.TabIndex = 0
        Me.GroupBoxTipo.TabStop = False
        Me.GroupBoxTipo.Text = "Tipo de Operación"
        '
        'ComboTipoOperacion
        '
        Me.ComboTipoOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboTipoOperacion.FormattingEnabled = True
        Me.ComboTipoOperacion.Items.AddRange(New Object() {"Mezclado", "Solo Suma", "Solo Resta", "Solo Multiplicación", "Solo División", "Suma y Resta", "Multiplicación y División"})
        Me.ComboTipoOperacion.Location = New System.Drawing.Point(90, 30)
        Me.ComboTipoOperacion.Name = "ComboTipoOperacion"
        Me.ComboTipoOperacion.Size = New System.Drawing.Size(250, 21)
        Me.ComboTipoOperacion.TabIndex = 1
        '
        'LabelTipo
        '
        Me.LabelTipo.AutoSize = True
        Me.LabelTipo.Location = New System.Drawing.Point(15, 33)
        Me.LabelTipo.Name = "LabelTipo"
        Me.LabelTipo.Size = New System.Drawing.Size(69, 13)
        Me.LabelTipo.TabIndex = 0
        Me.LabelTipo.Text = "Operaciones:"
        '
        'GroupBoxCifras
        '
        Me.GroupBoxCifras.Controls.Add(Me.ComboNumeroCifras)
        Me.GroupBoxCifras.Controls.Add(Me.LabelCifras)
        Me.GroupBoxCifras.Location = New System.Drawing.Point(12, 88)
        Me.GroupBoxCifras.Name = "GroupBoxCifras"
        Me.GroupBoxCifras.Size = New System.Drawing.Size(360, 70)
        Me.GroupBoxCifras.TabIndex = 1
        Me.GroupBoxCifras.TabStop = False
        Me.GroupBoxCifras.Text = "Número de Cifras"
        '
        'ComboNumeroCifras
        '
        Me.ComboNumeroCifras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboNumeroCifras.FormattingEnabled = True
        Me.ComboNumeroCifras.Items.AddRange(New Object() {"1 cifra (1-9)", "2 cifras (10-99)", "3 cifras (100-999)", "4 cifras (1000-9999)"})
        Me.ComboNumeroCifras.Location = New System.Drawing.Point(90, 30)
        Me.ComboNumeroCifras.Name = "ComboNumeroCifras"
        Me.ComboNumeroCifras.Size = New System.Drawing.Size(250, 21)
        Me.ComboNumeroCifras.TabIndex = 1
        '
        'LabelCifras
        '
        Me.LabelCifras.AutoSize = True
        Me.LabelCifras.Location = New System.Drawing.Point(15, 33)
        Me.LabelCifras.Name = "LabelCifras"
        Me.LabelCifras.Size = New System.Drawing.Size(38, 13)
        Me.LabelCifras.TabIndex = 0
        Me.LabelCifras.Text = "Cifras:"
        '
        'GroupBoxCantidad
        '
        Me.GroupBoxCantidad.Controls.Add(Me.NumericCantidad)
        Me.GroupBoxCantidad.Controls.Add(Me.LabelCantidad)
        Me.GroupBoxCantidad.Location = New System.Drawing.Point(12, 164)
        Me.GroupBoxCantidad.Name = "GroupBoxCantidad"
        Me.GroupBoxCantidad.Size = New System.Drawing.Size(360, 70)
        Me.GroupBoxCantidad.TabIndex = 2
        Me.GroupBoxCantidad.TabStop = False
        Me.GroupBoxCantidad.Text = "Cantidad de Ejercicios"
        '
        'NumericCantidad
        '
        Me.NumericCantidad.Location = New System.Drawing.Point(90, 31)
        Me.NumericCantidad.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.NumericCantidad.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericCantidad.Name = "NumericCantidad"
        Me.NumericCantidad.Size = New System.Drawing.Size(120, 20)
        Me.NumericCantidad.TabIndex = 1
        Me.NumericCantidad.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'LabelCantidad
        '
        Me.LabelCantidad.AutoSize = True
        Me.LabelCantidad.Location = New System.Drawing.Point(15, 33)
        Me.LabelCantidad.Name = "LabelCantidad"
        Me.LabelCantidad.Size = New System.Drawing.Size(52, 13)
        Me.LabelCantidad.TabIndex = 0
        Me.LabelCantidad.Text = "Cantidad:"
        '
        'GroupBoxFormato
        '
        Me.GroupBoxFormato.Controls.Add(Me.RadioVertical)
        Me.GroupBoxFormato.Controls.Add(Me.RadioHorizontal)
        Me.GroupBoxFormato.Location = New System.Drawing.Point(12, 240)
        Me.GroupBoxFormato.Name = "GroupBoxFormato"
        Me.GroupBoxFormato.Size = New System.Drawing.Size(360, 70)
        Me.GroupBoxFormato.TabIndex = 3
        Me.GroupBoxFormato.TabStop = False
        Me.GroupBoxFormato.Text = "Formato"
        '
        'RadioVertical
        '
        Me.RadioVertical.AutoSize = True
        Me.RadioVertical.Location = New System.Drawing.Point(200, 30)
        Me.RadioVertical.Name = "RadioVertical"
        Me.RadioVertical.Size = New System.Drawing.Size(60, 17)
        Me.RadioVertical.TabIndex = 1
        Me.RadioVertical.Text = "Vertical"
        Me.RadioVertical.UseVisualStyleBackColor = True
        '
        'RadioHorizontal
        '
        Me.RadioHorizontal.AutoSize = True
        Me.RadioHorizontal.Checked = True
        Me.RadioHorizontal.Location = New System.Drawing.Point(18, 30)
        Me.RadioHorizontal.Name = "RadioHorizontal"
        Me.RadioHorizontal.Size = New System.Drawing.Size(72, 17)
        Me.RadioHorizontal.TabIndex = 0
        Me.RadioHorizontal.TabStop = True
        Me.RadioHorizontal.Text = "Horizontal"
        Me.RadioHorizontal.UseVisualStyleBackColor = True
        '
        'BtnGenerar
        '
        Me.BtnGenerar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGenerar.Location = New System.Drawing.Point(12, 325)
        Me.BtnGenerar.Name = "BtnGenerar"
        Me.BtnGenerar.Size = New System.Drawing.Size(170, 35)
        Me.BtnGenerar.TabIndex = 4
        Me.BtnGenerar.Text = "Generar Ejercicios"
        Me.BtnGenerar.UseVisualStyleBackColor = True
        '
        'BtnCancelar
        '
        Me.BtnCancelar.Location = New System.Drawing.Point(202, 325)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(170, 35)
        Me.BtnCancelar.TabIndex = 5
        Me.BtnCancelar.Text = "Cancelar"
        Me.BtnCancelar.UseVisualStyleBackColor = True
        '
        'FormGenerarEjercicios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 372)
        Me.Controls.Add(Me.BtnCancelar)
        Me.Controls.Add(Me.BtnGenerar)
        Me.Controls.Add(Me.GroupBoxFormato)
        Me.Controls.Add(Me.GroupBoxCantidad)
        Me.Controls.Add(Me.GroupBoxCifras)
        Me.Controls.Add(Me.GroupBoxTipo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormGenerarEjercicios"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Generar Ejercicios Automáticamente"
        Me.GroupBoxTipo.ResumeLayout(False)
        Me.GroupBoxTipo.PerformLayout()
        Me.GroupBoxCifras.ResumeLayout(False)
        Me.GroupBoxCifras.PerformLayout()
        Me.GroupBoxCantidad.ResumeLayout(False)
        Me.GroupBoxCantidad.PerformLayout()
        CType(Me.NumericCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxFormato.ResumeLayout(False)
        Me.GroupBoxFormato.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBoxTipo As GroupBox
    Friend WithEvents ComboTipoOperacion As ComboBox
    Friend WithEvents LabelTipo As Label
    Friend WithEvents GroupBoxCifras As GroupBox
    Friend WithEvents ComboNumeroCifras As ComboBox
    Friend WithEvents LabelCifras As Label
    Friend WithEvents GroupBoxCantidad As GroupBox
    Friend WithEvents NumericCantidad As NumericUpDown
    Friend WithEvents LabelCantidad As Label
    Friend WithEvents GroupBoxFormato As GroupBox
    Friend WithEvents RadioVertical As RadioButton
    Friend WithEvents RadioHorizontal As RadioButton
    Friend WithEvents BtnGenerar As Button
    Friend WithEvents BtnCancelar As Button
End Class
