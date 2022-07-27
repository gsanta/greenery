namespace Character.ability
{
    public interface IAbility
    {
        public AbilityType AbilityType { get; }
        public bool IsActive { get; set; }
    }
}