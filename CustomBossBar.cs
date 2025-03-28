using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Godspeed
{
    class CustomBossBar : MonoBehaviour, IEnemyHealthDetails
    {
        public string fullName;
        private float health;

        public string FullName
        {
            get
            {
                return this.fullName;
            }
        }

        public float Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public bool Dead
        {
            get
            {
                return false;
            }
        }

        public bool Blessed
        {
            get
            {
                return false;
            }
        }

        public void ForceGetHealth()
        {
            return;
        }
    }
}
