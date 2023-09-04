namespace SplooshGameEngine.Model
{
    public class Square
    {
        private Squid? _squid;
        private EnvironmentVariables _enviromentVariables;

        public Square(EnvironmentVariables environmentVariables)
        {
            _enviromentVariables = environmentVariables;
            ImagePath = _enviromentVariables.SquareStartImagePath;
        }
    
        public Uri? ImagePath { get; set; }
        public bool AttackStatus { get; set; }

        public Squid? Squid
        {
            get { return _squid; }
            set
            {
                _squid = value;
                if (value != null)
                    AttackStatus = true;
            }
        }

        public AttackResultCode AttackSquid()
        {
            if (Squid != null && AttackStatus == true)
            {
                ImagePath = _enviromentVariables.SquareHitPath;
                return Squid.Attack();
            }
            else
            {
                ImagePath = _enviromentVariables.SquareMissPath;
                return AttackResultCode.Miss;
            }

        }
    }
}