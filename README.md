# Pizarra Electrónica - Práctica de Cálculos Matemáticos

Una aplicación de escritorio tipo pizarra electrónica diseñada para que los niños practiquen cálculos matemáticos básicos y ecuaciones, con validación automática de respuestas.

![Pizarra Electrónica](screenshot.png)

## 🎯 Características

### 📝 Modos de Funcionamiento

- **Modo EDICIÓN (F2)**: El adulto prepara ejercicios
  - INTRO realiza retorno de carro
  - Auto-completado de `=` en operaciones
  - Saltos de línea inteligentes (SHIFT+INTRO = 1 línea, INTRO = 2 líneas)
  - **Campos de resultado**: Define campos con `=RRRR` (tantas R como espacio necesites)
    - Ejemplo: `2+2=RRRR` crea un campo de 4 caracteres para la respuesta

- **Modo COMPROBACIÓN**: El niño resuelve ejercicios
  - INTRO valida la respuesta automáticamente
  - Navegación automática a la siguiente operación
  - Superíndices elegantes para exponentes (2³ en vez de 2^3)
  - **Campos protegidos**: Solo se puede escribir en los campos de resultado (sombreados)
  - Las teclas de navegación saltan entre campos de resultado


### ✨ Funcionalidades de Edición

- **Pizarra dinámica adaptable**:
  - Tamaño automático según la ventana (hasta 200x100 caracteres máximo)
  - Se adapta al redimensionar la ventana
  - El cursor siempre permanece dentro de los límites visibles

- **Navegación completa con teclado**:
  - Cursores para moverse por la pizarra
  - HOME/END inteligentes (primera pulsación = primer/último carácter, segunda = inicio/fin absoluto)
  - BACKSPACE avanzado (salta a línea anterior inteligentemente)
  - TAB/SHIFT+TAB con tab stops cada 4 columnas

- **Edición avanzada**:
  - INSERT alterna entre modo insertar/sobreescribir
  - Selección con SHIFT + cursores
  - Copiar (CTRL+C), Cortar (CTRL+X), Pegar (CTRL+V)
  - DELETE elimina y compacta texto

### 🧮 Operaciones Matemáticas Soportadas

#### Operaciones Básicas
- Suma, resta, multiplicación, división: `2+2=4`, `10/2=5`
- Con formato: `1) 2+2=4`, `a) 15*3=45`

#### Operaciones Avanzadas
- **Exponentes**: `2^3=8` (se visualiza como 2³)
- **Constante PI**: `pi` o `PI` (se visualiza como π en modo NO EDICIÓN)
  - Expresiones compactas: `2pi`, `2PI`, `3.14pi`
  - En funciones: `cos(2pi)=1`, `sin(pi/2)=1`, `2pi*sin(x)`
- **Funciones trigonométricas**: `sin(90)=1`, `cos(0)=1`, `tan(45)=1`
  - Soporte de **radianes**: Menú Opciones > Usar Radianes
- **Raíces cuadradas**: `sqrt(16)=4`
- **Logaritmos**: `log(100)=2` (base 10), `ln(2.718)=1` (natural)
- **Exponencial**: `exp(1)=2.718`, `exp(0)=1`
- **Valor absoluto**: `abs(-5)=5`
- **Combinaciones**: `2^2+3^2=13`, `sin(30)+cos(60)=1`, `log(100)+ln(e)=3`

#### Ecuaciones con Incógnita (x)
- Lineales: `2x+2=12 -> x=5`
- Cuadráticas: `x^2=16 -> x=4`
- Con paréntesis: `2(x+1)=4 -> x=1` (se interpreta como `2*(x+1)`)
- Trigonométricas: `sin(x)=1 -> x=90`, `2cos(x+2)=0 -> x=88`
- Notación simplificada: `2x` = `2*x`, `2sin(x)` = `2*sin(x)`

**Resolución**: Método de Newton-Raphson con precisión de 3 decimales

