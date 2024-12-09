class AOC8 {
    static void Main() {
        StreamReader reader = File.OpenText("input.txt");
        List<String> lines = [];
        string input_line;
        List<Char> unique_chars = [];
        List<(int,int)> coords_for_chars;
        List<(int,int)> dirty_antinodes_part1 = [];
        List<(int,int)> dirty_antinodes_part2 = [];
        int antinode_count = 0;
        while ((input_line = reader.ReadLine()) != null) {
            lines.Add(input_line);
        }
        
        char[,] map = new char[lines[0].Length, lines.Count];

        for(int y = 0; y < lines.Count; y++) {
            for(int x = 0; x < lines[y].Length; x++) {
                map[x, y] = lines[x][y];
                if (lines[x][y] != '.' && !unique_chars.Contains(lines[x][y])) {
                    unique_chars.Add(lines[x][y]);
                }
            }
        }

        // print_map();

        foreach (Char c in unique_chars) {
            coords_for_chars = [];
            for(int x = 0; x < map.GetLength(0); x++) {
                for(int y = 0; y < map.GetLength(1); y++) {
                    if(map[x, y] == c) {
                        coords_for_chars.Add((x, y));
                        // Console.WriteLine("For c: {0} adding coords: {1}", c, (x, y));
                    }
                }
            }

            get_dirty_antinodes_part1();
            get_dirty_antinodes_part2();
        }

        foreach((int,int) antinode in dirty_antinodes_part1) {
            if (antinode.Item1 < map.GetLength(0) &&
                antinode.Item2 < map.GetLength(1) &&
                antinode.Item1 >= 0 &&
                antinode.Item2 >= 0) {
                if (map[antinode.Item1, antinode.Item2] != '#') {
                    antinode_count++;
                    map[antinode.Item1, antinode.Item2] = '#';
                }
            }
        }

        print_map();

        Console.WriteLine("Count_1 {0}", antinode_count);
        // Part 1 357
        


        foreach((int,int) antinode in dirty_antinodes_part2) {
            if (antinode.Item1 < map.GetLength(0) &&
                antinode.Item2 < map.GetLength(1) &&
                antinode.Item1 >= 0 &&
                antinode.Item2 >= 0) {
                if (map[antinode.Item1, antinode.Item2] != '#') {
                    antinode_count++;
                    map[antinode.Item1, antinode.Item2] = '#';
                }
            }
        }
        Console.WriteLine("Count_2 {0}", antinode_count);
        print_map();

        (int,int) get_distance_between_coords((int,int) coord_1, (int,int) coord_2) {
            return (coord_2.Item1-coord_1.Item1, coord_2.Item2-coord_1.Item2);
        }

        void get_dirty_antinodes_part1() {
            for(int i = 0; i < coords_for_chars.Count-1; i++) {
                for(int j = i+1; j < coords_for_chars.Count; j++) {
                    // Console.WriteLine("Checking charcoords for c: {0} i: {1}, j: {2}", c, coords_for_chars[i], coords_for_chars[j]);
                    // Console.WriteLine("Distance: {0}", get_distance_between_coords(coords_for_chars[i], coords_for_chars[j]));
                    (int,int) distance = get_distance_between_coords(coords_for_chars[i], coords_for_chars[j]);
                    (int,int) new_a = (coords_for_chars[i].Item1 - distance.Item1,coords_for_chars[i].Item2 - distance.Item2);
                    (int,int) new_b = (coords_for_chars[j].Item1 + distance.Item1,coords_for_chars[j].Item2 + distance.Item2);
                    // Console.WriteLine("Antinodes: {0}, {1}", new_a, new_b);
                    dirty_antinodes_part1.Add(new_a);
                    dirty_antinodes_part1.Add(new_b);
                }
            }
        }

        void get_dirty_antinodes_part2() {
            for(int i = 0; i < coords_for_chars.Count; i++) {dirty_antinodes_part2.Add(coords_for_chars[i]);}
            for(int i = 0; i < coords_for_chars.Count-1; i++) {
                for(int j = i+1; j < coords_for_chars.Count; j++) {
                    (int,int) distance = get_distance_between_coords(coords_for_chars[i], coords_for_chars[j]);

                    int i_x = coords_for_chars[i].Item1;
                    int i_y = coords_for_chars[i].Item2;
                    int j_x = coords_for_chars[j].Item1;
                    int j_y = coords_for_chars[j].Item2;
                    while (i_x - distance.Item1 >= 0 && i_y - distance.Item2 >= 0) {
                        (int,int) new_a = (i_x - distance.Item1,i_y - distance.Item2);
                        dirty_antinodes_part2.Add(new_a);
                        i_x -= distance.Item1;
                        i_y -= distance.Item2;
                    }
                    while (j_x + distance.Item1 < map.GetLength(0) && j_y + distance.Item2 < map.GetLength(1)) {
                        (int,int) new_b = (j_x + distance.Item1,j_y + distance.Item2);
                        dirty_antinodes_part2.Add(new_b);
                        j_x += distance.Item1;
                        j_y += distance.Item2;
                    }
                }
            }
        }

        void print_map() {
            for(int x = 0; x < map.GetLength(0); x++) {
                for(int y = 0; y < map.GetLength(1); y++) {
                    Console.Write("{0}",map[x, y]);
                }
                Console.Write("\n");
            }
        }
    }
}

