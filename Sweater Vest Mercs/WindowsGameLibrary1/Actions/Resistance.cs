using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVMLib.Actions
{
  public class Resistance
  {
    public DamageType Type { get; set; }
    public float Amount { get; set; }

    public Resistance(DamageType type, float amount)
    {
      Type = type;
      Amount = amount;
    }
  }
}
