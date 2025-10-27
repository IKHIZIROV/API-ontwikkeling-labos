using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoorbeeldDiSamurai
{
    public class Gun : IWeapon, ITrigger
    {
        public ITrigger _trigger { get; set; }

        public Gun(ITrigger trigger)
        {
            _trigger = trigger;
        }

        public void Hit(string target)
        {
            Pull();
            Console.WriteLine($"Shot {target} between his eyes");
        }

        public void Pull()
        {
            throw new NotImplementedException();
        }
    }
}
