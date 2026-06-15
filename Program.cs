using Examples.Text;
using static Examples.Text.Comparisons;

string inputWord = "James";
string inputWord2 = "John";

decimal levenshteinDistance = inputWord.LevenshteinDifference(inputWord2);
SoundexSimilarity soundexDifference = inputWord.SoundexDifference(inputWord2);

Console.WriteLine($"Soundex Diff: {inputWord} : {inputWord2} : {soundexDifference}");
Console.WriteLine($"Levenshtein Distance: {inputWord} : {inputWord2} : {levenshteinDistance}");