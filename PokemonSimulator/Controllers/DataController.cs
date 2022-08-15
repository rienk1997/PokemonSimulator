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

    public Pokemon GetPokemon(string name) => _pokemonByName.TryGetValue(name, out var pokemon) ? pokemon : null;
    public Move GetMove(string name) => _movesByName.TryGetValue(name, out var move) ? move : null;
    public Trainer GetTrainer(int index) => _trainers[index];
    public List<Trainer> GetTrainers() => _trainers;
    public int GetTrainersCount() => _trainers.Count;

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
              return new TrainerPokemon()
              {
                PokedexNumber = pokemon.PokedexNumber,
                Name = pokemon.Name,
                Level = x.Level,
                HP = StatCalculator.CalculateHP(pokemon.HP, x.Level),
                CurrentHP = StatCalculator.CalculateHP(pokemon.HP, x.Level),
                Attack = StatCalculator.CalculateStat(pokemon.Attack, x.Level),
                Defense = StatCalculator.CalculateStat(pokemon.Defense, x.Level),
                Moves = x.Moves.Move.Select(y => GetMove(y)).ToList()
              };
            }
            return null;
          }).ToList();
        }

        _trainers = trainerData.Trainers.Trainer;
      }
    }
  }
}
