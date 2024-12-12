class AOC11 {
    static void Main() {
        StreamReader reader = File.OpenText("input.txt");
        String line = reader.ReadLine();
        List<String> stones = [.. line.Split(" ")];;
        List<String> stones_new = [];
        List<String> stone_tmp = [];
        int blinks = 75;

        List<String> stone_25 = split_stones(stones, blinks);
        Console.WriteLine(stone_25.Count);

        // for(int i = 0; i < stone_25.Count; i+=14) {
        //     stone_tmp.Clear();
        //     stone_tmp.Add(stone_25[i]);
        //     stone_tmp.Add(stone_25[i+1]);
        //     stone_tmp.Add(stone_25[i+2]);
        //     stone_tmp.Add(stone_25[i+3]);
        //     stone_tmp.Add(stone_25[i+4]);
        //     stone_tmp.Add(stone_25[i+5]);
        //     stone_tmp.Add(stone_25[i+6]);
        //     stone_tmp.Add(stone_25[i+7]);
        //     stone_tmp.Add(stone_25[i+8]);
        //     stone_tmp.Add(stone_25[i+9]);
        //     stone_tmp.Add(stone_25[i+10]);
        //     stone_tmp.Add(stone_25[i+11]);
        //     stone_tmp.Add(stone_25[i+12]);
        //     stone_tmp.Add(stone_25[i+13]);
        //     List<String> new_stones = split_stones(stone_tmp, 25);
        //     Console.WriteLine("Count for i: {0}, count: {1}", i, new_stones.Count);
        // }

        List<String> split_stones(List<String> stones_input, int blinks) {
            List<String> stones_temp = [.. stones_input];

            for (int a = 0; a < blinks; a++) {
                for(int i = 0; i < stones_temp.Count; i++) {
                    long number = long.Parse(stones_temp[i]);
                    if (number == 0) {
                        stones_new.Add("1");
                    } else if (stones_temp[i].Length%2==0) {
                        String left = long.Parse(stones_temp[i].Substring(0,stones_temp[i].Length/2)).ToString();
                        String right = long.Parse(stones_temp[i].Substring(stones_temp[i].Length/2,stones_temp[i].Length/2)).ToString();
                        stones_new.Add(left);
                        stones_new.Add(right);
                    } else {
                        number *= 2024;
                        stones_new.Add(number.ToString());
                    }
                }
                Console.WriteLine("Blink: {0}, Count: {1}", a, stones_temp.Count);
                stones_temp.Clear();
                foreach(String s in stones_new) {
                    stones_temp.Add(s);
                }
                stones_new.Clear();
            }
            return stones_temp;
        }


    }   // Part 1 183484
}
