using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace TruthOrLie_Demo
{
    public class HelpInfoPanel : BaseController
    {
        void Start()
        {
            transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.GetSystem<UISystem>().CloseUI("HelpInfoPanel");
            });
        }
    }
}