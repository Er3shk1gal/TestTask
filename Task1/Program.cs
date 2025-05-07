using Task1.Utils;

Console.Write("Enter a string to compress (lowercase letters only): ");
string userInput = Console.ReadLine();

try
{
    string compressed = StringCompressor.Compress(userInput);
    string decompressed = StringCompressor.Decompress(compressed);

    Console.WriteLine($"Original: {userInput}");
    Console.WriteLine($"Compressed: {compressed}"); 
    Console.WriteLine($"Decompressed: {decompressed}");
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}
