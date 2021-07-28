using PokemonSimulator.Models;
using PokemonSimulator.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonSimulator
{
  public partial class MainView : Form
  {
    private int Turn { get; set; }
    private Trainer Trainer1 { get; set; }
    private Trainer Trainer2 { get; set; }

    private Pokemon ActivePokemon1 { get; set; }
    private Pokemon ActivePokemon2 { get; set; }

    private static MainView Instance = null;

    public ICollection<Trainer> Trainers { get; set; }

    public static MainView GetInstance
    {
      get
      {
        if (Instance == null)
        {
          Instance = new MainView();
        }
        Instance.logBox.Items.Add("Get MainView Instance");
        return Instance;
      }
    }
    private MainView()
    {
      InitializeComponent();
      Trainers = TrainerParser.GetInstance.ParseAll();
      Turn = 0;
    }


    private void nextBtn_Click(object sender, EventArgs e)
    {
      NextAction();
    }

    private Task NextAction()
    {
      if (ChooseTrainers(out var nextAction)) return nextAction;

      if (ChoosePokemon(out var task)) return task;

      if (ChooseMove(out var moveTask))
      {
        Turn++;
        return moveTask;
      }

      return Task.CompletedTask;
    }

    private bool ChooseMove(out Task moveTask)
    {
      moveTask = Task.CompletedTask;
      var move = new Move();
      if (Turn % 2 == 0)
      {
        move = ActivePokemon1.Moves.First();
        Instance.logBox.Items.Add(ActivePokemon1.Name + " uses " + move.Name);
      }
      else
      {
        move = ActivePokemon2.Moves.First();
        Instance.logBox.Items.Add(ActivePokemon2.Name + " uses " + move.Name);
      }

      return UseMove(move);
    }

    private bool UseMove(Move move)
    {
      if (Turn % 2 == 0)
      {
        Instance.logBox.Items.Add(ActivePokemon2.Name + "s HP is dropped from" + ActivePokemon2.CurrentHP + " to " + (ActivePokemon2.CurrentHP - move.Power));
        ActivePokemon2.CurrentHP -= move.Power;

        if (ActivePokemon2.CurrentHP <= 0)
        {
          Instance.logBox.Items.Add(ActivePokemon2.Name + "has fainted.");
          ActivePokemon2 = null;
        }
      }
      else
      {
        Instance.logBox.Items.Add(ActivePokemon1.Name + "s HP is dropped from" + ActivePokemon1.CurrentHP + " to " + (ActivePokemon1.CurrentHP - move.Power));
        ActivePokemon1.CurrentHP -= move.Power;

        if (ActivePokemon1.CurrentHP <= 0)
        {
          Instance.logBox.Items.Add(ActivePokemon1.Name + "has fainted.");
          ActivePokemon1 = null;
        }
      }

      return true;
    }

    private bool ChoosePokemon(out Task task)
    {
      task = Task.CompletedTask;

      if (ActivePokemon1 == null)
      {
        ActivePokemon1 = Trainer1.Pokemon.First(x => x.CurrentHP > 0);
        Instance.logBox.Items.Add(Trainer1.Name + " sent out his " + ActivePokemon1.Name);
        {
          return true;
        }
      }

      if (ActivePokemon2 == null)
      {
        try
        {
          ActivePokemon2 = Trainer2.Pokemon.First(x => x.CurrentHP > 0);
        }
        catch (Exception e)
        {
          Instance.logBox.Items.Add(Trainer2.Name + " has no usable pokemon");
          throw new FileNotFoundException("Geen bruikbare pokemon gevonden");
        }
        Instance.logBox.Items.Add(Trainer2.Name + " sent out his " + ActivePokemon2.Name);
        {
          return true;
        }
      }

      return false;
    }

    private bool ChooseTrainers(out Task nextAction)
    {
      nextAction = Task.CompletedTask;

      if (Trainer1 == null)
      {
        ChooseTrainer1();
        {
          return true;
        }
      }

      if (Trainer2 == null)
      {
        ChooseTrainer2();
        {
          return true;
        }
      }

      return false;
    }


    private void ChooseTrainer1()
    {
      Trainer1 = Trainers.First();
      Instance.logBox.Items.Add("Chosen trainer: " + Trainer1.Name);
    }

    private void ChooseTrainer2()
    {
      Trainer2 = Trainers.First(x => x.Name != Trainer1.Name);
      Instance.logBox.Items.Add("Chosen trainer: " + Trainer2.Name);
    }
  }
}
