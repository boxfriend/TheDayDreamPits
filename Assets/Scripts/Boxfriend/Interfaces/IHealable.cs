namespace Boxfriend
{
    public interface IHealable
    {
        public int Health { get; }
        public bool Heal (int amount);
    }
}
