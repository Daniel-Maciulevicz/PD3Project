
namespace PD3Stars.Commands
{
    public interface ICommand
    {
        public float ExecutionTime { get; set; }
        public void Execute();
    }
}