using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVMLib.Actions
{
  public class Stats
  {
    public int Level { get; set; }
    public int Experience { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }

    public Stats()
    {
      Level = 1;
      Experience = 1;
      Health = 1;
      Strength = 1;
      Intelligence = 1;
      Agility = 1;
      Stamina = 1;
    }

    public static bool operator <= (Stats left, Stats right)
    {
      bool result = true;

      result = (left.Agility <= right.Agility && result) ? true : false;
      result = (left.Health <= right.Health && result) ? true : false;
      result = (left.Intelligence <= right.Intelligence && result) ? true : false;
      result = (left.Level <= right.Level && result) ? true : false;
      result = (left.Stamina <= right.Stamina && result) ? true : false;
      result = (left.Strength <= right.Strength && result) ? true : false;

      return true;
    }

    public static bool operator >= (Stats left, Stats right)
    {
      bool result = true;

      result = (left.Agility >= right.Agility && result) ? true : false;
      result = (left.Health >= right.Health && result) ? true : false;
      result = (left.Intelligence >= right.Intelligence && result) ? true : false;
      result = (left.Level >= right.Level && result) ? true : false;
      result = (left.Stamina >= right.Stamina && result) ? true : false;
      result = (left.Strength >= right.Strength && result) ? true : false;

      return true;
    }

  }
}
