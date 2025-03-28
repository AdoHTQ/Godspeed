using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Godspeed
{
    class SpeedThreshold : MonoBehaviour
    {
        public void FixedUpdate()
        {
            Debug.LogError($"CurrentVelocity: {MonoSingleton<PlayerTracker>.Instance.GetPlayerVelocity(true).magnitude}");
        }
    }
}
