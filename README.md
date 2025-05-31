# ReportesMVC (.NET 8 + Razor Pages + MySQL)

![Image_Alt](https://github.com/jaykob07/ASPCORE-MYSQ-LOGIN/blob/81eca062821cda742373c2f887ee53879283a77c/pic.png)

![Image_Alt](https://github.com/jaykob07/ASPCORE-MYSQ-LOGIN/blob/e22ffeb595c2f5336704345267366dbd8b3b5ca5/pic2.png)

![Image_Alt](https://github.com/jaykob07/ASPCORE-MYSQ-LOGIN/blob/823ccff74dc330cf8d6b38efeb1da445ad2f9898/pic3.png)

Proyecto MVC con Razor Pages en .NET 8, utilizando **Pomelo.EntityFrameworkCore.MySql** para conexión con base de datos **MySQL**, ejecutado y configurado sobre **Mac (M1/M2)**.

## ✅ Requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [MySQL Server](https://dev.mysql.com/downloads/mysql/)
* [MySQL Workbench](https://dev.mysql.com/downloads/workbench/) 
* Git

---

## 🚀 Instalación

### 1. Clonar el proyecto

```bash
git clone https://github.com/tu-usuario/ReportesMVC.git
cd ReportesMVC
```

### 2. Verificar o cambiar el origen remoto (si es un fork)

```bash
git remote -v
git remote set-url origin https://github.com/tu-usuario/ReportesMVC.git
```

---

## 🧩 Paquetes NuGet

Se utiliza **Pomelo** para compatibilidad óptima con MySQL en .NET 8 y arquitectura ARM ya que ese fue mi caso:

```bash
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.5
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0
```

Evitar el uso de `MySql.EntityFrameworkCore`, ya que genera errores en tiempo de ejecución en Mac ARM (como `System.TypeLoadException` o problemas con `mysql_native_password`).

---

## 🛠️ Configuración del proyecto

### 1. appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BDReportes;User=root;Password=tu_contraseña;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### 2. ApplicationDbContext.cs

```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Persona> Personas { get; set; }
}
```

### 3. Program.cs

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 34))
    ));
```

---

## 🧱 Migraciones

### Crear migraciones iniciales

```bash
dotnet ef migrations add Init
dotnet ef database update
```

> Si necesitas cambiar el nombre de una columna como me ocurrio :

1. Modifica la propiedad en el modelo.
2. Crea una nueva migración:

```bash
dotnet ef migrations add RenameTelefonoColumn
dotnet ef database update
```

---

## 👤 Crear usuario manualmente

Desde **MySQL Workbench** o la terminal:

```sql
USE BDReportes;

INSERT INTO Usuarios (Correo, Contrasena)
VALUES ('admin@bento.com', 'admin1234');
```

> Asegúrate de haber seleccionado la base de datos antes de ejecutar (`USE BDReportes;`), o verás el error **"No database selected"**.

---

## 📋 Estructura esperada de la tabla `Personas`

```sql
CREATE TABLE Personas (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100),
    ApPaterno VARCHAR(100),
    ApMaterno VARCHAR(100),
    IIdSexo INT,
    Correo VARCHAR(100),
    TelefonoOCelular VARCHAR(50),
    IIdTipoDocumento INT,
    NumeroIdentificacion VARCHAR(50)
);
```

---

## 🧪 Verificar conexión

```bash
dotnet build
dotnet run
```

Si todo está bien, verás algo como:

```
Now listening on: http://localhost:5114
Application started. Press Ctrl+C to shut down.
```

---

## 📤 Git y repositorio remoto

1. Asegúrate de tener el control remoto correcto:

```bash
git remote set-url origin https://github.com/tu-usuario/ReportesMVC.git
```

2. Hacer push de los cambios:

```bash
git add .
git commit -m "Primer commit con migraciones y configuración"
git push origin main
```

---

## 📌 Observaciones


* Los cambios en modelos requieren **una nueva migración** y `dotnet ef database update`.

---

## 🔗 Recursos útiles

* [Documentación de Pomelo](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
* [EF Core Migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
* [Guía para dotnet-ef](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)


