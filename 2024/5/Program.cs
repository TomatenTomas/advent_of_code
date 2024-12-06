class AOC5 {
    static void Main() {
        StreamReader reader = File.OpenText("input.txt");
        List<String> input_updates = [];
        List<String> before = [];
        List<String> after = [];
        string input_line;
        while ((input_line = reader.ReadLine()) != null) {
            if (input_line.Contains('|')) {
                before.Add(input_line.Split('|')[0]);
                after.Add(input_line.Split('|')[1]);
                // Console.WriteLine("Line: {0}, before: {1}, after: {2}", input_line, input_line.Split('|')[0],input_line.Split('|')[1]);
            } else if (input_line.Contains(',')) {
                input_updates.Add(input_line);
            }
        }

        List<String> update;
        List<String> imperfect_updates = [];
        int sum = 0;
        foreach(String updates in input_updates) {
            update = [.. updates.Split(',')]; // Same as "updates.Split(',').ToList<String>();".. magic
            bool correct = check_if_correctly_ordered(update, before, after);

            if (correct) {
                sum += int.Parse(update[update.Count()/2]);
            } else {
                imperfect_updates.Add(updates);
            }
        }
        Console.WriteLine("Sum: {0}", sum);
        // Part 1 result 4185

        // Part 2
        sum = 0;
        foreach (String imp_update in imperfect_updates) {
            update = [.. imp_update.Split(',')]; // Same as "updates.Split(',').ToList<String>();".. magic
            List<String> temp_update = [];

            temp_update.Add(update[0]);

            int i = 0;
            String new_item = "";
            while(i < update.Count) {
                if (check_if_correctly_ordered(temp_update, before, after)) {
                    i++;
                    if (i != update.Count) {
                        temp_update.Add(update[i]);
                        new_item = update[i];
                    }
                } else {
                    int index = temp_update.IndexOf(new_item);
                    String tmp = temp_update[index-1];
                    temp_update[index-1] = new_item;
                    temp_update[index] = tmp;
                }
            }
            sum += int.Parse(temp_update[temp_update.Count()/2]);
        }
        Console.WriteLine("Sum: {0}", sum);
        // Part 2 result 4480
    }

    public static bool check_if_correctly_ordered(List<String> update, List<String> before, List<String> after) {
        List<String> required_pre_numbers;
        required_pre_numbers = [];
        foreach (String number in update) {
            // For each number, save the numbers that are required to before this number.
            // If the next number is a number in the required to be before numbers, we have failed
            if (required_pre_numbers.Contains(number)) {
                return false;
            }
            required_pre_numbers.AddRange(get_all_before_numbers(number, before, after));
        }
        return true;
    }

    public static List<String> get_all_before_numbers(String after_number, List<String> before, List<String> after) {
        List<String> before_numbers = [];
        for (int i = 0; i < before.Count(); i++) {
            if (after[i] == after_number) {
                before_numbers.Add(before[i]);
            }
        }
        return before_numbers;
    }
}