using DemoSdkContpaqi.Exceptions;
using DemoSdkContpaqi.Models;
using DemoSdkContpaqi.Services;

Console.Title = "Demo SDK CONTPAQi Comercial Premium 12.10";

Console.WriteLine("=================================================");
Console.WriteLine(" DEMO SDK CONTPAQi COMERCIAL PREMIUM 12.10");
Console.WriteLine(" Alta y actualización de proveedor");
Console.WriteLine("=================================================");
Console.WriteLine();

IContpaqiSdk sdk = new ContpaqiSdkService();
var proveedorService = new ProveedorService(sdk);

var empresa = @"C:\Compac\Empresas\ad_EMPRESA_DEMO";

var proveedor = new ProveedorDto
{
    Codigo = "PRV001",
    Rfc = "XAXX010101000",
    RazonSocial = "PROVEEDOR CAPACITACION SA DE CV",
    Correo = "demo@proveedor.com",
    Activo = true
};

try
{
    Console.WriteLine("PRIMERA OPERACIÓN");
    Console.WriteLine("-----------------");

    var resultado1 = await proveedorService.ProcesarAsync(
        proveedor,
        empresa);

    MostrarResultado(resultado1);

    Console.WriteLine();
    Console.WriteLine("SEGUNDA OPERACIÓN CON EL MISMO RFC");
    Console.WriteLine("----------------------------------");

    proveedor.Correo = "nuevo-correo@proveedor.com";

    var resultado2 = await proveedorService.ProcesarAsync(
        proveedor,
        empresa);

    MostrarResultado(resultado2);
}
catch (ContpaqiSdkException ex)
{
    Console.WriteLine();
    Console.WriteLine($"[ERROR SDK] Código: {ex.Codigo}");
    Console.WriteLine($"[ERROR SDK] Mensaje: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine($"[ERROR GENERAL] {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Demo terminada.");

static void MostrarResultado(ProveedorContpaqi proveedor)
{
    Console.WriteLine();
    Console.WriteLine("[RESULTADO]");
    Console.WriteLine($"ID:           {proveedor.Id}");
    Console.WriteLine($"Código:       {proveedor.Codigo}");
    Console.WriteLine($"RFC:          {proveedor.Rfc}");
    Console.WriteLine($"Razón social: {proveedor.RazonSocial}");
    Console.WriteLine($"Correo:       {proveedor.Correo}");
    Console.WriteLine($"Activo:       {proveedor.Activo}");
}
