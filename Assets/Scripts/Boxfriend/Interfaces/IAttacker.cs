namespace Boxfriend
{
    internal interface IAttacker
    {
        public int AttackDamage { get; }
        public void Attack();
    }
}
