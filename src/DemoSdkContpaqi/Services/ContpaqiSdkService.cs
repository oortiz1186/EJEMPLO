using DemoSdkContpaqi.Exceptions;
using DemoSdkContpaqi.Models;

namespace DemoSdkContpaqi.Services;

/// <summary>
/// Simulación del SDK de CONTPAQi.
/// Reemplazar internamente estos métodos por las llamadas reales del SDK 12.10.
/// </summary>
public class ContpaqiSdkService : IContpaqiSdk
{
    private readonly List<ProveedorContpaqi> _proveedores = new();
    private bool _inicializado;
    private bool _empresaAbierta;
    private int _secuencia = 1000;

    public Task InicializarAsync()
    {
        Console.WriteLine("[SDK] Inicializando SDK CONTPAQi 12.10...");
        _inicializado = true;
        return Task.CompletedTask;
    }

    public Task AbrirEmpresaAsync(string empresa)
    {
        ValidarInicializacion();

        if (string.IsNullOrWhiteSpace(empresa))
            throw new ContpaqiSdkException(1001, "La ruta de empresa es obligatoria.");

        Console.WriteLine($"[SDK] Abriendo empresa: {empresa}");
        _empresaAbierta = true;

        return Task.CompletedTask;
    }

    public Task<ProveedorContpaqi?> BuscarProveedorPorRfcAsync(string rfc)
    {
        ValidarEmpresaAbierta();

        Console.WriteLine($"[SDK] Buscando proveedor por RFC: {rfc}");

        var proveedor = _proveedores.FirstOrDefault(
            x => x.Rfc.Equals(rfc, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(proveedor);
    }

    public Task<ProveedorContpaqi> CrearProveedorAsync(ProveedorDto proveedor)
    {
        ValidarEmpresaAbierta();
        ValidarProveedor(proveedor);

        var existente = _proveedores.FirstOrDefault(
            x => x.Rfc.Equals(proveedor.Rfc, StringComparison.OrdinalIgnoreCase));

        if (existente is not null)
            throw new ContpaqiSdkException(
                2001,
                $"Ya existe un proveedor con RFC {proveedor.Rfc}.");

        var nuevo = new ProveedorContpaqi
        {
            Id = ++_secuencia,
            Codigo = proveedor.Codigo,
            Rfc = proveedor.Rfc,
            RazonSocial = proveedor.RazonSocial,
            Correo = proveedor.Correo,
            Activo = proveedor.Activo
        };

        _proveedores.Add(nuevo);

        Console.WriteLine($"[SDK] Proveedor creado. ID CONTPAQi: {nuevo.Id}");

        return Task.FromResult(nuevo);
    }

    public Task<ProveedorContpaqi> ActualizarProveedorAsync(
        int idProveedor,
        ProveedorDto proveedor)
    {
        ValidarEmpresaAbierta();
        ValidarProveedor(proveedor);

        var existente = _proveedores.FirstOrDefault(x => x.Id == idProveedor);

        if (existente is null)
            throw new ContpaqiSdkException(
                2002,
                $"No existe el proveedor con ID {idProveedor}.");

        existente.Codigo = proveedor.Codigo;
        existente.RazonSocial = proveedor.RazonSocial;
        existente.Correo = proveedor.Correo;
        existente.Activo = proveedor.Activo;

        Console.WriteLine($"[SDK] Proveedor actualizado. ID CONTPAQi: {existente.Id}");

        return Task.FromResult(existente);
    }

    public Task CerrarEmpresaAsync()
    {
        if (_empresaAbierta)
        {
            Console.WriteLine("[SDK] Cerrando empresa...");
            _empresaAbierta = false;
        }

        return Task.CompletedTask;
    }

    private void ValidarInicializacion()
    {
        if (!_inicializado)
            throw new ContpaqiSdkException(
                1000,
                "El SDK no ha sido inicializado.");
    }

    private void ValidarEmpresaAbierta()
    {
        ValidarInicializacion();

        if (!_empresaAbierta)
            throw new ContpaqiSdkException(
                1002,
                "No hay una empresa abierta.");
    }

    private static void ValidarProveedor(ProveedorDto proveedor)
    {
        if (string.IsNullOrWhiteSpace(proveedor.Rfc))
            throw new ContpaqiSdkException(3001, "El RFC es obligatorio.");

        if (string.IsNullOrWhiteSpace(proveedor.RazonSocial))
            throw new ContpaqiSdkException(
                3002,
                "La razón social es obligatoria.");
    }
}
