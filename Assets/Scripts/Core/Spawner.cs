using UnityEngine;

namespace Core
{
    public static class Spawner
    {
        public static GameObject Spawn(Object gameObject)
        {
            return (GameObject) Object.Instantiate(gameObject);
        }
    }
}