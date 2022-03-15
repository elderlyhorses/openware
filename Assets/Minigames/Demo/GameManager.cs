using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo {
    public class GameManager : MonoBehaviour
    {
        public MinigameCompletionHandler MinigameCompletionHandler;

        public void Win() {
            print("win");
            MinigameCompletionHandler.WinCallback.Invoke();
        }

        public void Lose() {
            print("lose");
            MinigameCompletionHandler.LoseCallback.Invoke();
        }
    }
}