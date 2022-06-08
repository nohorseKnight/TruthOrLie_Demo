using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace TruthOrLie_Demo
{
    public class MenuPanel : BaseController
    {
        void Start()
        {
            transform.Find("ResetButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<InitGameCommand>();
            });

            transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
            });
        }
    }
}