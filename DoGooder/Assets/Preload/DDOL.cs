using UnityEngine;

namespace DoGooder.Preload
{
    public class DDOL : MonoBehaviour
    {

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
