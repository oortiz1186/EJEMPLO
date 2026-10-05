namespace DemoSdkContpaqi.Exceptions;

public class ContpaqiSdkException : Exception
{
    public int Codigo { get; }

    public ContpaqiSdkException(int codigo, string mensaje)
        : base(mensaje)
    {
        Codigo = codigo;
    }
}
