using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace TruthOrLie_Demo
{
    public enum Element
    {
        NONE,
        FIRE,
        WATER,
        PLANT,
        ROCK,
    }
    public class Player : BaseController
    {
        public struct ChangeShieldElemntEvent
        {
            public Element element;
        }
        public struct ChangeSwordElemntEvent
        {
            public Element element;
        }
        Transform _shieldTrans;
        [SerializeField] Element _shieldElement;
        public Element ShieldElement
        {
            get { return _shieldElement; }
            set { _shieldElement = value; }
        }
        Transform _swordTrans;
        [SerializeField] Element _swordElement;
        public Element SwordElement
        {
            get { return _swordElement; }
            set { _swordElement = value; }
        }
        public Button UpBtn;
        public Button DownBtn;
        public Button LeftBtn;
        public Button RightBtn;
        int _mapX;
        int _mapY;
        float _hp;
        public float HP
        {
            get { return _hp; }
            set { _hp = value; }
        }
        public int StepNumber;
        void Start()
        {
            GameRuntimeModel gameRuntimeModel = this.GetModel<GameRuntimeModel>();

            _shieldTrans = transform.Find("Shield");
            _swordTrans = transform.Find("Sword");

            UpBtn.onClick.AddListener(() =>
            {
                if (_mapY + 1 >= gameRuntimeModel.Map.GetLength(1)) return;
                _mapY++;
                Vector3 pos = transform.position;
                transform.position = pos + new Vector3(0, 2, 0);
                this.SendCommand(new ReachNodeCommand(this, gameRuntimeModel.Map[_mapX, _mapY].GetComponent<Node>()));
            });

            DownBtn.onClick.AddListener(() =>
            {
                if (_mapY - 1 < 0) return;
                _mapY--;
                Vector3 pos = transform.position;
                transform.position = pos + new Vector3(0, -2, 0);
                this.SendCommand(new ReachNodeCommand(this, gameRuntimeModel.Map[_mapX, _mapY].GetComponent<Node>()));
            });

            LeftBtn.onClick.AddListener(() =>
            {
                if (_mapX - 1 < 0) return;
                _mapX--;
                Vector3 pos = transform.position;
                transform.position = pos + new Vector3(-2, 0, 0);
                this.SendCommand(new ReachNodeCommand(this, gameRuntimeModel.Map[_mapX, _mapY].GetComponent<Node>()));
            });

            RightBtn.onClick.AddListener(() =>
            {
                if (_mapX + 1 >= gameRuntimeModel.Map.GetLength(0)) return;
                _mapX++;
                Vector3 pos = transform.position;
                transform.position = pos + new Vector3(2, 0, 0);
                this.SendCommand(new ReachNodeCommand(this, gameRuntimeModel.Map[_mapX, _mapY].GetComponent<Node>()));
            });

            this.RegisterEvent<ChangeShieldElemntEvent>(updateEvent =>
            {
                _shieldElement = updateEvent.element;
                switch (updateEvent.element)
                {
                    case Element.FIRE: _shieldTrans.GetComponent<SpriteRenderer>().color = new Color(236f / 255, 68f / 255, 51f / 255, 1f); break;
                    case Element.WATER: _shieldTrans.GetComponent<SpriteRenderer>().color = new Color(1f / 255, 156f / 255, 255f / 255, 1f); break;
                    case Element.PLANT: _shieldTrans.GetComponent<SpriteRenderer>().color = new Color(58f / 255, 194f / 255, 47f / 255, 1f); break;
                    case Element.ROCK: _shieldTrans.GetComponent<SpriteRenderer>().color = new Color(223f / 255, 166f / 255, 38f / 255, 1f); break;
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<ChangeSwordElemntEvent>(updateEvent =>
            {
                _swordElement = updateEvent.element;
                switch (updateEvent.element)
                {
                    case Element.FIRE: _swordTrans.GetComponent<SpriteRenderer>().color = new Color(236f / 255, 68f / 255, 51f / 255, 1f); break;
                    case Element.WATER: _swordTrans.GetComponent<SpriteRenderer>().color = new Color(1f / 255, 156f / 255, 255f / 255, 1f); break;
                    case Element.PLANT: _swordTrans.GetComponent<SpriteRenderer>().color = new Color(58f / 255, 194f / 255, 47f / 255, 1f); break;
                    case Element.ROCK: _swordTrans.GetComponent<SpriteRenderer>().color = new Color(223f / 255, 166f / 255, 38f / 255, 1f); break;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            PlayerInit();

        }

        public void PlayerInit()
        {
            GameRuntimeModel gameRuntimeModel = this.GetModel<GameRuntimeModel>();

            _shieldElement = Element.FIRE;
            _swordElement = Element.FIRE;
            _hp = 100f;

            _mapX = 0;
            _mapY = 0;

            StepNumber = -1;

            transform.position = Vector3.zero;

            this.SendCommand(new ReachNodeCommand(this, gameRuntimeModel.Map[_mapX, _mapY].GetComponent<Node>()));
        }

    }
}