using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace PokemonSimulator.Models
{
  public class PokemonData
  {
    [XmlArray(ElementName = "PokemonList")]
    [XmlArrayItem(ElementName = "Pokemon")]
    public List<Pokemon> Pokemon { get; set; }
  }

  public class Pokemon
  {
    [XmlElement(ElementName ="Id")]
    public int PokedexNumber { get; set; }
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }
    [XmlElement(ElementName = "BaseHP")]
    public int HP { get; set; }
    [XmlElement(ElementName = "BaseAttack")]
    public int Attack { get; set; }
    [XmlElement(ElementName = "BaseDefense")]
    public int Defense { get; set; }
  }
}
