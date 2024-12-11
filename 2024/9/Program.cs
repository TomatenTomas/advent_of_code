class AOC9 {
    static void Main() {
        StreamReader reader = File.OpenText("input.txt");
        List<String> hdd = [];
        long checksum = 0;

        String line = reader.ReadLine();
        Console.WriteLine("line: {0}", line);

        for(int i = 0; i < line.Length;i++) {
            int id = i/2;
            int length = int.Parse(line[i].ToString());

            for(int j = 0; j < length; j++) {
                if (i%2 == 0) { //even, add data
                    hdd.Add(id.ToString());
                } else { //odd, dots
                    hdd.Add(".");
                }
            }
        }
        Console.WriteLine(String.Join("", hdd));
        List<String> hdd2 = [.. hdd];

        sort_part_1();
        sort_part_2();

        for(int i = 0; i < hdd.Count; i++) {
            if (hdd[i] == ".") {break;}
            checksum += long.Parse(hdd[i])*i;
        }
        Console.WriteLine("Checksum 1: {0}", checksum);
        // Part 1 6216544403458

        checksum = 0;
        for(int i = 0; i < hdd2.Count; i++) {
            if (hdd2[i] == ".") {continue;}
            checksum += long.Parse(hdd2[i])*i;
        }
        Console.WriteLine("Checksum 2: {0}", checksum);
        // Part 2


        void sort_part_1(){
            while(true) {
                int index_first_dot = hdd.FindIndex(x => x.Equals("."));
                int index_last_number = hdd.FindLastIndex(x => !x.Equals("."));
                if (index_first_dot > index_last_number) {
                    Console.WriteLine("Sort done part 1");
                    break;
                }
                hdd[index_first_dot] = hdd[index_last_number];
                hdd[index_last_number] = ".";
                // Console.WriteLine(String.Join("", hdd));
            }
        }

        void sort_part_2(){
            String number = hdd2[^1];
            for(int i = int.Parse(number); i > 0; i--) {
                int first_index = hdd2.FindIndex(x => x.Equals(i.ToString()));
                int last_index = hdd2.FindLastIndex(x => x.Equals(i.ToString()));
                int number_of_IDs = last_index - first_index + 1;

                int dot_index = find_index_of_dots_length(number_of_IDs, first_index);

                if (dot_index == -1) {
                    continue;
                    }
                if (dot_index > first_index) {
                    continue;
                }
                for (int j = 0; j < number_of_IDs; j++) {
                    hdd2[dot_index + j] = hdd2[first_index + j];
                    hdd2[first_index + j] = ".";
                }
                // Console.WriteLine(String.Join("", hdd2));
            }
            // Console.WriteLine(String.Join("", hdd2));
            
        }

        int find_index_of_dots_length(int length, int limit) {
            int index = 0;
            while(index < limit) {
                index = hdd2.IndexOf(".", index);
                if (index == -1) {return -1;}

                for (int i = 1; i <= length; i++) {
                    if (hdd2[index+i-1] != ".") {
                        index += i;
                        break;
                    } else {
                        if (i == length) {
                            return index;
                        }
                    }
                }

            }
            return -1;
        }
    }
}
