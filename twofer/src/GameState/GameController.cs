using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twofer.src.UI.GameUI;
using twofer.src.Network;

namespace twofer.src.GameState
{
    class GameController
    {
        private GameState GameState { get; }
        private GameUI GameUI { get; }
        private GameLink GameLink { get; }

        private void GameLoop()
        {

            // StepSpecifics
        }

        private void FullRoundOfPriority()
        {
            // MISSING players in APNAP order
            Player[] playersInOrder = null;
            int castingPlayerIndex = 0;

            while (castingPlayerIndex < playersInOrder.Length)
            {
                var castingPlayer = playersInOrder[castingPlayerIndex];
                var priorityResult = GivePriority(castingPlayer);

                if (priorityResult.Passed)
                {
                    castingPlayerIndex++;
                    continue;
                }

                GameState.CastAbility(priorityResult.AbilityActivation);
            }
        }

        private PriorityResult GivePriority(Player castingPlayer)
        {
            var chosenAbility = ChooseAbility(castingPlayer);
            if (chosenAbility == null)
            {
                return PriorityResult.PlayerPassed;
            }

            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }

        private Ability ChooseAbility(Player choosingPlayer)
        {
            var card = ChooseCard(choosingPlayer);

            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }

        private Card ChooseCard(Player choosingPlayer)
        {
            var chosenObject = MakeChoicePrimitive(choosingPlayer, o =>
            {
                if (o is Card)
                {
                    return true;
                }

                if (o is Tile)
                {
                    var tile = (Tile)o;
                    if (tile.Pawn != null) return true;
                }

                return false;
            });

            if (chosenObject is Card) { return chosenObject as Card; }
            if (chosenObject is Tile) { return (chosenObject as Tile).Pawn.Card; }

            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }

        private GameObject MakeChoicePrimitive(Player                   choosingPlayer,
                                               Func<GameObject, bool>   filter)
        {
            GameObject rt;

            if (choosingPlayer.IsHero)
            {
                while (true)
                {
                    var chosenObject = GameUI.GameObjectChooser.ChooseGameObject();
                    if (filter(chosenObject))
                    {
                        rt = chosenObject;
                        break;
                    }
                }
                GameLink.SendGameObject(rt);
            }
            else
            {
                rt = GameLink.ReceiveGameObject();
            }

            return rt;
        }
    }

    class PriorityResult
    {
        public AbilityActivation AbilityActivation { get; }

        public bool Passed => (((string)(null)).ToString()) == "";

        public static PriorityResult PlayerPassed => null;

    }
}
