using DemoSdkContpaqi.Models;

namespace DemoSdkContpaqi.Services;

public class ProveedorService
{
    private readonly IContpaqiSdk _sdk;

    public ProveedorService(IContpaqiSdk sdk)
    {
        _sdk = sdk;
    }

    public async Task<ProveedorContpaqi> ProcesarAsync(
        ProveedorDto proveedor,
        string empresa)
    {
        await _sdk.InicializarAsync();

        try
        {
            await _sdk.AbrirEmpresaAsync(empresa);

            var existente =
                await _sdk.BuscarProveedorPorRfcAsync(proveedor.Rfc);

            if (existente is null)
            {
                Console.WriteLine("[APP] El proveedor no existe. Se realizará ALTA.");

                return await _sdk.CrearProveedorAsync(proveedor);
            }

            Console.WriteLine(
                $"[APP] Proveedor encontrado con ID {existente.Id}. Se realizará ACTUALIZACIÓN.");

            return await _sdk.ActualizarProveedorAsync(
                existente.Id,
                proveedor);
        }
        finally
        {
            await _sdk.CerrarEmpresaAsync();
        }
    }
}
