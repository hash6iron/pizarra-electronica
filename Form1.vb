Public Class Form1
    ' Límites máximos
    Private Const MAX_COLUMNAS As Integer = 200
    Private Const MAX_FILAS As Integer = 100
    Private Const TAMANO_CHAR As Integer = 16

    ' Dimensiones dinámicas
    Private COLUMNAS As Integer = 80
    Private FILAS As Integer = 25

    Private pizarra(MAX_FILAS - 1, MAX_COLUMNAS - 1) As Char
    Private colores(MAX_FILAS - 1, MAX_COLUMNAS - 1) As Color
    Private esCampoResultado(MAX_FILAS - 1, MAX_COLUMNAS - 1) As Boolean ' Marca las celdas que son campos de resultado
    Private cursorX As Integer = 0
    Private cursorY As Integer = 0
    Private parpadeo As Boolean = True
    Private WithEvents timerCursor As New Timer()

    Private haySeleccion As Boolean = False
    Private selStartX As Integer = 0
    Private selStartY As Integer = 0
    Private selEndX As Integer = 0
    Private selEndY As Integer = 0
    Private portapapeles As String = ""
    Private modoInsertar As Boolean = True
    Private ultimaTeclaInicio As Boolean = False
    Private ultimaTeclaFin As Boolean = False
    Private archivoActual As String = ""
    Private pizarraModificada As Boolean = False
    Private usarRadianes As Boolean = False

    Private historialEjercicios As New List(Of EjercicioHistorial)
    Private ejerciciosCorrectos As Integer = 0
    Private ejerciciosIncorrectos As Integer = 0
    Private ejerciciosTotales As Integer = 0

    Private temaActual As TemaColor = TemaColor.Clasico

    Public Enum TemaColor
        Clasico
        OscuroAzul
        Matriz
        Retro
    End Enum

    Public Class ConfiguracionTema
        Public Property ColorFondo As Color
        Public Property ColorTexto As Color
        Public Property ColorCorrecto As Color
        Public Property ColorIncorrecto As Color
        Public Property ColorCursor As Color
        Public Property ColorSeleccion As Color
    End Class

    Private Function ObtenerTema(tema As TemaColor) As ConfiguracionTema
        Dim config As New ConfiguracionTema()

        Select Case tema
            Case TemaColor.Clasico
                config.ColorFondo = Color.Black
                config.ColorTexto = Color.White
                config.ColorCorrecto = Color.Lime
                config.ColorIncorrecto = Color.Red
                config.ColorCursor = Color.Yellow
                config.ColorSeleccion = Color.DarkBlue

            Case TemaColor.OscuroAzul
                config.ColorFondo = Color.FromArgb(15, 15, 35)
                config.ColorTexto = Color.FromArgb(200, 220, 255)
                config.ColorCorrecto = Color.FromArgb(100, 255, 150)
                config.ColorIncorrecto = Color.FromArgb(255, 100, 100)
                config.ColorCursor = Color.Cyan
                config.ColorSeleccion = Color.FromArgb(40, 60, 100)

            Case TemaColor.Matriz
                config.ColorFondo = Color.Black
                config.ColorTexto = Color.FromArgb(0, 255, 0)
                config.ColorCorrecto = Color.FromArgb(150, 255, 150)
                config.ColorIncorrecto = Color.FromArgb(255, 100, 0)
                config.ColorCursor = Color.FromArgb(0, 255, 0)
                config.ColorSeleccion = Color.FromArgb(0, 50, 0)

            Case TemaColor.Retro
                config.ColorFondo = Color.FromArgb(40, 40, 120)
                config.ColorTexto = Color.FromArgb(255, 255, 180)
                config.ColorCorrecto = Color.FromArgb(150, 255, 200)
                config.ColorIncorrecto = Color.FromArgb(255, 150, 150)
                config.ColorCursor = Color.White
                config.ColorSeleccion = Color.FromArgb(80, 80, 160)
        End Select

        Return config
    End Function

    Public Class EjercicioHistorial
        Public Property Ejercicio As String
        Public Property RespuestaUsuario As String
        Public Property RespuestaCorrecta As String
        Public Property EsCorrecto As Boolean
        Public Property Fecha As DateTime

        Public Sub New(ej As String, respUsr As String, respCorr As String, correcto As Boolean)
            Ejercicio = ej
            RespuestaUsuario = respUsr
            RespuestaCorrecta = respCorr
            EsCorrecto = correcto
            Fecha = DateTime.Now
        End Sub
    End Class

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Habilitar doble buffer para eliminar el parpadeo
        PanelPizarra.GetType().InvokeMember("DoubleBuffered",
            Reflection.BindingFlags.SetProperty Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic,
            Nothing, PanelPizarra, New Object() {True})

        Me.WindowState = FormWindowState.Maximized

        ' Calcular dimensiones basadas en el tamaño de la ventana
        CalcularDimensionesPizarra()
        InicializarPizarra()
        ConfigurarTimer()
        ActualizarBarraEstado()
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        ' Recalcular dimensiones cuando cambia el tamaño de la ventana
        If Me.WindowState <> FormWindowState.Minimized Then
            CalcularDimensionesPizarra()
            PanelPizarra.Invalidate()
        End If
    End Sub

    Private Sub CalcularDimensionesPizarra()
        ' Calcular cuántas columnas y filas caben en el panel
        Dim anchoPanelUtil As Integer = PanelPizarra.ClientSize.Width
        Dim altoPanelUtil As Integer = PanelPizarra.ClientSize.Height

        ' Calcular columnas y filas que caben
        Dim columnasCalculadas As Integer = Math.Max(1, anchoPanelUtil \ TAMANO_CHAR)
        Dim filasCalculadas As Integer = Math.Max(1, altoPanelUtil \ TAMANO_CHAR)

        ' Aplicar límites máximos
        COLUMNAS = Math.Min(columnasCalculadas, MAX_COLUMNAS)
        FILAS = Math.Min(filasCalculadas, MAX_FILAS)

        ' Asegurar que el cursor no se salga de los límites
        If cursorX >= COLUMNAS Then cursorX = COLUMNAS - 1
        If cursorY >= FILAS Then cursorY = FILAS - 1
    End Sub

    Private Sub InicializarPizarra()
        Dim config = ObtenerTema(temaActual)
        ' Inicializar todo el array (usar MAX para cubrir todo)
        For y = 0 To MAX_FILAS - 1
            For x = 0 To MAX_COLUMNAS - 1
                pizarra(y, x) = " "c
                colores(y, x) = config.ColorTexto
                esCampoResultado(y, x) = False
            Next
        Next
        PanelPizarra.BackColor = config.ColorFondo
        PanelPizarra.Invalidate()
    End Sub

    Private Sub ConfigurarTimer()
        timerCursor.Interval = 500
        timerCursor.Start()
    End Sub

    Private Sub TimerCursor_Tick(sender As Object, e As EventArgs) Handles timerCursor.Tick
        parpadeo = Not parpadeo
        PanelPizarra.Invalidate()
    End Sub

    Private Sub PanelPizarra_Paint(sender As Object, e As PaintEventArgs) Handles PanelPizarra.Paint
        Dim g As Graphics = e.Graphics
        Dim fuente As New Font("Courier New", 12, FontStyle.Bold)
        Dim fuenteSuperindice As New Font("Courier New", 8, FontStyle.Bold)
        Dim config = ObtenerTema(temaActual)
        Dim brochaCursor As New SolidBrush(config.ColorCursor)
        Dim brochaFondoCampo As New SolidBrush(Color.FromArgb(50, 50, 50)) ' Sombreado para campos de resultado

        For y = 0 To FILAS - 1
            Dim x As Integer = 0
            Dim offsetAcumulado As Integer = 0 ' Para ajustar posición en modo NO EDICIÓN

            While x <= COLUMNAS - 1
                Dim posX As Integer = x * TAMANO_CHAR - offsetAcumulado
                Dim posY As Integer = y * TAMANO_CHAR
                Dim estaSeleccionado As Boolean = EstaDentroSeleccion(x, y)

                ' En modo NO EDICIÓN, detectar y dibujar superíndices y símbolos especiales
                Dim esSuperindice As Boolean = False
                Dim saltarCaracter As Boolean = False
                Dim caracterDibujar As String = pizarra(y, x).ToString()

                ' Detectar "pi" y convertir a símbolo π en modo NO EDICIÓN
                If Not MenuEditar.Checked AndAlso x < COLUMNAS - 1 Then
                    If (pizarra(y, x) = "p"c OrElse pizarra(y, x) = "P"c) AndAlso
                       (pizarra(y, x + 1) = "i"c OrElse pizarra(y, x + 1) = "I"c) Then
                        ' Encontramos "pi" o "PI", reemplazar por símbolo π y quitar espacio extra
                        caracterDibujar = "π"
                        ' Acumular offset para quitar el espacio del "i"
                        offsetAcumulado += TAMANO_CHAR
                    ElseIf x > 0 AndAlso
                           (pizarra(y, x - 1) = "p"c OrElse pizarra(y, x - 1) = "P"c) AndAlso
                           (pizarra(y, x) = "i"c OrElse pizarra(y, x) = "I"c) Then
                        ' Este es el "i" de "pi", saltarlo
                        saltarCaracter = True
                    End If
                End If

                If Not MenuEditar.Checked AndAlso x < COLUMNAS - 1 AndAlso pizarra(y, x) = "^"c Then
                    ' Encontramos ^, saltarlo visualmente y acumular offset
                    saltarCaracter = True
                    offsetAcumulado += TAMANO_CHAR - 10 ' Restar el espacio del ^ pero dejar pequeño espacio
                ElseIf Not MenuEditar.Checked AndAlso x > 0 AndAlso pizarra(y, x - 1) = "^"c Then
                    ' Este es un número que va después de ^, dibujarlo como superíndice
                    esSuperindice = True
                    posX = (x - 1) * TAMANO_CHAR - (offsetAcumulado - (TAMANO_CHAR - 10)) + 10 ' Posicionarlo cerca de la base
                End If

                ' Dibujar fondo sombreado para campos de resultado
                If esCampoResultado(y, x) Then
                    g.FillRectangle(brochaFondoCampo, posX, posY, TAMANO_CHAR, TAMANO_CHAR)
                End If

                If Not saltarCaracter Then
                    If x = cursorX AndAlso y = cursorY AndAlso parpadeo Then
                        g.FillRectangle(brochaCursor, posX, posY, TAMANO_CHAR, TAMANO_CHAR)
                        Dim brochaNegra As New SolidBrush(config.ColorFondo)
                        If esSuperindice Then
                            g.DrawString(caracterDibujar, fuenteSuperindice, brochaNegra, posX, posY - 4)
                        Else
                            g.DrawString(caracterDibujar, fuente, brochaNegra, posX, posY)
                        End If
                        brochaNegra.Dispose()
                    ElseIf estaSeleccionado Then
                        Dim brochaFondoSeleccion As New SolidBrush(config.ColorSeleccion)
                        g.FillRectangle(brochaFondoSeleccion, posX, posY, TAMANO_CHAR, TAMANO_CHAR)
                        brochaFondoSeleccion.Dispose()
                        Dim brochaSeleccion As New SolidBrush(config.ColorTexto)
                        If esSuperindice Then
                            g.DrawString(caracterDibujar, fuenteSuperindice, brochaSeleccion, posX, posY - 4)
                        Else
                            g.DrawString(caracterDibujar, fuente, brochaSeleccion, posX, posY)
                        End If
                        brochaSeleccion.Dispose()
                    Else
                        Dim brocha As New SolidBrush(colores(y, x))
                        If esSuperindice Then
                            g.DrawString(caracterDibujar, fuenteSuperindice, brocha, posX, posY - 4)
                        Else
                            g.DrawString(caracterDibujar, fuente, brocha, posX, posY)
                        End If
                        brocha.Dispose()
                    End If
                End If

                x += 1
            End While
        Next

        fuente.Dispose()
        fuenteSuperindice.Dispose()
        brochaCursor.Dispose()
        brochaFondoCampo.Dispose()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Dim shiftPresionado As Boolean = e.Shift

        ' Resetear flags de teclas especiales si se pulsa otra tecla
        If e.KeyCode <> Keys.Home Then
            ultimaTeclaInicio = False
        End If
        If e.KeyCode <> Keys.End Then
            ultimaTeclaFin = False
        End If

        Select Case e.KeyCode
            Case Keys.Home
                If ultimaTeclaInicio Then
                    ' Segunda pulsación: ir al inicio de la línea (columna 0)
                    cursorX = 0
                    ultimaTeclaInicio = False
                Else
                    ' Primera pulsación: ir al primer carácter no-espacio
                    Dim primerCaracter As Integer = ObtenerPrimerCaracterLinea(cursorY)
                    cursorX = primerCaracter
                    ultimaTeclaInicio = True
                End If
                haySeleccion = False
                e.Handled = True

            Case Keys.End
                If ultimaTeclaFin Then
                    ' Segunda pulsación: ir al final de la línea (columna 79)
                    cursorX = COLUMNAS - 1
                    ultimaTeclaFin = False
                Else
                    ' Primera pulsación: ir después del último carácter
                    Dim ultimoCaracter As Integer = ObtenerUltimoCaracterLinea(cursorY)
                    cursorX = ultimoCaracter
                    ultimaTeclaFin = True
                End If
                haySeleccion = False
                e.Handled = True

            Case Keys.Left
                If shiftPresionado Then
                    IniciarOActualizarSeleccion()
                Else
                    haySeleccion = False
                End If
                If MenuEditar.Checked Then
                    ' En modo EDICIÓN, movimiento libre
                    If cursorX > 0 Then cursorX -= 1
                Else
                    ' En modo NO EDICIÓN, moverse solo dentro de campos de resultado
                    If cursorX > 0 Then
                        ' Solo moverse si estamos en un campo y la siguiente posición también es campo
                        If esCampoResultado(cursorY, cursorX) AndAlso esCampoResultado(cursorY, cursorX - 1) Then
                            cursorX -= 1
                        End If
                    End If
                End If
                If shiftPresionado Then ActualizarFinSeleccion()
                e.Handled = True

            Case Keys.Right
                If shiftPresionado Then
                    IniciarOActualizarSeleccion()
                Else
                    haySeleccion = False
                End If
                If MenuEditar.Checked Then
                    ' En modo EDICIÓN, movimiento libre
                    If cursorX < COLUMNAS - 1 Then cursorX += 1
                Else
                    ' En modo NO EDICIÓN, moverse solo dentro de campos de resultado
                    If cursorX < COLUMNAS - 1 Then
                        ' Solo moverse si estamos en un campo y la siguiente posición también es campo
                        If esCampoResultado(cursorY, cursorX) AndAlso esCampoResultado(cursorY, cursorX + 1) Then
                            cursorX += 1
                        End If
                    End If
                End If
                If shiftPresionado Then ActualizarFinSeleccion()
                e.Handled = True

            Case Keys.Up
                If shiftPresionado Then
                    IniciarOActualizarSeleccion()
                Else
                    haySeleccion = False
                End If
                If cursorY > 0 Then cursorY -= 1
                If shiftPresionado Then ActualizarFinSeleccion()
                e.Handled = True

            Case Keys.Down
                If shiftPresionado Then
                    IniciarOActualizarSeleccion()
                Else
                    haySeleccion = False
                End If
                If cursorY < FILAS - 1 Then cursorY += 1
                If shiftPresionado Then ActualizarFinSeleccion()
                e.Handled = True

            Case Keys.Back
                ' En modo NO EDICIÓN, solo permitir borrar en campos de resultado
                If Not MenuEditar.Checked AndAlso Not esCampoResultado(cursorY, cursorX) Then
                    e.Handled = True
                ElseIf cursorX > 0 Then
                    cursorX -= 1
                    pizarra(cursorY, cursorX) = " "c
                ElseIf cursorY > 0 Then
                    cursorY -= 1
                    ' Verificar si la línea anterior tiene contenido
                    If LineaEstaVacia(cursorY) Then
                        cursorX = 0
                    Else
                        cursorX = ObtenerUltimoCaracterLinea(cursorY)
                    End If
                End If
                haySeleccion = False
                e.Handled = True

            Case Keys.Delete
                ' En modo NO EDICIÓN, solo permitir borrar en campos de resultado
                If Not MenuEditar.Checked AndAlso Not esCampoResultado(cursorY, cursorX) Then
                    e.Handled = True
                ElseIf haySeleccion Then
                    EliminarSeleccion()
                Else
                    ' Eliminar carácter actual y traer el texto de la derecha
                    TraerTextoAIzquierda(cursorY, cursorX)
                End If
                e.Handled = True

            Case Keys.Tab
                If e.Shift Then
                    ' SHIFT+TAB: retroceder al anterior tab stop
                    RetrocederTab()
                Else
                    ' TAB: avanzar al siguiente tab stop
                    AvanzarTab()
                End If
                e.Handled = True

            Case Keys.Enter
                If MenuEditar.Checked Then
                    If e.Shift Then
                        MoverCursorSiguienteLineaEdicion(1)
                    Else
                        MoverCursorSiguienteLineaEdicion(2)
                    End If
                Else
                    MoverCursorSiguienteLineaComprobacion()
                End If
                e.Handled = True

            Case Keys.Escape
                LimpiarLinea()
                e.Handled = True

            Case Keys.Insert
                modoInsertar = Not modoInsertar
                ActualizarBarraEstado()
                e.Handled = True

            Case Keys.C
                If e.Control Then
                    Copiar()
                    e.Handled = True
                End If

            Case Keys.X
                If e.Control Then
                    Cortar()
                    e.Handled = True
                End If

            Case Keys.V
                If e.Control Then
                    Pegar()
                    e.Handled = True
                End If

            Case Keys.F2
                ' Alternar modo EDICIÓN / COMPROBACIÓN
                If MenuEditar.Checked Then
                    ' Pasando de EDICIÓN a NO EDICIÓN: detectar campos
                    MenuEditar.Checked = False
                    DetectarCamposResultado()
                    ' Mover el cursor al primer campo de resultado
                    MoverCursorPrimerCampo()
                Else
                    ' Pasando de NO EDICIÓN a EDICIÓN: restaurar las R
                    MenuEditar.Checked = True
                    RestaurarCamposResultado()
                End If
                e.Handled = True
        End Select

        ActualizarBarraEstado()
        PanelPizarra.Invalidate()
    End Sub

    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        Dim c As Char = e.KeyChar

        If Char.IsLetterOrDigit(c) OrElse "+-*/=.,()^ ".Contains(c) Then
            ' En modo NO EDICIÓN, solo permitir escritura en campos de resultado
            If Not MenuEditar.Checked AndAlso Not esCampoResultado(cursorY, cursorX) Then
                e.Handled = True
                Return
            End If

            If haySeleccion Then
                EliminarSeleccion()
            End If

            If modoInsertar Then
                ' Modo INSERTAR: empujar el texto hacia la derecha
                EmpujarTextoADerecha(cursorY, cursorX)
                pizarra(cursorY, cursorX) = c
                colores(cursorY, cursorX) = Color.White
            Else
                ' Modo SOBREESCRIBIR: reemplazar el carácter actual
                pizarra(cursorY, cursorX) = c
                colores(cursorY, cursorX) = Color.White
            End If

            If cursorX < COLUMNAS - 1 Then
                ' Solo avanzar si la siguiente posición también es campo de resultado o estamos en modo EDICIÓN
                If MenuEditar.Checked OrElse esCampoResultado(cursorY, cursorX + 1) Then
                    cursorX += 1
                End If
            End If

            pizarraModificada = True
            PanelPizarra.Invalidate()
            e.Handled = True
        End If
    End Sub

    Private Sub ValidarCalculo()
        Dim lineaTexto As String = ObtenerLineaActual().Trim()

        If String.IsNullOrWhiteSpace(lineaTexto) Then Return

        ' Extraer la operación (cadena continua sin espacios que contiene =)
        Dim operacion As String = ExtraerOperacion(lineaTexto)

        If String.IsNullOrEmpty(operacion) Then Return

        If operacion.Contains("="c) Then
            Dim partes() As String = operacion.Split("="c)

            ' Verificar si es una ecuación con incógnita x
            If partes.Length >= 2 AndAlso (partes(0).Contains("x") OrElse partes(0).Contains("X")) Then
                ValidarEcuacion(operacion, partes)
            ElseIf partes.Length = 2 Then
                ValidarOperacionNormal(operacion, partes)
            End If
        End If
    End Sub

    Private Sub ValidarOperacionNormal(operacion As String, partes() As String)
        Dim expresion As String = partes(0).Trim()
        Dim resultadoUsuario As String = partes(1).Trim()

        Try
            Dim resultadoCorrecto As Double = EvaluarExpresion(expresion)
            Dim resultadoUsuarioNum As Double

            If Double.TryParse(resultadoUsuario, resultadoUsuarioNum) Then
                Dim correcto As Boolean = Math.Abs(resultadoCorrecto - resultadoUsuarioNum) < 0.001
                Dim config = ObtenerTema(temaActual)

                If correcto Then
                    ColorearOperacion(cursorY, operacion, config.ColorCorrecto)
                    ejerciciosCorrectos += 1
                Else
                    ColorearOperacion(cursorY, operacion, config.ColorIncorrecto)
                    AgregarResultadoCorrecto(resultadoCorrecto, operacion)
                    ejerciciosIncorrectos += 1
                End If

                ejerciciosTotales += 1
                historialEjercicios.Add(New EjercicioHistorial(expresion, resultadoUsuario, resultadoCorrecto.ToString(), correcto))
            End If

        Catch ex As Exception
            Dim config = ObtenerTema(temaActual)
            ColorearOperacion(cursorY, operacion, config.ColorIncorrecto)
        End Try
    End Sub

    Private Sub ValidarEcuacion(operacion As String, partes() As String)
        Try
            ' Formato esperado: ecuacion -> x=resultado
            ' Ej: 2x+2=12 -> x=5
            Dim ecuacion As String = partes(0).Trim()
            Dim ladoDerecho As String = partes(1).Trim()
            Dim config = ObtenerTema(temaActual)

            ' Verificar si el usuario puso x=resultado
            If ladoDerecho.StartsWith("x=", StringComparison.OrdinalIgnoreCase) Then
                Dim resultadoUsuarioStr As String = ladoDerecho.Substring(2).Trim()
                Dim resultadoUsuario As Double

                If Double.TryParse(resultadoUsuarioStr, resultadoUsuario) Then
                    ' Resolver la ecuación
                    Dim solucion As Double = ResolverEcuacion(ecuacion)

                    If Not Double.IsNaN(solucion) Then
                        Dim correcto As Boolean = Math.Abs(solucion - resultadoUsuario) < 0.001

                        If correcto Then
                            ColorearOperacion(cursorY, operacion, config.ColorCorrecto)
                            ejerciciosCorrectos += 1
                        Else
                            ColorearOperacion(cursorY, operacion, config.ColorIncorrecto)
                            AgregarResultadoEcuacion(solucion, operacion)
                            ejerciciosIncorrectos += 1
                        End If

                        ejerciciosTotales += 1
                        historialEjercicios.Add(New EjercicioHistorial(ecuacion, "x=" & resultadoUsuario.ToString(), "x=" & solucion.ToString(), correcto))
                    Else
                        ColorearOperacion(cursorY, operacion, config.ColorIncorrecto)
                    End If
                End If
            Else
                ' El usuario aún no ha puesto x=, buscar el valor correcto
                Dim valorDerecho As Double
                If Double.TryParse(ladoDerecho, valorDerecho) Then
                    Dim solucion As Double = ResolverEcuacionConValor(ecuacion, valorDerecho)
                    If Not Double.IsNaN(solucion) Then
                        ' Agregar -> x= al final si no está
                        AgregarSolucionEcuacion(solucion, operacion)
                    End If
                End If
            End If

        Catch ex As Exception
            Dim config = ObtenerTema(temaActual)
            ColorearOperacion(cursorY, operacion, config.ColorIncorrecto)
        End Try
    End Sub

    Private Function ExtraerOperacion(lineaTexto As String) As String
        ' Dividir por espacios
        Dim palabras() As String = lineaTexto.Split(" "c)

        ' Buscar la palabra que contenga = y operadores matemáticos
        For Each palabra In palabras
            If palabra.Contains("="c) And ContieneOperadorMatematico(palabra) Then
                Return palabra
            End If
        Next

        Return ""
    End Function

    Private Function ContieneOperadorMatematico(texto As String) As Boolean
        Return texto.Contains("+"c) OrElse texto.Contains("-"c) OrElse
               texto.Contains("*"c) OrElse texto.Contains("/"c) OrElse
               texto.Contains("^"c) OrElse texto.Contains("x"c) OrElse texto.Contains("X"c) OrElse
               texto.Contains("("c) OrElse texto.Contains(")"c)
    End Function

    Private Function ObtenerLineaActual() As String
        Dim sb As New System.Text.StringBuilder()
        For x = 0 To COLUMNAS - 1
            sb.Append(pizarra(cursorY, x))
        Next
        Return sb.ToString()
    End Function

    Private Function EvaluarExpresion(expresion As String) As Double
        expresion = expresion.Replace(" ", "")

        ' Reemplazar PI por su valor (case insensitive)
        expresion = System.Text.RegularExpressions.Regex.Replace(expresion, "\bpi\b", Math.PI.ToString(System.Globalization.CultureInfo.InvariantCulture), System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        ' Expandir notación implícita antes de evaluar
        expresion = ExpandirNotacionImplicita(expresion)

        ' Usar evaluación avanzada que soporta funciones trigonométricas, exponentes, etc.
        Return EvaluarExpresionAvanzada(expresion)
    End Function

    Private Sub ColorearLinea(linea As Integer, color As Color)
        For x = 0 To COLUMNAS - 1
            If pizarra(linea, x) <> " "c Then
                colores(linea, x) = color
            End If
        Next
        PanelPizarra.Invalidate()
    End Sub

    Private Sub ColorearOperacion(linea As Integer, operacion As String, color As Color)
        ' Buscar la operación en la línea y colorearla
        Dim lineaTexto As String = ObtenerLineaActual()
        Dim posInicio As Integer = lineaTexto.IndexOf(operacion)

        If posInicio >= 0 Then
            For i = 0 To operacion.Length - 1
                If posInicio + i < COLUMNAS Then
                    colores(linea, posInicio + i) = color
                End If
            Next
        End If

        PanelPizarra.Invalidate()
    End Sub

    Private Sub AgregarResultadoCorrecto(resultadoCorrecto As Double, operacion As String)
        Dim textoResultado As String = $" [{resultadoCorrecto}]"
        Dim lineaTexto As String = ObtenerLineaActual()
        Dim posOperacion As Integer = lineaTexto.IndexOf(operacion)
        Dim config = ObtenerTema(temaActual)

        If posOperacion >= 0 Then
            Dim posInicio As Integer = posOperacion + operacion.Length

            For i = 0 To textoResultado.Length - 1
                If posInicio + i < COLUMNAS Then
                    pizarra(cursorY, posInicio + i) = textoResultado(i)
                    colores(cursorY, posInicio + i) = config.ColorIncorrecto
                End If
            Next
        End If

        PanelPizarra.Invalidate()
    End Sub

    Private Sub MoverCursorSiguienteLineaEdicion(saltoLineas As Integer)
        ' Si la línea actual no tiene =, agregarlo automáticamente
        If Not LineaContieneOperacion(cursorY) Then
            AgregarIgualAlFinal(cursorY)
        End If

        Dim columnaPrimerCaracter As Integer = 0

        ' Buscar el primer carácter de la línea actual
        For x = 0 To COLUMNAS - 1
            If pizarra(cursorY, x) <> " "c Then
                columnaPrimerCaracter = x
                Exit For
            End If
        Next

        ' Saltar el número de líneas especificado
        For i = 1 To saltoLineas
            If cursorY < FILAS - 1 Then
                cursorY += 1
            Else
                cursorY = 0
            End If
        Next

        cursorX = columnaPrimerCaracter
        PanelPizarra.Invalidate()
    End Sub

    Private Sub MoverCursorSiguienteLineaComprobacion()
        ' Primero validar el cálculo de la línea actual si tiene contenido
        ValidarCalculo()

        ' Buscar la siguiente línea con una operación (que contenga =)
        Dim lineaBuscada As Integer = cursorY + 1
        Dim encontrada As Boolean = False

        ' Buscar desde la línea siguiente hasta el final
        For y = lineaBuscada To FILAS - 1
            If LineaContieneOperacion(y) Then
                cursorY = y
                cursorX = ObtenerPosicionDespuesIgual(y)
                encontrada = True
                Exit For
            End If
        Next

        ' Si no encontró, buscar desde el principio hasta la línea actual
        If Not encontrada Then
            For y = 0 To cursorY - 1
                If LineaContieneOperacion(y) Then
                    cursorY = y
                    cursorX = ObtenerPosicionDespuesIgual(y)
                    encontrada = True
                    Exit For
                End If
            Next
        End If

        ' Si no se encontró ninguna línea con operación, el cursor se queda donde está
        PanelPizarra.Invalidate()
    End Sub

    Private Function LineaContieneOperacion(linea As Integer) As Boolean
        For x = 0 To COLUMNAS - 1
            If pizarra(linea, x) = "="c Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function ObtenerPosicionDespuesIgual(linea As Integer) As Integer
        For x = 0 To COLUMNAS - 1
            If pizarra(linea, x) = "="c Then
                Return Math.Min(x + 1, COLUMNAS - 1)
            End If
        Next
        Return 0
    End Function

    Private Sub AgregarIgualAlFinal(linea As Integer)
        ' Buscar la última posición con contenido
        Dim ultimaPos As Integer = -1
        For x = 0 To COLUMNAS - 1
            If pizarra(linea, x) <> " "c Then
                ultimaPos = x
            End If
        Next

        ' Si hay contenido y el último carácter es un número, agregar =
        If ultimaPos >= 0 And ultimaPos < COLUMNAS - 1 Then
            If Char.IsDigit(pizarra(linea, ultimaPos)) Then
                pizarra(linea, ultimaPos + 1) = "="c
                colores(linea, ultimaPos + 1) = Color.White
            End If
        End If
    End Sub

    Private Sub LimpiarLinea()
        For x = 0 To COLUMNAS - 1
            pizarra(cursorY, x) = " "c
        Next
        cursorX = 0
        ActualizarBarraEstado()
        PanelPizarra.Invalidate()
    End Sub

    Private Sub ActualizarBarraEstado()
        LabelPosicion.Text = $"Línea: {cursorY + 1}, Columna: {cursorX + 1}"

        Dim estadoTexto As String = ""

        If MenuEditar.Checked Then
            estadoTexto = "EDICIÓN"
        End If

        If Not modoInsertar Then
            If estadoTexto <> "" Then
                estadoTexto &= " | "
            End If
            estadoTexto &= "SOBREESCRIBIR"
        End If

        If usarRadianes Then
            If estadoTexto <> "" Then
                estadoTexto &= " | "
            End If
            estadoTexto &= "RAD"
        End If

        LabelModo.Text = estadoTexto
        LabelModo.ForeColor = Color.Blue
    End Sub

    Private Sub MenuEditar_CheckedChanged(sender As Object, e As EventArgs) Handles MenuEditar.CheckedChanged
        ActualizarBarraEstado()
    End Sub

    Private Sub MenuRadianes_CheckedChanged(sender As Object, e As EventArgs) Handles MenuRadianes.CheckedChanged
        usarRadianes = MenuRadianes.Checked
        ActualizarBarraEstado()
    End Sub

    Private Sub MenuVerEstadisticas_Click(sender As Object, e As EventArgs) Handles MenuVerEstadisticas.Click
        Dim porcentaje As Double = 0
        If ejerciciosTotales > 0 Then
            porcentaje = (ejerciciosCorrectos / ejerciciosTotales) * 100
        End If

        MessageBox.Show($"Estadísticas de la sesión:{vbCrLf}{vbCrLf}" &
                       $"Ejercicios totales: {ejerciciosTotales}{vbCrLf}" &
                       $"Correctos: {ejerciciosCorrectos}{vbCrLf}" &
                       $"Incorrectos: {ejerciciosIncorrectos}{vbCrLf}" &
                       $"Porcentaje de aciertos: {porcentaje:F1}%",
                       "Estadísticas", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MenuVerHistorial_Click(sender As Object, e As EventArgs) Handles MenuVerHistorial.Click
        If historialEjercicios.Count = 0 Then
            MessageBox.Show("No hay ejercicios en el historial aún.", "Historial", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine("HISTORIAL DE EJERCICIOS")
        sb.AppendLine("========================")
        sb.AppendLine()

        For i = historialEjercicios.Count - 1 To Math.Max(0, historialEjercicios.Count - 20) Step -1
            Dim ej = historialEjercicios(i)
            Dim estado As String = If(ej.EsCorrecto, "✓", "✗")
            sb.AppendLine($"{estado} {ej.Ejercicio} = {ej.RespuestaUsuario} [{ej.RespuestaCorrecta}] - {ej.Fecha:HH:mm:ss}")
        Next

        If historialEjercicios.Count > 20 Then
            sb.AppendLine()
            sb.AppendLine($"(Mostrando los últimos 20 de {historialEjercicios.Count} ejercicios)")
        End If

        MessageBox.Show(sb.ToString(), "Historial", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MenuReiniciarEstadisticas_Click(sender As Object, e As EventArgs) Handles MenuReiniciarEstadisticas.Click
        Dim resultado = MessageBox.Show("¿Desea reiniciar todas las estadísticas y borrar el historial?",
                                       "Confirmar reinicio",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question)

        If resultado = DialogResult.Yes Then
            historialEjercicios.Clear()
            ejerciciosCorrectos = 0
            ejerciciosIncorrectos = 0
            ejerciciosTotales = 0
            MessageBox.Show("Estadísticas reiniciadas correctamente.", "Reinicio completado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub MenuTemaClasico_Click(sender As Object, e As EventArgs) Handles MenuTemaClasico.Click
        CambiarTema(TemaColor.Clasico)
    End Sub

    Private Sub MenuTemaOscuroAzul_Click(sender As Object, e As EventArgs) Handles MenuTemaOscuroAzul.Click
        CambiarTema(TemaColor.OscuroAzul)
    End Sub

    Private Sub MenuTemaMatriz_Click(sender As Object, e As EventArgs) Handles MenuTemaMatriz.Click
        CambiarTema(TemaColor.Matriz)
    End Sub

    Private Sub MenuTemaRetro_Click(sender As Object, e As EventArgs) Handles MenuTemaRetro.Click
        CambiarTema(TemaColor.Retro)
    End Sub

    Private Sub MenuGraficar_Click(sender As Object, e As EventArgs) Handles MenuGraficar.Click
        ' Solicitar al usuario la función a graficar
        Dim resultado As String = InputBox("Introduce la función a graficar en términos de x:" & vbCrLf & vbCrLf &
                                          "Ejemplos:" & vbCrLf &
                                          "  • x^2" & vbCrLf &
                                          "  • sin(x)" & vbCrLf &
                                          "  • 2*x+3" & vbCrLf &
                                          "  • x^2+2*x-3" & vbCrLf &
                                          "  • cos(x)*sin(x)" & vbCrLf &
                                          "  • exp(x)" & vbCrLf &
                                          "  • log(x)" & vbCrLf &
                                          "  • abs(x)" & vbCrLf &
                                          "  • sqrt(x)",
                                          "Graficar Función", "x^2")

        If Not String.IsNullOrWhiteSpace(resultado) Then
            Try
                ' Crear y mostrar el formulario de gráfico
                Dim formGraf As New FormGrafico(resultado, usarRadianes)
                formGraf.ShowDialog()
            Catch ex As Exception
                MessageBox.Show("Error al graficar la función: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub CambiarTema(tema As TemaColor)
        temaActual = tema

        MenuTemaClasico.Checked = (tema = TemaColor.Clasico)
        MenuTemaOscuroAzul.Checked = (tema = TemaColor.OscuroAzul)
        MenuTemaMatriz.Checked = (tema = TemaColor.Matriz)
        MenuTemaRetro.Checked = (tema = TemaColor.Retro)

        Dim config = ObtenerTema(tema)
        PanelPizarra.BackColor = config.ColorFondo

        For y = 0 To FILAS - 1
            For x = 0 To COLUMNAS - 1
                If colores(y, x) = Color.White OrElse colores(y, x) = Color.FromArgb(200, 220, 255) OrElse
                   colores(y, x) = Color.FromArgb(0, 255, 0) OrElse colores(y, x) = Color.FromArgb(255, 255, 180) Then
                    colores(y, x) = config.ColorTexto
                End If
            Next
        Next

        PanelPizarra.Invalidate()
    End Sub

    Private Sub IniciarOActualizarSeleccion()
        If Not haySeleccion Then
            haySeleccion = True
            selStartX = cursorX
            selStartY = cursorY
        End If
    End Sub

    Private Sub ActualizarFinSeleccion()
        selEndX = cursorX
        selEndY = cursorY
    End Sub

    Private Function EstaDentroSeleccion(x As Integer, y As Integer) As Boolean
        If Not haySeleccion Then Return False

        Dim minX As Integer = Math.Min(selStartX, selEndX)
        Dim maxX As Integer = Math.Max(selStartX, selEndX)
        Dim minY As Integer = Math.Min(selStartY, selEndY)
        Dim maxY As Integer = Math.Max(selStartY, selEndY)

        If y < minY Or y > maxY Then Return False
        If y = minY And y = maxY Then
            Return x >= minX And x <= maxX
        ElseIf y = minY Then
            Return x >= minX
        ElseIf y = maxY Then
            Return x <= maxX
        Else
            Return True
        End If
    End Function

    Private Function ObtenerTextoSeleccion() As String
        If Not haySeleccion Then Return ""

        Dim sb As New System.Text.StringBuilder()
        Dim minX As Integer = Math.Min(selStartX, selEndX)
        Dim maxX As Integer = Math.Max(selStartX, selEndX)
        Dim minY As Integer = Math.Min(selStartY, selEndY)
        Dim maxY As Integer = Math.Max(selStartY, selEndY)

        For y = minY To maxY
            If y = minY And y = maxY Then
                For x = minX To maxX
                    sb.Append(pizarra(y, x))
                Next
            ElseIf y = minY Then
                For x = minX To COLUMNAS - 1
                    sb.Append(pizarra(y, x))
                Next
                sb.AppendLine()
            ElseIf y = maxY Then
                For x = 0 To maxX
                    sb.Append(pizarra(y, x))
                Next
            Else
                For x = 0 To COLUMNAS - 1
                    sb.Append(pizarra(y, x))
                Next
                sb.AppendLine()
            End If
        Next

        Return sb.ToString()
    End Function

    Private Sub EliminarSeleccion()
        If Not haySeleccion Then Return

        Dim minX As Integer = Math.Min(selStartX, selEndX)
        Dim maxX As Integer = Math.Max(selStartX, selEndX)
        Dim minY As Integer = Math.Min(selStartY, selEndY)
        Dim maxY As Integer = Math.Max(selStartY, selEndY)

        For y = minY To maxY
            If y = minY And y = maxY Then
                For x = minX To maxX
                    pizarra(y, x) = " "c
                    colores(y, x) = Color.White
                Next
            ElseIf y = minY Then
                For x = minX To COLUMNAS - 1
                    pizarra(y, x) = " "c
                    colores(y, x) = Color.White
                Next
            ElseIf y = maxY Then
                For x = 0 To maxX
                    pizarra(y, x) = " "c
                    colores(y, x) = Color.White
                Next
            Else
                For x = 0 To COLUMNAS - 1
                    pizarra(y, x) = " "c
                    colores(y, x) = Color.White
                Next
            End If
        Next

        cursorX = minX
        cursorY = minY
        haySeleccion = False
    End Sub

    Private Sub Copiar()
        If haySeleccion Then
            portapapeles = ObtenerTextoSeleccion()
        End If
    End Sub

    Private Sub Cortar()
        If haySeleccion Then
            portapapeles = ObtenerTextoSeleccion()
            EliminarSeleccion()
        End If
    End Sub

    Private Sub Pegar()
        If String.IsNullOrEmpty(portapapeles) Then Return

        If haySeleccion Then
            EliminarSeleccion()
        End If

        Dim lineas() As String = portapapeles.Split(New String() {vbCrLf, vbLf}, StringSplitOptions.None)
        Dim lineaActual As Integer = cursorY
        Dim columnaActual As Integer = cursorX

        For Each linea In lineas
            For i = 0 To linea.Length - 1
                If columnaActual < COLUMNAS And lineaActual < FILAS Then
                    pizarra(lineaActual, columnaActual) = linea(i)
                    colores(lineaActual, columnaActual) = Color.White
                    columnaActual += 1
                End If
            Next

            If lineaActual < FILAS - 1 And lineas.Length > 1 Then
                lineaActual += 1
                columnaActual = 0
            End If
        Next

        cursorX = columnaActual
        cursorY = lineaActual
    End Sub

    Private Function ObtenerUltimoCaracterLinea(linea As Integer) As Integer
        For x = COLUMNAS - 1 To 0 Step -1
            If pizarra(linea, x) <> " "c Then
                Return x + 1
            End If
        Next
        Return COLUMNAS - 1
    End Function

    Private Sub EmpujarTextoADerecha(linea As Integer, columnaInicio As Integer)
        ' Empujar todos los caracteres una posición a la derecha desde el final
        For x = COLUMNAS - 2 To columnaInicio Step -1
            pizarra(linea, x + 1) = pizarra(linea, x)
            colores(linea, x + 1) = colores(linea, x)
        Next
    End Sub

    Private Function ObtenerPrimerCaracterLinea(linea As Integer) As Integer
        For x = 0 To COLUMNAS - 1
            If pizarra(linea, x) <> " "c Then
                Return x
            End If
        Next
        Return 0
    End Function

    Private Sub TraerTextoAIzquierda(linea As Integer, columnaInicio As Integer)
        ' Traer todos los caracteres una posición a la izquierda
        For x = columnaInicio To COLUMNAS - 2
            pizarra(linea, x) = pizarra(linea, x + 1)
            colores(linea, x) = colores(linea, x + 1)
        Next
        ' Limpiar la última posición
        pizarra(linea, COLUMNAS - 1) = " "c
        colores(linea, COLUMNAS - 1) = Color.White
    End Sub

    Private Sub AvanzarTab()
        Const TAB_SIZE As Integer = 4
        ' Calcular el siguiente múltiplo de TAB_SIZE
        Dim siguienteTab As Integer = ((cursorX \ TAB_SIZE) + 1) * TAB_SIZE

        If siguienteTab < COLUMNAS Then
            If modoInsertar Then
                ' En modo insertar, empujar el texto
                Dim espaciosInsertar As Integer = siguienteTab - cursorX
                For i = 1 To espaciosInsertar
                    EmpujarTextoADerecha(cursorY, cursorX)
                    pizarra(cursorY, cursorX) = " "c
                    colores(cursorY, cursorX) = Color.White
                    If cursorX < COLUMNAS - 1 Then cursorX += 1
                Next
            Else
                ' En modo sobreescribir, solo mover el cursor
                cursorX = siguienteTab
            End If
        Else
            cursorX = COLUMNAS - 1
        End If
        haySeleccion = False
    End Sub

    Private Sub RetrocederTab()
        Const TAB_SIZE As Integer = 4
        ' Calcular el anterior múltiplo de TAB_SIZE
        Dim anteriorTab As Integer

        If cursorX Mod TAB_SIZE = 0 And cursorX > 0 Then
            ' Si ya estamos en un tab stop, retroceder al anterior
            anteriorTab = cursorX - TAB_SIZE
        Else
            ' Si no, ir al tab stop anterior
            anteriorTab = (cursorX \ TAB_SIZE) * TAB_SIZE
        End If

        If anteriorTab < 0 Then anteriorTab = 0
        cursorX = anteriorTab
        haySeleccion = False
    End Sub

    Private Sub MenuAbrir_Click(sender As Object, e As EventArgs) Handles MenuAbrir.Click
        Dim dialogo As New OpenFileDialog()
        dialogo.Filter = "Archivos de Pizarra (*.piz)|*.piz|Todos los archivos (*.*)|*.*"
        dialogo.Title = "Abrir Pizarra"

        If dialogo.ShowDialog() = DialogResult.OK Then
            AbrirArchivo(dialogo.FileName)
        End If
    End Sub

    Private Sub MenuGuardar_Click(sender As Object, e As EventArgs) Handles MenuGuardar.Click
        If String.IsNullOrEmpty(archivoActual) Then
            MenuGuardarComo_Click(sender, e)
        Else
            GuardarArchivo(archivoActual)
        End If
    End Sub

    Private Sub MenuGuardarComo_Click(sender As Object, e As EventArgs) Handles MenuGuardarComo.Click
        Dim dialogo As New SaveFileDialog()
        dialogo.Filter = "Archivos de Pizarra (*.piz)|*.piz|Todos los archivos (*.*)|*.*"
        dialogo.Title = "Guardar Pizarra Como"
        dialogo.DefaultExt = "piz"

        If dialogo.ShowDialog() = DialogResult.OK Then
            GuardarArchivo(dialogo.FileName)
        End If
    End Sub

    Private Sub GuardarArchivo(rutaArchivo As String)
        Try
            Using escritor As New System.IO.StreamWriter(rutaArchivo, False, System.Text.Encoding.UTF8)
                ' Guardar cada línea de la pizarra
                For y = 0 To FILAS - 1
                    Dim lineaTexto As New System.Text.StringBuilder()
                    Dim lineaColores As New System.Text.StringBuilder()

                    For x = 0 To COLUMNAS - 1
                        lineaTexto.Append(pizarra(y, x))
                        ' Guardar color como código: W=White, G=Green(Lime), R=Red
                        If colores(y, x) = Color.Lime Then
                            lineaColores.Append("G")
                        ElseIf colores(y, x) = Color.Red Then
                            lineaColores.Append("R")
                        Else
                            lineaColores.Append("W")
                        End If
                    Next

                    ' Escribir línea de texto
                    escritor.WriteLine(lineaTexto.ToString())
                    ' Escribir línea de colores
                    escritor.WriteLine(lineaColores.ToString())
                Next
            End Using

            archivoActual = rutaArchivo
            pizarraModificada = False
            Me.Text = $"Pizarra Electrónica - {System.IO.Path.GetFileName(rutaArchivo)}"
            MessageBox.Show("Pizarra guardada correctamente.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"Error al guardar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AbrirArchivo(rutaArchivo As String)
        Try
            Using lector As New System.IO.StreamReader(rutaArchivo, System.Text.Encoding.UTF8)
                For y = 0 To FILAS - 1
                    ' Leer línea de texto
                    Dim lineaTexto As String = lector.ReadLine()
                    If lineaTexto Is Nothing Then Exit For

                    ' Leer línea de colores
                    Dim lineaColores As String = lector.ReadLine()
                    If lineaColores Is Nothing Then Exit For

                    For x = 0 To Math.Min(lineaTexto.Length - 1, COLUMNAS - 1)
                        pizarra(y, x) = lineaTexto(x)

                        ' Restaurar color
                        If x < lineaColores.Length Then
                            Select Case lineaColores(x)
                                Case "G"c
                                    colores(y, x) = Color.Lime
                                Case "R"c
                                    colores(y, x) = Color.Red
                                Case Else
                                    colores(y, x) = Color.White
                            End Select
                        Else
                            colores(y, x) = Color.White
                        End If
                    Next

                    ' Rellenar el resto de la línea con espacios si es necesario
                    For x = lineaTexto.Length To COLUMNAS - 1
                        pizarra(y, x) = " "c
                        colores(y, x) = Color.White
                    Next
                Next
            End Using

            archivoActual = rutaArchivo
            pizarraModificada = False
            Me.Text = $"Pizarra Electrónica - {System.IO.Path.GetFileName(rutaArchivo)}"
            cursorX = 0
            cursorY = 0
            PanelPizarra.Invalidate()
            MessageBox.Show("Pizarra cargada correctamente.", "Abrir", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"Error al abrir el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuSalir_Click(sender As Object, e As EventArgs) Handles MenuSalir.Click
        Me.Close()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If pizarraModificada Then
            Dim resultado As DialogResult = MessageBox.Show(
                "¿Desea guardar los cambios en la pizarra antes de salir?",
                "Guardar cambios",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question)

            Select Case resultado
                Case DialogResult.Yes
                    MenuGuardar_Click(sender, New EventArgs())
                    ' Si el usuario cancela el diálogo de guardar, cancelar el cierre
                    If pizarraModificada Then
                        e.Cancel = True
                    End If
                Case DialogResult.Cancel
                    e.Cancel = True
            End Select
        End If
    End Sub

    Private Function LineaEstaVacia(linea As Integer) As Boolean
        For x = 0 To COLUMNAS - 1
            If pizarra(linea, x) <> " "c Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Function ResolverEcuacionConValor(ecuacion As String, valorDerecho As Double) As Double
        ' Resolver ecuación del tipo: 2x+2=12
        ' ecuacion = "2x+2", valorDerecho = 12
        Try
            ' Expandir notación implícita: 2x -> 2*x, 2(x+1) -> 2*(x+1), etc.
            ecuacion = ExpandirNotacionImplicita(ecuacion)

            ' Método de Newton-Raphson para resolver f(x) = valorDerecho
            ' Reescribimos como f(x) - valorDerecho = 0
            Dim tolerancia As Double = 0.001
            Dim maxIteraciones As Integer = 100
            Dim x As Double = 1.0 ' Valor inicial

            For i As Integer = 0 To maxIteraciones
                Dim fx As Double = EvaluarExpresionConX(ecuacion, x) - valorDerecho

                If Math.Abs(fx) < tolerancia Then
                    Return Math.Round(x, 3)
                End If

                ' Calcular derivada numérica
                Dim h As Double = 0.0001
                Dim fxh As Double = EvaluarExpresionConX(ecuacion, x + h) - valorDerecho
                Dim derivada As Double = (fxh - fx) / h

                If Math.Abs(derivada) < 0.00001 Then
                    ' Probar con otro valor inicial
                    x = 10.0
                    Continue For
                End If

                x = x - fx / derivada
            Next

            ' Verificar la solución
            Dim verificacion As Double = EvaluarExpresionConX(ecuacion, x)
            If Math.Abs(verificacion - valorDerecho) < 0.1 Then
                Return Math.Round(x, 3)
            End If

        Catch ex As Exception
        End Try

        Return Double.NaN
    End Function

    Private Function ResolverEcuacion(ecuacionCompleta As String) As Double
        ' Dividir ecuación por =
        Dim partes() As String = ecuacionCompleta.Split("="c)
        If partes.Length < 2 Then Return Double.NaN

        Dim ladoIzquierdo As String = partes(0).Trim()
        Dim ladoDerecho As String = partes(1).Trim()

        ' Si el lado derecho empieza con x=, extraer solo el valor numérico
        If ladoDerecho.StartsWith("x=", StringComparison.OrdinalIgnoreCase) Then
            ladoDerecho = ladoDerecho.Substring(2).Trim()
        End If

        Dim valorDerecho As Double
        If Double.TryParse(ladoDerecho, valorDerecho) Then
            Return ResolverEcuacionConValor(ladoIzquierdo, valorDerecho)
        End If

        Return Double.NaN
    End Function

    Private Function ExpandirNotacionImplicita(expresion As String) As String
        Dim resultado As String = expresion

        ' Expandir funciones trigonométricas y matemáticas comunes
        ' sin(x), cos(x), tan(x), sqrt(x), etc.
        ' La notación 2sin(x) debe convertirse en 2*sin(x)

        ' Patrón: número seguido de letra o paréntesis -> insertar *
        ' 2x -> 2*x
        ' 2(x+1) -> 2*(x+1)
        ' 2sin(x) -> 2*sin(x)

        Dim nuevoResultado As New System.Text.StringBuilder()
        For i As Integer = 0 To resultado.Length - 1
            nuevoResultado.Append(resultado(i))

            If i < resultado.Length - 1 Then
                Dim actual As Char = resultado(i)
                Dim siguiente As Char = resultado(i + 1)

                ' Si el carácter actual es un dígito y el siguiente es una letra, x, o (
                If Char.IsDigit(actual) AndAlso (Char.IsLetter(siguiente) OrElse siguiente = "("c OrElse siguiente = "x"c OrElse siguiente = "X"c) Then
                    nuevoResultado.Append("*")
                End If

                ' Si el carácter actual es ) y el siguiente es un dígito, letra o (
                If actual = ")"c AndAlso (Char.IsDigit(siguiente) OrElse Char.IsLetter(siguiente) OrElse siguiente = "("c OrElse siguiente = "x"c OrElse siguiente = "X"c) Then
                    nuevoResultado.Append("*")
                End If
            End If
        Next

        Return nuevoResultado.ToString()
    End Function

    Private Function EvaluarExpresionConX(expresion As String, valorX As Double) As Double
        ' Reemplazar x por el valor numérico
        Dim expresionConValor As String = expresion.Replace("x", valorX.ToString(System.Globalization.CultureInfo.InvariantCulture))
        expresionConValor = expresionConValor.Replace("X", valorX.ToString(System.Globalization.CultureInfo.InvariantCulture))

        ' Evaluar usando NCalc o similar
        Return EvaluarExpresionAvanzada(expresionConValor)
    End Function

    Private Function EvaluarExpresionAvanzada(expresion As String) As Double
        Try
            ' Soporte para funciones trigonométricas y exponentes
            ' Usamos DataTable.Compute con algunas sustituciones

            ' Primero intentar con DataTable para expresiones simples
            If Not expresion.Contains("sin") AndAlso Not expresion.Contains("cos") AndAlso
               Not expresion.Contains("tan") AndAlso Not expresion.Contains("sqrt") AndAlso
               Not expresion.Contains("^") Then
                Dim tabla As New DataTable()
                Return Convert.ToDouble(tabla.Compute(expresion, Nothing))
            End If

            ' Para expresiones más complejas, usar evaluación manual
            Return EvaluarExpresionMatematica(expresion)

        Catch ex As Exception
            Return Double.NaN
        End Try
    End Function

    Private Function EvaluarExpresionMatematica(expresion As String) As Double
        Try
            ' Procesar ^ (exponente) -> Math.Pow
            While expresion.Contains("^")
                Dim pos As Integer = expresion.IndexOf("^")
                Dim baseNum As Double = ExtraerNumeroAntes(expresion, pos)
                Dim exponente As Double = ExtraerNumeroDespues(expresion, pos + 1)
                Dim resultado As Double = Math.Pow(baseNum, exponente)

                ' Reemplazar en la expresión
                Dim inicio As Integer = EncontrarInicioNumero(expresion, pos - 1)
                Dim fin As Integer = EncontrarFinNumero(expresion, pos + 1)
                expresion = expresion.Substring(0, inicio) & resultado.ToString(System.Globalization.CultureInfo.InvariantCulture) & expresion.Substring(fin + 1)
            End While

            ' Procesar funciones trigonométricas
            expresion = ProcesarFuncionesTrigonometricas(expresion)

            ' Evaluar la expresión resultante
            Dim tabla As New DataTable()
            Return Convert.ToDouble(tabla.Compute(expresion, Nothing))

        Catch ex As Exception
            Return Double.NaN
        End Try
    End Function

    Private Function ProcesarFuncionesTrigonometricas(expresion As String) As String
        Dim resultado As String = expresion

        ' Procesar log(x) - logaritmo natural (ln)
        While resultado.Contains("log(")
            Dim pos As Integer = resultado.IndexOf("log(")
            Dim argumento As String = ExtraerArgumentoFuncion(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionAvanzada(argumento)
            Dim valorLog As Double = Math.Log10(valorArg)
            resultado = resultado.Replace("log(" & argumento & ")", valorLog.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        ' Procesar ln(x) - logaritmo natural
        While resultado.Contains("ln(")
            Dim pos As Integer = resultado.IndexOf("ln(")
            Dim argumento As String = ExtraerArgumentoFuncion(resultado, pos + 3)
            Dim valorArg As Double = EvaluarExpresionAvanzada(argumento)
            Dim valorLn As Double = Math.Log(valorArg)
            resultado = resultado.Replace("ln(" & argumento & ")", valorLn.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        ' Procesar exp(x) - exponencial e^x
        While resultado.Contains("exp(")
            Dim pos As Integer = resultado.IndexOf("exp(")
            Dim argumento As String = ExtraerArgumentoFuncion(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionAvanzada(argumento)
            Dim valorExp As Double = Math.Exp(valorArg)
            resultado = resultado.Replace("exp(" & argumento & ")", valorExp.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        ' Procesar abs(x) - valor absoluto
        While resultado.Contains("abs(")
            Dim pos As Integer = resultado.IndexOf("abs(")
            Dim argumento As String = ExtraerArgumentoFuncion(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionAvanzada(argumento)
            Dim valorAbs As Double = Math.Abs(valorArg)
            resultado = resultado.Replace("abs(" & argumento & ")", valorAbs.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        ' Procesar sin(x)
        While resultado.Contains("sin(")
            Dim pos As Integer = resultado.IndexOf("sin(")
            Dim argumento As String = ExtraerArgumentoFuncion(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionAvanzada(argumento)
            Dim valorSin As Double
            If usarRadianes Then
                valorSin = Math.Sin(valorArg)
            Else
                valorSin = Math.Sin(valorArg * Math.PI / 180)
            End If
            resultado = resultado.Replace("sin(" & argumento & ")", valorSin.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        ' Procesar cos(x)
        While resultado.Contains("cos(")
            Dim pos As Integer = resultado.IndexOf("cos(")
            Dim argumento As String = ExtraerArgumentoFuncion(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionAvanzada(argumento)
            Dim valorCos As Double
            If usarRadianes Then
                valorCos = Math.Cos(valorArg)
            Else
                valorCos = Math.Cos(valorArg * Math.PI / 180)
            End If
            resultado = resultado.Replace("cos(" & argumento & ")", valorCos.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        ' Procesar tan(x)
        While resultado.Contains("tan(")
            Dim pos As Integer = resultado.IndexOf("tan(")
            Dim argumento As String = ExtraerArgumentoFuncion(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionAvanzada(argumento)
            Dim valorTan As Double
            If usarRadianes Then
                valorTan = Math.Tan(valorArg)
            Else
                valorTan = Math.Tan(valorArg * Math.PI / 180)
            End If
            resultado = resultado.Replace("tan(" & argumento & ")", valorTan.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        ' Procesar sqrt(x)
        While resultado.Contains("sqrt(")
            Dim pos As Integer = resultado.IndexOf("sqrt(")
            Dim argumento As String = ExtraerArgumentoFuncion(resultado, pos + 5)
            Dim valorArg As Double = EvaluarExpresionAvanzada(argumento)
            Dim valorSqrt As Double = Math.Sqrt(valorArg)
            resultado = resultado.Replace("sqrt(" & argumento & ")", valorSqrt.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        Return resultado
    End Function

    Private Function ExtraerArgumentoFuncion(expresion As String, posInicio As Integer) As String
        Dim contadorParentesis As Integer = 1
        Dim i As Integer = posInicio

        While i < expresion.Length AndAlso contadorParentesis > 0
            If expresion(i) = "("c Then
                contadorParentesis += 1
            ElseIf expresion(i) = ")"c Then
                contadorParentesis -= 1
            End If
            i += 1
        End While

        Return expresion.Substring(posInicio, i - posInicio - 1)
    End Function

    Private Function ExtraerNumeroAntes(expresion As String, pos As Integer) As Double
        Dim inicio As Integer = EncontrarInicioNumero(expresion, pos - 1)
        Dim numeroStr As String = expresion.Substring(inicio, pos - inicio)
        Return Convert.ToDouble(numeroStr, System.Globalization.CultureInfo.InvariantCulture)
    End Function

    Private Function ExtraerNumeroDespues(expresion As String, pos As Integer) As Double
        Dim fin As Integer = EncontrarFinNumero(expresion, pos)
        Dim numeroStr As String = expresion.Substring(pos, fin - pos + 1)
        Return Convert.ToDouble(numeroStr, System.Globalization.CultureInfo.InvariantCulture)
    End Function

    Private Function EncontrarInicioNumero(expresion As String, pos As Integer) As Integer
        While pos > 0 AndAlso (Char.IsDigit(expresion(pos)) OrElse expresion(pos) = "."c OrElse expresion(pos) = "-"c)
            pos -= 1
        End While
        Return pos + 1
    End Function

    Private Function EncontrarFinNumero(expresion As String, pos As Integer) As Integer
        While pos < expresion.Length - 1 AndAlso (Char.IsDigit(expresion(pos)) OrElse expresion(pos) = "."c)
            pos += 1
        End While
        If pos < expresion.Length - 1 AndAlso Not (Char.IsDigit(expresion(pos)) OrElse expresion(pos) = "."c) Then
            pos -= 1
        End If
        Return pos
    End Function

    Private Sub AgregarSolucionEcuacion(solucion As Double, operacion As String)
        Dim textoSolucion As String = $" -> x={solucion}"
        Dim lineaTexto As String = ObtenerLineaActual()
        Dim posOperacion As Integer = lineaTexto.IndexOf(operacion)

        If posOperacion >= 0 Then
            Dim posInicio As Integer = posOperacion + operacion.Length

            For i = 0 To textoSolucion.Length - 1
                If posInicio + i < COLUMNAS Then
                    pizarra(cursorY, posInicio + i) = textoSolucion(i)
                    colores(cursorY, posInicio + i) = Color.Lime
                End If
            Next
        End If

        PanelPizarra.Invalidate()
    End Sub

    Private Sub AgregarResultadoEcuacion(solucion As Double, operacion As String)
        Dim textoResultado As String = $" [x={solucion}]"
        Dim lineaTexto As String = ObtenerLineaActual()
        Dim posOperacion As Integer = lineaTexto.IndexOf(operacion)
        Dim config = ObtenerTema(temaActual)

        If posOperacion >= 0 Then
            Dim posInicio As Integer = posOperacion + operacion.Length

            For i = 0 To textoResultado.Length - 1
                If posInicio + i < COLUMNAS Then
                    pizarra(cursorY, posInicio + i) = textoResultado(i)
                    colores(cursorY, posInicio + i) = config.ColorIncorrecto
                End If
            Next
        End If

        PanelPizarra.Invalidate()
    End Sub

    ' Función para detectar y marcar campos de resultado (RRRR)
    Private Sub DetectarCamposResultado()
        ' Buscar patrones RRRR en toda la pizarra
        For y = 0 To FILAS - 1
            Dim lineaTexto As String = ""
            For x = 0 To COLUMNAS - 1
                lineaTexto &= pizarra(y, x)
            Next

            ' Buscar secuencias de R después del signo =
            Dim posIgual As Integer = lineaTexto.IndexOf("="c)
            If posIgual >= 0 AndAlso posIgual < COLUMNAS - 1 Then
                ' Buscar secuencias de R después del =
                Dim inicioR As Integer = -1
                For x = posIgual + 1 To COLUMNAS - 1
                    If pizarra(y, x) = "R"c OrElse pizarra(y, x) = "r"c Then
                        If inicioR = -1 Then
                            inicioR = x
                        End If
                    Else
                        ' Si encontramos una secuencia de R, marcarla como campo de resultado
                        If inicioR >= 0 Then
                            For rx = inicioR To x - 1
                                esCampoResultado(y, rx) = True
                                pizarra(y, rx) = " "c ' Limpiar las R para dejar el campo vacío
                            Next
                            inicioR = -1
                        End If
                        ' Salir del bucle si encontramos algo que no es espacio después de la secuencia
                        If pizarra(y, x) <> " "c Then
                            Exit For
                        End If
                    End If
                Next
                ' Procesar si terminó la línea con R
                If inicioR >= 0 Then
                    For rx = inicioR To COLUMNAS - 1
                        If pizarra(y, rx) = "R"c OrElse pizarra(y, rx) = "r"c Then
                            esCampoResultado(y, rx) = True
                            pizarra(y, rx) = " "c
                        Else
                            Exit For
                        End If
                    Next
                End If
            End If
        Next
        PanelPizarra.Invalidate()
    End Sub

    ' Función para restaurar las R cuando se vuelve a modo EDICIÓN
    Private Sub RestaurarCamposResultado()
        ' Primero, limpiar mensajes de error y soluciones
        For y = 0 To FILAS - 1
            Dim lineaTexto As String = ""
            For x = 0 To COLUMNAS - 1
                lineaTexto &= pizarra(y, x)
            Next

            ' Buscar y eliminar patrones [texto] que son mensajes de error
            Dim posInicio As Integer = lineaTexto.IndexOf("["c)
            While posInicio >= 0
                Dim posFin As Integer = lineaTexto.IndexOf("]"c, posInicio)
                If posFin > posInicio Then
                    ' Limpiar todo desde [ hasta ] inclusive
                    For x = posInicio To posFin
                        If x < COLUMNAS Then
                            pizarra(y, x) = " "c
                            Dim config = ObtenerTema(temaActual)
                            colores(y, x) = config.ColorTexto
                        End If
                    Next
                    ' Actualizar lineaTexto para buscar más corchetes
                    lineaTexto = ""
                    For x = 0 To COLUMNAS - 1
                        lineaTexto &= pizarra(y, x)
                    Next
                    posInicio = lineaTexto.IndexOf("["c)
                Else
                    Exit While
                End If
            End While

            ' Buscar y eliminar patrones " -> x=" que son soluciones de ecuaciones
            Dim posFlecha As Integer = lineaTexto.IndexOf(" -> ")
            If posFlecha >= 0 Then
                ' Limpiar desde " -> " hasta el final de la expresión
                For x = posFlecha To COLUMNAS - 1
                    If pizarra(y, x) <> " "c Then
                        pizarra(y, x) = " "c
                        Dim config = ObtenerTema(temaActual)
                        colores(y, x) = config.ColorTexto
                    Else
                        Exit For
                    End If
                Next
            End If
        Next

        ' Restaurar las R en los campos de resultado
        For y = 0 To FILAS - 1
            For x = 0 To COLUMNAS - 1
                If esCampoResultado(y, x) Then
                    pizarra(y, x) = "R"c
                End If
            Next
        Next

        ' Limpiar los marcadores
        For y = 0 To FILAS - 1
            For x = 0 To COLUMNAS - 1
                esCampoResultado(y, x) = False
            Next
        Next
        PanelPizarra.Invalidate()
    End Sub

    ' Función para verificar si una posición es editable en modo NO EDICIÓN
    Private Function EsPosicionEditable(x As Integer, y As Integer) As Boolean
        If MenuEditar.Checked Then
            ' En modo EDICIÓN, todo es editable
            Return True
        Else
            ' En modo NO EDICIÓN, solo los campos de resultado son editables
            Return esCampoResultado(y, x)
        End If
    End Function

    ' Función para mover el cursor al siguiente campo de resultado
    Private Sub MoverCursorSiguienteCampo()
        ' Buscar el siguiente campo de resultado
        Dim encontrado As Boolean = False
        Dim yInicio As Integer = cursorY
        Dim xInicio As Integer = cursorX + 1

        ' Buscar desde la posición actual hacia adelante
        For y = yInicio To FILAS - 1
            Dim xStart As Integer = If(y = yInicio, xInicio, 0)
            For x = xStart To COLUMNAS - 1
                If esCampoResultado(y, x) Then
                    cursorX = x
                    cursorY = y
                    encontrado = True
                    Exit For
                End If
            Next
            If encontrado Then Exit For
        Next

        ' Si no se encontró, buscar desde el inicio
        If Not encontrado Then
            For y = 0 To yInicio
                Dim xEnd As Integer = If(y = yInicio, xInicio - 1, COLUMNAS - 1)
                For x = 0 To xEnd
                    If esCampoResultado(y, x) Then
                        cursorX = x
                        cursorY = y
                        encontrado = True
                        Exit For
                    End If
                Next
                If encontrado Then Exit For
            Next
        End If
    End Sub

    ' Función para mover el cursor al primer campo de resultado (arriba-izquierda)
    Private Sub MoverCursorPrimerCampo()
        ' Buscar el primer campo de resultado desde arriba-izquierda
        For y = 0 To FILAS - 1
            For x = 0 To COLUMNAS - 1
                If esCampoResultado(y, x) Then
                    cursorX = x
                    cursorY = y
                    PanelPizarra.Invalidate()
                    Return
                End If
            Next
        Next
    End Sub
End Class
