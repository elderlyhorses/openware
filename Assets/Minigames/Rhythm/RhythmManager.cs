using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rhythm
{

    enum Player
    {
        AI1, AI2, Human, AI3
    }
    
    class Beat
    {
        public float Time;
        public Player Player;
        public Sprite Sprite;

        public Beat(float t, Player p, Sprite s)
        {
            Time = t;
            Player = p;
            Sprite = s;
        }
    }

    public class RhythmManager : MonoBehaviour
    {
        public SpriteRenderer AI1;
        public SpriteRenderer AI2;
        public SpriteRenderer Human;
        public SpriteRenderer AI3;
        public AudioClip Song;
        public GameObject WinText;
        public GameObject LoseText;
        public AudioClip Boo;
        public AudioClip Cheer;

        public Sprite DanceMove1;
        public Sprite DanceMove2;
        public RectTransform Nice;
        public MinigameCompletionHandler MinigameCompletionHandler;
        
        private SoundManager _soundManager;
        private List<Beat> Beats = new List<Beat>();
        private float GracePeriod = 0.16f;
        private bool isOver = false;

        void Start()
        {
            Beats.Add(new Beat(0.996f, Player.AI1, DanceMove1));
            Beats.Add(new Beat(1.939f, Player.AI2, DanceMove1));
            Beats.Add(new Beat(2.888f, Player.Human, DanceMove1));
            Beats.Add(new Beat(3.831f, Player.AI3, DanceMove1));
            Beats.Add(new Beat(4.78f, Player.AI1, DanceMove2));
            Beats.Add(new Beat(5.728f, Player.AI2, DanceMove2));
            Beats.Add(new Beat(6.672f, Player.Human, DanceMove2));
            Beats.Add(new Beat(7.615f, Player.AI3, DanceMove2));
            
            _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            _soundManager.SetClipAndPlay(Song);

            WinText.SetActive(false);
            LoseText.SetActive(false);
            Nice.gameObject.SetActive(false);
        }

        void Update()
        {
            if (isOver)
            {
                return;
            }
            
            if (Beats.Count == 0)
            {
                StartCoroutine("Win");
                return;
            }
            
            if (Beats[0].Player != Player.Human && Beats[0].Time < _soundManager.GetTime())
            {
                CharacterSpriteRenderer(Beats[0].Player).sprite = Beats[0].Sprite;
                Beats.RemoveAt(0);
                return;
            }

            if (Beats[0].Player == Player.Human && Beats[0].Time + GracePeriod < _soundManager.GetTime())
            {
                StartCoroutine("Lose");
                return;
            }

            if (Input.anyKeyDown)
            {
                HandleHumanDanceInput();
            }
        }

        void HandleHumanDanceInput()
        {
            if (Beats[0].Player != Player.Human)
            {
                StartCoroutine("Lose");
                return;
            }

            var t = Beats[0].Time;
            if (_soundManager.GetTime() > t - GracePeriod && t < _soundManager.GetTime() + GracePeriod)
            {
                Nice.gameObject.SetActive(true);
                Nice.DOPunchScale(Vector3.one / 3f, 0.5f).OnComplete(() =>
                {
                    Nice.gameObject.SetActive(false);
                });
                
                CharacterSpriteRenderer(Beats[0].Player).sprite = Beats[0].Sprite;
                Beats.RemoveAt(0);
                return;
            }

            StartCoroutine("Lose");
        }

        IEnumerator Lose()
        {
            isOver = true;
            _soundManager.Stop();
            LoseText.SetActive(true);
            _soundManager.PlayOneShot(Boo);
            yield return new WaitForSeconds(2f);
            _soundManager.Stop();
            yield return new WaitForEndOfFrame();
            MinigameCompletionHandler.LoseCallback.Invoke();
        }

        IEnumerator Win()
        {
            isOver = true;
            WinText.SetActive(true);
            _soundManager.PlayOneShot(Cheer);
            
            yield return new WaitForSeconds(2f);
            _soundManager.Stop();
            yield return new WaitForEndOfFrame();
            MinigameCompletionHandler.WinCallback.Invoke();
        }

        SpriteRenderer CharacterSpriteRenderer(Player p)
        {
            switch (p)
            {
                case Player.AI1:
                    return AI1;
                case Player.AI2:
                    return AI2;
                case Player.Human:
                    return Human;
                case Player.AI3:
                    return AI3;
            }

            return AI1;
        }
    }
}