using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSimulator.Models
{
  public class Trainer
  {
    public string Name { get; set; }
    public ICollection<Pokemon> Pokemon { get; set; }
  }
}
