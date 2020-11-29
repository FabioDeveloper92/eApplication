namespace Infrastructure.Core
{
    public enum UnitOfWorkStatus
    {
        NotInitialized = 0,
        Pending = 1,
        Committed = 2,
        Rollbacked = 3
    }
}
