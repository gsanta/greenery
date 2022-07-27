using System.Collections.Generic;

namespace game.character.ability
{
    public class AbilityHandler
    {
        private readonly List<IAbility> _abilities = new();

        public void Add(IAbility ability)
        {
            _abilities.Add(ability);
        }
        
        public IAbility Get(AbilityType type)
        {
            return _abilities.Find((ability) => ability.AbilityType == type);
        }
    }
}