using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSimulator.Calculators
{
  public static class StatCalculator
  {
    public static int CalculateHP(int baseStat, int level)
    {
      return (int)Math.Floor((decimal)(2 * baseStat) * level / 100 + level + 10);
    }

    public static int CalculateStat(int baseStat, int level)
    {
      return (int)Math.Floor((decimal)(2 * baseStat) * level / 100 + 5);
    }
  }
}
