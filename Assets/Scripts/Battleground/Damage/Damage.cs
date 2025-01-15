namespace Battleground
{
    public class Damage
    {
        public float Value { get; private set; }
        public IDamageable Spellcaster { get; private set; }
        public bool IsCanBeBack => Spellcaster != null;

        public Damage(float value, IDamageable spellcaster)
        {
            Value = value;
            Spellcaster = spellcaster;
        }

        public Damage(float value)
        {
            Value = value;
        }
    }
}