### 🎨 Validación Visual

- ✅ **Verde**: Respuesta correcta
- ❌ **Rojo**: Respuesta incorrecta (muestra `[respuesta_correcta]`)
- **Blanco**: Sin validar

### 📊 Estadísticas y Progreso

- **Historial de Ejercicios**: Guarda todos los ejercicios completados con fecha/hora
- **Estadísticas de Sesión**:
  - Total de ejercicios realizados
  - Ejercicios correctos e incorrectos
  - Porcentaje de aciertos
- **Ver Historial**: Muestra los últimos 20 ejercicios con sus resultados
- **Reiniciar Estadísticas**: Limpia el historial y contadores

### 🎨 Temas de Color Personalizables

Elige entre 4 temas visuales en **Opciones > Temas de Color**:

1. **Clásico** 🖤: Fondo negro, texto blanco, verde/rojo para validación
2. **Oscuro Azul** 🌌: Tonos azulados modernos, cursor cyan
3. **Matriz** 💚: Estilo Matrix con tonos verdes sobre negro
4. **Retro** 💙: Azul retro con texto amarillento

Cada tema ajusta:
- Color de fondo
- Color de texto normal
- Colores de validación (correcto/incorrecto)
- Color del cursor
- Color de selección

### 📈 Representación Gráfica de Ecuaciones

**Nueva funcionalidad**: Visualiza funciones matemáticas en un plano cartesiano interactivo.

#### Características del Graficador:
- **Menú Ver > Graficar Función (F5)**: Abre el diálogo de graficación
- **Funciones soportadas**:
  - Polinómicas: `x^2`, `x^3-2*x+1`
  - Trigonométricas: `sin(x)`, `cos(x)`, `tan(x)`, `sin(2pi*x)`
  - Exponenciales: `exp(x)`, `2^x`
  - Logarítmicas: `log(x)`, `ln(x)`
  - Raíces: `sqrt(x)`
  - Valor absoluto: `abs(x)`
  - Con constante PI: `cos(2pi)`, `sin(pi/2)`, `2pi*x`
  - Combinaciones: `sin(x)*cos(x)`, `x^2+sin(x)`

#### Controles Interactivos:
- 🖱️ **Rueda del ratón**: Zoom in/out
- 🖱️ **Arrastrar con botón izquierdo**: Mover el plano
- ➕ **Botón +**: Acercar zoom
- ➖ **Botón -**: Alejar zoom
- 🔄 **Botón Reset**: Volver a la vista inicial (-10 a 10 en ambos ejes)

#### Visualización:
- Ejes cartesianos con etiquetas numéricas
- Cuadrícula automática adaptativa al nivel de zoom
- Curva suave con antialiasing
- Manejo de discontinuidades (ej: `1/x` en x=0)
- Indicador de rango visible y nivel de zoom
- Respeta el modo radianes/grados para funciones trigonométricas

**Ejemplo de uso**:
1. Presiona **F5** o ve a **Ver > Graficar Función**
2. Escribe la función, por ejemplo: `sin(x)*cos(x)`
3. Explora el gráfico usando zoom y arrastre
4. Observa cómo cambia la curva

### 💾 Gestión de Archivos

- **Guardar (CTRL+S)**: Guarda la pizarra actual
- **Guardar como**: Crea un nuevo archivo `.piz`
- **Abrir (CTRL+O)**: Carga una pizarra guardada
- **Salir (ALT+F4)**: Con confirmación si hay cambios sin guardar

Formato `.piz` guarda:
- Todo el contenido de la pizarra (80x25)
- Colores de validación (verde/rojo/blanco)
- Codificación UTF-8

### 📊 Barra de Estado

Muestra constantemente:
- Posición del cursor (Línea, Columna)
- Modo EDICIÓN (cuando está activo)
- Modo SOBREESCRIBIR (cuando INSERT está activo)
- **RAD** (cuando está en modo radianes)

## 🚀 Requisitos

