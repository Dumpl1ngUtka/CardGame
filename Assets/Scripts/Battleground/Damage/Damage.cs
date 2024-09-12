namespace Battleground
{
    public class Damage
    {
        public float Value { get; private set; }
        public IDamageable Target { get; private set; }
        public IDamageable Spellcaster { get; private set; }
        public bool IsCanBeBack => Spellcaster != null;

        public Damage(float value, IDamageable target, IDamageable spellcaster)
        {
            Value = value;
            Target = target;
            Spellcaster = spellcaster;
        }

        public Damage(float value, IDamageable target)
        {
            Value = value;
            Target = target;
        }
    }
}