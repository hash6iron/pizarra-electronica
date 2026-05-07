Public Class FormGenerarEjercicios
    Public Property TipoOperacion As String = "Mezclado"
    Public Property NumeroCifras As Integer = 2
    Public Property CantidadEjercicios As Integer = 10
    Public Property FormatoVertical As Boolean = False
    Public Property GenerarEjercicios As Boolean = False

    Private Sub FormGenerarEjercicios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configurar valores predeterminados
        ComboTipoOperacion.SelectedIndex = 0
        ComboNumeroCifras.SelectedIndex = 1 ' 2 cifras por defecto
        NumericCantidad.Value = 10
        RadioHorizontal.Checked = True
    End Sub

    Private Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click
        ' Obtener tipo de operación seleccionada
        TipoOperacion = ComboTipoOperacion.SelectedItem.ToString()

        ' Obtener número de cifras
        NumeroCifras = Integer.Parse(ComboNumeroCifras.SelectedItem.ToString().Split(" "c)(0))

        ' Obtener cantidad de ejercicios
        CantidadEjercicios = CInt(NumericCantidad.Value)

        ' Obtener formato
        FormatoVertical = RadioVertical.Checked

        GenerarEjercicios = True
        Me.Close()
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        GenerarEjercicios = False
        Me.Close()
    End Sub
End Class