- Windows con .NET Framework 4.7.2 o superior
- Visual Studio 2019 o superior (para desarrollo)

## 📦 Instalación

1. Clona el repositorio:
```bash
git clone https://github.com/TU_USUARIO/pizarra-electronica.git
```

2. Abre `Profe.sln` en Visual Studio

3. Compila y ejecuta (F5)

## 🎮 Guía de Uso Rápido

### Para el Adulto (Preparar Ejercicios)

1. Pulsa **F2** para activar modo EDICIÓN
2. Escribe las operaciones:
   ```
   Ejercicios de potencias

   1) 2^2=
   2) 3^2=
   3) 2^2+3^2=
   ```
3. Pulsa **F2** para desactivar modo EDICIÓN
4. Guarda con **CTRL+S**

### Para el Niño (Resolver Ejercicios)

1. Abre el archivo con ejercicios (**CTRL+O**)
2. Navega y completa las respuestas
3. Pulsa **INTRO** en cada operación para validar
4. Verde = ¡Correcto! / Rojo = Ver respuesta correcta

## ⌨️ Atajos de Teclado

| Atajo | Función |
|-------|---------|
| **F2** | Alternar modo EDICIÓN/COMPROBACIÓN |
| **CTRL+S** | Guardar |
| **CTRL+O** | Abrir |
| **CTRL+C/X/V** | Copiar/Cortar/Pegar |
| **INSERT** | Alternar Insertar/Sobreescribir |
| **HOME/END** | Ir a inicio/fin de línea (doble pulsación = absoluto) |
| **TAB/SHIFT+TAB** | Avanzar/Retroceder tab stop |
| **ESC** | Limpiar línea actual |
| **SHIFT+Cursores** | Seleccionar texto |
| **F5** | Graficar función |

## 🛠️ Tecnologías Utilizadas

- **Visual Basic .NET** - Lenguaje principal
- **Windows Forms** - Framework UI
- **GDI+** - Renderizado gráfico personalizado
- **DataTable.Compute** - Evaluación de expresiones matemáticas
- **Método Newton-Raphson** - Resolución de ecuaciones

## 📝 Ejemplos de Uso

### Progresión Pedagógica

**Nivel 1 - Operaciones básicas:**
```
1) 2+2=4
2) 3*5=15
3) 10/2=5
```

**Nivel 2 - Exponentes:**
```
1) 2²=4
2) 3²=9
3) 2³=8
```

**Nivel 3 - Funciones:**
```
1) sin(90)=1
2) cos(0)=1
3) sqrt(16)=4
```

**Nivel 4 - Ecuaciones:**
```
1) 2x+2=12 -> x=5
2) x²=16 -> x=4
3) sin(x)=1 -> x=90
```

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

## 👨‍💻 Autor

Desarrollado con ❤️ para facilitar el aprendizaje de matemáticas.

## 🐛 Reporte de Bugs

Si encuentras algún bug, por favor abre un [issue](https://github.com/TU_USUARIO/pizarra-electronica/issues) con:
- Descripción del problema
- Pasos para reproducirlo
- Comportamiento esperado vs. comportamiento actual
- Capturas de pantalla (si aplica)

## 🔮 Próximas Características

- [x] ✅ Más funciones matemáticas (logaritmos, exponenciales, valor absoluto)
- [x] ✅ Soporte para radianes en funciones trigonométricas
- [x] ✅ Historial de ejercicios completados
- [x] ✅ Estadísticas de progreso
- [x] ✅ Temas de color personalizables (4 temas disponibles)
- [x] ✅ Representación gráfica de ecuaciones (dibujar curvas) con zoom y arrastre
- [ ] Exportar gráficos a imagen
- [ ] Exportar a PDF
- [ ] Modo de examen con temporizador
- [ ] Generador automático de ejercicios
- [ ] Múltiples funciones en un mismo gráfico

---

⭐ Si este proyecto te ha sido útil, considera darle una estrella en GitHub
