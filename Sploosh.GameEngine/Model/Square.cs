namespace Sploosh.GameEngine.Model
{
    public class Square
    {
        public Square()
        {
            SquareStatus = SquareStatus.Start;
        }

        public SquareStatus SquareStatus { get; set; }

        public Squid? Squid { get; set; }

        public AttackResultCode AttackSquid()
        {
            //Check to see if square has already been selected
            if (SquareStatus != SquareStatus.Start)
                return AttackResultCode.None;
            else
            {
                if (Squid != null)
                {
                    SquareStatus = SquareStatus.Hit;
                    return Squid.Attack();
                }
                else
                {
                    SquareStatus = SquareStatus.Miss;
                    return AttackResultCode.Miss;
                }
            }
        }
    }
}