using System.Text.RegularExpressions;

string[] lines = File.ReadAllLines("input.txt");

// part 1
List<NumberPair> operations = new List<NumberPair>();

foreach (string line in lines)
{
    MatchCollection mulMatches = Regex.Matches(line, @"mul\(\d+,\d+\)");

    foreach (Match match in mulMatches)
    {
        string matchValue = match.Value;

        MatchCollection numberMatches = Regex.Matches(matchValue, @"\d+");

        int firstNumber = int.Parse(numberMatches[0].Value);
        int secondNumber = int.Parse(numberMatches[1].Value);

        operations.Add(new NumberPair(firstNumber, secondNumber));
    }
}

int operationsSum = 0;

foreach (NumberPair numberPair in operations) 
{
    operationsSum += numberPair.getProduct();
}

Console.WriteLine($"The sum of all operations is {operationsSum}");

// part 2

List<NumberPair> enabledOperations = new List<NumberPair>();
bool isEnabled = true;

foreach (string line in lines)
{
    MatchCollection mulMatches = Regex.Matches(line, @"(mul\(\d+,\d+\))|(do\(\))|(don\'t\(\))");

    foreach (Match match in mulMatches)
    {
        string matchValue = match.Value;

        if (matchValue.Equals("do()"))
        {
            isEnabled = true;
        }
        else if (matchValue.Equals("don\'t()")) 
        {
            isEnabled = false;
        }
        else 
        {
            if (isEnabled) 
            {
                MatchCollection numberMatches = Regex.Matches(matchValue, @"\d+");

                int firstNumber = int.Parse(numberMatches[0].Value);
                int secondNumber = int.Parse(numberMatches[1].Value);

                enabledOperations.Add(new NumberPair(firstNumber, secondNumber));
            }
        }
    }
}

int enabledOperationsSum = 0;

foreach (NumberPair numberPair in enabledOperations) 
{
    enabledOperationsSum += numberPair.getProduct();
}

Console.WriteLine($"The sum of all enabled operations is {enabledOperationsSum}");


class NumberPair 
{
    private int a;
    private int b;

    public NumberPair(int a, int b) 
    {
        this.a = a;
        this.b = b;
    }

    public int getProduct()
    {
        return a * b;
    }
}