Public Class FormGrafico
    Private ecuacion As String
    Private minX As Double = -10
    Private maxX As Double = 10
    Private minY As Double = -10
    Private maxY As Double = 10
    Private usarRadianes As Boolean = False
    Private puntosFuncion As New List(Of PointF)

    Public Sub New(ecuacionTexto As String, modoRadianes As Boolean)
        InitializeComponent()
        ecuacion = ecuacionTexto.ToLower().Trim()
        usarRadianes = modoRadianes
        Me.Text = $"Gráfico de: {ecuacionTexto}"

        ' Habilitar doble buffer
        PanelGrafico.GetType().InvokeMember("DoubleBuffered",
            Reflection.BindingFlags.SetProperty Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic,
            Nothing, PanelGrafico, New Object() {True})
    End Sub

    Private Sub FormGrafico_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(800, 600)
        Me.CenterToParent()
        CalcularPuntos()
        LabelInfo.Text = $"X: [{minX:F1}, {maxX:F1}] | Y: [{minY:F1}, {maxY:F1}]"
    End Sub

    Private Sub CalcularPuntos()
        puntosFuncion.Clear()

        Dim numPuntos As Integer = 300
        Dim paso As Double = (maxX - minX) / numPuntos

        For i As Integer = 0 To numPuntos
            Dim x As Double = minX + i * paso
            Try
                Dim y As Double = Evaluar(x)
                If Not Double.IsNaN(y) AndAlso Not Double.IsInfinity(y) Then
                    puntosFuncion.Add(New PointF(CSng(x), CSng(y)))
                End If
            Catch
                ' Ignorar errores
            End Try
        Next
    End Sub

    Private Function Evaluar(x As Double) As Double
        ' Evaluar directamente según el tipo de función
        Dim expr As String = ecuacion

        ' Casos especiales comunes
        If expr = "x" Then Return x
        If expr = "x^2" Then Return x * x
        If expr = "x^3" Then Return x * x * x

        ' Funciones trigonométricas simples
        If expr = "sin(x)" Then
            Return If(usarRadianes, Math.Sin(x), Math.Sin(x * Math.PI / 180))
        End If
        If expr = "cos(x)" Then
            Return If(usarRadianes, Math.Cos(x), Math.Cos(x * Math.PI / 180))
        End If
        If expr = "tan(x)" Then
            Return If(usarRadianes, Math.Tan(x), Math.Tan(x * Math.PI / 180))
        End If

        ' Otras funciones simples
        If expr = "exp(x)" Then Return Math.Exp(x)
        If expr = "log(x)" Then Return Math.Log10(x)
        If expr = "ln(x)" Then Return Math.Log(x)
        If expr = "sqrt(x)" Then Return Math.Sqrt(x)
        If expr = "abs(x)" Then Return Math.Abs(x)

        ' Para expresiones complejas, usar evaluación simple
        Return EvaluarExpresion(expr, x)
    End Function

    Private Function EvaluarExpresion(expresion As String, valorX As Double) As Double
        ' Reemplazar pi/PI por el valor de Math.PI
        Dim expr As String = expresion
        expr = System.Text.RegularExpressions.Regex.Replace(expr, "\bpi\b", Math.PI.ToString(System.Globalization.CultureInfo.InvariantCulture), System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        ' Reemplazar x por el valor
        expr = expr.Replace("x", valorX.ToString(System.Globalization.CultureInfo.InvariantCulture))

        ' Expandir multiplicación implícita
        expr = System.Text.RegularExpressions.Regex.Replace(expr, "(\d)\(", "$1*(")

        ' Procesar funciones básicas
        expr = ProcesarFuncionesSimple(expr)

        ' Evaluar
        Dim dt As New DataTable()
        Return Convert.ToDouble(dt.Compute(expr, ""))
    End Function

    Private Function ProcesarFuncionesSimple(expr As String) As String
        Dim resultado As String = expr
        Dim cambios As Boolean = True
        Dim iter As Integer = 0

        While cambios AndAlso iter < 20
            iter += 1
            cambios = False

            ' sin
            If resultado.Contains("sin(") Then
                Dim match = System.Text.RegularExpressions.Regex.Match(resultado, "sin\(([\d\.]+)\)")
                If match.Success Then
                    Dim num As Double = Double.Parse(match.Groups(1).Value, System.Globalization.CultureInfo.InvariantCulture)
                    Dim val As Double = If(usarRadianes, Math.Sin(num), Math.Sin(num * Math.PI / 180))
                    resultado = resultado.Replace(match.Value, val.ToString(System.Globalization.CultureInfo.InvariantCulture))
                    cambios = True
                End If
            End If

            ' cos
            If resultado.Contains("cos(") Then
                Dim match = System.Text.RegularExpressions.Regex.Match(resultado, "cos\(([\d\.]+)\)")
                If match.Success Then
                    Dim num As Double = Double.Parse(match.Groups(1).Value, System.Globalization.CultureInfo.InvariantCulture)
                    Dim val As Double = If(usarRadianes, Math.Cos(num), Math.Cos(num * Math.PI / 180))
                    resultado = resultado.Replace(match.Value, val.ToString(System.Globalization.CultureInfo.InvariantCulture))
                    cambios = True
                End If
            End If

            ' exp
            If resultado.Contains("exp(") Then
                Dim match = System.Text.RegularExpressions.Regex.Match(resultado, "exp\(([\d\.]+)\)")
                If match.Success Then
                    Dim num As Double = Double.Parse(match.Groups(1).Value, System.Globalization.CultureInfo.InvariantCulture)
                    resultado = resultado.Replace(match.Value, Math.Exp(num).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    cambios = True
                End If
            End If

            ' sqrt
            If resultado.Contains("sqrt(") Then
                Dim match = System.Text.RegularExpressions.Regex.Match(resultado, "sqrt\(([\d\.]+)\)")
                If match.Success Then
                    Dim num As Double = Double.Parse(match.Groups(1).Value, System.Globalization.CultureInfo.InvariantCulture)
                    resultado = resultado.Replace(match.Value, Math.Sqrt(num).ToString(System.Globalization.CultureInfo.InvariantCulture))
                    cambios = True
                End If
            End If
        End While

        Return resultado
    End Function

    Private Sub PanelGrafico_Paint(sender As Object, e As PaintEventArgs) Handles PanelGrafico.Paint
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        Dim ancho As Integer = PanelGrafico.Width
        Dim alto As Integer = PanelGrafico.Height

        ' Dibujar cuadrícula
        DibujarCuadricula(g, ancho, alto)

        ' Dibujar ejes
        DibujarEjes(g, ancho, alto)

        ' Dibujar función
        DibujarFuncion(g, ancho, alto)
    End Sub

    Private Sub DibujarCuadricula(g As Graphics, ancho As Integer, alto As Integer)
        Using pen As New Pen(Color.LightGray, 1)
            pen.DashStyle = Drawing2D.DashStyle.Dot

            ' Líneas verticales
            For i As Integer = 0 To 20
                Dim x As Integer = CInt(i * ancho / 20)
                g.DrawLine(pen, x, 0, x, alto)
            Next

            ' Líneas horizontales
            For i As Integer = 0 To 20
                Dim y As Integer = CInt(i * alto / 20)
                g.DrawLine(pen, 0, y, ancho, y)
            Next
        End Using
    End Sub

    Private Sub DibujarEjes(g As Graphics, ancho As Integer, alto As Integer)
        Using penEje As New Pen(Color.Black, 2)
            ' Eje Y (centro)
            Dim centroX As Integer = ancho \ 2
            g.DrawLine(penEje, centroX, 0, centroX, alto)

            ' Eje X (centro)
            Dim centroY As Integer = alto \ 2
            g.DrawLine(penEje, 0, centroY, ancho, centroY)

            ' Etiquetas
            Using fuente As New Font("Arial", 8)
                Using brocha As New SolidBrush(Color.Black)
                    ' Etiquetas X
                    For i As Integer = CInt(minX) To CInt(maxX) Step Math.Max(1, CInt((maxX - minX) / 10))
                        Dim screenX As Integer = CoordXaPantalla(i, ancho)
                        g.DrawString(i.ToString(), fuente, brocha, screenX - 10, centroY + 5)
                    Next

                    ' Etiquetas Y
                    For i As Integer = CInt(minY) To CInt(maxY) Step Math.Max(1, CInt((maxY - minY) / 10))
                        If i <> 0 Then
                            Dim screenY As Integer = CoordYaPantalla(i, alto)
                            g.DrawString(i.ToString(), fuente, brocha, centroX + 5, screenY - 8)
                        End If
                    Next
                End Using
            End Using
        End Using
    End Sub

    Private Sub DibujarFuncion(g As Graphics, ancho As Integer, alto As Integer)
        If puntosFuncion.Count < 2 Then
            Using fuente As New Font("Arial", 12, FontStyle.Bold)
                g.DrawString("No se pudo graficar la función", fuente, Brushes.Red, 20, 20)
            End Using
            Return
        End If

        Using penFuncion As New Pen(Color.Blue, 2)
            Dim puntosScreen As New List(Of PointF)

            For Each punto In puntosFuncion
                Dim screenX As Integer = CoordXaPantalla(punto.X, ancho)
                Dim screenY As Integer = CoordYaPantalla(punto.Y, alto)

                ' Solo puntos dentro del área visible
                If screenY >= 0 AndAlso screenY <= alto Then
                    puntosScreen.Add(New PointF(screenX, screenY))
                Else
                    ' Discontinuidad - dibujar lo acumulado
                    If puntosScreen.Count > 1 Then
                        g.DrawLines(penFuncion, puntosScreen.ToArray())
                    End If
                    puntosScreen.Clear()
                End If
            Next

            ' Dibujar puntos restantes
            If puntosScreen.Count > 1 Then
                g.DrawLines(penFuncion, puntosScreen.ToArray())
            End If
        End Using
    End Sub

    Private Function CoordXaPantalla(x As Double, ancho As Integer) As Integer
        Return CInt((x - minX) / (maxX - minX) * ancho)
    End Function

    Private Function CoordYaPantalla(y As Double, alto As Integer) As Integer
        Return CInt(alto - (y - minY) / (maxY - minY) * alto)
    End Function

    Private Sub BtnZoomIn_Click(sender As Object, e As EventArgs) Handles BtnZoomIn.Click
        Dim rangoX As Double = (maxX - minX) / 2
        Dim rangoY As Double = (maxY - minY) / 2
        Dim centroX As Double = (maxX + minX) / 2
        Dim centroY As Double = (maxY + minY) / 2

        minX = centroX - rangoX / 2
        maxX = centroX + rangoX / 2
        minY = centroY - rangoY / 2
        maxY = centroY + rangoY / 2

        CalcularPuntos()
        LabelInfo.Text = $"X: [{minX:F1}, {maxX:F1}] | Y: [{minY:F1}, {maxY:F1}]"
        PanelGrafico.Invalidate()
    End Sub

    Private Sub BtnZoomOut_Click(sender As Object, e As EventArgs) Handles BtnZoomOut.Click
        Dim rangoX As Double = (maxX - minX) * 2
        Dim rangoY As Double = (maxY - minY) * 2
        Dim centroX As Double = (maxX + minX) / 2
        Dim centroY As Double = (maxY + minY) / 2

        minX = centroX - rangoX / 2
        maxX = centroX + rangoX / 2
        minY = centroY - rangoY / 2
        maxY = centroY + rangoY / 2

        CalcularPuntos()
        LabelInfo.Text = $"X: [{minX:F1}, {maxX:F1}] | Y: [{minY:F1}, {maxY:F1}]"
        PanelGrafico.Invalidate()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        minX = -10
        maxX = 10
        minY = -10
        maxY = 10

        CalcularPuntos()
        LabelInfo.Text = $"X: [{minX:F1}, {maxX:F1}] | Y: [{minY:F1}, {maxY:F1}]"
        PanelGrafico.Invalidate()
    End Sub
End Class
