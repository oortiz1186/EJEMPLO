namespace DemoSdkContpaqi.Models;

public class ProveedorContpaqi
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Rfc { get; set; } = string.Empty;
    public string RazonSocial { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public bool Activo { get; set; }
}
