using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace TruthOrLie_Demo
{
    public class PopupPanel : BaseController
    {
        void Start()
        {
            transform.Find("ResetButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<InitGameCommand>();
                this.GetSystem<UISystem>().CloseUI("PopupPanel");
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