using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonSimulator.Models;

namespace PokemonSimulator.Actions
{
  public class ChooseTrainersAction: ISimulatorAction
  {
    private readonly MainView Instance;

    public ChooseTrainersAction(MainView instance)
    {
      Instance = instance;
    }

    public void Execute()
    {
      if (Instance.State == SimulationState.Trainer1Win)
        Instance.Trainer2Index++;

      if (Instance.State == SimulationState.Trainer2Win)
        Instance.Trainer1Index++;

      if (Instance.Trainer2Index > Instance.Trainers.Count - 2)
      {
        Instance.Log("Trainer: " + Instance.Trainer1.Name + " has battled every opponent");
        Instance.Trainer1Index++;
        Instance.Trainer2Index = 0;
      }

      if (Instance.Trainer1Index > Instance.Trainers.Count - 1)
      {
        Instance.Log("All battles done!");
        Instance.State = SimulationState.Done;
        return;
      }
      ChooseTrainer1();
      ChooseTrainer2();

      Instance.State = SimulationState.Trainer1Turn;
    }

    private void ChooseTrainer1()
    {
      Instance.Trainer1 = Instance.Trainers[Instance.Trainer1Index].CreateCopy();
      Instance.Log("Chosen trainer: " + Instance.Trainer1.Name);
    }

    private void ChooseTrainer2()
    {
      Instance.Trainer2 = Instance.Trainers.Where(x => x.Name != Instance.Trainer1.Name).ToList()[Instance.Trainer2Index].CreateCopy();
      Instance.Log("Chosen trainer: " + Instance.Trainer2.Name);
    }
  }
}
