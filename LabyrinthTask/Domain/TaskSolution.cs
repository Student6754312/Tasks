using System;
using System.Collections.Generic;
using System.Linq;
using IOServices.Interfaces;
using IOServices.ServiceFactory;
using LabyrinthTask.Services;

namespace LabyrinthTask.Domain
{
    public class TaskSolution : ITaskSolution
    {
        private readonly IOutputService _outputService;
        private readonly IInputService _inputService;
        private readonly ILabyrinthService _labyrinthService;

        public TaskSolution(IOutputServiceFactory outputServiceFactory, IInputServiceFactory inputServiceFactory,
            ILabyrinthService labyrinthService)
        {
            _outputService = outputServiceFactory.GetService();
            _labyrinthService = labyrinthService;
            _inputService = inputServiceFactory.GetService();

        }

        public void Input(List<ILabyrinth> labyrinthList)
        {
            int l, r, c;

            //Input Labyrinths
            while (true)
            {
                _outputService.Output("L R C");

                //Input Parameters
                string? inputString = _inputService.Input();

                var parameters = inputString!.Split(' ').Where(s => s != "").ToArray();

                if (parameters.Length != 3)
                {
                    throw new FormatException("Wrong Labyrinth Parameters");
                }

                l = Convert.ToInt32(parameters[0]);
                r = Convert.ToInt32(parameters[1]);
                c = Convert.ToInt32(parameters[2]);

                if (l == 0 && r == 0 && c == 0)
                {
                    _outputService.Output("");
                    break;
                }

                if (l <= 0 || r <= 0 || c <= 0)
                {
                    throw new FormatException("Wrong Labyrinth Parameters");
                }

                var labyrinth = new Labyrinth(l, r, c);

                _labyrinthService.CreateLabyrinth(labyrinth);

                labyrinthList.Add(labyrinth);

                _outputService.Output("");
            }
        }

        public void Output(List<ILabyrinth> labyrinthList)
        {
            if (labyrinthList.Count == 0)
            {
                _outputService.Output("Labyrinth List ist Leer\n");
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