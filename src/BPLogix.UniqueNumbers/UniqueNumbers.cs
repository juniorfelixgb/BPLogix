namespace BPLogix.UniqueNumbers
{
    public class UniqueNumbers
    {
        // Process the input file numbersFile and output only the unique values.
        // Input: numbersFile = [1,2,3,4]
        // Output: [1,2,3,4]
        // Explanation: Entire input file is unique, therefore the output will be the same
        //Constraints:
        // 0 <= [count of numbers in input file] <= 10 ^ 9
        // 0 <= [range of value in input file] <= 2 ^ 64
        public int[] ProcessUniqueNumbers(List<int> numbersFile)
        {
            if (ProcessUniqueNumbersValidation(numbersFile))
            {
                throw new ArgumentOutOfRangeException($"The {nameof(numbersFile)} is not valid.");
            }

            var result = new List<int>();
            numbersFile.Sort();
            for (int i = 0; i < numbersFile.Count; i++)
            {
                var number = numbersFile[i];
                if (!result.Contains(number)) result.Add(number);
            }

            return result.ToArray();
        }

        private bool ProcessUniqueNumbersValidation(List<int> numbersFile)
        {
            return numbersFile.Count <= 0 &&
                   numbersFile.Count <= Math.Pow(10, 9) &&
                   numbersFile.Count <= Math.Pow(2, 64);
        }
    }
}
