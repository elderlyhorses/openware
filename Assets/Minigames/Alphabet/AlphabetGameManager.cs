using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Alphabet
{
    public class AlphabetGameManager : MonoBehaviour
    {
        public MinigameCompletionHandler MinigameCompletionHandler;
        public TMP_InputField InputField;
        public GameObject WinText;
        public GameObject LoseText;
        public TextMeshProUGUI CountdownText;

        public GameObject PositiveFeedbackImage;
        public GameObject PositiveFeedbackText;

        public GameObject NegativeFeedbackImage;
        public GameObject NegativeFeedbackText;

        public GameObject LetterPrefab;
        public Canvas Canvas;

        private SoundManager _soundManager;
        private List<AudioClip> letterAudioClips = new List<AudioClip>();
        
        int secondsRemaining = 13;
        bool gameIsOver = false;
        string fullAlphabet = "abcdefghijklmnopqrstuvwxyz";
        string lastText = "";

        void Start()
        {
            StartCoroutine("Countdown");
            WinText.SetActive(false);
            LoseText.SetActive(false);
            UnityEngine.Object[] objs = Resources.LoadAll("Letter Audio", typeof(AudioClip));
            foreach (UnityEngine.Object obj in objs)
            {
                letterAudioClips.Add((AudioClip)obj);                
            }
            
            letterAudioClips.Sort(delegate(AudioClip a, AudioClip b)
            {
                return a.name.CompareTo(b.name);
            });

            for (int x = 0; x < letterAudioClips.Count; x++)
            {
                print(x + " " + letterAudioClips[x].name);   
            }

            _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        }

        void Update()
        {
            if (gameIsOver)
            {
                return;
            }

            if (Input.anyKeyDown)
            {
                string val = GetCurrentKeyDown().ToString();
                if (val.Length == 0)
                {
                    return;
                }
                
                ValidateInput();
                GameObject letter = Instantiate(LetterPrefab, Vector3.zero, Quaternion.identity);
                letter.transform.SetParent(Canvas.transform);
                letter.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                letter.GetComponentInChildren<TextMeshProUGUI>().text = val;

                for (int x = 0; x < letterAudioClips.Count; x++)
                {
                    print(letterAudioClips[x].name + " -- " + "Letter" + val.ToUpper());
                    if (letterAudioClips[x].name == "Letter" + val.ToUpper())
                    {
                        print("play one shot");
                        _soundManager.PlayOneShot(letterAudioClips[x]);
                        break;
                    }
                }
            }
        }

        void ValidateInput()
        {
            string text = InputField.text.Trim();

            if (text == "")
            {
                PositiveFeedbackImage.SetActive(false);
                PositiveFeedbackText.SetActive(false);

                NegativeFeedbackImage.SetActive(false);
                NegativeFeedbackText.SetActive(false);
                return;
            }

            if (text == lastText)
            {
                return;
            }

            lastText = text;

            PositiveFeedbackImage.SetActive(false);
            PositiveFeedbackText.SetActive(false);

            NegativeFeedbackImage.SetActive(false);
            NegativeFeedbackText.SetActive(false);

            if (text == fullAlphabet)
            {
                StartCoroutine("Win");
            }
            else if (fullAlphabet.StartsWith(text))
            {
                PositiveFeedbackImage.SetActive(true);
                PositiveFeedbackText.SetActive(true);
            }
            else
            {
                NegativeFeedbackImage.SetActive(true);
                NegativeFeedbackText.SetActive(true);
            }
        }

        IEnumerator Countdown()
        {
            yield return new WaitForSeconds(0);

            if (!gameIsOver)
            {
                secondsRemaining -= 1;
                CountdownText.text = "" + secondsRemaining;

                if (secondsRemaining <= 0)
                {
                    StartCoroutine("Lose");
                }
                else
                {
                    yield return new WaitForSeconds(1);
                    StartCoroutine("Countdown");
                }
            }
        }

        IEnumerator Lose()
        {
            yield return new WaitForSeconds(0f);

            if (!gameIsOver)
            {
                gameIsOver = true;
                LoseText.SetActive(true);
                yield return new WaitForSeconds(2f);
                MinigameCompletionHandler.LoseCallback.Invoke();
            }
        }

        IEnumerator Win()
        {
            yield return new WaitForSeconds(0f);

            if (!gameIsOver)
            {
                gameIsOver = true;
                WinText.SetActive(true);
                yield return new WaitForSeconds(2f);
                MinigameCompletionHandler.WinCallback.Invoke();
            }
        }
        
        // From https://forum.unity.com/threads/find-out-which-key-was-pressed.385250/
        private static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
            .Cast<KeyCode>()
            .Where(k => ((int)k < (int)KeyCode.Mouse0))
            .ToArray();
        private static KeyCode? GetCurrentKeyDown()
        {
            if (!Input.anyKey)
            {
                return null;
            }
 
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKey(keyCodes[i]))
                {
                    return keyCodes[i];
                }
            }
            return null;
        }
    }
}

