using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PokemonSimulator.Models
{
  public class TrainerPokemon
  {
    public int PokedexNumber { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int CurrentHP { get; set; }
    public List<Move> Moves { get; set; }
    public Nature Nature { get; set; } = Nature.None;


    public Stats Stats { get; set; }
    public Stats IVs { get; set; }
    public Stats EVs { get; set; }
  }

  public class Stats
  {
    [XmlElement(ElementName = "HP")]
    public int HP { get; set; }
    [XmlElement(ElementName = "Attack")]
    public int Attack { get; set; }
    [XmlElement(ElementName = "Defense")]
    public int Defense { get; set; }
    [XmlElement(ElementName = "SpAttack")]
    public int SpAttack { get; set; }
    [XmlElement(ElementName = "SpDefense")]
    public int SpDefense { get; set; }
    [XmlElement(ElementName = "Speed")]
    public int Speed { get; set; }
  }
}
