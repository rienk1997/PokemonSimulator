using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonSimulator.Models;

namespace PokemonSimulator.Actions
{
  public class TrainerTurnAction: ISimulatorAction
  {
    private readonly Trainer _trainer;
    private readonly Pokemon _pokemon;

    private readonly Pokemon _enemyPokemon;

    private readonly MainView Instance;
    public TrainerTurnAction(Trainer trainer, Pokemon pokemon, Pokemon enemyPokemon, MainView instance)
    {
      _trainer = trainer;
      _pokemon = pokemon;
      Instance = instance;
      _enemyPokemon = enemyPokemon;
    }

    public void Execute()
    {
      if (_pokemon == null)
      {
        ChoosePokemon();
        Instance.FlipTurn();
        return;
      }

      ChooseMove();
      Instance.FlipTurn();
    }

    private void ChooseMove()
    {
      var move = _pokemon.Moves.First();

      UseMove(move);
    }

    private void UseMove(Move move)
    {
      var damage = move.Power;
      Instance.logBox.Items.Add(_enemyPokemon.Name + "s HP is dropped from" + _enemyPokemon.CurrentHP + " to " + (_enemyPokemon.CurrentHP - damage));

      ApplyDamage(damage);
    }

    private void ApplyDamage(int damage)
    {
      switch (Instance.State)
      {
        case SimulationState.Trainer1Turn:
          Instance.ActivePokemon2.CurrentHP -= damage;
          if (Instance.ActivePokemon2.CurrentHP <= 0)
          {
            Instance.logBox.Items.Add(Instance.ActivePokemon2.Name + "has fainted.");
            Instance.ActivePokemon2 = null;
          }
          break;
        case SimulationState.Trainer2Turn:
          Instance.ActivePokemon1.CurrentHP -= damage;
          if (Instance.ActivePokemon1.CurrentHP <= 0)
          {
            Instance.logBox.Items.Add(Instance.ActivePokemon1.Name + "has fainted.");
            Instance.ActivePokemon1 = null;
          }
          break;
        default:
          throw new InvalidOperationException("State isn't handled correctly");
      }
    }

    private void ChoosePokemon()
    {
      if (_trainer.Pokemon.Any(x => x.CurrentHP > 0))
      {
        switch (Instance.State)
        {
          case SimulationState.Trainer1Turn:
            Instance.ActivePokemon1 = _trainer.Pokemon.First(x => x.CurrentHP > 0);
            Instance.logBox.Items.Add(_trainer.Name + " sent out his " + Instance.ActivePokemon1.Name);
            break;
          case SimulationState.Trainer2Turn:
            Instance.ActivePokemon2 = _trainer.Pokemon.First(x => x.CurrentHP > 0);
            Instance.logBox.Items.Add(_trainer.Name + " sent out his " + Instance.ActivePokemon2.Name);
            break;
        }
      }
      else
      {
        Instance.logBox.Items.Add(_trainer.Name + " is out of usable Pokemon");
        Instance.State = Instance.Turn % 2 == 0 ? SimulationState.Trainer1Win : SimulationState.Trainer2Win;
      }
    }
  }
}
