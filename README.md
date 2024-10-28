# WordFinder

WordFinder is a C# utility for finding and ranking the most frequently occurring words within a character matrix. The project demonstrates a simple and efficient word search algorithm, including both linear search and manual sorting techniques without using LINQ, ensuring more control over the execution flow. It is designed for educational purposes, performance analysis, and understanding search algorithm implementations.

## Features

- **Character Matrix Search**: Supports searching for specified words in a matrix of characters.
- **Manual Sorting**: Implements a custom sorting mechanism for ranking words based on their frequency without relying on LINQ.
- **Performance Analysis**: Designed to demonstrate different search techniques and their efficiency with BenchmarkDotNet.

## Implementation Details

### How it Works
The program performs a sequential linear search across the matrix to find the specified words. The search is done horizontally (left-to-right) and vertically (top-to-bottom). The algorithm counts the occurrences of each word and ranks the top N most frequently found words.

### Performance Decisions

To optimize the application's performance, several key decisions were made regarding the runtime and configuration settings:

1. **Native AOT (Ahead-of-Time Compilation)**
   
   The project is configured with `<PublishAot>true</PublishAot>` and `<SelfContained>true</SelfContained>`, enabling Native AOT. This approach compiles the application ahead of time into a platform-specific executable, providing the following benefits:
   - **Improved Startup Time**: By eliminating the need for just-in-time (JIT) compilation, Native AOT reduces startup time, which is especially useful for applications where fast responses are required.
   - **Lower Memory Usage**: Since the executable contains only the necessary code, it reduces memory consumption compared to the regular .NET runtime.
   - **Self-Contained Deployment**: The application is packaged as a single native executable, which simplifies deployment in environments without .NET installed or where .NET compatibility is uncertain.

   This choice ensures that the application can achieve consistent and predictable performance across different environments, making it well-suited for scenarios where low memory usage and fast startup times are critical.

2. **Manual Iteration vs. LINQ**

   The algorithm avoids using LINQ for searching and sorting operations, opting instead for a manual implementation. This approach was chosen to:
   - Gain more control over the execution flow.
   - Reduce overhead in performance-critical scenarios.
   - Demonstrate how manual sorting can be implemented when performance is a must, and how it can result in lower garbage collection activity and improved memory efficiency.

These optimization choices were made to achieve a balance between execution speed, memory consumption, and deployment simplicity, ensuring that the application performs optimally even in resource-constrained or cloud-based environments.

### Sorting and Ranking Logic
The top N words are manually ranked using a custom insertion-based approach to maintain a sorted list. This helps avoid the overhead associated with LINQ-based sorting and allows handling tie-breaking cases by inserting elements at the end of the current priority group.

### Complexity Analysis

- **Complexity**: `O(n * m * w)`
  - `n`: Number of rows in the matrix
  - `m`: Length of each row (number of columns)
  - `w`: Number of words in the word list

The execution time of the algorithm grows linearly with the product of the number of rows, the length of each row, and the number of words.

## Benchmark Results

The performance tests were executed using `BenchmarkDotNet` to compare two different implementations of a word search algorithm: one using manual iteration (`BenchmarkLinearFind`) and the other using LINQ (`BenchmarkLinearFind_WithLinq`). Below are the results:

![Performance Report](docs/performance%20report.PNG)

### Analysis
- **Execution Time**: The execution times for both implementations are quite similar, with only a slight difference in mean execution time. The manual iteration (`BenchmarkLinearFind`) took approximately 4.105 ms, while the LINQ-based implementation (`BenchmarkLinearFind_WithLinq`) took around 4.063 ms.
- **Memory Allocation**: The manual iteration approach showed a significant reduction in memory usage, allocating only 55.99 KB compared to 160.66 KB with the LINQ-based version. This reduction in memory allocation can be beneficial in memory-constrained environments or when processing large datasets. Additionally, lower memory usage can help reduce costs in cloud services where memory consumption directly affects pricing.
- **Garbage Collection**: The number of garbage collection operations (Gen0) was also lower for the manual iteration (23.4375 collections per 1000 operations) compared to the LINQ approach (70.3125 collections per 1000 operations). This suggests that the manual approach results in less pressure on the garbage collector, leading to more efficient memory management.

