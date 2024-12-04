string[] characterGrid = File.ReadAllLines("input.txt");

// part 1
List<string> wordsToFind = new List<string> {"XMAS"};

Dictionary<string, int> results = SolveWordSearch(characterGrid, wordsToFind);

foreach (KeyValuePair<string, int> word in results)
{
    Console.WriteLine($"Word: {word.Key}, Count: {word.Value}");
}

// part 2
int xmasPatternCount = CountXmasPatterns(characterGrid);
Console.WriteLine($"Number of X-MAS patterns: {xmasPatternCount}");


// functions of part 1
Dictionary<string, int> SolveWordSearch(string[] characterGrid, List<string> wordsToFind)
{
    int rows = characterGrid.Length;
    int columns = characterGrid[0].Length;

    Dictionary<string, int> wordCounts = new Dictionary<string, int>();

    // directions: (rowOffset, colOffset)
    int[,] directions = {
        {-1, 0}, {1, 0},    // vertical: going up, going down
        {0, -1}, {0, 1},    // horizontal: going left, going right
        {-1, -1}, {-1, 1}, {1, -1}, {1, 1}  // diagonal: going top left, going top right, going bottom left, going bottom right
    };

    foreach (string word in wordsToFind)
    {
        wordCounts.Add(word, 0);
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // check for word in every direction
                for (int direction = 0; direction < directions.GetLength(0); direction++)
                {
                    int rowOffset = directions[direction, 0];
                    int columnOffset = directions[direction, 1];
                    
                    if (MatchesSearchWord(characterGrid, word, row, column, rowOffset, columnOffset))
                    {
                        wordCounts[word]++;
                    }
                }
            }
        }
    }
    return wordCounts;
}

bool MatchesSearchWord(string[] characterGrid, string word, int startRow, int startColumn, int rowOffset, int columnOffset)
{
    int wordLength = word.Length;
    int rows = characterGrid.Length;
    int columns = characterGrid[0].Length;

    for (int i = 0; i < wordLength; i++)
    {
        int currentRow = startRow + i * rowOffset;
        int currentColumn = startColumn + i * columnOffset;

        // check if we are outside of the grid bounds
        if (currentRow < 0 || currentRow >= rows || currentColumn < 0 || currentColumn >= columns)
        {
            return false;
        }

        // check if the current grid character does not match with the current word character
        if (characterGrid[currentRow][currentColumn] != word[i])
        {
            return false;
        }
    }

    return true;
}

// functions of part 2
int CountXmasPatterns(string[] characterGrid)
{
    int rows = characterGrid.Length;
    int columns = characterGrid[0].Length;
    int count = 0;

    // start row on 1 and limit it to "row < rows - 1" to avoid edges of grid
    for (int row = 1; row < rows - 1; row++) 
    {
        // start column on 1 and limit it to "column < columns - 1" to avoid edges of grid
        for (int columnn = 1; columnn < columns - 1; columnn++) 
        {
            // check if current character is 'A'
            if (characterGrid[row][columnn] == 'A')
            {
                if (IsXmasPattern(characterGrid, row, columnn))
                {
                    count++;
                }
            }
        }
    }
    return count;
}

bool IsXmasPattern(string[] characterGrid, int row, int column)
{
    bool isXmasPattern = false;

    // check top-left to bottom-right diagonal
    bool topLeftToBottomRight =
        characterGrid[row - 1][column - 1] == 'M' && // top-left 'M'
        characterGrid[row + 1][column + 1] == 'S';   // bottom-right 'S'

    bool bottomRightToTopLeft = 
        characterGrid[row - 1][column - 1] == 'S' && // top-left 'S'
        characterGrid[row + 1][column + 1] == 'M';   // bottom-right 'M'


    // check top-right to bottom-left diagonal
    bool topRightToBottomLeft =
        characterGrid[row - 1][column + 1] == 'M' && // Top-right 'M'
        characterGrid[row + 1][column - 1] == 'S';   // Bottom-left 'S'

    bool bottomLeftToTopRight =
        characterGrid[row - 1][column + 1] == 'S' && // Top-right 'S'
        characterGrid[row + 1][column - 1] == 'M';   // Bottom-left 'M'

    // check if there is a "MAS" in any orientation on BOTH diagonals
    if ((topLeftToBottomRight || bottomRightToTopLeft) && (topRightToBottomLeft || bottomLeftToTopRight)) 
    {
        isXmasPattern = true;
    }

    return isXmasPattern;
}