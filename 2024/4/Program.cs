using System.Numerics;
using System.Text.RegularExpressions;

class AOC4 {
    static void Main() {
        StreamReader reader = File.OpenText("input.txt");
        List<String> lines = [];    // I realize i don't have to do String[] here but i cba now to fix
        string input_line;
        while ((input_line = reader.ReadLine()) != null) {
            // Console.WriteLine("Line: {0}", input_line);
            lines.Add(input_line);
        }

        // Part 1 2336
        int xmases = 0;
        for(int i = 0; i < lines.Count; i++) {
            for(int j = 0; j < lines[i].Length; j++) {
                if (lines[i][j] == 'X') {
                    xmases += find_xmas(lines, i, j, lines.Count, lines[i].Length);
                }
            }
        }
        Console.WriteLine("Xmases: {0}", xmases);


        // Part 2 1831
        int mases = 0;
        for(int i = 1; i < lines.Count-1; i++) {
            for(int j = 1; j < lines[i].Length-1; j++) {
                if (lines[i][j] == 'A') {
                    mases += find_mas(lines, i, j, lines.Count, lines[i].Length);
                }
            }
        }
        Console.WriteLine("mases: {0}", mases);
    }

    private static int find_mas(List<String> lines, int i, int j, int imax, int jmax) {
        bool mas_NW2SE = find_mas_NW2SE(lines, i, j);
        bool mas_SW2NE = find_mas_SW2NE(lines, i, j);
        if (mas_NW2SE && mas_SW2NE) {
            return 1;
        }
        return 0;
    }
    private static bool find_mas_NW2SE(List<String> lines, int i, int j) {
        if (lines[i-1][j-1] == 'M') {
            if (lines[i+1][j+1] == 'S') {
                return true;
            }
        } if (lines[i-1][j-1] == 'S') {
            if (lines[i+1][j+1] == 'M') {
                return true;
            }
        }
        return false;
    }
    private static bool find_mas_SW2NE(List<String> lines, int i, int j) {
        if (lines[i+1][j-1] == 'M') {
            if (lines[i-1][j+1] == 'S') {
                return true;
            }
        } if (lines[i+1][j-1] == 'S') {
            if (lines[i-1][j+1] == 'M') {
                return true;
            }
        }
        return false;
    }
    private static int find_xmas(List<String> lines, int i, int j, int imax, int jmax) {
        int xmas_counter = 0;
        if (i > 2) {
            xmas_counter += find_xmas_N(lines, i, j);
            if (j > 2) {
                xmas_counter += find_xmas_NW(lines, i, j);
            } if (j < jmax-3) {
                xmas_counter += find_xmas_NE(lines, i, j);
            }
        } if (i < imax-3) {
            xmas_counter += find_xmas_S(lines, i, j);
            if (j > 2) {
                xmas_counter += find_xmas_SW(lines, i, j);
            }
            if (j < jmax-3) {
                xmas_counter += find_xmas_SE(lines, i, j);
            }
        } if (j > 2) {
            xmas_counter += find_xmas_W(lines, i, j);
        } if (j < jmax-3) {
            xmas_counter += find_xmas_E(lines, i, j);
        }
        return xmas_counter;
    }

    private static int find_xmas_N(List<String> lines, int i, int j) {
        int found = 0;
        if (lines[i-1][j] == 'M') {
            if (lines[i-2][j] == 'A') {
                if (lines [i-3][j] == 'S') {
                    found = 1;
                }
            }
        }
        return found;
    }
    private static int find_xmas_NW(List<String> lines, int i, int j) {
        int found = 0;
        if (lines[i-1][j-1] == 'M') {
            if (lines[i-2][j-2] == 'A') {
                if (lines [i-3][j-3] == 'S') {
                    found = 1;
                }
            }
        }
        return found;
    }
    private static int find_xmas_NE(List<String> lines, int i, int j) {
        int found = 0;
        if (lines[i-1][j+1] == 'M') {
            if (lines[i-2][j+2] == 'A') {
                if (lines [i-3][j+3] == 'S') {
                    found = 1;
                }
            }
        }
        return found;
    }
    private static int find_xmas_S(List<String> lines, int i, int j) {
        int found = 0;
        if (lines[i+1][j] == 'M') {
            if (lines[i+2][j] == 'A') {
                if (lines [i+3][j] == 'S') {
                    found = 1;
                }
            }
        }
        return found;
    }
    private static int find_xmas_SW(List<String> lines, int i, int j) {
        int found = 0;
        if (lines[i+1][j-1] == 'M') {
            if (lines[i+2][j-2] == 'A') {
                if (lines [i+3][j-3] == 'S') {
                    found = 1;
                }
            }
        }
        return found;
    }
    private static int find_xmas_SE(List<String> lines, int i, int j) {
        int found = 0;
        if (lines[i+1][j+1] == 'M') {
            if (lines[i+2][j+2] == 'A') {
                if (lines [i+3][j+3] == 'S') {
                    found = 1;
                }
            }
        }
        return found;
    }
    private static int find_xmas_E(List<String> lines, int i, int j) {
        int found = 0;
        if (lines[i][j+1] == 'M') {
            if (lines[i][j+2] == 'A') {
                if (lines [i][j+3] == 'S') {
                    found = 1;
                }
            }
        }
        return found;
    }
    private static int find_xmas_W(List<String> lines, int i, int j) {
        int found = 0;
        if (lines[i][j-1] == 'M') {
            if (lines[i][j-2] == 'A') {
                if (lines [i][j-3] == 'S') {
                    found = 1;
                }
            }
        }
        return found;
    }
}
