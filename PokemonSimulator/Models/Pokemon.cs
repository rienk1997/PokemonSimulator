﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSimulator.Models
{
  public class Pokemon
  {
    public int PokedexNumber { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public int CurrentHP { get; set; }
    public ICollection<Move> Moves { get; set; }
  }
}