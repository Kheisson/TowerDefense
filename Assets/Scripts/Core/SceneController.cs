using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneController : MonoBehaviour
    {
        #region Consts

        private const string GAME_SCENE_NAME = "Main";
        private const string GAME_OVER_SCENE_NAME = "GameOverScene";
        private const string MAIN_MENU_SCENE_NAME = "MainMenu";

        #endregion
        
        #region Methods

        public void StartGame()
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
        }

        public void LoadGameOverScene()
        { 
            SceneManager.LoadSceneAsync(GAME_OVER_SCENE_NAME, LoadSceneMode.Additive);
        }

        public void BackToMainMenu()
        {
            SceneManager.UnloadSceneAsync(GAME_OVER_SCENE_NAME);
            SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
        }

        #endregion
    }
}