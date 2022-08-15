using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonSimulator.Calculators;
using PokemonSimulator.Models;

namespace PokemonSimulator.Actions
{
  public class TrainerTurnAction: ISimulatorAction
  {
    private readonly Trainer _trainer;
    private readonly TrainerPokemon _pokemon;

    private readonly TrainerPokemon _enemyPokemon;

    private readonly MoveDamageCalculator _damageCalculator;

    private readonly MainView Instance;
    public TrainerTurnAction(Trainer trainer, TrainerPokemon pokemon, TrainerPokemon enemyPokemon, MainView instance)
    {
      _trainer = trainer;
      _pokemon = pokemon;
      Instance = instance;
      _enemyPokemon = enemyPokemon;

      _damageCalculator = new MoveDamageCalculator(pokemon, enemyPokemon);
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
      Instance.Log(_pokemon.Name + " used " + move.Name + "!");
      var damage = _damageCalculator.CalculateDamage(move);
      Instance.Log(_enemyPokemon.Name + "s HP is dropped from" + _enemyPokemon.CurrentHP + " to " + (_enemyPokemon.CurrentHP - damage));

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
            Instance.Log(Instance.ActivePokemon2.Name + "has fainted.");
            Instance.ActivePokemon2 = null;
          }
          break;
        case SimulationState.Trainer2Turn:
          Instance.ActivePokemon1.CurrentHP -= damage;
          if (Instance.ActivePokemon1.CurrentHP <= 0)
          {
            Instance.Log(Instance.ActivePokemon1.Name + "has fainted.");
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
            Instance.Log(_trainer.Name + " sent out his " + Instance.ActivePokemon1.Name);
            break;
          case SimulationState.Trainer2Turn:
            Instance.ActivePokemon2 = _trainer.Pokemon.First(x => x.CurrentHP > 0);
            Instance.Log(_trainer.Name + " sent out his " + Instance.ActivePokemon2.Name);
            break;
        }
      }
      else
      {
        Instance.Log(_trainer.Name + " is out of usable Pokemon");
        Instance.RemovePokemon();
        Instance.State = Instance.Turn % 2 == 0 ? SimulationState.Trainer1Win : SimulationState.Trainer2Win;
      }
    }
  }
}
