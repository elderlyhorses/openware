using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

namespace SceneCoordinator
{
    public class SceneCoordinator : MonoBehaviour
    {
        int lives;
        int score;

        enum Screen
        {
            MainMenu, HighScoreScoreScreen, HighScoreMinigame, PracticeMenu, PracticeMinigame
        }

        List<string> minigameSceneNames = new List<string>();
        List<string> unplayedMinigameSceneNames = new List<string>();
        Screen currentScreen;
        string currentMinigame;
        bool sceneLoaded = false;

        IEnumerator Start()
        {
            minigameSceneNames = Utilities.MinigameScenes();
            currentScreen = Screen.MainMenu;

            AsyncOperation main = SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Additive);
            while (!main.isDone)
            {
                yield return null;
            }

            SetupMainMenu();
        }

        void SetupMainMenu()
        {
            GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(DidTapMainMenuPlay);
            GameObject.Find("Play specific minigames").GetComponent<Button>().onClick.AddListener(DidTapMainMenuPlaySpecificMinigames);
            GameObject.Find("High score text").GetComponent<TextMeshProUGUI>().text = "High score: " + PlayerPrefs.GetInt("HighScore", 0);
        }

        void DidTapMainMenuPlay()
        {
            unplayedMinigameSceneNames = minigameSceneNames;
            lives = 3;
            score = 0;

            StartCoroutine(ShowSceneWithTransition("Score and lives", Screen.HighScoreScoreScreen, CompleteScoreAndLivesSetup));
        }

        void CompleteScoreAndLivesSetup()
        {
            GameObject.Find("Score text").GetComponent<TextMeshProUGUI>().text = "" + score;
            UpdateLifeIcons();
            StartCoroutine("ContinueHighScoreAfterDelay");
        }

        IEnumerator ContinueHighScoreAfterDelay()
        {
            yield return new WaitForSeconds(2);
            ContinueHighScoreGame();
        }

        void ContinueHighScoreGame()
        {
            switch (currentScreen)
            {
                case Screen.HighScoreScoreScreen:
                    // If we're dead or out of mini games to play
                    if (lives <= 0 || unplayedMinigameSceneNames.Count == 0)
                    {
                        // Update high score if higher
                        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
                        if (score > currentHighScore)
                        {
                            PlayerPrefs.SetInt("HighScore", score);
                        }

                        // Back to main menu
                        StartCoroutine(ShowSceneWithTransition("Main Menu", Screen.MainMenu, SetupMainMenu));
                    }
                    else
                    {
                        // Choose a random minigame to play next
                        int ind = Random.Range(0, unplayedMinigameSceneNames.Count - 1);
                        currentMinigame = unplayedMinigameSceneNames[ind];
                        unplayedMinigameSceneNames.RemoveAt(ind);
                        StartCoroutine(ShowSceneWithTransition(currentMinigame, Screen.HighScoreMinigame, SetupHighScoreMinigameCompletionHandler));
                    }
                    break;

                case Screen.HighScoreMinigame:
                    // We were in a minigame so return to the score screen
                    currentScreen = Screen.HighScoreMinigame;
                    StartCoroutine(ShowSceneWithTransition("Score and lives", Screen.HighScoreScoreScreen, CompleteScoreAndLivesSetup));
                    break;
            }
        }

        void DidWinMinigame()
        {
            score += 1;
            ContinueHighScoreGame();
        }

        void DidLoseMinigame()
        {
            lives -= 1;
            ContinueHighScoreGame();
        }

        void DidCompleteSpecificMinigame()
        {
            currentScreen = Screen.PracticeMenu;
            StartCoroutine(ShowSceneWithTransition("Minigame Menu", Screen.PracticeMenu, CompleteMiniGameMenuSetup));
        }

