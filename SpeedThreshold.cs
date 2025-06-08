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

        int currentDamage = 5;

        public void Start()
        {
            bossSource = gameObject.AddComponent<CustomBossBar>();
            CreateBar();
        }
        public void Update()
        {
            if (Godspeed.player.dead) currentDamage = 5;
            if (Godspeed.player.dead || !MonoSingleton<PlayerTracker>.Instance.levelStarted) return;

            float velocity = MonoSingleton<PlayerTracker>.Instance.GetPlayerVelocity(true).magnitude;

            if (timeLeft < 0f && velocity < Godspeed.speedThreshold.value)
            {
                timeLeft = Godspeed.leniency.value;
            }
            else if (timeLeft >= 0f)
            {
                if (velocity > Godspeed.speedThreshold.value)
                {
                    timeLeft = -1f;
                    bossSource.Health = Godspeed.leniency.value;
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
                        case Godspeed.Punishments.Ramping:
                            Godspeed.player.GetHurt(currentDamage, false);
                            currentDamage += 5;
                            break;
                        case Godspeed.Punishments.Restart:
                            MonoSingleton<OptionsManager>.Instance.RestartMission();
                            break;
                        //case Godspeed.Punishments.Maurice:
                        //    //Instantiate<SpiderBody>();
                        //    break;
                        default:
                            break;
                    }

                    timeLeft = -1f;
                }
            }
        }

        private void CreateBar()
        {
            bossSource.Health = Godspeed.leniency.value + 0.001f;

            bossBar = gameObject.AddComponent<BossHealthBar>();
            bossBar.bossName = "SPEED UP";
            bossBar.secondaryBar = false;
            bossBar.source = bossSource;
        }
    }
}
