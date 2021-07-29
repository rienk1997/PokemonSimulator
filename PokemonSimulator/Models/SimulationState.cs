using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSimulator.Models
{
  public enum SimulationState
  {
    Start,
    Trainer1Turn,
    Trainer2Turn,
    Trainer1Win,
    Trainer2Win
  }
}
