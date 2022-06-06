using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UITextMeshPro = TMPro.TMP_Text;

namespace TruthOrLie_Demo
{
    public class InfoPanel : BaseController
    {
        Dropdown _ATKDropdown;
        Dropdown _DEFDropdown;
        void Start()
        {
            _ATKDropdown = transform.Find("ATKDropdown").GetComponent<Dropdown>();
            _ATKDropdown.onValueChanged.AddListener(ATKDropdownValueChanged);
            _DEFDropdown = transform.Find("DEFDropdown").GetComponent<Dropdown>();
            _DEFDropdown.onValueChanged.AddListener(DEFDropdownValueChanged);
        }

        void ATKDropdownValueChanged(int value)
        {
            Debug.Log($"value: {value}");
            switch (value)
            {
                case 0:
                    Debug.Log("_ATKDropdown 0");
                    this.SendCommand(new ChangePlayerATKElementCommand(Element.FIRE));
                    break;
                case 1:
                    Debug.Log("_ATKDropdown 1");
                    this.SendCommand(new ChangePlayerATKElementCommand(Element.WATER));
                    break;
                case 2:
                    Debug.Log("_ATKDropdown 2");
                    this.SendCommand(new ChangePlayerATKElementCommand(Element.PLANT));
                    break;
                case 3:
                    Debug.Log("_ATKDropdown 3");
                    this.SendCommand(new ChangePlayerATKElementCommand(Element.ROCK));
                    break;
                default:
                    Debug.Log("_ATKDropdown default");
                    break;
            }
        }

        void DEFDropdownValueChanged(int value)
        {
            Debug.Log($"value: {value}");
            switch (value)
            {
                case 0:
                    Debug.Log("_DEFDropdown 0");
                    break;
                case 1:
                    Debug.Log("_DEFDropdown 1");
                    break;
                case 2:
                    Debug.Log("_DEFDropdown 2");
                    break;
                case 3:
                    Debug.Log("_DEFDropdown 3");
                    break;
                default:
                    Debug.Log("_DEFDropdown default");
                    break;
            }
        }
    }
}