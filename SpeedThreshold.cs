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
        CustomBossBar bossSource;
        BossHealthBar bossBar;

        float timeLeft = -1f;

        public void Start()
        {
            bossSource = gameObject.AddComponent<CustomBossBar>();
        }
        public void Update()
        {
            if (!MonoSingleton<PlayerTracker>.Instance.levelStarted) return;
            if (Godspeed.player.dead) return;
            
            float velocity = MonoSingleton<PlayerTracker>.Instance.GetPlayerVelocity(true).magnitude;

            if (timeLeft < 0f && velocity < Godspeed.speedThreshold.value)
            {
                ShowBar();
                timeLeft = Godspeed.leniency.value;
            }
            else if (timeLeft >= 0f)
            {
                if (velocity > Godspeed.speedThreshold.value)
                {
                    timeLeft = -1f;
                    HideBar();
                    return;
                }

                timeLeft -= Time.deltaTime;
                bossSource.Health = timeLeft;

                if (timeLeft < 0f)
                {
                    switch (Godspeed.punishment.value)
                    {
                        case Godspeed.Punishments.Death:
                            Godspeed.player.GetHurt(5000000, false);
                            break;
                        case Godspeed.Punishments.Damage:
                            Godspeed.player.GetHurt(20, false);
                            break;
                        case Godspeed.Punishments.Maurice:
                            
                            break;
                        default:
                            break;
                    }

                    HideBar();

                    timeLeft = -1f;
                }
            }
        }

        private void ShowBar()
        {
            bossSource.Health = Godspeed.leniency.value;

            bossBar = gameObject.AddComponent<BossHealthBar>();
            bossBar.bossName = "SPEED UP";
            bossBar.secondaryBar = false;
            bossBar.source = bossSource;
            Debug.LogError("wawa");
        }
        private void HideBar()
        {
            bossBar.DisappearBar();
            Component.Destroy(bossBar);
        }
    }
}
