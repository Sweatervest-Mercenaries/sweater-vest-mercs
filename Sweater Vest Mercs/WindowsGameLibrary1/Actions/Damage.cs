using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVMLib.Actions;

namespace SVMLib.Actions
{
  public enum DamageType
  {
    Physical, Fire, Water, Ice, Wind, Electric, Light, Shadow
  }

  public class Damage
  {
    public DamageType Type { get; set; }
    public float Amount { get; set; }

    public Damage(DamageType type, float amount)
    {
      Type = type;
      Amount = amount;
    }

    public static Damage calcTrue(Damage dmg, Resistance res)
    {
      if (res != null)
        return new Damage(dmg.Type, dmg.Amount - res.Amount);
      else
        return dmg;
    }
  }
}
