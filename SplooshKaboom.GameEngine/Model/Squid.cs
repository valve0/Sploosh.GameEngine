namespace SplooshGameEngine.Model
{
    public class Squid
    {
        public Squid() { }

        public Squid(int length)
        {
            Length = length;
            HitCounter = 0;
        }

        public int Length { get; set; }
        public int HitCounter { get; set; }

        /// <summary>
        /// When a squid is attacked, increase hit counter 
        /// and returns true status if killed, else false
        public AttackResultCode Attack()
        {
            HitCounter++;
            if (HitCounter == Length)
                return AttackResultCode.SquidDead; //squid hit and now dead
            else
                return AttackResultCode.Hit; //squid hit but still alive
        }
    }
}
