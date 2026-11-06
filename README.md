# Notas MVVM - Persistencia local con SQLite

Aplicación .NET MAUI integradora del primer parcial con **Shell**, **MVVM** y **SQLite**.

## Modelo de Datos

```
┌──────────────────────┐       ┌──────────────────────────┐
│        Nota           │       │        Historial          │
├──────────────────────┤       ├──────────────────────────┤
│ Id (PK, AutoIncr)    │       │ Id (PK, AutoIncrement)   │
│ Titulo (max 100)     │       │ Accion (string)          │
│ Contenido (string)   │       │ Descripcion (string)     │
│ FechaCreacion (DateTime)│    │ Fecha (DateTime)         │
└──────────────────────┘       └──────────────────────────┘
```

## Arquitectura MVVM

```
┌─────────────────────────────────────────────────┐
│  VIEWS (XAML + compiled bindings)                │
│  NotasPage ─── HistorialPage ─── DetalleNotaPage│
│       │              │                │          │
│       ▼              ▼                ▼          │
│  VIEWMODELS (CommunityToolkit.Mvvm)              │
│  NotasViewModel  HistorialViewModel  DetalleVM   │
│       │              │                │          │
│       └──────────────┼────────────────┘          │
│                      ▼                           │
│  SERVICES                                        │
│  NotasDatabase (SQLiteAsyncConnection)           │
│       │                                          │
│       ▼                                          │
│  MODELS                                          │
│  Nota ──── Historial                             │
└─────────────────────────────────────────────────┘
```

## Estructura del Proyecto

```
NotasMVVM/
├── Models/
│   ├── Nota.cs              # Modelo de nota
│   └── Historial.cs         # Modelo de historial
├── Services/
│   └── NotasDatabase.cs     # Servicio SQLite (CRUD + historial)
├── ViewModels/
│   ├── NotasViewModel.cs    # Lista, búsqueda, eliminar
│   ├── DetalleNotaViewModel.cs  # Crear/Editar nota
│   └── HistorialViewModel.cs    # Listado de historial
├── Views/
│   ├── NotasPage.xaml/.cs       # Tab Notas con SearchBar
│   ├── DetalleNotaPage.xaml/.cs # Formulario de nota
│   └── HistorialPage.xaml/.cs   # Tab Historial
├── AppShell.xaml/.cs        # Shell con pestañas y ruta
├── MauiProgram.cs           # Inyección de dependencias
└── App.xaml.cs              # Punto de entrada
```

## Funcionalidades

- CRUD completo de notas con SQLite
- Historial de acciones (crear, editar, eliminar)
- Shell con pestañas: Notas e Historial
- Validación: título obligatorio, máximo 100 caracteres
- Búsqueda por título con SearchBar
- Confirmación antes de eliminar
- Patrón MVVM con CommunityToolkit.Mvvm

## Capturas

### Pantalla de Notas
*(Captura de la lista de notas con SearchBar)*

### Detalle de Nota
*(Captura del formulario de crear/editar nota)*

### Historial
*(Captura del historial de acciones)*

## Tecnologías

- .NET MAUI (.NET 10)
- SQLite (sqlite-net-pcl + SQLitePCLRaw.bundle_green)
- CommunityToolkit.Mvvm 8.4.0
- Shell Navigation
- Compiled Bindings (x:DataType)
