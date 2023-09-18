namespace SelfFianceServer.Services.Interfaces
{
    public interface ICommonService
    {
        StringContent Serialize<T>(T obj) where T : class;

        Task<bool> CallDeletingDialog(string objectType, string objectString);
    }
}
