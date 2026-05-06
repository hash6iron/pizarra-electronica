Public Class FormHorasReloj
    Private horaActual As Integer
    Private minutoActual As Integer
    Private esMañana As Boolean
    Private ejerciciosRealizados As Integer = 0
    Private ejerciciosCorrectos As Integer = 0
    Private opcionCorrecta As Integer = 0

    Private Sub FormHorasReloj_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InicializarControles()
        GenerarNuevaHora()
    End Sub

    Private Sub InicializarControles()
        ' Configuración inicial
        CheckBoxModoTexto.Checked = False
    End Sub

    Private Sub GenerarNuevaHora()
        Dim random As New Random()

        ' Generar hora entre 1 y 12
        horaActual = random.Next(1, 13)

        ' Generar minutos: 0, 15, 30 o 45
        Dim minutosOpciones() As Integer = {0, 15, 30, 45}
        minutoActual = minutosOpciones(random.Next(0, 4))

        ' Decidir si es mañana o tarde
        esMañana = random.Next(0, 2) = 0

        ' Actualizar etiqueta de periodo
        If esMañana Then
            LabelPeriodo.Text = "Mañana"
            LabelPeriodo.ForeColor = Color.Orange
        Else
            LabelPeriodo.Text = "Tarde"
            LabelPeriodo.ForeColor = Color.Blue
        End If

        ' Verificar modo de juego
        If CheckBoxModoTexto.Checked Then
            ' Modo texto: ocultar cajas de texto, mostrar radio buttons
            MostrarModoTexto()
        Else
            ' Modo normal: mostrar cajas de texto, ocultar radio buttons
            MostrarModoNormal()
        End If

        ' Redibujar el reloj
        PanelReloj.Invalidate()
    End Sub

    Private Sub MostrarModoNormal()
        LabelHora.Visible = True
        TextBoxHora.Visible = True
        LabelDosPuntos.Visible = True
        TextBoxMinutos.Visible = True
        TextBoxHora.Text = ""
        TextBoxMinutos.Text = ""
        TextBoxHora.Focus()

        RadioButtonOpcion1.Visible = False
        RadioButtonOpcion2.Visible = False
        RadioButtonOpcion3.Visible = False
        RadioButtonOpcion1.Checked = False
        RadioButtonOpcion2.Checked = False
        RadioButtonOpcion3.Checked = False

        LabelInstrucciones.Text = "¿Qué hora marca el reloj?" & vbCrLf & "Escribe la hora en formato 24h"
    End Sub

    Private Sub MostrarModoTexto()
        LabelHora.Visible = False
        TextBoxHora.Visible = False
        LabelDosPuntos.Visible = False
        TextBoxMinutos.Visible = False

        RadioButtonOpcion1.Visible = True
        RadioButtonOpcion2.Visible = True
        RadioButtonOpcion3.Visible = True
        RadioButtonOpcion1.Checked = False
        RadioButtonOpcion2.Checked = False
        RadioButtonOpcion3.Checked = False

        LabelInstrucciones.Text = "¿Qué hora marca el reloj?" & vbCrLf & "Selecciona la respuesta correcta"

        ' Generar las tres opciones
        GenerarOpcionesTexto()
    End Sub

    Private Sub GenerarOpcionesTexto()
        Dim random As New Random()
        Dim opciones(2) As String
        Dim horaCorrecta As String = ConvertirHoraATexto(horaActual, minutoActual)

        ' La opción correcta estará en una posición aleatoria
        opcionCorrecta = random.Next(0, 3)
        opciones(opcionCorrecta) = horaCorrecta

        ' Generar dos opciones incorrectas
        Dim opcionesIncorrectas As New List(Of String)
        For i As Integer = 0 To 2
            If i <> opcionCorrecta Then
                Dim horaIncorrecta As String
                Do
                    Dim horaRandom As Integer = random.Next(1, 13)
                    Dim minutosOpciones() As Integer = {0, 15, 30, 45}
                    Dim minutoRandom As Integer = minutosOpciones(random.Next(0, 4))
                    horaIncorrecta = ConvertirHoraATexto(horaRandom, minutoRandom)
                Loop While horaIncorrecta = horaCorrecta OrElse opcionesIncorrectas.Contains(horaIncorrecta)

                opciones(i) = horaIncorrecta
                opcionesIncorrectas.Add(horaIncorrecta)
            End If
        Next

        ' Asignar opciones a los radio buttons
        RadioButtonOpcion1.Text = opciones(0)
        RadioButtonOpcion2.Text = opciones(1)
        RadioButtonOpcion3.Text = opciones(2)
    End Sub

    Private Function ConvertirHoraATexto(hora As Integer, minutos As Integer) As String
        Dim horaFormateada As String = hora.ToString("D2")
        Dim minutosFormateada As String = minutos.ToString("D2")

        ' Formato: "10:45 son las 11 menos cuarto"
        Select Case minutos
            Case 0
                Return $"{horaFormateada}:00 son las {hora} en punto"
            Case 15
                Return $"{horaFormateada}:15 son las {hora} y cuarto"
            Case 30
                Return $"{horaFormateada}:30 son las {hora} y media"
            Case 45
                Dim horaSiguiente As Integer = If(hora = 12, 1, hora + 1)
                Return $"{horaFormateada}:45 son las {horaSiguiente} menos cuarto"
            Case Else
                ' Para otros minutos no estándar
                Dim minutosRestantes As Integer = 60 - minutos
                If minutos > 30 Then
                    Dim horaSiguiente As Integer = If(hora = 12, 1, hora + 1)
                    Return $"{horaFormateada}:{minutosFormateada} son las {horaSiguiente} menos {minutosRestantes}"
                Else
                    Return $"{horaFormateada}:{minutosFormateada} son las {hora} y {minutos}"
                End If
        End Select
    End Function

    Private Sub PanelReloj_Paint(sender As Object, e As PaintEventArgs) Handles PanelReloj.Paint
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ' Calcular centro y radio
        Dim centerX As Integer = PanelReloj.Width \ 2
        Dim centerY As Integer = PanelReloj.Height \ 2
        Dim radio As Integer = Math.Min(centerX, centerY) - 20

        ' Dibujar fondo del reloj
        g.FillEllipse(Brushes.White, centerX - radio, centerY - radio, radio * 2, radio * 2)
        g.DrawEllipse(New Pen(Color.Black, 3), centerX - radio, centerY - radio, radio * 2, radio * 2)

        ' Dibujar marcas de las horas (1-12)
        Dim fuente As New Font("Arial", 14, FontStyle.Bold)
        For i As Integer = 1 To 12
            Dim angulo As Double = (i * 30 - 90) * Math.PI / 180
            Dim x As Integer = centerX + CInt((radio - 25) * Math.Cos(angulo))
            Dim y As Integer = centerY + CInt((radio - 25) * Math.Sin(angulo))

            Dim tamaño As SizeF = g.MeasureString(i.ToString(), fuente)
            g.DrawString(i.ToString(), fuente, Brushes.Black, x - tamaño.Width / 2, y - tamaño.Height / 2)
        Next

        ' Dibujar marcas de los cuartos (0, 15, 30, 45)
        Dim fuentePequeña As New Font("Arial", 10, FontStyle.Regular)
        Dim minutosTexto() As String = {"0", "15", "30", "45"}
        Dim posiciones() As Integer = {0, 15, 30, 45}

        For i As Integer = 0 To 3
            Dim angulo As Double = (posiciones(i) * 6 - 90) * Math.PI / 180
            Dim x As Integer = centerX + CInt((radio - 50) * Math.Cos(angulo))
            Dim y As Integer = centerY + CInt((radio - 50) * Math.Sin(angulo))

            Dim tamaño As SizeF = g.MeasureString(minutosTexto(i), fuentePequeña)
            g.FillEllipse(Brushes.LightGray, x - 15, y - 10, 30, 20)
            g.DrawString(minutosTexto(i), fuentePequeña, Brushes.DarkBlue, x - tamaño.Width / 2, y - tamaño.Height / 2)
        Next

        ' Dibujar punto central
        g.FillEllipse(Brushes.Black, centerX - 5, centerY - 5, 10, 10)

        ' Calcular ángulos de las agujas
        Dim anguloMinutero As Double = (minutoActual * 6 - 90) * Math.PI / 180
        Dim anguloHorario As Double = ((horaActual * 30) + (minutoActual * 0.5) - 90) * Math.PI / 180

        ' Dibujar minutero (más largo y delgado) con 80% opacidad (20% transparencia)
        Dim minuteroX As Integer = centerX + CInt((radio - 15) * Math.Cos(anguloMinutero))
        Dim minuteroY As Integer = centerY + CInt((radio - 15) * Math.Sin(anguloMinutero))
        Dim lapizMinutero As New Pen(Color.FromArgb(204, Color.Blue), 4)
        g.DrawLine(lapizMinutero, centerX, centerY, minuteroX, minuteroY)
        lapizMinutero.Dispose()

        ' Dibujar horario (más corto y grueso) con 80% opacidad (20% transparencia)
        Dim horarioX As Integer = centerX + CInt((radio * 0.6) * Math.Cos(anguloHorario))
        Dim horarioY As Integer = centerY + CInt((radio * 0.6) * Math.Sin(anguloHorario))
        Dim lapizHorario As New Pen(Color.FromArgb(204, Color.Red), 6)
        g.DrawLine(lapizHorario, centerX, centerY, horarioX, horarioY)
        lapizHorario.Dispose()

        fuente.Dispose()
        fuentePequeña.Dispose()
    End Sub

    Private Sub ButtonComprobar_Click(sender As Object, e As EventArgs) Handles ButtonComprobar.Click
        ComprobarRespuesta()
    End Sub

    Private Sub ComprobarRespuesta()
        ejerciciosRealizados += 1
        Dim esCorrecta As Boolean = False

        If CheckBoxModoTexto.Checked Then
            ' Modo texto: verificar radio button seleccionado
            Dim seleccionado As Integer = -1
            If RadioButtonOpcion1.Checked Then seleccionado = 0
            If RadioButtonOpcion2.Checked Then seleccionado = 1
            If RadioButtonOpcion3.Checked Then seleccionado = 2

            If seleccionado = -1 Then
                MessageBox.Show("Por favor, selecciona una opción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ejerciciosRealizados -= 1
                Return
            End If

            esCorrecta = (seleccionado = opcionCorrecta)
        Else
            ' Modo normal: verificar cajas de texto
            Dim horaIngresada As Integer
            Dim minutosIngresados As Integer

            If Not Integer.TryParse(TextBoxHora.Text, horaIngresada) OrElse
               Not Integer.TryParse(TextBoxMinutos.Text, minutosIngresados) Then
                MessageBox.Show("Por favor, ingresa números válidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ejerciciosRealizados -= 1
                Return
            End If

            ' Calcular hora correcta (en formato 24 horas)
            Dim horaCorrecta24 As Integer = horaActual
            If Not esMañana AndAlso horaActual <> 12 Then
                horaCorrecta24 = horaActual + 12
            ElseIf esMañana AndAlso horaActual = 12 Then
                horaCorrecta24 = 0
            End If

            esCorrecta = (horaIngresada = horaCorrecta24 AndAlso minutosIngresados = minutoActual)
        End If

        ' Mostrar resultado
        If esCorrecta Then
            ejerciciosCorrectos += 1
            MessageBox.Show("¡Correcto! ¡Muy bien!", "¡Excelente!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim horaTexto As String = If(esMañana, horaActual.ToString(), (horaActual + If(horaActual = 12, 0, 12)).ToString())
            Dim respuestaCorrecta As String = ConvertirHoraATexto(horaActual, minutoActual)
            MessageBox.Show($"Incorrecto. La hora correcta es {horaTexto}:{minutoActual:D2}" & vbCrLf & respuestaCorrecta, "Intenta de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        ' Actualizar estadísticas
        LabelEstadisticas.Text = $"Ejercicios: {ejerciciosRealizados} | Correctos: {ejerciciosCorrectos} | Porcentaje: {If(ejerciciosRealizados > 0, Math.Round(ejerciciosCorrectos * 100.0 / ejerciciosRealizados, 1), 0)}%"

        ' Generar nueva hora
        GenerarNuevaHora()
    End Sub

    Private Sub ButtonSiguiente_Click(sender As Object, e As EventArgs) Handles ButtonSiguiente.Click
        GenerarNuevaHora()
    End Sub

    Private Sub TextBoxHora_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxHora.KeyPress
        ' Solo permitir números
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBoxMinutos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxMinutos.KeyPress
        ' Solo permitir números
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBoxHora_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxHora.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            TextBoxMinutos.Focus()
        End If
    End Sub

    Private Sub TextBoxMinutos_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxMinutos.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ComprobarRespuesta()
        End If
    End Sub

    Private Sub CheckBoxModoTexto_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxModoTexto.CheckedChanged
        ' Reiniciar estadísticas al cambiar de modo
        ejerciciosRealizados = 0
        ejerciciosCorrectos = 0
        LabelEstadisticas.Text = "Ejercicios: 0 | Correctos: 0 | Porcentaje: 0%"

        ' Generar nueva hora con el nuevo modo
        GenerarNuevaHora()
    End Sub
End Class
