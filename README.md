# Tarius - Sistema de Gestión de Administradores

Tarius es una aplicación web desarrollada en ASP.NET Core MVC, diseñada para la gestión de usuarios administradores. El sistema permite crear, editar, eliminar y visualizar administradores, además de gestionar roles y autenticar usuarios.

---

##  **Despliegue del Proyecto**

###  **Requerimientos Previos**
- .NET 6.0 o superior
- Visual Studio 2022
- SQL Server
- Sass (para compilar los archivos SCSS)

###  **Instalación de Dependencias**
1. Clonar el repositorio:
   ```bash
   git clone https://github.com/Hmateo205/Tarius.git
   cd Tarius
   ```

2. Restaurar paquetes:
   ```bash
   dotnet restore
   ```

3. Instalar herramientas de Entity Framework:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. Configurar la cadena de conexión en `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=TariusDB;Trusted_Connection=True;"
   }
   ```

5. Crear la base de datos y aplicar migraciones:
   ```bash
   dotnet ef database update
   ```

### 🛠️ **Compilar y Ejecutar**
1. Compilar el proyecto:
   ```bash
   dotnet build
   ```
2. Ejecutar el proyecto:
   ```bash
   dotnet run
   ```
3. Acceder desde el navegador:
   ```
   http://localhost:5000
   ```

###  **Compilación de SCSS a CSS**
1. Instalar Sass:
   ```bash
   npm install -g sass
   ```
2. Compilar SCSS:
   ```bash
   sass wwwroot/sass/main.scss wwwroot/css/main.css
   ```

---

##  **Estructura del Proyecto**
```
Tarius/
├── Controllers/     # Lógica del servidor
├── Models/          # Modelos de datos
├── Views/           # Vistas en Razor
├── wwwroot/         # Archivos estáticos (CSS, JS, imágenes)
└── Data/            # Contexto de la base de datos
```

---

##  **Funcionalidades Principales**
- **CRUD de Administradores:**
  - Crear, Editar, Eliminar y Listar administradores.
  - Asignación de roles: SuperAdmin, Admin, Usuario.
  
- **Autenticación:**
  - Login y Logout de administradores.
  - Protección de rutas usando middleware de autenticación.

- **Dashboard Dinámico:**
  - Acceso según el estado de autenticación.
  - Navegación entre vistas protegidas.

---

##  **Comentarios sobre los Commits**
| Commit                                 | Descripción                                      |
|----------------------------------------|--------------------------------------------------|
| Inicialización del Proyecto             | Creación de la estructura básica de ASP.NET Core. |
| Implementación del Login                | Validación de credenciales en el backend.         |
| Implementación del Dashboard            | Creación de la vista principal del panel.         |
| CRUD de Administradores                 | Gestión completa de usuarios desde el dashboard.  |
| Integración de Roles en Administradores | Añadido el campo "Rol" en el modelo de administrador. |
| Mejoras de Estilo con SASS               | Implementación de SCSS para mejorar la UI.        |
| Fix de Rutas en el Layout               | Actualización de los enlaces en la barra de navegación. |
| Actualización del README                | Documentación de despliegue y estructura del proyecto. |

---

##  **Consideraciones de Seguridad**
- Las contraseñas se almacenan de forma segura utilizando algoritmos de hashing.
- Validaciones en el backend para garantizar la integridad de los datos.
- Uso de middleware de autenticación para proteger el acceso al dashboard.

---

##  **Contribuciones**
Las contribuciones son bienvenidas. Por favor, sigue el flujo estándar de GitHub:
1. Crea un fork del repositorio.
2. Realiza los cambios en una nueva rama.
3. Envía un pull request detallando los cambios realizados.
