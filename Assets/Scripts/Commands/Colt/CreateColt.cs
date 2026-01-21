using PD3Stars.Models;

namespace PD3Stars.Commands
{
    public class CreateColt : ICommand
    {
        public float ExecutionTime { get; set; }
        public void Execute()
        {
            Brawler colt = new Colt();
            colt.ID = _coltID++;
            Singleton<PD3StarsGame>.Instance.Add(colt);
        }

        public static void Reset()
        {
            _coltID = 0;
        }
        private static int _coltID;
    }
}