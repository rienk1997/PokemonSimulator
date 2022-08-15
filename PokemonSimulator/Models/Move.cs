using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PokemonSimulator.Models
{
  public class MoveData
  {
    [XmlArray(ElementName = "Moves")]
    [XmlArrayItem(ElementName = "Move")]
    public List<Move> Moves { get; set; }
  }
  
  public class Move
  {
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }
    [XmlElement(ElementName = "Power")]
    public int Power { get; set; }
    [XmlElement(ElementName = "Accuracy")]
    public int Accuracy { get; set; }
  }
}
