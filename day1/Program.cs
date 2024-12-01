using System.Text.RegularExpressions;

// part 1

List<int> list1 = new List<int>();
List<int> list2 = new List<int>();

using (StreamReader streamReader = new StreamReader("input.txt")) 
{
    string? line;

    while ((line = streamReader.ReadLine()) != null) 
    {
        string[] lineValuesStrings = Regex.Split(line, @"\s+");
        list1.Add(Int32.Parse(lineValuesStrings[0]));
        list2.Add(Int32.Parse(lineValuesStrings[1]));
    }
}

list1.Sort();
list2.Sort();

int absDiff = 0;

foreach (var (value1, value2) in list1.Zip(list2))
{
    absDiff += Math.Abs(value1 - value2);

    Console.WriteLine($"{value1} - {value2} -> absDiff = {absDiff}");
}

Console.WriteLine("\nThe absolute difference between the two lists is " + absDiff);

// part 2

List<int> intersectList = list1.Intersect(list2).ToList();
List<KeyValuePair<int, int>> similiarityList = new List<KeyValuePair<int, int>>();

foreach (int intersectValue in intersectList) 
{
    similiarityList.Add(new KeyValuePair<int, int>(intersectValue, list2.FindAll(value => value == intersectValue).Count));
}

int similiarityScore = 0;

foreach (KeyValuePair<int, int> similiarityPair in similiarityList) 
{
    int value = similiarityPair.Key;
    int multiplier = similiarityPair.Value;
    similiarityScore += value * multiplier;
    Console.WriteLine($"{value} * {multiplier} -> similiarityScore = {similiarityScore}");
}

Console.WriteLine("\nThe similiarity score between the two lists is " + similiarityScore);