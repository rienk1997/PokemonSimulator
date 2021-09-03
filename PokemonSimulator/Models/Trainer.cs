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

    public Trainer CreateCopy()
    {
      return new Trainer()
      {
        Name = Name,
        Pokemon = Pokemon.Select(CopyPokemon).ToList()
      };
    }

    private Pokemon CopyPokemon(Pokemon pokemon)
    {
      return new Pokemon()
      {
        PokedexNumber = pokemon.PokedexNumber,
        Name = pokemon.Name,
        CurrentHP = pokemon.CurrentHP,
        Level = pokemon.Level,
        Moves = pokemon.Moves
      };
    }
  }
}
