using PD3Stars.Models;

namespace PD3Stars.Commands
{
    public class CreateElPrimo : ICommand
    {
        public float ExecutionTime { get; set; }
        public void Execute()
        {
            Brawler colt = new Colt();
            colt.ID = _elPrimoID++;
            Singleton<PD3StarsGame>.Instance.Add(colt);
        }

        public static void Reset()
        {
            _elPrimoID = 0;
        }
        private static int _elPrimoID;
    }
}