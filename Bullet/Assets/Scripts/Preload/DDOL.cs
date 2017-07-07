using UnityEngine;

namespace Bullet.Preload
{
    public class DDOL : MonoBehaviour
    {

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
