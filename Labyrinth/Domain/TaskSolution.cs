using System;
using System.Collections.Generic;
using IOServices.Base;
using IOServices.ServiceFactory;
using Labyrinth.Services;

namespace Labyrinth.Domain
{
    public class TaskSolution : ITaskSolution
    {
        private readonly IOutputService _outputService;
        private readonly IInputService _inputService;
        private readonly ILabyrinthService _labyrinthService;

        public TaskSolution(IOutputServiceFactory outputServiceFactory, IInputServiceFactory inputServiceFactory,
            ILabyrinthService labyrinthService)
        {
            _labyrinthService = labyrinthService;
            _inputService = inputServiceFactory.GetService();
            _outputService = outputServiceFactory.GetService();
        }

        public void  Input(List<ILabyrinth> labyrinthList)
        {
            int l, r, c;
            
            //Input Labyrinths
            while (true)
            {
                _outputService.Output("L R C");

                //Input Parameters
                string? inputString = _inputService.Input();

                var parameters = inputString!.Split(' ');

                if (parameters.Length != 3)
                {
                    throw new FormatException("Wrong Labyrinth Parameters");
                }

                l = Convert.ToInt32(parameters[0]);
                r = Convert.ToInt32(parameters[1]);
                c = Convert.ToInt32(parameters[2]);

                if (l == 0 && r == 0 && c == 0)
                {
                    _outputService.Output(Environment.NewLine);
                    break;
                }

                if (l <= 0 || r <= 0 || c <= 0)
                {
                    throw new FormatException("Wrong Labyrinth Parameters");
                }

                var labyrinth = new Labyrinth(l, r, c);

                _labyrinthService.CreateLabyrinth(labyrinth);

                labyrinthList.Add(labyrinth);

                _outputService.Output(Environment.NewLine);
            }
        }

        public void Output(List<ILabyrinth> labyrinthList)
        {
            if (labyrinthList.Count == 0)
            {
                _outputService.Output("Labyrinth List ist Leer");
                return;
            }

            //Output Result
            _outputService.Output("Ausgabe:\n");

            foreach (var labyrinth in labyrinthList)
            {
                if (!_labyrinthService.BreadthFirstSearch(labyrinth, out List<IQuader> shortestPathList))
                {
                    _outputService.Output("Gefangen :-(\n");
                }
                else
                {
                    var minTime = shortestPathList[1].Value;
                    _outputService.Output($"Entkommen in {minTime} Minute(n)!)\n");
                }
            }
        }
    }
}