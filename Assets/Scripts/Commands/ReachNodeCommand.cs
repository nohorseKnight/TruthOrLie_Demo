using UnityEngine;
using QFramework;

namespace TruthOrLie_Demo
{
    public class ReachNodeCommand : AbstractCommand
    {
        float[,] ATKEffectTable =
        {
            {0, 0, 0, 0, 0},
            {0, 0, -1, 1, 0},
            {0, 1, 0, -1, 0},
            {0, -1, 1, 0, 0},
            {0, 0, 0, 0, 1}
        };
        float[,] DEFEffectTable =
        {
            {0, 0, 0, 0, 0},
            {0, 1, 2, 0, 0.8f},
            {0, 0, 1, 2, 0.8f},
            {0, 2, 0, 1, 0.8f},
            {0, 0.8f, 0.8f, 0.8f, 2}
        };
        Player _player;
        Node _node;
        public ReachNodeCommand(Player player, Node node)
        {
            _player = player;
            _node = node;
        }
        protected override void OnExecute()
        {
            Debug.Log(_node.Info());

            if (_node.IsVisited)
            {
                Debug.Log("visited");
                MapInfoPanel.MapInfoPanelUpdateEvent updateEvent1 = new MapInfoPanel.MapInfoPanelUpdateEvent();
                updateEvent1.Env = _node.Env;
                updateEvent1.EnemyCountText = $"x {_node.EnemyCount}";
                updateEvent1.HPChangeText = "+0";
                updateEvent1.TipsList = _node.TipsList;
                _node.transform.Find("Environment").gameObject.SetActive(true);
                this.SendEvent(updateEvent1);
                return;
            }

            Debug.Log("not visited");
            _node.IsVisited = true;
            float preHp = _player.HP;

            if (_node.Env == Environment.HP_INCREASE)
            {
                _player.HP += 30f;
            }
            else if (_node.Env == Environment.TRAP)
            {
                _player.HP -= 30f;
            }
            else
            {
                _player.HP += ATKEffectTable[(int)_player.SwordElement, (int)_node.EnemyElement] * 20f * _node.EnemyCount;
                _player.HP -= DEFEffectTable[(int)_player.SwordElement, (int)_node.EnemyElement] * 20f * _node.EnemyCount;
            }

            if (_player.HP > 100f)
            {
                _player.HP = 100f;
            }
            else if (_player.HP < 0)
            {
                this.GetSystem<UISystem>().OpenPopup("Game Over");
            }

            MapInfoPanel.MapInfoPanelUpdateEvent updateEvent2 = new MapInfoPanel.MapInfoPanelUpdateEvent();
            updateEvent2.Env = _node.Env;
            updateEvent2.EnemyElement = _node.EnemyElement;
            updateEvent2.EnemyCountText = $"x {_node.EnemyCount}";
            updateEvent2.HPChangeText = (_player.HP - preHp) < 0 ? $"{_player.HP - preHp}" : $"+{_player.HP - preHp}";
            updateEvent2.TipsList = _node.TipsList;
            _node.transform.Find("Environment").gameObject.SetActive(true);
            this.SendEvent(updateEvent2);

            InfoPanel.UpdateHPEvent updateEvent = new InfoPanel.UpdateHPEvent();
            updateEvent.HP = _player.HP;
            this.SendEvent(updateEvent);
        }
    }
}