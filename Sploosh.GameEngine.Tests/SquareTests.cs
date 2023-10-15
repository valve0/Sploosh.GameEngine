using Sploosh.GameEngine;
using Sploosh.GameEngine.Model;
using Xunit;

namespace SplooshGameEngine
{
    public class SquareTests
    {
        private Square _square;

        public SquareTests()
        {
            _square = new Square();
        }

        [Fact]
        public void ReturnHitWhenAttackSquareWithSquid()
        {
            //Create 2 long squid
            Squid squid = new Squid(2);

            _square.Squid = squid;

            // Act
            var returnValue = _square.AttackSquid();
            Assert.Equal(AttackResultCode.Hit, returnValue);
        }

        [Fact]
        public void ReturnMissWhenAttackSquareWithNoSquid()
        {
            // Act
            var returnValue = _square.AttackSquid();
            Assert.Equal(AttackResultCode.Miss, returnValue);
        }

        [Fact]
        public void ReturnSquidDeadWhenAttackSquareWithSquidAlmostDead()
        {
            //Create 2 long squid with Hit Counter = 1
            Squid squid = new Squid(2);
            squid.HitCounter = 1;
            _square.Squid = squid;

            // Act
            var returnValue = _square.AttackSquid();
            Assert.Equal(AttackResultCode.SquidDead, returnValue);
        }
    }
}
