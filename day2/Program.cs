string[] lines = File.ReadAllLines("input.txt");

int minDeviation = 1;
int maxDeviation = 3;

// part 1
int safeReportCount = 0;

foreach (string line in lines) 
{
    int[] levelSequence = Array.ConvertAll(line.Split(" "), int.Parse);

    if (isSequenceSafe(levelSequence)) 
    {
        safeReportCount++;
    }
}

Console.WriteLine($"The number of safe reports is {safeReportCount}");

// part 2
int dampenedSafeReportCount = 0;

foreach (string line in lines) 
{
    int[] levelSequence = Array.ConvertAll(line.Split(" "), int.Parse);
    
    bool isSafe = isSequenceSafe(levelSequence);

    // if the sequence is not safe then try removing a single element at an index until it works or until the end of the array is reached
    for (int i = 0; i < levelSequence.Length && !isSafe; i++)
    {
        List<int> modifiedList = new List<int>(levelSequence);
        modifiedList.RemoveAt(i);
        int[] modifiedLevelSequence = modifiedList.ToArray<int>();

        isSafe = isSequenceSafe(modifiedLevelSequence);
    }

    if (isSafe) 
    {
        dampenedSafeReportCount++;
    }
}

Console.WriteLine("The number of dampened safe reports is " + dampenedSafeReportCount);


bool isSequenceSafe(int[] sequence) 
{
    bool isIncreasing = sequence[0] > sequence[1];
    bool isSafe = true;

    for (int i = 0; i < sequence.Length - 1 && isSafe; i++)
    {
        int leftLevel = sequence[i];
        int rightLevel = sequence[i + 1];

        int levelDiff = Math.Abs(leftLevel - rightLevel);

        if ((leftLevel > rightLevel) != isIncreasing || levelDiff < minDeviation || levelDiff > maxDeviation) 
        {
            isSafe = false;
        }
    }

    return isSafe;
}