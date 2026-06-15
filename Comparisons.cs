using System.Text;

namespace Examples.Text
{
    public static class Comparisons
    {
        public enum SoundexSimilarity
        {
            None = 0,
            Weak = 1,
            Moderate = 2,
            Strong = 3,
            Exact = 4
        }

        extension(string source)
        {
            /// <summary>
            /// Calculate the difference between 2 strings using the Levenshtein distance algorithm
            /// </summary>
            public int LevenshteinDistance(string destination)
            {
                var source1Length = source.Length;
                var source2Length = destination.Length;

                var matrix = new int[source1Length + 1, source2Length + 1];

                // First calculation, if one entry is empty return full length
                if (source1Length == 0)
                    return source2Length;

                if (source2Length == 0)
                    return source1Length;

                // Initialization of matrix with row size source1Length and columns size source2Length
                for (var i = 0; i <= source1Length; matrix[i, 0] = i++) { }
                for (var j = 0; j <= source2Length; matrix[0, j] = j++) { }

                // Calculate rows and collumns distances
                for (var i = 1; i <= source1Length; i++)
                {
                    for (var j = 1; j <= source2Length; j++)
                    {
                        var cost = (destination[j - 1] == source[i - 1]) ? 0 : 1;

                        matrix[i, j] = Math.Min(
                            Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                            matrix[i - 1, j - 1] + cost);
                    }
                }
                // return result
                return matrix[source1Length, source2Length];
            }

            public decimal LevenshteinDifference(string destination)
            {
                int distance = source.LevenshteinDistance(destination);
                return 1 - (decimal)distance / Math.Max(source.Length, destination.Length);
            }

            /// <summary>
            /// Calculate the Soundex code for a given word
            /// </summary>
            public string Soundex()
            {
                if (string.IsNullOrWhiteSpace(source))
                    return string.Empty;

                // Convert to uppercase for consistency
                source = source.ToUpperInvariant();

                // Soundex mapping table
                string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string codes = "01230120022455012623010202";

                // Keep the first letter
                StringBuilder result = new StringBuilder();
                result.Append(source[0]);

                // Encode the rest of the letters
                char prevCode = codes[letters.IndexOf(source[0])];
                for (int i = 1; i < source.Length; i++)
                {
                    int letterIndex = letters.IndexOf(source[i]);
                    if (letterIndex == -1) continue; // Skip non-alphabetic characters

                    char code = codes[letterIndex];

                    // Skip duplicates and vowels (code '0')
                    if (code != '0' && code != prevCode)
                        result.Append(code);

                    prevCode = code;
                }

                // Pad with zeros or trim to ensure length is 4
                while (result.Length < 4)
                    result.Append('0');

                return result.ToString(0, 4);
            }

            public SoundexSimilarity SoundexDifference(string destination)
            {
                int result = 0;
                if (source.Equals(string.Empty) || destination.Equals(string.Empty))
                    return 0;
                string soundexSource = source.Soundex();
                string soundexDestination = destination.Soundex();
                if (soundexSource.Equals(soundexDestination))
                    result = 4;
                else
                {
                    if (soundexSource[0] == soundexDestination[0])
                        result = 1;
                    string sub1 = soundexSource.Substring(1, 3); //characters 2, 3, 4
                    if (soundexDestination.IndexOf(sub1) > -1)
                    {
                        result += 3;
                        return (SoundexSimilarity)result;
                    }
                    string sub2 = soundexSource.Substring(2, 2); //characters 3, 4
                    if (soundexDestination.IndexOf(sub2) > -1)
                    {
                        result += 2;
                        return (SoundexSimilarity)result;
                    }
                    string sub3 = soundexSource.Substring(1, 2); //characters 2, 3
                    if (soundexDestination.IndexOf(sub3) > -1)
                    {
                        result += 2;
                        return (SoundexSimilarity)result;
                    }
                    char sub4 = soundexSource[1];
                    if (soundexDestination.IndexOf(sub4) > -1)
                        result++;
                    char sub5 = soundexSource[2];
                    if (soundexDestination.IndexOf(sub5) > -1)
                        result++;
                    char sub6 = soundexSource[3];
                    if (soundexDestination.IndexOf(sub6) > -1)
                        result++;
                }
                return (SoundexSimilarity)result;
            }
        }
    }
}