        void UpdateLifeIcons()
        {
            GameObject left = GameObject.Find("Life icon left");
            GameObject right = GameObject.Find("Life icon right");
            GameObject center = GameObject.Find("Life icon center");
            GameObject gameOver = GameObject.Find("Game over text");
            GameObject livesTitle = GameObject.Find("Lives title text");

            if (lives <= 0)
            {
                gameOver.SetActive(true);
                gameOver.GetComponent<TextMeshProUGUI>().text = "Game over";
                livesTitle.SetActive(false);
                left.SetActive(false);
                center.SetActive(false);
                right.SetActive(false);
            }
            else
            {
                gameOver.SetActive(false);
                livesTitle.SetActive(true);

                switch (lives)
                {
                    case 1:
                        left.SetActive(false);
                        center.SetActive(true);
                        right.SetActive(false);
                        break;
                    case 2:
                        left.SetActive(true);
                        center.SetActive(true);
                        right.SetActive(false);
                        break;
                    case 3:
                        left.SetActive(true);
                        center.SetActive(true);
                        right.SetActive(true);
                        break;
                }
            }
        }

        void DidTapMainMenuPlaySpecificMinigames()
        {
            currentScreen = Screen.PracticeMenu;
            StartCoroutine(ShowSceneWithTransition("Minigame Menu", Screen.PracticeMenu, CompleteMiniGameMenuSetup));
        }

        void BackToMainMenu()
        {
            StartCoroutine(ShowSceneWithTransition("Main Menu", Screen.MainMenu, SetupMainMenu));
        }

        void CompleteMiniGameMenuSetup()
        {
            GameObject.Find("Back button").GetComponent<Button>().onClick.AddListener(BackToMainMenu);

            // Add new minigame here
            // Here we set the callback on the button for each minigame. Make sure the button name is correct.
            GameObject.Find("Alphabet").GetComponent<Button>().onClick.AddListener(DidTapAlphabetGame);
            GameObject.Find("Alphabetize").GetComponent<Button>().onClick.AddListener(DidTapAlphabetizeGame);
            GameObject.Find("Awp").GetComponent<Button>().onClick.AddListener(DidTapAwpGame);
            GameObject.Find("Button Mash").GetComponent<Button>().onClick.AddListener(DidTapButtonMashGame);
            GameObject.Find("Capsta").GetComponent<Button>().onClick.AddListener(DidTapCapstaGame);
            GameObject.Find("Fally Bird").GetComponent<Button>().onClick.AddListener(DidTapFallyBirdGame);
            GameObject.Find("Fast or Slow You Decide").GetComponent<Button>().onClick.AddListener(DidTapFastOrSlowYouDecideGame);
            GameObject.Find("Field Goal").GetComponent<Button>().onClick.AddListener(DidTapFieldGoalGame);
            GameObject.Find("Graduation").GetComponent<Button>().onClick.AddListener(DidTapGraduationGame);
            GameObject.Find("Jump Rope").GetComponent<Button>().onClick.AddListener(DidTapJumpRopeGame);
            GameObject.Find("Keepie Uppie").GetComponent<Button>().onClick.AddListener(DidTapKeepieUppieGame);
            GameObject.Find("Relax").GetComponent<Button>().onClick.AddListener(DidTapRelaxGame);
            GameObject.Find("Mouse Maze").GetComponent<Button>().onClick.AddListener(DidTapMouseMazeGame);
            GameObject.Find("Bubble Pop").GetComponent<Button>().onClick.AddListener(DidTapBubblePopGame);
            GameObject.Find("Split").GetComponent<Button>().onClick.AddListener(DidTapSplitGame);
            GameObject.Find("Punch").GetComponent<Button>().onClick.AddListener(DidTapPunchGame);
        }

        // Specific minigame menu callbacks        
        // Add new minigame here: a function that gets called when a player clicks the new minigame button
        void DidTapAlphabetGame()
        {
            ShowAndSetupSpecificMinigame("Alphabet");
        }

        void DidTapAlphabetizeGame()
        {
            ShowAndSetupSpecificMinigame("Alphabetize");
        }

        void DidTapAwpGame()
        {
            ShowAndSetupSpecificMinigame("Awp");
        }

