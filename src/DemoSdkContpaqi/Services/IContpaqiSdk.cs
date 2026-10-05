using DemoSdkContpaqi.Models;

namespace DemoSdkContpaqi.Services;

public interface IContpaqiSdk
{
    Task InicializarAsync();
    Task AbrirEmpresaAsync(string empresa);
    Task<ProveedorContpaqi?> BuscarProveedorPorRfcAsync(string rfc);
    Task<ProveedorContpaqi> CrearProveedorAsync(ProveedorDto proveedor);
    Task<ProveedorContpaqi> ActualizarProveedorAsync(int idProveedor, ProveedorDto proveedor);
    Task CerrarEmpresaAsync();
}
