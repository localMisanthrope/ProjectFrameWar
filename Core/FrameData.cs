namespace ProjectFrameWar.Core
{
    internal struct FrameData(int healthMax, int shieldMax, int energyMax, int armorMax, float sprintSpeed, bool isPrime = false, bool isUmbra = false)
    {
        public bool isPrime = isPrime;
        public bool isUmbra = isUmbra;

        public int healthMax = healthMax;
        public int shieldsMax = shieldMax;
        public int armorMax = armorMax;
        public int energyMax = energyMax;

        public float sprintSpeed = sprintSpeed;
    }
}