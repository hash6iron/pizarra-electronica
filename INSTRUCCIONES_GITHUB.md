# Instrucciones para Subir a GitHub

## Opción 1: Usar GitHub Desktop (Recomendado para principiantes)

1. Descarga e instala GitHub Desktop desde: https://desktop.github.com/
2. Abre GitHub Desktop
3. Ve a: File → Add Local Repository
4. Selecciona la carpeta de tu proyecto
5. Haz clic en "Create Repository"
6. Escribe un commit message: "Initial commit - Pizarra Electrónica completa"
7. Haz clic en "Commit to main"
8. Haz clic en "Publish repository"
9. Marca/desmarca "Keep this code private" según prefieras
10. Haz clic en "Publish repository"

## Opción 2: Usar Git desde línea de comandos

### Paso 1: Instalar Git
Si no tienes Git instalado, descárgalo desde: https://git-scm.com/download/win

### Paso 2: Inicializar repositorio local
Abre PowerShell en la carpeta del proyecto y ejecuta:

```powershell
git init
git add .
git commit -m "Initial commit - Pizarra Electrónica completa"
```

### Paso 3: Crear repositorio en GitHub
1. Ve a https://github.com/new
2. Nombre del repositorio: `pizarra-electronica`
3. Descripción: "Aplicación educativa de pizarra electrónica para práctica de cálculos matemáticos"
4. Elige público o privado
5. NO marques "Add a README" ni "Add .gitignore" (ya los tenemos)
6. Haz clic en "Create repository"

### Paso 4: Conectar y subir
GitHub te mostrará comandos. Usa estos (reemplaza YOUR_USERNAME):

```powershell
git remote add origin https://github.com/YOUR_USERNAME/pizarra-electronica.git
git branch -M main
git push -u origin main
```

## Opción 3: Usar Visual Studio

1. En Visual Studio, ve a: Git → Create Git Repository
2. Selecciona "GitHub"
3. Inicia sesión con tu cuenta de GitHub
4. Rellena los datos:
   - Repository name: `pizarra-electronica`
   - Description: "Aplicación educativa de pizarra electrónica"
   - Visibility: Public o Private
5. Haz clic en "Create and Push"

## Verificar que se subió correctamente

Una vez subido, verifica en GitHub que tienes:
- ✅ Todos los archivos .vb
- ✅ Archivos .Designer.vb
- ✅ Archivo .vbproj
- ✅ Archivo .sln
- ✅ README.md
- ✅ LICENSE
- ✅ .gitignore

## Archivos importantes del proyecto

Estos son los archivos principales que deben estar en el repositorio:

```
pizarra-electronica/
├── .gitignore              (Creado ✓)
├── LICENSE                 (Creado ✓)
├── README.md               (Creado ✓)
├── Profe.sln
├── Profe.vbproj
├── Form1.vb
├── Form1.Designer.vb
├── My Project/
│   ├── Application.Designer.vb
│   ├── AssemblyInfo.vb
│   ├── Resources.Designer.vb
│   └── Settings.Designer.vb
└── INSTRUCCIONES_GITHUB.md (Este archivo)
```

## Actualizar el repositorio después de cambios

Cuando hagas cambios en el futuro:

```powershell
git add .
git commit -m "Descripción de los cambios"
git push
```

## ¿Necesitas ayuda?

- Documentación de Git: https://git-scm.com/doc
- Guías de GitHub: https://guides.github.com/
- GitHub Desktop: https://docs.github.com/en/desktop

---

**Nota**: Recuerda actualizar el README.md con tu nombre de usuario de GitHub en los enlaces después de crear el repositorio.
