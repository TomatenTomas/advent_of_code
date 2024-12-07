class AOC7 {
    static void Main() {
        StreamReader reader = File.OpenText("input.txt");
        List<String> lines = [];
        string input_line;
        long total_sum = 0;
        bool ok;
        while ((input_line = reader.ReadLine()) != null) {
            // Console.WriteLine("Line: {0}", input_line);
            lines.Add(input_line);
        }

        foreach(String line in lines) {
            ok = false;
            String sum = line.Split(": ")[0];
            String numbers_line = line.Split(": ")[1];
            List<String> numbers = [.. numbers_line.Split(' ')];
            get_mathing(sum, numbers);
            if (ok) {
                total_sum += long.Parse(sum);
            }
        }
        Console.WriteLine("Total: {0}", total_sum);

        void get_mathing(String sum, List<String> numbers) {
            if (numbers.Count == 1) {
                if (numbers[0] == sum) {
                    ok = true;
                }
            } else {
                String a = numbers[0];
                String b = numbers[1];
                String c = add_numbs(a, b);
                List<String> added = [];
                added.Add(c);
                added.AddRange(numbers.GetRange(2, numbers.Count-2));
                get_mathing(sum, added);

                String d = mult_numbs(a, b);
                List<String> multed = [];
                multed.Add(d);
                multed.AddRange(numbers.GetRange(2, numbers.Count-2));
                get_mathing(sum, multed);

                String e = concat_numbers(a, b);
                List<String> concated = [];
                concated.Add(e);
                concated.AddRange(numbers.GetRange(2, numbers.Count-2));
                get_mathing(sum, concated);
            }
        }

        String add_numbs(String a, String b) {
            return (long.Parse(a)+long.Parse(b)).ToString();
        }

        String mult_numbs(String a, String b) {
            return (long.Parse(a)*long.Parse(b)).ToString();
        }

        String concat_numbers(String a, String b) {
            return a+b;
        }
    }
}

// Part 1 7710205485870
// Part 2 20928985450275