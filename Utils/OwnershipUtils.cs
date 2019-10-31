﻿using Sandbox.Game.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Game.Entity;

namespace ALE_Core.Utils {

    public class OwnershipUtils {

        public static Dictionary<long, BuildStats> FindBuildStatsPerPlayer() {

            Dictionary<long, BuildStats> stats = new Dictionary<long, BuildStats>();
            
            foreach(MyEntity entity in MyEntities.GetEntities()) { 

                if (!(entity is MyCubeGrid grid))
                    continue;

                foreach(var block in grid.GetBlocks()) { 

                    long buildBy = block.BuiltBy;

                    if(!stats.TryGetValue(buildBy, out BuildStats statsForPlayer)) {
                        statsForPlayer = new BuildStats();
                        stats.Add(buildBy, statsForPlayer);
                    }

                    statsForPlayer.BlockCount++;
                    statsForPlayer.PcuCount += BlockUtils.GetPcu(block);
                }
            }

            return stats;
        }

        public class BuildStats {

            public int PcuCount { get; set; }
            public int BlockCount { get; set; }
        }
    }
}