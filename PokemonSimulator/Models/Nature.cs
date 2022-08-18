using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PokemonSimulator.Models
{
  public enum Nature
  {
    [XmlEnum("None")]
    None,
    [XmlEnum("Adamant")]
    Adamant,
    [XmlEnum("Bashful")]
    Bashful,
    [XmlEnum("Bold")]
    Bold,
    [XmlEnum("Brave")]
    Brave,
    [XmlEnum("Calm")]
    Calm,
    [XmlEnum("Careful")]
    Careful,
    [XmlEnum("Docile")]
    Docile,
    [XmlEnum("Gentle")]
    Gentle,
    [XmlEnum("Hardy")]
    Hardy,
    [XmlEnum("Hasty")]
    Hasty,
    [XmlEnum("Impish")]
    Impish,
    [XmlEnum("Jolly")]
    Jolly,
    [XmlEnum("Lax")]
    Lax,
    [XmlEnum("Lonely")]
    Lonely,
    [XmlEnum("Mild")]
    Mild,
    [XmlEnum("Modest")]
    Modest,
    [XmlEnum("Naive")]
    Naive,
    [XmlEnum("Naughty")]
    Naughty,
    [XmlEnum("Quiet")]
    Quiet,
    [XmlEnum("Quirky")]
    Quirky,
    [XmlEnum("Rash")]
    Rash,
    [XmlEnum("Relaxed")]
    Relaxed,
    [XmlEnum("Sassy")]
    Sassy,
    [XmlEnum("Serious")]
    Serious,
    [XmlEnum("Timid")]
    Timid,
  }
}
