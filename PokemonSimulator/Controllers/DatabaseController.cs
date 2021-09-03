using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonSimulator.Calculators;
using PokemonSimulator.Data;
using PokemonSimulator.Models;

namespace PokemonSimulator.Controllers
{
  public class DatabaseController
  {


    public static List<Trainer> LoadTrainers()
    {
      var trainers = new List<Trainer>();
      using (var conn = new SqlConnection(ConnectionStringProvider.ConnectionString))
      {
        var sqlString = @"SELECT TrainerPokemonId, TrainerId, Name FROM TrainerPokemon tp
                             Inner JOIN Trainers t on t.Id = tp.TrainerId";
        using (var command = new SqlCommand(sqlString, conn))
        {
          conn.Open();
          var reader = command.ExecuteReader();

          if (reader.HasRows)
          {
            while (reader.Read())
            {
              var trainer = new Trainer();
              var trainerId = reader.GetInt32(1);
              trainer.Name = reader.GetString(2);
              trainer.Pokemon = LoadPokemon(trainerId);
              trainers.Add(trainer);
            }
          }
          else
          {
            Console.WriteLine("No trainers found.");
          }

          reader.Close();
        }
      }

      return trainers;
    }

    private static ICollection<Pokemon> LoadPokemon(int trainerId)
    {
      var pokemons = new List<Pokemon>();

      var sqlString =
        @"SELECT TrainerPokemonId, TrainerId, Id, Name, Level, BaseHP, BaseAttack, BaseDefense From TrainerPokemon tp
                        INNER JOIN Pokemon p on tp.PokemonId = p.Id
                        WHERE tp.TrainerId = @TrainerId";
      using (var conn = new SqlConnection(ConnectionStringProvider.ConnectionString))
      {
        using (var command = new SqlCommand(sqlString, conn))
        {
          conn.Open();

          command.Parameters.Add("@TrainerId", SqlDbType.Int);
          command.Parameters["@TrainerId"].Value = trainerId;

          var reader = command.ExecuteReader();

          if (reader.HasRows)
          {
            while (reader.Read())
            {
              var pokemon = new Pokemon();
              var pokemonId = reader.GetInt32(2);

              pokemon.Name = reader.GetString(3);
              pokemon.PokedexNumber = pokemonId;
              pokemon.Level = reader.GetInt32(4);
              pokemon.HP = pokemon.CurrentHP = StatCalculator.CalculateHP(reader.GetInt32(5), reader.GetInt32(4));
              pokemon.Attack = StatCalculator.CalculateHP(reader.GetInt32(6), reader.GetInt32(4));
              pokemon.Defense = StatCalculator.CalculateHP(reader.GetInt32(7), reader.GetInt32(4));

              pokemon.Moves = LoadMoves(reader.GetInt32(0));

              pokemons.Add(pokemon);
            }
          }
          else
          {
            Console.WriteLine("No pokemon found for trainerid: " + trainerId);
          }
        }
        return pokemons;
      }
    }

    private static ICollection<Move> LoadMoves(int trainerPokemonId)
    {
      var moves = new List<Move>();

      var sqlString =
        @"SELECT TrainerPokemonId, MoveId, Name, Power, Accuracy From TrainerPokemonMoves tpm
          INNER JOIN Moves m on tpm.MoveId = m.Id
          WHERE tpm.TrainerPokemonId = @TrainerPokemonId";

      using (var conn = new SqlConnection(ConnectionStringProvider.ConnectionString))
      {
        using (var command = new SqlCommand(sqlString, conn))
        {
          conn.Open();

          command.Parameters.Add("@TrainerPokemonId", SqlDbType.Int);
          command.Parameters["@TrainerPokemonId"].Value = trainerPokemonId;

          var reader = command.ExecuteReader();

          if (reader.HasRows)
          {
            while (reader.Read())
            {
              var move = new Move
              {
                Name = reader.GetString(2), 
                Power = reader.GetInt32(3), 
                Accuracy = reader.GetInt32(4)
              };


              moves.Add(move);
            }
          }
          else
          {
            Console.WriteLine("No moves found for trainerpokemonid: " + trainerPokemonId);
          }
        }
        return moves;
      }
    }
  }
}
