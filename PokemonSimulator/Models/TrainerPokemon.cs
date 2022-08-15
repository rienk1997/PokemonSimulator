using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSimulator.Models
{
  public class TrainerPokemon
  {
    public int PokedexNumber { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int CurrentHP { get; set; }
    public List<Move> Moves { get; set; }

    public int HP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
  }
}
