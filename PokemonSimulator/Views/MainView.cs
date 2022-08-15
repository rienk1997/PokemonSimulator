using PokemonSimulator.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PokemonSimulator.Actions;
using PokemonSimulator.Controllers;

namespace PokemonSimulator
{
  public partial class MainView : Form
  {
    public SimulationState State { get; set; }
    public int Turn { get; set; }
    public int Trainer1Index { get; set; }
    public int Trainer2Index { get; set; }
    public Trainer Trainer1 { get; set; }
    public Trainer Trainer2 { get; set; }
    public TrainerPokemon ActivePokemon1 { get; set; }
    public TrainerPokemon ActivePokemon2 { get; set; }

    private static MainView Instance;

    public DataController DataController { get; set; }

    public static MainView GetInstance
    {
      get
      {
        if (Instance == null)
        {
          Instance = new MainView();
          Instance.Log("Initiating MainView Instance");
        }
        return Instance;
      }
    }
    private MainView()
    {
      InitializeComponent();

      DataController = DataController.GetInstance;

      State = SimulationState.Start;
      Turn = 0;
      Trainer1Index = 0;
      Trainer2Index = 0;
    }


    private void nextBtn_Click(object sender, EventArgs e)
    {
      NextAction();
    }

    private void NextAction()
    {
      var action = ChooseAction(State);

      action.Execute();
    }

    private ISimulatorAction ChooseAction(SimulationState state)
    {
      switch (state)
      {
        case SimulationState.Start:
        case SimulationState.Trainer1Win:
        case SimulationState.Trainer2Win:
          return new ChooseTrainersAction(Instance);
        case SimulationState.Trainer1Turn:
          return new TrainerTurnAction(Trainer1, ActivePokemon1, ActivePokemon2, Instance);
        case SimulationState.Trainer2Turn:
          return new TrainerTurnAction(Trainer2, ActivePokemon2, ActivePokemon1, Instance);
        default:
          throw new ArgumentOutOfRangeException(nameof(state), state, "State is not handled properly");
      }
    }

    public void FlipTurn()
    {
      if (State == SimulationState.Trainer1Turn)
      {
        State = SimulationState.Trainer2Turn;
        return;
      }

      if (State == SimulationState.Trainer2Turn)
      {
        State = SimulationState.Trainer1Turn;
      }
    }

    public void RemovePokemon()
    {
      ActivePokemon1 = null;
      ActivePokemon2 = null;
    }

    public void Log(string message)
    {
      if(Instance.logBox.Items.Count > 15)
        Instance.logBox.Items.RemoveAt(0);
      Instance.logBox.Items.Add(message);
    }
  }
}
