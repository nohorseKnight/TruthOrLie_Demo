using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UITextMeshPro = TMPro.TMP_Text;

namespace TruthOrLie_Demo
{
    public class InfoPanel : BaseController
    {
        public struct UpdateHPEvent
        {
            public float HP;
        }
        Dropdown _ATKDropdown;
        Dropdown _DEFDropdown;
        Image _HpImage;
        UITextMeshPro _HpTextMesh;
        void Start()
        {
            _ATKDropdown = transform.Find("ATKDropdown").GetComponent<Dropdown>();
            _ATKDropdown.onValueChanged.AddListener(ATKDropdownValueChanged);
            _DEFDropdown = transform.Find("DEFDropdown").GetComponent<Dropdown>();
            _DEFDropdown.onValueChanged.AddListener(DEFDropdownValueChanged);

            _HpImage = transform.Find("HPImage").GetComponent<Image>();
            _HpTextMesh = transform.Find("HPNumber").GetComponent<UITextMeshPro>();

            this.RegisterEvent<UpdateHPEvent>(updateEvent =>
            {
                updateEvent.HP = updateEvent.HP > 100f ? 100f : updateEvent.HP;
                _HpImage.fillAmount = updateEvent.HP / 100f;
                _HpTextMesh.text = $"{updateEvent.HP.ToString("0.")}/100";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void ATKDropdownValueChanged(int value)
        {
            Debug.Log($"value: {value}");
            switch (value)
            {
                case 0:
                    this.SendCommand(new ChangePlayerATKElementCommand(Element.FIRE));
                    break;
                case 1:
                    this.SendCommand(new ChangePlayerATKElementCommand(Element.WATER));
                    break;
                case 2:
                    this.SendCommand(new ChangePlayerATKElementCommand(Element.PLANT));
                    break;
                case 3:
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
                    this.SendCommand(new ChangePlayerDEFElementCommand(Element.FIRE));
                    break;
                case 1:
                    this.SendCommand(new ChangePlayerDEFElementCommand(Element.WATER));
                    break;
                case 2:
                    this.SendCommand(new ChangePlayerDEFElementCommand(Element.PLANT));
                    break;
                case 3:
                    this.SendCommand(new ChangePlayerDEFElementCommand(Element.ROCK));
                    break;
                default:
                    Debug.Log("_DEFDropdown default");
                    break;
            }
        }
    }
}