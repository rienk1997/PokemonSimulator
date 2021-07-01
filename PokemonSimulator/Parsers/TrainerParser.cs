using Newtonsoft.Json;
using PokemonSimulator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSimulator.Parsers
{
  public class TrainerParser
  {
    public ICollection<Trainer> Trainers { get; set; }
    public ICollection<Pokemon> Pokemon { get; set; }
    public ICollection<Move> Moves { get; set; }

    private static TrainerParser Instance = null;
    public static TrainerParser GetInstance
    {
      get
      {
        if (Instance == null)
        {
          Instance = new TrainerParser();
        }
        return Instance;
      }
    }

    private TrainerParser()
    {
      ReadData();
    }

    private void ReadData()
    {
      using (StreamReader r = new StreamReader("D:/Projects/C#/PokemonSimulator/PokemonSimulator/Data/Trainers.json"))
      {
        string json = r.ReadToEnd();
        Trainers = JsonConvert.DeserializeObject<List<Trainer>>(json);
      }

      using (StreamReader r = new StreamReader("D:/Projects/C#/PokemonSimulator/PokemonSimulator/Data/Pokemon.json"))
      {
        string json = r.ReadToEnd();
        Pokemon = JsonConvert.DeserializeObject<List<Pokemon>>(json);
      }

      using (StreamReader r = new StreamReader("D:/Projects/C#/PokemonSimulator/PokemonSimulator/Data/Moves.json"))
      {
        string json = r.ReadToEnd();
        Moves = JsonConvert.DeserializeObject<List<Move>>(json);
      }
    }
    public ICollection<Trainer> ParseAll()
    {
      foreach (var trainer in Trainers)
      {
        LoadTrainer(trainer);
      }

      return Trainers;
    }

    private void LoadTrainer(Trainer trainer)
    {
      foreach (var pokemon in trainer.Pokemon)
      {
        LoadPokemon(pokemon);
      }
    }

    private void LoadPokemon(Pokemon pokemon)
    {
      var pokemonData = Pokemon.FirstOrDefault(x => x.Name == pokemon.Name);

      pokemon.PokedexNumber = pokemonData.PokedexNumber;
      pokemon.HP = pokemonData.HP;
      pokemon.CurrentHP = pokemonData.HP;

      foreach (var move in pokemon.Moves)
      {
        LoadMove(move);
      }
    }

    private void LoadMove(Move move)
    {
      var moveData = Moves.FirstOrDefault(x => x.Name == move.Name);
      move.Power = moveData.Power;
      move.Accuracy = moveData.Accuracy;
    }
  }
}
