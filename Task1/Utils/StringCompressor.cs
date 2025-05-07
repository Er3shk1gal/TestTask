using System.Text;

namespace Task1.Utils;

public static class StringCompressor
{
    public static string Compress(string inputString)
    {
        if (string.IsNullOrEmpty(inputString))
        {
            Console.WriteLine("Input string is empty!");
            return "";
        }

        if (inputString.Any(x => !char.IsLower(x) || !char.IsLetter(x)))
        {
            Console.WriteLine("Input string should consist of lowercase letters of the Latin alphabet!");
            throw new Exception("Input string should consist of lowercase letters of the Latin alphabet!");
        }

        StringBuilder compressedStringBuilder = new StringBuilder();

        int sameLetterCount = 1;
        
        for (int i = 0; i < inputString.Length-1; i++)
        {
            if (inputString[i] == inputString[i + 1])
            {
                sameLetterCount += 1;
            }
            else
            {
                compressedStringBuilder.Append(inputString[i]);
                if (sameLetterCount > 1)
                {
                    compressedStringBuilder.Append(sameLetterCount);
                    sameLetterCount = 1;
                }
                
            }
        }
        
        compressedStringBuilder.Append(inputString[^1]);
        if (sameLetterCount>1)
        {
            compressedStringBuilder.Append(sameLetterCount);
        }
        
        return compressedStringBuilder.ToString();
    }

    public static string Decompress(string compresedString)
    {
        if (string.IsNullOrEmpty(compresedString))
        {
            Console.WriteLine("Input string is empty!");
            return string.Empty;
        }
        
        if (char.IsDigit(compresedString[0]) || 
            compresedString.Any(c => !char.IsLower(c) && !char.IsDigit(c)))
        {
            Console.WriteLine("Input string should consist of lowercase letters of the Latin alphabet!");
            throw new Exception("Input string should consist of lowercase letters of the Latin alphabet!");
        }
        
        StringBuilder decompressedStringBuilder = new StringBuilder();
        
        int i = 0;
        
        while (i < compresedString.Length)
        {
            char currentChar = compresedString[i++];
            StringBuilder numberStringBuilder = new StringBuilder();
            
            while (i < compresedString.Length && char.IsDigit(compresedString[i]))
            {
                numberStringBuilder.Append(compresedString[i++]);
            }
            
            int count = 1;
            if (numberStringBuilder.Length > 0)
            {
                string numberStr = numberStringBuilder.ToString();
                
                if (numberStr.Length > 1 && numberStr[0] == '0')
                {
                    Console.WriteLine($"Invalid format '{numberStr}'!");
                    throw new ArgumentException($"Invalid format '{numberStr}'!");
                }
                
                if (!int.TryParse(numberStr, out count) || count < 2)
                {
                    Console.WriteLine($"Invalid format '{numberStr}'!");
                    throw new ArgumentException($"Invalid format '{numberStr}'!");
                }
            }

            decompressedStringBuilder.Append(currentChar, count);
        }
        
        return decompressedStringBuilder.ToString();
    }
}