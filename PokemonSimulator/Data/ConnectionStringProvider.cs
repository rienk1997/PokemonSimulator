using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSimulator.Data
{
  public static class ConnectionStringProvider
  {
    public static string ConnectionString =
      "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\PokemonSimulator\\PokemonSimulator\\Data\\Trainers.mdf;Integrated Security=True";
  }
}
