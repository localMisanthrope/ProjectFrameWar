namespace ProjectFrameWar.Core
{
    internal struct FrameData(int healthMax, int shieldMax, int energyMax, float sprintSpeed, bool isPrime = false, bool isUmbra = false)
    {
        public bool isPrime = isPrime;
        public bool isUmbra = isUmbra;

        public int healthMax;
        public int shieldsMax;
        public int energyMax;

        public float sprintSpeed;
    }
}