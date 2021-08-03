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
    private MainView Instance;

    public ChooseTrainersAction(MainView instance)
    {
      Instance = instance;
    }

    public void Execute()
    {
      ChooseTrainer1();
      ChooseTrainer2();

      Instance.State = SimulationState.Trainer1Turn;
    }

    private void ChooseTrainer1()
    {
      Instance.Trainer1 = Instance.Trainers.First();
      Instance.logBox.Items.Add("Chosen trainer: " + Instance.Trainer1.Name);
    }

    private void ChooseTrainer2()
    {
      Instance.Trainer2 = Instance.Trainers.First(x => x.Name != Instance.Trainer1.Name);
      Instance.logBox.Items.Add("Chosen trainer: " + Instance.Trainer2.Name);
    }
  }
}
