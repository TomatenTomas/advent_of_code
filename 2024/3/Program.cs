using System.Text.RegularExpressions;

class Tokenizer {
    static void Main() {
        Valid_tokens tokens = new Valid_tokens();
        int sum = 0;

        StreamReader reader = File.OpenText("input.txt");
        string input_full = reader.ReadToEnd().Replace("\n", "");

        MatchCollection matched_muls = tokens.regex_mul.Matches(input_full);
        MatchCollection matched_dos = tokens.regex_do.Matches(input_full);
        MatchCollection matched_donts = tokens.regex_dont.Matches(input_full);

        IEnumerable<Match> combined = matched_muls.OfType<Match>()
                              .Concat(matched_dos.OfType<Match>())
                              .Concat(matched_donts.OfType<Match>());

        bool add = true;
        foreach(Match match in combined.OrderBy(match => match.Index)) {
            if (match.Value == "do()") {
                add = true;
            } else if (match.Value == "don\'t()") {
                add = false;
            } else {
                if (add) {
                    int mul1 = int.Parse(match.Value.Split(",")[0].Split("(")[1]);
                    int mul2 = int.Parse(match.Value.Split(",")[1].Split(")")[0]);
                    sum += mul1*mul2;
                }
            }
        }
        Console.WriteLine("Sum: {0}", sum);
    }
}

public class Valid_tokens {
    public Regex regex_mul = new Regex(@"(mul\(\d{1,}\,\d{1,}\))");
    public Regex regex_do = new Regex(@"(do\(\))");
    public Regex regex_dont = new Regex(@"(don\'t\(\))");
}


// Part 1 169021493
// Part 2 111762583