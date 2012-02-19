using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVMLib.Actions;
using SVMLib.Items;

namespace SVMLib.Entities
{
    public class LivingEntity : AnimatedEntity
    {
        private bool _alive;
        public bool Alive { get { return _alive; } set { Visable = value; _alive = value; } }
        public int Heath { get; set; }
        public List<Resistance> Resistances { get; set; }
        public List<Item> EquipedItems { get; set; }

        public LivingEntity()
            : base()
        {
            Alive = true;
            Resistances = new List<Resistance>();
            EquipedItems = new List<Item>();
        }

        public void DoDamage( Damage dmg )
        {
            Resistance resist = null;

            foreach ( Resistance res in Resistances )
            {
                if ( res.Type.Equals( dmg.Type ) )
                {
                    resist = res;
                    break;
                }
            }

            Damage trueDmg = Damage.calcTrue( dmg, resist );

            Heath -= (int) Math.Round( trueDmg.Amount );

            if ( Heath <= 0 )
            {
                Alive = false;
            }
        }
    }
}
