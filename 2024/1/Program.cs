// Part 1

StreamReader reader = File.OpenText("input.txt");
string line;
int total_diff = 0;
List<int> firsts = [];
List<int> seconds = [];

while ((line = reader.ReadLine()) != null) {
    string[] items = line.Split(' ');
    firsts.Add(int.Parse(items[0]));
    seconds.Add(int.Parse(items[^1])); // ^1 = same as length-1
}

firsts.Sort();
seconds.Sort();

for (int i = 0; i < firsts.Count(); i++) {
    int diff = Math.Abs(firsts[i] - seconds[i]);
    // Console.WriteLine("Diff: {0}", diff);
    total_diff += diff;
}
Console.WriteLine("Total diff: {0}", total_diff);

//End of part 1

int total = 0;
for (int i = 0; i < firsts.Count(); i++) {
    int first_item = firsts[i];
    int counts = seconds.Count(x => x == first_item);
    total += first_item * counts;
}
Console.WriteLine("Total counts: {0}", total);
