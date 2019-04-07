using System.Collections.Generic;

namespace ChallangeX1.Services
{
    public class GlobalDataManager: IGlobalDataManager
    {
        private List<int> GlobalGuesses { get; set; }
        public object locker;
        public GlobalDataManager()
        {
            GlobalGuesses = new List<int>();
            locker = new object();
        }

        public bool CheckGlobalGuesses(int guess)
        {
            lock (locker)
            {
                if (GlobalGuesses.Contains(guess))
                {
                    return true;
                }
                return false;
            }
        }

        public void SetGlobalGuesses(int guess)
        {
            lock (locker)
            {
                if (!GlobalGuesses.Contains(guess))
                {
                    GlobalGuesses.Add(guess);
                }
            }
        }
    }
}
