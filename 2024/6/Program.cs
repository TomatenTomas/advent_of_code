class AOC6 {
    static void Main() {
        StreamReader reader = File.OpenText("input.txt");
        List<String> lines = [];
        string input_line;
        bool loop = false;
        while ((input_line = reader.ReadLine()) != null) {
            // Console.WriteLine("Line: {0}", input_line);
            lines.Add(input_line);
        }

        (int, int) current_pos = (0, 0);
        (int, int) start_pos = (0,0);
        (int, int) current_test = (0,0);
        
        String[,] map = new String[lines.Count, lines[0].Length];
        List<(int,int)> visited_blocks = [];
        List<(int,int)> perm_blocks_list = [];

        int loopers = 0;

        for(int i = 0; i < lines.Count; i++) {
            for(int j = 0; j < lines[i].Length; j++) {
                map[i, j] = lines[i][j].ToString();
                if (lines[i][j] == '^') {
                    // Console.WriteLine("i, j, = {0},{1}", i, j);
                    current_pos = (i, j);
                    start_pos = (i,j);
                }
            }
        }

        String[,] og_map = map.Clone() as String[,];
        String[,] loop_map;

        walk(map, current_pos, false, "N");

        int count = 0;
        for (int i = 0; i < map.GetLength(0); i++) {
            for (int j = 0; j < map.GetLength(1); j++) {
                if (map[i, j].Contains("X")) {
                    count++;
                }
            }
        }
        Console.WriteLine("Count: {0}", count);
        // Part 1 4722

        // for (int i = 0; i < map.GetLength(0); i++) {
        //     for (int j = 0; j < map.GetLength(1); j++) {
        //         Console.Write(map[i, j]);
        //     }
        //     Console.Write("\n");
        // }


        // Part 2 1602
        Console.WriteLine("Loopers: {0}", loopers);


        void walk(String[,] maperino, (int,int) pos, bool loop, String dir) {
            // Console.WriteLine("Starting new walk with pos {0} dir {1}", pos, dir);
            int steps;
            while(pos.Item1 > 0 && pos.Item1 < map.GetLength(1) && pos.Item2 > 0 && pos.Item2 < map.GetLength(0)){
                steps = 0;
                if (dir == "N") {
                    steps += go_north(loop, maperino, pos);
                    pos = (pos.Item1-steps, pos.Item2);
                    dir = "E";
                } else if (dir == "E") {
                    steps += go_east(loop, maperino, pos);
                    pos = (pos.Item1, pos.Item2+steps);
                    dir = "S";
                } else if (dir == "S") {
                    steps += go_south(loop, maperino, pos);
                    pos = (pos.Item1+steps, pos.Item2);
                    dir = "W";
                } else if (dir == "W") {
                    steps += go_west(loop, maperino, pos);
                    pos = (pos.Item1, pos.Item2-steps);
                    dir = "N";
                }
            }
        }

        int go_north(bool loop_check, String[,] maperino, (int,int) pos) {
            // Console.WriteLine("IN NEW AND LOOP IS {0}, pos: {1}", loop_check, pos);
            int step_counter = 0;

            for (int i = pos.Item1; i > 0; i--) {
                if (i != 0) {
                    if (maperino[i-1, pos.Item2] == "#") {
                        if (loop_check) {
                            if (visited_blocks.Contains((i-1, pos.Item2))) {
                                Console.WriteLine("Loopers!");
                                if (perm_blocks_list.Contains(current_test)) {
                                    Console.WriteLine("DUPLICATE");
                                } else {

                                    perm_blocks_list.Add(current_test);
                                    loopers++;
                                }

                                current_test = (-1,-1);
                                return map.GetLength(1);
                            } else {
                                visited_blocks.Add((i-1, pos.Item2));
                            }
                        }
                        return step_counter;
                    } else if (!loop_check && !(i-1, pos.Item2).Equals(start_pos)) {
                        loop_map = og_map.Clone() as String[,];
                        loop_map[i-1, pos.Item2] = "#";
                        current_test = (i-1, pos.Item2);
                        // Console.WriteLine("Starting new loop thing N");
                        visited_blocks = [];
                        // walk(loop_map, (i, pos.Item2), true, "N");
                        walk(loop_map, start_pos, true, "N");
                    }
                }

                if (maperino[i, pos.Item2] != "X") {
                    maperino[i, pos.Item2] = "X";
                }
                step_counter++;
            }
            return step_counter;
        }

        int go_east(bool loop_check, String[,] maperino, (int,int) pos) {
            int step_counter = 0;
            for (int j = pos.Item2; j < maperino.GetLength(0); j++) {
                if (j != maperino.GetLength(0)-1) {
                    if (maperino[pos.Item1, j+1] == "#") {return step_counter;}
                    else if (!loop_check && !(pos.Item1, j+1).Equals(start_pos)) {
                        loop_map = og_map.Clone() as String[,];
                        loop_map[pos.Item1, j+1] = "#";
                        current_test = (pos.Item1, j+1);
                        // Console.WriteLine("Starting new loop thing E");
                        visited_blocks = [];
                        // walk(loop_map, (pos.Item1, j), true, "E");
                        walk(loop_map, start_pos, true, "N");
                    }
                }
                
                if (maperino[pos.Item1, j] != "X") {
                    maperino[pos.Item1, j] = "X";
                }
                step_counter++;
            }
            return step_counter;
        }

        int go_south(bool loop_check, String[,] maperino, (int,int) pos) {
            int step_counter = 0;
            for (int i = pos.Item1; i < maperino.GetLength(1); i++) {
                if (i != maperino.GetLength(1)-1) {
                    if (maperino[i+1, pos.Item2] == "#") {return step_counter;} 
                    else if (!loop_check && !(i+1, pos.Item2).Equals(start_pos)) {
                        loop_map = og_map.Clone() as String[,];
                        loop_map[i+1, pos.Item2] = "#";
                        current_test = (i+1, pos.Item2);
                        // Console.WriteLine("Starting new loop thing S");
                        visited_blocks = [];
                        // walk(loop_map, (i, pos.Item2), true, "S");
                        walk(loop_map, start_pos, true, "N");
                    }
                }
                
                if (maperino[i, pos.Item2] != "X") {
                    maperino[i, pos.Item2] = "X";
                }
                step_counter++;
            }
            return step_counter;
        }

        int go_west(bool loop_check, String[,] maperino, (int,int) pos) {
            int step_counter = 0;
            for (int j = pos.Item2; j > 0; j--) {
                if (j != 0) {
                    if (maperino[pos.Item1, j-1] == "#") {return step_counter;}
                    else if (!loop_check && !(pos.Item1, j-1).Equals(start_pos)) {
                        loop_map = og_map.Clone() as String[,];
                        loop_map[pos.Item1, j-1] = "#";
                        current_test = (pos.Item1, j-1);
                        // Console.WriteLine("Starting new loop thing W");
                        visited_blocks = [];
                        // walk(loop_map, (pos.Item1, j), true, "W");
                        walk(loop_map, start_pos, true, "N");
                    }
                }
                
                if (maperino[pos.Item1, j] != "X") {
                    maperino[pos.Item1, j] = "X";
                }
                step_counter++;
            }
            return step_counter;
        }
    }
}