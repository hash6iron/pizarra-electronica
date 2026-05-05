Public Class FormGrafico
    Private ecuacion As String
    Private minX As Double = -10
    Private maxX As Double = 10
    Private minY As Double = -10
    Private maxY As Double = 10
    Private usarRadianes As Boolean = False
    Private zoom As Double = 1.0
    Private offsetX As Double = 0
    Private offsetY As Double = 0
    Private arrastrandoMouse As Boolean = False
    Private ultimoPuntoMouse As Point

    Public Sub New(ecuacionTexto As String, modoRadianes As Boolean)
        InitializeComponent()
        ecuacion = ecuacionTexto
        usarRadianes = modoRadianes
        Me.Text = $"Gráfico de: {ecuacion}"

        ' Habilitar doble buffer para evitar parpadeo
        PanelGrafico.GetType().InvokeMember("DoubleBuffered",
            Reflection.BindingFlags.SetProperty Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic,
            Nothing, PanelGrafico, New Object() {True})
    End Sub

    Private Sub FormGrafico_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Normal
        Me.Size = New Size(800, 600)
        ActualizarEtiquetas()
    End Sub

    Private Sub PanelGrafico_Paint(sender As Object, e As PaintEventArgs) Handles PanelGrafico.Paint
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim ancho As Integer = PanelGrafico.Width
        Dim alto As Integer = PanelGrafico.Height

        ' Calcular rango visible con zoom y offset
        Dim rangoX As Double = (maxX - minX) / zoom
        Dim rangoY As Double = (maxY - minY) / zoom
        Dim centroX As Double = (maxX + minX) / 2 + offsetX
        Dim centroY As Double = (maxY + minY) / 2 + offsetY

        Dim visibleMinX As Double = centroX - rangoX / 2
        Dim visibleMaxX As Double = centroX + rangoX / 2
        Dim visibleMinY As Double = centroY - rangoY / 2
        Dim visibleMaxY As Double = centroY + rangoY / 2

        ' Fondo
        g.Clear(Color.White)

        ' Dibujar cuadrícula
        DibujarCuadricula(g, ancho, alto, visibleMinX, visibleMaxX, visibleMinY, visibleMaxY)

        ' Dibujar ejes
        DibujarEjes(g, ancho, alto, visibleMinX, visibleMaxX, visibleMinY, visibleMaxY)

        ' Dibujar la función
        DibujarFuncion(g, ancho, alto, visibleMinX, visibleMaxX, visibleMinY, visibleMaxY)
    End Sub

    Private Sub DibujarCuadricula(g As Graphics, ancho As Integer, alto As Integer,
                                   minX As Double, maxX As Double, minY As Double, maxY As Double)
        Dim penCuadricula As New Pen(Color.LightGray, 1)
        penCuadricula.DashStyle = Drawing2D.DashStyle.Dot

        ' Líneas verticales
        Dim pasoX As Double = CalcularPasoAutomatico(maxX - minX)
        Dim x As Double = Math.Ceiling(minX / pasoX) * pasoX
        While x <= maxX
            Dim screenX As Integer = ConvertirXAPantalla(x, ancho, minX, maxX)
            g.DrawLine(penCuadricula, screenX, 0, screenX, alto)
            x += pasoX
        End While

        ' Líneas horizontales
        Dim pasoY As Double = CalcularPasoAutomatico(maxY - minY)
        Dim y As Double = Math.Ceiling(minY / pasoY) * pasoY
        While y <= maxY
            Dim screenY As Integer = ConvertirYAPantalla(y, alto, minY, maxY)
            g.DrawLine(penCuadricula, 0, screenY, ancho, screenY)
            y += pasoY
        End While

        penCuadricula.Dispose()
    End Sub

    Private Sub DibujarEjes(g As Graphics, ancho As Integer, alto As Integer,
                            minX As Double, maxX As Double, minY As Double, maxY As Double)
        Dim penEje As New Pen(Color.Black, 2)
        Dim fuente As New Font("Arial", 8)
        Dim brocha As New SolidBrush(Color.Black)

        ' Eje Y (x=0)
        If minX <= 0 AndAlso maxX >= 0 Then
            Dim screenX As Integer = ConvertirXAPantalla(0, ancho, minX, maxX)
            g.DrawLine(penEje, screenX, 0, screenX, alto)

            ' Etiquetas del eje Y
            Dim pasoY As Double = CalcularPasoAutomatico(maxY - minY)
            Dim y As Double = Math.Ceiling(minY / pasoY) * pasoY
            While y <= maxY
                If Math.Abs(y) > 0.001 Then
                    Dim screenY As Integer = ConvertirYAPantalla(y, alto, minY, maxY)
                    g.DrawString(y.ToString("F1"), fuente, brocha, screenX + 5, screenY - 8)
                    g.DrawLine(penEje, screenX - 3, screenY, screenX + 3, screenY)
                End If
                y += pasoY
            End While
        End If

        ' Eje X (y=0)
        If minY <= 0 AndAlso maxY >= 0 Then
            Dim screenY As Integer = ConvertirYAPantalla(0, alto, minY, maxY)
            g.DrawLine(penEje, 0, screenY, ancho, screenY)

            ' Etiquetas del eje X
            Dim pasoX As Double = CalcularPasoAutomatico(maxX - minX)
            Dim x As Double = Math.Ceiling(minX / pasoX) * pasoX
            While x <= maxX
                If Math.Abs(x) > 0.001 Then
                    Dim screenX As Integer = ConvertirXAPantalla(x, ancho, minX, maxX)
                    g.DrawString(x.ToString("F1"), fuente, brocha, screenX - 15, screenY + 5)
                    g.DrawLine(penEje, screenX, screenY - 3, screenX, screenY + 3)
                End If
                x += pasoX
            End While
        End If

        penEje.Dispose()
        fuente.Dispose()
        brocha.Dispose()
    End Sub

    Private Sub DibujarFuncion(g As Graphics, ancho As Integer, alto As Integer,
                               minX As Double, maxX As Double, minY As Double, maxY As Double)
        Dim penFuncion As New Pen(Color.Blue, 2)
        Dim puntos As New List(Of PointF)

        ' Calcular puntos de la función
        Dim paso As Double = (maxX - minX) / ancho
        Dim x As Double = minX

        While x <= maxX
            Try
                Dim y As Double = EvaluarFuncionGrafica(ecuacion, x)

                ' Verificar que y está en rango visible
                If Not Double.IsNaN(y) AndAlso Not Double.IsInfinity(y) Then
                    If y >= minY AndAlso y <= maxY Then
                        Dim screenX As Integer = ConvertirXAPantalla(x, ancho, minX, maxX)
                        Dim screenY As Integer = ConvertirYAPantalla(y, alto, minY, maxY)
                        puntos.Add(New PointF(screenX, screenY))
                    ElseIf puntos.Count > 0 Then
                        ' Discontinuidad - dibujar lo acumulado y reiniciar
                        If puntos.Count > 1 Then
                            g.DrawLines(penFuncion, puntos.ToArray())
                        End If
                        puntos.Clear()
                    End If
                ElseIf puntos.Count > 0 Then
                    ' Discontinuidad - dibujar lo acumulado y reiniciar
                    If puntos.Count > 1 Then
                        g.DrawLines(penFuncion, puntos.ToArray())
                    End If
                    puntos.Clear()
                End If
            Catch ex As Exception
                ' Error al evaluar - discontinuidad
                If puntos.Count > 1 Then
                    g.DrawLines(penFuncion, puntos.ToArray())
                End If
                puntos.Clear()
            End Try

            x += paso
        End While

        ' Dibujar los puntos restantes
        If puntos.Count > 1 Then
            g.DrawLines(penFuncion, puntos.ToArray())
        End If

        penFuncion.Dispose()
    End Sub

    Private Function EvaluarFuncionGrafica(expresion As String, valorX As Double) As Double
        ' Reemplazar x por el valor
        Dim expresionEvaluable As String = expresion.ToLower()
        expresionEvaluable = expresionEvaluable.Replace("x", valorX.ToString(System.Globalization.CultureInfo.InvariantCulture))

        ' Expandir notación implícita
        expresionEvaluable = ExpandirNotacion(expresionEvaluable)

        ' Procesar funciones matemáticas
        expresionEvaluable = ProcesarFunciones(expresionEvaluable)

        ' Evaluar la expresión
        Dim dt As New DataTable()
        Dim resultado As Object = dt.Compute(expresionEvaluable, "")
        Return Convert.ToDouble(resultado)
    End Function

    Private Function ExpandirNotacion(expresion As String) As String
        Dim resultado As String = expresion

        ' Números seguidos de paréntesis: 2(x) -> 2*(x)
        resultado = System.Text.RegularExpressions.Regex.Replace(resultado, "(\d)\(", "$1*(")

        ' Paréntesis seguidos de paréntesis: )( -> )*(
        resultado = resultado.Replace(")(", ")*(")

        Return resultado
    End Function

    Private Function ProcesarFunciones(expresion As String) As String
        Dim resultado As String = expresion

        ' Procesar funciones matemáticas
        While resultado.Contains("log(")
            Dim pos As Integer = resultado.IndexOf("log(")
            Dim argumento As String = ExtraerArgumento(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionSimple(argumento)
            Dim valorLog As Double = Math.Log10(valorArg)
            resultado = resultado.Replace("log(" & argumento & ")", valorLog.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        While resultado.Contains("ln(")
            Dim pos As Integer = resultado.IndexOf("ln(")
            Dim argumento As String = ExtraerArgumento(resultado, pos + 3)
            Dim valorArg As Double = EvaluarExpresionSimple(argumento)
            Dim valorLn As Double = Math.Log(valorArg)
            resultado = resultado.Replace("ln(" & argumento & ")", valorLn.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        While resultado.Contains("exp(")
            Dim pos As Integer = resultado.IndexOf("exp(")
            Dim argumento As String = ExtraerArgumento(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionSimple(argumento)
            Dim valorExp As Double = Math.Exp(valorArg)
            resultado = resultado.Replace("exp(" & argumento & ")", valorExp.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        While resultado.Contains("abs(")
            Dim pos As Integer = resultado.IndexOf("abs(")
            Dim argumento As String = ExtraerArgumento(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionSimple(argumento)
            Dim valorAbs As Double = Math.Abs(valorArg)
            resultado = resultado.Replace("abs(" & argumento & ")", valorAbs.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        While resultado.Contains("sin(")
            Dim pos As Integer = resultado.IndexOf("sin(")
            Dim argumento As String = ExtraerArgumento(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionSimple(argumento)
            Dim valorSin As Double = If(usarRadianes, Math.Sin(valorArg), Math.Sin(valorArg * Math.PI / 180))
            resultado = resultado.Replace("sin(" & argumento & ")", valorSin.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        While resultado.Contains("cos(")
            Dim pos As Integer = resultado.IndexOf("cos(")
            Dim argumento As String = ExtraerArgumento(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionSimple(argumento)
            Dim valorCos As Double = If(usarRadianes, Math.Cos(valorArg), Math.Cos(valorArg * Math.PI / 180))
            resultado = resultado.Replace("cos(" & argumento & ")", valorCos.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        While resultado.Contains("tan(")
            Dim pos As Integer = resultado.IndexOf("tan(")
            Dim argumento As String = ExtraerArgumento(resultado, pos + 4)
            Dim valorArg As Double = EvaluarExpresionSimple(argumento)
            Dim valorTan As Double = If(usarRadianes, Math.Tan(valorArg), Math.Tan(valorArg * Math.PI / 180))
            resultado = resultado.Replace("tan(" & argumento & ")", valorTan.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        While resultado.Contains("sqrt(")
            Dim pos As Integer = resultado.IndexOf("sqrt(")
            Dim argumento As String = ExtraerArgumento(resultado, pos + 5)
            Dim valorArg As Double = EvaluarExpresionSimple(argumento)
            Dim valorSqrt As Double = Math.Sqrt(valorArg)
            resultado = resultado.Replace("sqrt(" & argumento & ")", valorSqrt.ToString(System.Globalization.CultureInfo.InvariantCulture))
        End While

        Return resultado
    End Function

    Private Function ExtraerArgumento(expresion As String, posInicio As Integer) As String
        Dim nivel As Integer = 1
        Dim resultado As New System.Text.StringBuilder()

        For i As Integer = posInicio To expresion.Length - 1
            Dim c As Char = expresion(i)
            If c = "("c Then
                nivel += 1
                resultado.Append(c)
            ElseIf c = ")"c Then
                nivel -= 1
                If nivel = 0 Then
                    Return resultado.ToString()
                End If
                resultado.Append(c)
            Else
                resultado.Append(c)
            End If
        Next

        Return resultado.ToString()
    End Function

    Private Function EvaluarExpresionSimple(expresion As String) As Double
        Dim dt As New DataTable()
        Dim resultado As Object = dt.Compute(expresion, "")
        Return Convert.ToDouble(resultado)
    End Function

    Private Function ConvertirXAPantalla(x As Double, ancho As Integer, minX As Double, maxX As Double) As Integer
        Return CInt((x - minX) / (maxX - minX) * ancho)
    End Function

    Private Function ConvertirYAPantalla(y As Double, alto As Integer, minY As Double, maxY As Double) As Integer
        Return CInt(alto - (y - minY) / (maxY - minY) * alto)
    End Function

    Private Function CalcularPasoAutomatico(rango As Double) As Double
        Dim magnitude As Double = Math.Pow(10, Math.Floor(Math.Log10(rango)))
        Dim paso As Double = magnitude

        If rango / paso > 10 Then
            paso *= 2
        ElseIf rango / paso < 5 Then
            paso /= 2
        End If

        Return paso
    End Function

    Private Sub PanelGrafico_MouseWheel(sender As Object, e As MouseEventArgs) Handles PanelGrafico.MouseWheel
        ' Zoom con la rueda del ratón
        If e.Delta > 0 Then
            zoom *= 1.2
        Else
            zoom /= 1.2
        End If

        zoom = Math.Max(0.1, Math.Min(zoom, 100))
        ActualizarEtiquetas()
        PanelGrafico.Invalidate()
    End Sub

    Private Sub PanelGrafico_MouseDown(sender As Object, e As MouseEventArgs) Handles PanelGrafico.MouseDown
        If e.Button = MouseButtons.Left Then
            arrastrandoMouse = True
            ultimoPuntoMouse = e.Location
            PanelGrafico.Cursor = Cursors.SizeAll
        End If
    End Sub

    Private Sub PanelGrafico_MouseMove(sender As Object, e As MouseEventArgs) Handles PanelGrafico.MouseMove
        If arrastrandoMouse Then
            Dim dx As Integer = e.X - ultimoPuntoMouse.X
            Dim dy As Integer = e.Y - ultimoPuntoMouse.Y

            Dim rangoX As Double = (maxX - minX) / zoom
            Dim rangoY As Double = (maxY - minY) / zoom

            offsetX -= dx / PanelGrafico.Width * rangoX
            offsetY += dy / PanelGrafico.Height * rangoY

            ultimoPuntoMouse = e.Location
            ActualizarEtiquetas()
            PanelGrafico.Invalidate()
        End If
    End Sub

    Private Sub PanelGrafico_MouseUp(sender As Object, e As MouseEventArgs) Handles PanelGrafico.MouseUp
        arrastrandoMouse = False
        PanelGrafico.Cursor = Cursors.Default
    End Sub

    Private Sub BtnZoomIn_Click(sender As Object, e As EventArgs) Handles BtnZoomIn.Click
        zoom *= 1.5
        ActualizarEtiquetas()
        PanelGrafico.Invalidate()
    End Sub

    Private Sub BtnZoomOut_Click(sender As Object, e As EventArgs) Handles BtnZoomOut.Click
        zoom /= 1.5
        zoom = Math.Max(0.1, zoom)
        ActualizarEtiquetas()
        PanelGrafico.Invalidate()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        zoom = 1.0
        offsetX = 0
        offsetY = 0
        ActualizarEtiquetas()
        PanelGrafico.Invalidate()
    End Sub

    Private Sub ActualizarEtiquetas()
        Dim rangoX As Double = (maxX - minX) / zoom
        Dim rangoY As Double = (maxY - minY) / zoom
        Dim centroX As Double = (maxX + minX) / 2 + offsetX
        Dim centroY As Double = (maxY + minY) / 2 + offsetY

        Dim visibleMinX As Double = centroX - rangoX / 2
        Dim visibleMaxX As Double = centroX + rangoX / 2
        Dim visibleMinY As Double = centroY - rangoY / 2
        Dim visibleMaxY As Double = centroY + rangoY / 2

        LabelInfo.Text = $"X: [{visibleMinX:F2}, {visibleMaxX:F2}] | Y: [{visibleMinY:F2}, {visibleMaxY:F2}] | Zoom: {zoom:F2}x"
    End Sub
End Class
