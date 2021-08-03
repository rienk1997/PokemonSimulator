using PokemonSimulator.Models;
using PokemonSimulator.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PokemonSimulator.Actions;

namespace PokemonSimulator
{
  public partial class MainView : Form
  {
    public SimulationState State { get; set; }
    public int Turn { get; set; }
    public Trainer Trainer1 { get; set; }
    public Trainer Trainer2 { get; set; }
    public Pokemon ActivePokemon1 { get; set; }
    public Pokemon ActivePokemon2 { get; set; }

    private static MainView Instance;

    public ICollection<Trainer> Trainers { get; set; }

    public static MainView GetInstance
    {
      get
      {
        if (Instance == null)
        {
          Instance = new MainView();
          Instance.logBox.Items.Add("Initiating MainView Instance");
        }
        return Instance;
      }
    }
    private MainView()
    {
      InitializeComponent();
      Trainers = TrainerParser.GetInstance.ParseAll();
      State = SimulationState.Start;
      Turn = 0;
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
  }
}
