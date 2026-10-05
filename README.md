# EJEMPLO - Demo SDK CONTPAQi Comercial Premium 12.10

Proyecto de capacitación para explicar una integración básica con CONTPAQi Comercial Premium mediante una capa de SDK.

## Objetivo

Demostrar el flujo:

1. Recibir datos de un proveedor.
2. Inicializar el SDK.
3. Abrir una empresa.
4. Buscar proveedor por RFC.
5. Crear o actualizar.
6. Mostrar resultado y errores.
7. Cerrar empresa.

> La implementación incluida usa un **modo simulación** para poder ejecutar la capacitación sin depender de una instalación real de CONTPAQi. La clase `ContpaqiSdkService` es el punto donde posteriormente se conectan las funciones nativas del SDK 12.10.

## Requisitos

- .NET 8 SDK
- Visual Studio 2022 o VS Code

## Ejecutar

```bash
dotnet run --project src/DemoSdkContpaqi/DemoSdkContpaqi.csproj
```

## Qué mostrar durante la capacitación

Ejecuta el proyecto una primera vez. El RFC no existe y se crea.

Después ejecuta nuevamente el mismo flujo dentro de la misma sesión. El proveedor ya existe y se actualiza.

Esto permite explicar:

- Separación entre código de negocio y SDK.
- Inicialización y apertura de empresa.
- Búsqueda por RFC.
- Alta vs actualización.
- Manejo de errores.
- Cierre seguro de la empresa.

## Estructura

```text
src/DemoSdkContpaqi/
├── Program.cs
├── Models/
│   ├── ProveedorDto.cs
│   └── ProveedorContpaqi.cs
├── Services/
│   ├── IContpaqiSdk.cs
│   ├── ContpaqiSdkService.cs
│   └── ProveedorService.cs
└── Exceptions/
    └── ContpaqiSdkException.cs
```

## Integración real con SDK 12.10

Cuando se tengan las firmas/DLL exactas del SDK instalado, la sustitución debe hacerse únicamente en:

```text
Services/ContpaqiSdkService.cs
```

El resto del proyecto puede mantenerse igual.
