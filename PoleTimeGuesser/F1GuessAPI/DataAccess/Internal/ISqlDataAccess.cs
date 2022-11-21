namespace F1GuessAPI.DataAccess.Internal
{
    public interface ISqlDataAccess
    {
       
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        void SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
        int SaveUser<T>(string storedProcedure, T parameters, string connectionStringName);
    }
}