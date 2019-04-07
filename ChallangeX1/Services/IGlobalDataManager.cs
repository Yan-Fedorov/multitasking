namespace ChallangeX1.Services
{
    public interface IGlobalDataManager
    {
        bool CheckGlobalGuesses(int guess);
        void SetGlobalGuesses(int guess);
    }
}
