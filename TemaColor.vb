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
