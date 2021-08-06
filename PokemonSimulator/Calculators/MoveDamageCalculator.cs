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
    private Pokemon _attackingPokemon;
    private Pokemon _defendingPokemon;

    public MoveDamageCalculator(Pokemon attackingPokemon, Pokemon defendingPokemon)
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
