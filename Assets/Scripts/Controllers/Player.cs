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
        Transform _swordTrans;
        [SerializeField] Element _swordElement;
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
        void Start()
        {
            GameRuntimeModel gameRuntimeModel = this.GetModel<GameRuntimeModel>();

            _shieldTrans = transform.Find("Shield");
            _swordTrans = transform.Find("Sword");

            PlayerInit();


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
                    case Element.FIRE: _shieldTrans.GetComponent<SpriteRenderer>().color = new Color(253f / 255, 146f / 255, 93f / 255, 1f); break;
                    case Element.WATER: _shieldTrans.GetComponent<SpriteRenderer>().color = new Color(33f / 255, 225f / 255, 225f / 255, 1f); break;
                    case Element.PLANT: _shieldTrans.GetComponent<SpriteRenderer>().color = new Color(35f / 255, 193f / 255, 38f / 255, 1f); break;
                    case Element.ROCK: _shieldTrans.GetComponent<SpriteRenderer>().color = new Color(227f / 255, 179f / 255, 66f / 255, 1f); break;
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<ChangeSwordElemntEvent>(updateEvent =>
            {
                _swordElement = updateEvent.element;
                switch (updateEvent.element)
                {
                    case Element.FIRE: _swordTrans.GetComponent<SpriteRenderer>().color = new Color(253f / 255, 146f / 255, 93f / 255, 1); break;
                    case Element.WATER: _swordTrans.GetComponent<SpriteRenderer>().color = new Color(33f / 255, 225f / 255, 225f / 255, 1); break;
                    case Element.PLANT: _swordTrans.GetComponent<SpriteRenderer>().color = new Color(35f / 255, 193f / 255, 38f / 255, 1); break;
                    case Element.ROCK: _swordTrans.GetComponent<SpriteRenderer>().color = new Color(227f / 255, 179f / 255, 66f / 255, 1); break;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

        }

        void PlayerInit()
        {
            GameRuntimeModel gameRuntimeModel = this.GetModel<GameRuntimeModel>();

            _shieldElement = Element.FIRE;
            _swordElement = Element.FIRE;
            _hp = 100f;

            _mapX = 0;
            _mapY = 0;

            transform.position = Vector3.zero;

            this.SendCommand(new ReachNodeCommand(this, gameRuntimeModel.Map[_mapX, _mapY].GetComponent<Node>()));
        }

    }
}