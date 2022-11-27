namespace PoleTimeGuesser.Api.DataAccess
{
    public interface ISqlDataAccess
    {
        string GetConnectionString(string name);
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        Task SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
        Task<string> SaveUser<T>(string storedProcedure, T parameters, string connectionStringName);
    }
}