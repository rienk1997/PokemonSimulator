using PokemonSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSimulator.Calculators
{
  // https://bulbapedia.bulbagarden.net/wiki/Stat
  public static class StatCalculator
  {
    public static int CalculateHP(int baseStat, int level, int ivs, int evs)
    {
      var baseRes = 2 * baseStat;
      var ivRes = baseRes + ivs;
      var evRes = ivRes + (evs / 4);
      var levelRes = evRes * level;
      var result = levelRes / 100;
      result = result + level + 10;
      //var result = (int)Math.Floor((decimal)(2 * baseStat) + ivs + (evs/4) * level / 100 + level + 10);
      return result;
    }

    public static int CalculateStat(int baseStat, int level, int ivs, int evs, double natureFactor)
    {
      var baseRes = 2 * baseStat;
      var ivRes = baseRes + ivs;
      var evRes = ivRes + (evs / 4);
      var levelRes = evRes * level;
      var result = levelRes / 100;
      result = result + 5;
      result = (int)(result * natureFactor);
      //var result = (int)Math.Floor((2 * baseStat) + ivs + (evs/4) * level / 100 + 5 * natureFactor);
      return result;
    }
  }
}
