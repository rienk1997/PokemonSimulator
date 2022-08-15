using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonSimulator.Models;

namespace PokemonSimulator.Calculators
{
  public class MoveDamageCalculator
  {
    private TrainerPokemon _attackingPokemon;
    private TrainerPokemon _defendingPokemon;

    public MoveDamageCalculator(TrainerPokemon attackingPokemon, TrainerPokemon defendingPokemon)
    {
      _attackingPokemon = attackingPokemon;
      _defendingPokemon = defendingPokemon;
    }

    public int CalculateDamage(Move move)
    {
      decimal levelFactor = (2 * _attackingPokemon.Level) / 5 + 2;

      decimal damage = (levelFactor * move.Power) / 50 + 2;
      return decimal.ToInt32(damage);
    }
  }
}
