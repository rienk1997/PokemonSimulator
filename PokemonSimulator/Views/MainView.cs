using Newtonsoft.Json;
using PokemonSimulator.Models;
using PokemonSimulator.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonSimulator
{
  public partial class MainView : Form
  {
    private static MainView Instance = null;

    public ICollection<Trainer> Trainers { get; set; }

    public static MainView GetInstance
    {
      get
      {
        if (Instance == null)
        {
          Instance = new MainView();
        }
        Instance.logBox.Items.Add("Get MainView Instance");
        return Instance;
      }
    }
    private MainView()
    {
      InitializeComponent();
      Trainers = TrainerParser.GetInstance.ParseAll();
    }
  }
}
