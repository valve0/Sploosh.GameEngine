using Sploosh.GameEngine;
using Sploosh.GameEngine.Model;
using Xunit;

namespace SplooshGameEngine
{
    public class SquidTests
    {
        private readonly Squid _twoLongSquid;
        private readonly Squid _threeLongSquid;
        private readonly Squid _fourLongSquid;

        public SquidTests()
        {
            // Arrange
            _twoLongSquid = new Squid(2);
            _threeLongSquid = new Squid(3);
            _fourLongSquid = new Squid(4);
        }

        [Fact]
        public void ShouldReturnSquidDeadIfAttacksEqualToLength()
        {

            //Arange
            List<Squid> testSquids = new()
            {
                _twoLongSquid,
                _threeLongSquid,
                _fourLongSquid
            };

            AttackResultCode AttackResult = 0;

            // Act
            foreach (Squid squid in testSquids)
            {
                for (int i = 0; i < squid.Length; i++)
                {
                    AttackResult = squid.Attack();
                }

                // Assert
                Assert.Equal(AttackResultCode.SquidDead, AttackResult);
            }

        }

        [Fact]
        public void ShouldReturnhitIfAttacksNotEqualToLength()
        {
            // Act
            var AttackResult = _twoLongSquid.Attack();

            // Assert
            Assert.Equal(AttackResultCode.Hit, AttackResult);
        }

    }
}