using System.Collections.Generic;
using System.Linq;

namespace LumosLib
{
    public static class DrawUtil
    {
        public static T GetWeighted<T>(IEnumerable<T> targets) where T : IDrawable
        {
            if (targets == null) 
                return default;
            
            var list = targets.ToList();
            if (list.Count == 0) 
                return default;
        
            if (list.Count == 1) 
                return list[0];

            float totalWeight = list.Sum(x => x.DrawWeight);
            
            if (totalWeight <= 0f) 
                return list[0];

            float roll = UnityEngine.Random.Range(0f, totalWeight);
            float cumulative = 0f;

            foreach (var item in list)
            {
                cumulative += item.DrawWeight;
            
                if (roll < cumulative) 
                    return item;
            }

            return list[^1];
        }
    }
}

