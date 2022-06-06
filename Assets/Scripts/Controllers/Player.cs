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
        void Start()
        {
            _shieldTrans = transform.Find("Shield");
            _swordTrans = transform.Find("Sword");
            _shieldElement = Element.FIRE;
            _swordElement = Element.FIRE;

            this.RegisterEvent<ChangeShieldElemntEvent>(updateEvent =>
            {
                _shieldElement = updateEvent.element;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<ChangeSwordElemntEvent>(updateEvent =>
            {
                _swordElement = updateEvent.element;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}