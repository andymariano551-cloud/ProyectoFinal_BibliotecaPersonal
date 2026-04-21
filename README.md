Read

BIBLIOTECA PERSONAL es una app movil desarrollada con .NET MAUI, esta permite a los usuarios gestionar de manera eficiente su coleccion de libros. La app combina almacenamiento local, consumo de APIs externas y una interfaz moderna e intuitiva para ofrecer una experiencia completa de organizacion y seguimiento de lecturas.


Sistema completo de gestion de libros mediante una base de datos con SQLITE, donde cada libro es representado por un modelo detallado con  multiples propiedades. Los usuarios pueden realizar operaciones como CRUD, aplicar filtros y visualizar estadisticas sobre su coleccion.

FUNCIONALIDADES PRINCIPALES:

Gestión de libros (CRUD completo):
Permite agregar, visualizar, editar y eliminar libros dentro de la biblioteca personal.

Búsqueda con API externa:
Integración con Google Books API para buscar libros por título o autor y agregarlos fácilmente a la colección local.

Base de datos SQLite:
Almacenamiento persistente de la información de los libros, con un modelo completo que incluye múltiples propiedades.

Estadísticas dinámicas:
Visualización de datos mediante gráficos:
Libros leídos vs pendientes (gráfico circular)
Distribución por género (gráfico de barras)

Navegación multi-página:
Implementada con Shell, incluyendo un menú lateral (FlyoutMenu) para acceder a todas las secciones.

Interfaz moderna y personalizable:
Uso de estilos globales y soporte para temas claro/oscuro mediante AppThemeBinding.

Arquitectura MVVM:
Uso de ViewModels, comandos y programación asíncrona (async/await) para una estructura limpia y organizada.

AQUI INSTRUCCIONES DE EJECUCION:

Abre Visual Studio
Haz clic en “Abrir un proyecto o solución”
Selecciona el archivo .sln
En la parte superior, elige dónde correr la app:
Windows (para escritorio)
Android Emulator
Dispositivo físico
Clic derecho sobre el proyecto principal
Selecciona: “Establecer como proyecto de inicio”
Presiona F5
o
Haz clic en el botón verde ▶️ “Iniciar”


SCREENSHOTS PRINCIPALES

 ## Pantalla principal
(imagenes/iamgen4.jpeg)

## Agregar Libros
![Add Book] (imagenes/imagen3.jpeg)

##  Buscar libros
![Search](imagenes/imagen2.jpeg)

## Estadisticas
![Stats](imagenes/imagen1.jpeg)
