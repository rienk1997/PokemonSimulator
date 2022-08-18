using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PokemonSimulator.Calculators;
using PokemonSimulator.Models;

namespace PokemonSimulator.Controllers
{
  public class DataController
  {
    private readonly Dictionary<string, Pokemon> _pokemonByName;
    private readonly Dictionary<string, Move> _movesByName;
    private readonly List<Trainer> _trainers;
    private readonly Dictionary<Nature, Tuple<Stat, Stat>> _natures;

    public Pokemon GetPokemon(string name) => _pokemonByName.TryGetValue(name, out var pokemon) ? pokemon : null;
    public Move GetMove(string name) => _movesByName.TryGetValue(name, out var move) ? move : null;
    public Trainer GetTrainer(int index) => _trainers[index];
    public List<Trainer> GetTrainers() => _trainers;
    public int GetTrainersCount() => _trainers.Count;
    public Tuple<Stat, Stat> GetNature(Nature nature) => _natures.TryGetValue(nature, out var tuple) ? tuple : new Tuple<Stat, Stat>(Stat.None, Stat.None);

    private static DataController Instance;
    public static DataController GetInstance
    {
      get
      {
        if (Instance == null)
        {
          Instance = new DataController();
        }
        return Instance;
      }
    }

    public DataController()
    {
      _natures = GetNaturesDictionary();
      XmlSerializer pokemonSerializer = new XmlSerializer(typeof(PokemonData), new XmlRootAttribute("Root"));

      using (Stream reader = new FileStream(@"D:\Projects\C#\PokemonSimulator\PokemonSimulator\Data\Pokemon.xml", FileMode.Open))
      {
        // Call the Deserialize method to restore the object's state.
        var pokemonData = (PokemonData)pokemonSerializer.Deserialize(reader);
        _pokemonByName = pokemonData.Pokemon.ToDictionary(x => x.Name, x => x);
      }

      XmlSerializer moveSerializer = new XmlSerializer(typeof(MoveData), new XmlRootAttribute("Root"));

      using (Stream reader = new FileStream(@"D:\Projects\C#\PokemonSimulator\PokemonSimulator\Data\Moves.xml", FileMode.Open))
      {
        // Call the Deserialize method to restore the object's state.
        var moveData = (MoveData)moveSerializer.Deserialize(reader);
        _movesByName = moveData.Moves.ToDictionary(x => x.Name, x => x);
      }

      XmlSerializer trainerSerializer = new XmlSerializer(typeof(TrainerData), new XmlRootAttribute("Root"));

      using (Stream reader = new FileStream(@"D:\Projects\C#\PokemonSimulator\PokemonSimulator\Data\Trainers.xml", FileMode.Open))
      {
        // Call the Deserialize method to restore the object's state.
        var trainerData = (TrainerData)trainerSerializer.Deserialize(reader);

        foreach (var trainer in trainerData.Trainers.Trainer)
        {
          trainer.Pokemon = trainer.PokemonList.Pokemon.Select(x =>
          {
            if (_pokemonByName.TryGetValue(x.Name, out var pokemon))
            {
              var nature = GetNature(x.Nature);
              return new TrainerPokemon()
              {
                PokedexNumber = pokemon.PokedexNumber,
                Name = pokemon.Name,
                Level = x.Level,
                CurrentHP = StatCalculator.CalculateHP(pokemon.BaseStats.HP, x.Level, x.IVs.HP, x.EVs.HP),
                Moves = x.Moves.Move.Select(y => GetMove(y)).ToList(),
                Nature = x.Nature,
                Stats = new Stats()
                {
                  HP = StatCalculator.CalculateHP(pokemon.BaseStats.HP, x.Level, x.IVs.HP, x.EVs.HP),
                  Attack = StatCalculator.CalculateStat(pokemon.BaseStats.Attack, x.Level, x.IVs.Attack, x.EVs.Attack, CalculateNatureFactor(nature, Stat.Attack)),
                  Defense = StatCalculator.CalculateStat(pokemon.BaseStats.Defense, x.Level, x.IVs.Defense, x.EVs.Defense, CalculateNatureFactor(nature, Stat.Defense)),
                  SpAttack = StatCalculator.CalculateStat(pokemon.BaseStats.SpAttack, x.Level, x.IVs.SpAttack, x.EVs.SpAttack, CalculateNatureFactor(nature, Stat.SpAttack)),
                  SpDefense = StatCalculator.CalculateStat(pokemon.BaseStats.SpDefense, x.Level, x.IVs.SpDefense, x.EVs.SpDefense, CalculateNatureFactor(nature, Stat.SpDefense)),
                  Speed = StatCalculator.CalculateStat(pokemon.BaseStats.Speed, x.Level, x.IVs.Speed, x.EVs.Speed, CalculateNatureFactor(nature, Stat.Speed)),
                }
              };
            }
            return null;
          }).ToList();
        }

        _trainers = trainerData.Trainers.Trainer;
      }
    }

