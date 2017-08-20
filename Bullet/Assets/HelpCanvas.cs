using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class HelpCanvas : MonoBehaviour
    {

        [SerializeField]
        private GameObject[] objArr;
        [SerializeField]
        private float timeTillDeath = 5f;

        [SerializeField]
        private GameObject readyText;
        [SerializeField]
        private GameObject goText;

        private void Start()
        {
            readyText.SetActive(false);
            goText.SetActive(false);
            for (int i = 0; i < objArr.Length; ++i)
            {
                objArr[i].SetActive(true);
            }
            StartCoroutine(Util.Func.WaitAndRunAction(timeTillDeath, 
                () => {
                    for (int i = 0; i < objArr.Length; ++i)
                        objArr[i].SetActive(false);
                    readyText.SetActive(true);
                    StartCoroutine(Util.Func.WaitAndRunAction(0.5f, 
                        () => {
                            readyText.SetActive(false);
                            goText.SetActive(true);
                            StartCoroutine(Util.Func.WaitAndRunAction(0.5f, 
                                () => { goText.SetActive(false); }));
                        }));
                }));
        }


    }
}
