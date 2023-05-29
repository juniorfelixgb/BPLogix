namespace BPLogix.UniqueNumbers.Tests.UnitTests
{
    public class UniqueNumbersTests
    {
        [Fact]
        public void ShouldBe_ThrowArgumentOutOfRangeException_WhenNumbersFileIsMinusToZero()
        {
            // Arrange
            List<int> numbersFile = new List<int> { };
            var processUniqueNumbers = new UniqueNumbers();
            var expected = new ArgumentOutOfRangeException("The numbersFile is not valid.");
            // Act
            var result = Assert.Throws<ArgumentOutOfRangeException>(() => processUniqueNumbers.ProcessUniqueNumbers(numbersFile));
            // Assert
            Assert.Equal(expected.Message, result.Message);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4 })]
        [InlineData(new int[] { 1, 1, 1, 1, 1 }, new int[] { 1 })]
        [InlineData(new int[] { 3, 3, 1, 1, 2, 2 }, new int[] { 1, 2, 3 })]
        public void ShouldBe_ExecuteProcessUniqueNumbers_WhenNumbersFileIsValid_Successfull(int[] numbersFile, int[] expected)
        {
            // Arrange
            var processUniqueNumbers = new UniqueNumbers();
            var request = numbersFile.ToList();
            // Act
            var result = processUniqueNumbers.ProcessUniqueNumbers(request);
            // Assert
            Assert.Equal(result, expected);
        }
    }
}