    private double CalculateNatureFactor(Tuple<Stat, Stat> nature, Stat stat)
    {
      if (nature.Item1 == stat)
        return 1.1;
      if (nature.Item2 == stat)
        return 0.9;

      return 1.0;
    }

    private Dictionary<Nature, Tuple<Stat, Stat>> GetNaturesDictionary()
    {
      return new Dictionary<Nature, Tuple<Stat, Stat>>()
      {
        { Nature.Adamant, new Tuple<Stat, Stat>(Stat.Attack, Stat.SpAttack) },
        { Nature.Bashful, new Tuple<Stat, Stat>(Stat.None, Stat.None) },
        { Nature.Bold, new Tuple<Stat, Stat>(Stat.Defense, Stat.Attack) },
        { Nature.Brave, new Tuple<Stat, Stat>(Stat.Attack, Stat.Speed) },
        { Nature.Calm, new Tuple<Stat, Stat>(Stat.SpDefense, Stat.Attack) },
        { Nature.Careful, new Tuple<Stat, Stat>(Stat.SpDefense, Stat.SpAttack) },
        { Nature.Docile, new Tuple<Stat, Stat>(Stat.None, Stat.None) },
        { Nature.Gentle, new Tuple<Stat, Stat>(Stat.SpDefense, Stat.Defense) },
        { Nature.Hardy, new Tuple<Stat, Stat>(Stat.None, Stat.None) },
        { Nature.Hasty, new Tuple<Stat, Stat>(Stat.Speed, Stat.Defense) },
        { Nature.Impish, new Tuple<Stat, Stat>(Stat.Defense, Stat.SpAttack) },
        { Nature.Jolly, new Tuple<Stat, Stat>(Stat.Speed, Stat.SpAttack) },
        { Nature.Lax, new Tuple<Stat, Stat>(Stat.Defense, Stat.SpDefense) },
        { Nature.Lonely, new Tuple<Stat, Stat>(Stat.Attack, Stat.Defense) },
        { Nature.Mild, new Tuple<Stat, Stat>(Stat.SpAttack, Stat.Defense) },
        { Nature.Modest, new Tuple<Stat, Stat>(Stat.SpAttack, Stat.Attack) },
        { Nature.Naive, new Tuple<Stat, Stat>(Stat.Speed, Stat.SpDefense) },
        { Nature.Naughty, new Tuple<Stat, Stat>(Stat.Attack, Stat.SpDefense) },
        { Nature.Quiet, new Tuple<Stat, Stat>(Stat.SpAttack, Stat.Speed) },
        { Nature.Quirky, new Tuple<Stat, Stat>(Stat.None, Stat.None) },
        { Nature.Rash, new Tuple<Stat, Stat>(Stat.SpAttack, Stat.SpDefense) },
        { Nature.Relaxed, new Tuple<Stat, Stat>(Stat.Defense, Stat.Speed) },
        { Nature.Sassy, new Tuple<Stat, Stat>(Stat.SpDefense, Stat.Speed) },
        { Nature.Serious, new Tuple<Stat, Stat>(Stat.None, Stat.None) },
        { Nature.Timid, new Tuple<Stat, Stat>(Stat.Speed, Stat.Attack) },
        { Nature.None, new Tuple<Stat, Stat>(Stat.None, Stat.None) },
      };
    }
  }
}
