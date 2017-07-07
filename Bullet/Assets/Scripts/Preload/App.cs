using UnityEngine;

namespace Bullet.Preload
{
    public class App : MonoBehaviour
    {
        private void Start()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
