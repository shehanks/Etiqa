namespace Etiqa.Services.Contract
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        void RemoveByPrefix(string prefix);
        bool SetData<T>(string key, T value, DateTimeOffset? expirationTime = null);
        bool RemoveData(string key);
    }
}