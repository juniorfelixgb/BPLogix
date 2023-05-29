
using BPLogix.UniqueNumbers;

var processUniqueNumbers = new UniqueNumbers();

var numbersFile = new List<int>() { 3, 3, 1, 1, 2, 2 };

var result = processUniqueNumbers.ProcessUniqueNumbers(numbersFile);

foreach (var number in result)
{
    Console.WriteLine(number);
}