These results indicate that while both implementations offer similar execution speed, avoiding LINQ significantly reduces memory usage and garbage collection overhead, making it a better choice for performance-critical scenarios.

## xUnit Tests

The program includes a set of automated tests to verify the functionality of the `WordFinder` class. The following 6 tests have been implemented:

1. **Find_ReturnsTop10MostRepeatedWords**: Verifies that the `Find` method returns the top 10 most repeated words in the sample matrix if they are present in the word list.

2. **Find_ReturnsTop10MostRepeatedWords_LargeSet**: Checks that the `Find` method works correctly with a larger dataset and returns the top 10 most repeated words.

3. **Find_ReturnsEmpty_WhenMatrixIsEmpty**: Ensures that the `Find` method returns an empty list when the input matrix is empty.

4. **Find_ReturnsEmpty_WhenWordsNotInMatrix**: Validates that the `Find` method returns an empty list when none of the words in the word stream are present in the matrix.

5. **Find_IgnoresDuplicateWordsInWordStream**: Verifies that the `Find` method ignores duplicate words in the input word stream and returns only unique words.

6. **Find_WorksCorrectly_With64x64RandomMatrix**: Checks that the `Find` method functions correctly with a maximum-sized matrix (64x64), testing with repeated words in each row.

To run the tests, ensure that the xUnit dependencies are installed in your project. You can install them via NuGet Package Manager in Visual Studio.

## Installation

To run this project, you need:

1. [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
2. Clone this repository:

   ```bash
   git clone https://github.com/yourusername/wordfinder.git
  
3. Build the project:

   ```bash
   cd src
   dotnet build

4. Run the application:

   ```bash
   dotnet run

## Example Usage

The following example demonstrates how to use the `WordFinder` and `ReportGenerator` classes to search for words in a matrix and generate a report of the most frequent words found:

1. **Define the matrix**: The matrix is represented as a list of strings, where each string corresponds to a row of characters. This is a 12x12 example matrix.

    ```csharp
    var matrix = new List<string>
    {
        "snowstormblz",
        "rainycloudyy",
        "foggywinddrz",
        "hailstormsun",
        "chillyfrosty",
        "autumnwinter",
        "icydrizzlerz",
        "stormyfreeze",
        "summerskydrz",
        "blizzardcold",
        "freezingrain",
        "windynightsk"
    };
    ```

2. **Define the word stream**: A list of words that we want to find within the matrix. The algorithm will search for these words and determine their frequency of occurrence.

    ```csharp
    var wordStream = new List<string>
    {
        "snow", "storm", "rain", "cloudy", "foggy", "wind",
        "drizzle", "hail", "sunny", "chilly", "frosty", "autumn",
        "winter", "icy", "freeze", "summer", "sky", "blizzard",
        "cold", "freezing", "rainy", "night"
    };
    ```

3. **Create an instance of WordFinder**: The `WordFinder` is initialized with the matrix. This instance will be used to perform the word search.

    ```csharp
    var wordFinder = new WordFinder(matrix);
    ```

4. **Find the word frequency**: The `Find` method is called with the word stream, and it returns a sorted list of the most frequently found words in the matrix, ordered by their frequency.

    ```csharp
    var wordFrequency = wordFinder.Find(wordStream);
    ```

5. **Generate the report**: The `ReportGenerator.GenerateTopNReport` method is then used to print the results to the console, displaying the top words found in the matrix.

    ```csharp
    ReportGenerator.GenerateTopNReport(wordFrequency);
    ```

This example illustrates how the `WordFinder` class can be used to search for specified words in a character matrix and how the results can be formatted into a report using the `ReportGenerator` utility.

## Sample Output

```
Top Words Report:
-----------------
storm
rain
wind
snow
cloudy
foggy
drizzle
hail
chilly
frosty
-----------------
End of report.
```

## Contributing

Feel free to fork this repository and submit pull requests. Contributions are welcome to improve the algorithm, add new features, or optimize the existing code.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
