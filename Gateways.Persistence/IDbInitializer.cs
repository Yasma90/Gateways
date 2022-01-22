namespace Gateways.Persistence
{
    public interface IDbInitializer
    {
        void Initialize();
        void SeedData();
    }
}