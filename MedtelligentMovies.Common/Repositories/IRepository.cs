namespace MedtelligentMovies.Common.Repositories
{
    public interface ITest
    {
        int TestIt();
    }

    public class Test : ITest
    {
        public int TestIt()
        {
            var x = 0;
            var y = x + 1;
            return y;
        }
    }
    public interface IRepository
    {
        
    }
}