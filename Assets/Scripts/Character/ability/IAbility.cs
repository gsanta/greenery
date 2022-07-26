namespace Character.ability
{
    public interface IAbility
    {
        public AbilityType AbilityType { get; }
        public bool IsAbilityActive { get; set; }
    }
}