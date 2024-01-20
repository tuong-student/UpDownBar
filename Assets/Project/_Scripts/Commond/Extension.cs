
using UnityEngine;

namespace Game.Extension
{
    public static class Vector3Extension
    {
        public static Vector3 ToVector3XZ (this Vector3 source)
        {
            return new Vector3(source.x, 0, source.z);
        }
    }
}
