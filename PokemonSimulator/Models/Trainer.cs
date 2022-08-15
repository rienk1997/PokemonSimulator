using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PokemonSimulator.Models
{
  [XmlRoot(ElementName = "Root")]
  public class TrainerData
  {

    [XmlElement(ElementName = "Trainers")]
    public Trainers Trainers;
  }

  [XmlRoot(ElementName = "Trainers")]
  public class Trainers
  {

    [XmlElement(ElementName = "Trainer")]
    public List<Trainer> Trainer;
  }

  [XmlRoot(ElementName = "Trainer")]
  public class Trainer
  {
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }
    [XmlIgnore]
    public ICollection<TrainerPokemon> Pokemon { get; set; }
    [XmlElement(ElementName = "PokemonList")]
    public PokemonList PokemonList;

    public Trainer CreateCopy()
    {
      return new Trainer()
      {
        Name = Name,
        Pokemon = Pokemon.Select(CopyPokemon).ToList()
      };
    }

    private TrainerPokemon CopyPokemon(TrainerPokemon pokemon)
    {
      return new TrainerPokemon()
      {
        PokedexNumber = pokemon.PokedexNumber,
        Name = pokemon.Name,
        CurrentHP = pokemon.CurrentHP,
        Level = pokemon.Level,
        Moves = pokemon.Moves
      };
    }
  }

  [XmlRoot(ElementName = "PokemonList")]
  public class PokemonList
  {

    [XmlElement(ElementName = "Pokemon")]
    public List<SerializedPokemon> Pokemon;
  }

  [XmlRoot(ElementName = "Pokemon")]
  public class SerializedPokemon
  {

    [XmlElement(ElementName = "Name")]
    public string Name;

    [XmlElement(ElementName = "Level")]
    public int Level;

    [XmlElement(ElementName = "Moves")]
    public Moves Moves;
  }

  [XmlRoot(ElementName = "Moves")]
  public class Moves
  {

    [XmlElement(ElementName = "Move")]
    public List<string> Move;
  }
}
