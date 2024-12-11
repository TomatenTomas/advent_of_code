class AOC9 {
    static void Main() {
        StreamReader reader = File.OpenText("input.txt");
        String input_line;
        List<String> lines = [];
        while ((input_line = reader.ReadLine()) != null) {
            lines.Add(input_line);
        }
        
        String[,] map = new String[lines[0].Length, lines.Count];
        int paths = 0;
        int all_paths = 0;
        List<(int,int)> current_found_coords = [];

        for(int y = 0; y < lines.Count; y++) {
            for(int x = 0; x < lines[y].Length; x++) {
                map[x, y] = lines[x][y].ToString();
            }
        }

        for(int x = 0; x < map.GetLength(0); x++) {
            for(int y = 0; y < map.GetLength(1); y++) {
                if (map[x,y] == "0") {
                    find_path((x,y), 0);
                    current_found_coords.Clear();
                }
            }
        }

        Console.WriteLine("Found {0} paths!", paths);   // 794
        Console.WriteLine("Found {0} all paths!", all_paths);   // 1706

        void find_path((int,int) coords, int current_number) {
            (int,int) new_coords = (-1,-1);
            for (int i = 2; i < 9; i+=2) {  // Check all locations around the 0 (or current numb)
                if (get_surrounding_number(coords, i) == current_number+1) {
                    if (i == 2) {new_coords = (coords.Item1,coords.Item2-1);}
                    if (i == 4) {new_coords = (coords.Item1-1,coords.Item2);}
                    if (i == 6) {new_coords = (coords.Item1+1,coords.Item2);}
                    if (i == 8) {new_coords = (coords.Item1,coords.Item2+1);}

                    if (current_number+1 == 9) {
                        all_paths += 1;
                        if (!current_found_coords.Contains(new_coords)) {
                            paths += 1;
                            current_found_coords.Add(new_coords);
                        }
                    } else {
                        find_path(new_coords, current_number+1);
                    }
                }
            }
        }

        int get_surrounding_number((int,int) coords, int location) {
            // Did this for diagonal as well even though they were not needed
            String val;
            if (location == 1) {
                if (coords.Item1 > 0 && coords.Item2 > 0) {return int.Parse(map[coords.Item1-1,coords.Item2-1]);}
                else {return -1;}
            } else if (location == 2){
                if (coords.Item2 > 0) {return int.Parse(map[coords.Item1,coords.Item2-1]);}
                else {return -1;}
            } else if (location == 3) {
                if (coords.Item1 < map.GetLength(0)-1 && coords.Item2 > 0) {return int.Parse(map[coords.Item1+1,coords.Item2-1]);}
                else {return -1;}
            } else if (location == 4) {
                if (coords.Item1 > 0) {return int.Parse(map[coords.Item1-1,coords.Item2]);}
                else {return -1;}
            } else if (location == 6) {
                if (coords.Item1 < map.GetLength(0)-1) {return int.Parse(map[coords.Item1+1,coords.Item2]);}
                else {return -1;}
            } else if (location == 7) {
                if (coords.Item1 > 0 && coords.Item2 < map.GetLength(1)-1) {return int.Parse(map[coords.Item1-1,coords.Item2+1]);}
                else {return -1;}
            } else if (location == 8) {
                if (coords.Item2 < map.GetLength(1)-1) {return int.Parse(map[coords.Item1,coords.Item2+1]);}
                else {return -1;}
            } else if (location == 9) {
                if (coords.Item1 < map.GetLength(0)-1 && coords.Item2 < map.GetLength(1)-1) {return int.Parse(map[coords.Item1+1,coords.Item2+1]);}
                else {return -1;}
            } else if (location == 5) {return -1;} 
            else {return -1;}
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
