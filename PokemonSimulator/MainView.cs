using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonSimulator
{
  public partial class MainView : Form
  {
    private static MainView Instance = null;

    public static MainView GetInstance
    {
      get
      {
        if (Instance == null)
        {
          Instance = new MainView();
        }
        return Instance;
      }
    }
    private MainView()
    {
      InitializeComponent();
    }
  }
}
