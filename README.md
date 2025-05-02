# BachataApi

API RESTful desarrollada en **.NET 8** para gestionar **figuras de baile de bachata** y sus respectivos pasos. El sistema implementa autenticación mediante **JWT (JSON Web Tokens)** y permite operaciones CRUD para figuras, pasos y usuarios.

---

##  Características principales

- CRUD de **Figuras** (nombre, detalle, fecha)
- CRUD de **Pasos** asociados a figuras (orden, tiempos, detalle)
- Autenticación con **JWT**
- Soporte de **Refresh Tokens**
- Gestión básica de **usuarios**
- Validaciones con `DataAnnotations`
- Estilo de respuesta centralizado (basado en `ApiControllerBase`)
- Preparado para Swagger (OpenAPI)
- MongoDB como base de datos

---

##  Autenticación JWT

- Registro de usuario (`POST /auth/register`)
- Login de usuario (`POST /auth/login`)
- Refresh de token (`POST /auth/refresh`)

Una vez autenticado, se debe enviar el token JWT en el header:

```
Authorization: Bearer {token}
```

---

##  Estructura de Carpetas

```
├── Controllers
│   └── FiguraController, PasoController, AuthController
├── DTOs
│   └── CreateFiguraDto, UpdateFiguraDto, CreatePasoDto, etc.
├── Models
│   └── Figura, Paso, User
├── Services
│   └── FiguraService, PasoService, UserService, JwtService
├── Helpers
│   └── PasswordHasher, Extensions, ApiControllerBase
├── appsettings.json
├── Program.cs
└── README.md
```

---

## Endpoints Principales

### Figuras

| Método | Ruta             | Descripción                   |
|--------|------------------|-------------------------------|
| GET    | `/api/figuras`   | Obtener todas las figuras     |
| GET    | `/api/figuras/{id}` | Obtener figura por ID      |
| POST   | `/api/figuras`   | Crear nueva figura            |
| PUT    | `/api/figuras/{id}` | Actualizar figura existente |
| DELETE | `/api/figuras/{id}` | Eliminar figura             |

### Pasos

| Método | Ruta                             | Descripción                    |
|--------|----------------------------------|--------------------------------|
| GET    | `/api/figuras/{id}/pasos`        | Listar pasos de una figura     |
| POST   | `/api/figuras/{id}/pasos`        | Agregar paso a figura          |
| PUT    | `/api/pasos/{id}`                | Actualizar paso existente      |
| DELETE | `/api/pasos/{id}`                | Eliminar paso                  |

---

##  Usuarios

| Método | Ruta           | Descripción             |
|--------|----------------|-------------------------|
| POST   | `/auth/register` | Registrar nuevo usuario |
| POST   | `/auth/login`  | Iniciar sesión          |
| POST   | `/auth/refresh`| Obtener nuevo token     |
| GET    | `/api/usuarios`| Listar todos los usuarios (autenticado) |

---

##  Swagger y Postman

- Swagger disponible en `/swagger`
- Para usar endpoints protegidos desde Swagger:
  - Hacé clic en el botón **Authorize**
  - Pegá el token: `Bearer {token}`

---

##  Base de Datos

Usa **MongoDB**. La configuración de conexión se define en `appsettings.json`.

---

##  Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- MongoDB local o remoto
- Visual Studio o VS Code

---

##  Futuras mejoras

- Roles de usuario
- Upload de videos por paso
- Dashboard estadístico
- Sistema de puntuación y favoritos

---

##  Licencia

MIT License