        void DidTapButtonMashGame()
        {
            ShowAndSetupSpecificMinigame("Button Mash");
        }

        void DidTapCapstaGame()
        {
            ShowAndSetupSpecificMinigame("Capsta");
        }

        void DidTapFallyBirdGame()
        {
            ShowAndSetupSpecificMinigame("Fally Bird");
        }

        void DidTapFastOrSlowYouDecideGame()
        {
            ShowAndSetupSpecificMinigame("Fast or Slow You Decide");
        }

        void DidTapFieldGoalGame()
        {
            ShowAndSetupSpecificMinigame("Field Goal");
        }

        void DidTapGraduationGame()
        {
            ShowAndSetupSpecificMinigame("Graduation");
        }

        void DidTapJumpRopeGame()
        {
            ShowAndSetupSpecificMinigame("Jump Rope");
        }

        void DidTapKeepieUppieGame()
        {
            ShowAndSetupSpecificMinigame("Keepie Uppie");
        }

        void DidTapMouseMazeGame()
        {
            ShowAndSetupSpecificMinigame("Mouse Maze");
        }

        void DidTapRelaxGame()
        {
            ShowAndSetupSpecificMinigame("Relax");
        }

        void DidTapBubblePopGame()
        {
            ShowAndSetupSpecificMinigame("Bubble Pop");
        }

        void DidTapSplitGame()
        {
            ShowAndSetupSpecificMinigame("Split");
        }

        void DidTapPunchGame()
        {
            ShowAndSetupSpecificMinigame("Punch");
        }

        void ShowAndSetupSpecificMinigame(string sceneName)
        {
            currentMinigame = sceneName;
            StartCoroutine(ShowSceneWithTransition(sceneName, Screen.PracticeMinigame, SetupSpecificMinigameCompletionHandler));
        }

        void SetupSpecificMinigameCompletionHandler()
        {
            MinigameCompletionHandler completionHandler = FindObjectOfType<MinigameCompletionHandler>();
            completionHandler.WinCallback = DidCompleteSpecificMinigame;
            completionHandler.LoseCallback = DidCompleteSpecificMinigame;
        }

        void SetupHighScoreMinigameCompletionHandler()
        {
            MinigameCompletionHandler completionHandler = FindObjectOfType<MinigameCompletionHandler>();
            completionHandler.WinCallback = DidWinMinigame;
            completionHandler.LoseCallback = DidLoseMinigame;
        }

        IEnumerator ShowSceneWithTransition(string sceneName, Screen screen, UnityAction callback)
        {
            currentScreen = screen;

            sceneLoaded = false;
            AsyncOperation transition = SceneManager.LoadSceneAsync("Transition", LoadSceneMode.Additive);
            while (!sceneLoaded)
            {
                yield return null;
            }

            Animator TransitionAnimator = GameObject.Find("Transition Canvas").GetComponent<Animator>();
            TransitionAnimator.SetTrigger("In");

            // The duration of the transition in animation
            yield return new WaitForSeconds(0.3f);

            for (int x = 0; x < SceneManager.sceneCount; x++)
            {
                Scene s = SceneManager.GetSceneAt(x);
                if (s.name != "SceneCoordinator" && s.name != "Transition")
                {
                    SceneManager.UnloadSceneAsync(s.name);
                }
            }

            sceneLoaded = false;
            AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!sceneLoaded)
            {
                yield return null;
            }

            AudioListener[] listeners = FindObjectsOfType<AudioListener>();
            foreach (AudioListener listener in listeners) {
                if (listener.gameObject.name != "SoundManager") {
                    Destroy(listener);
                }
            }
            
            TransitionAnimator.SetTrigger("Out");

            // The duration of the transition in animation
            yield return new WaitForSeconds(0.25f);

            SceneManager.UnloadSceneAsync("Transition");

            if (callback != null)
            {
                callback.Invoke();
            }
        }

        public void SceneLoaded()
        {
            sceneLoaded = true;
        }
    }
}
