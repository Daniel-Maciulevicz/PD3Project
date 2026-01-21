using System.Collections.Generic;
using UnityEngine;

namespace PD3Stars.Commands
{
    public class CommandManager
    {
        public List<ICommand> Commands = new List<ICommand>();

        public float StartTime { get; private set; }
        public float ElapsedTime { get { return Time.time - StartTime; } }

        public bool IsReplaying { get; private set; }

        public void Execute(ICommand command)
        {
            if (IsReplaying) return;

            command.ExecutionTime = ElapsedTime;
            Commands.Add(command);
            command.Execute();
        }

        public void Replay()
        {
            if (Commands.Count == 0) return;

            ResetCommands();

            IsReplaying = true;
            _replayIndex = 0;
        }
        public void ReplayUntil(float time)
        {
            while (_replayIndex < Commands.Count && Commands[_replayIndex].ExecutionTime < time)
            {
                Commands[_replayIndex].Execute();
                _replayIndex++;
            }
            if (_replayIndex >= Commands.Count)
            {
                IsReplaying = false;
            }
        }
        private int _replayIndex;

        private void ResetCommands()
        {
            CreateColt.Reset();
            CreateElPrimo.Reset();
        }

        private CommandManager()
        {
            StartTime = Time.time;
        }
    }
}