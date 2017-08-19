using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bullet.UI
{
    public class GUIManager : MonoBehaviour
    {
        [SerializeField]
        private nPlayer.PlayerController nPlayer;
        [SerializeField]
        private Text money;
        [SerializeField]
        private Image[] textImages;
        [SerializeField]
        private Sprite[] numberSprites;
        [SerializeField]
        private GameObject GameOverCanvas;
        [SerializeField]
        private Button backButton;
        [SerializeField]
        private Button retryButton;
        [SerializeField]
        private Text moneyText;

        static public GUIManager Instance { get { return _instance; } }
        static protected GUIManager _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("GUI Manager is already in play. Deleting new!", gameObject);
                Destroy(gameObject);
            }
            else
            { _instance = this; }
        }

        private void Start()
        {
            GameOverCanvas.SetActive(false);
        }

        void FixedUpdate()
        {
            /*SetScoreImage(player.score);*/
            money.text = "$"+nPlayer.money.ToString();
        }

        void SetScoreImage(int score)
        {
            textImages[0].sprite = numberSprites[(int)Mathf.Floor(score / 1000)];
            textImages[1].sprite = numberSprites[(int)Mathf.Floor(score / 100)];
            textImages[2].sprite = numberSprites[(int)Mathf.Floor(score / 10)];
            textImages[3].sprite = numberSprites[score % 10];

            if (score < 0)
            {
                for (int i = 0; i < textImages.Length; ++i)
                {
                    textImages[i].sprite = numberSprites[0];
                }
            }
            else if (score > 9 && score < 100)
            {
                textImages[0].sprite = numberSprites[0];
                textImages[1].sprite = numberSprites[0];
            }
            else if (score > 99 && score < 1000)
            {
                textImages[0].sprite = numberSprites[0];
            }
            else if (score >= 9999)
            {
                textImages[0].sprite = numberSprites[9];
                textImages[1].sprite = numberSprites[9];
                textImages[2].sprite = numberSprites[9];
                textImages[3].sprite = numberSprites[9];
            }
        }

        public void ActivateGameOver()
        {
            Cursor.visible = true;
            money.gameObject.SetActive(false);
            GameOverCanvas.SetActive(true);
            float pMoney = GameObject.Find("Player").GetComponent<nPlayer.PlayerController>().money;
            Debug.Log(pMoney);
            moneyText.text = "GOT $" + (int)pMoney;
            Bullet.PlayerMaster.Instance.Money += (int)pMoney;
        }

        public void RetryButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("_main");
        }

        public void BackButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Land_Control");
        }
    }
}
