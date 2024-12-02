// Part 1

using System.Runtime.CompilerServices;

StreamReader reader = File.OpenText("input.txt");
string line;
int safe_reports = 0;
List<String[]> reports = [];    // I realize i don't have to do String[] here but i cba now to fix

while ((line = reader.ReadLine()) != null) {
    // Console.WriteLine("Line: {0}", line.Split('\n'));
    reports.Add(line.Split('\n'));
}

foreach (String[] report_array in reports) {    // Get each string[] from all strings[]
    Console.WriteLine("Report: {0}", report_array);
    foreach (String report_string in report_array) {    // For each string in this string array (there's only one)
        Console.WriteLine("Number: {0}", report_string);
        if (!do_magic_trick(report_string)) {     // magic happens here
            safe_reports++;
        } 
        else {
            // We failed once, let's go agane but do some dampening whatever the fuck
            List<String> new_report_strings = [];
            String new_report_string;
            String[] list_of_numbers = report_string.Split(" ");        // A string array called list_of_numbers
            for (int i = 0; i < list_of_numbers.Length; i++) {
                new_report_string = "";
                // så många gånger som det finns siffror ska vi skapa nya strängar
                for (int j = 0; j < list_of_numbers.Length; j++) {
                    if (j != i) {
                        new_report_string += list_of_numbers[j];
                        new_report_string += " ";
                    }
                }
                new_report_strings.Add(new_report_string);
            }
            bool fail = true;
            foreach (String item in new_report_strings) {
                Console.WriteLine("Got these new things: {0}", item.TrimEnd());

                fail = do_magic_trick(item.TrimEnd());
                Console.WriteLine("Status: {0}", fail);
                if (!fail) {
                    break;
                }
            }
            Console.WriteLine("After all that, we status: {0}", fail);
            if (!fail) {
                safe_reports++;
            }
        }
    }
}

Console.WriteLine("Safe reports: {0}", safe_reports);
// Part 1 483
// Part 2 



static bool do_magic_trick(String report_string) {
    bool new_report = true;
    bool decreasing = false;
    bool increasing = false;
    bool fail = false;
    int comparison = 0;
    foreach (String number in report_string.Split(" ")) {   // A string called number
        int actually_a_number = int.Parse(number);          // We could just for loop this and get i-1 to compare each loop but eh let's do this
        if (new_report) {
            new_report = false;
        } else {
            if (Math.Abs(comparison - actually_a_number) >= 4) {
                Console.WriteLine("Diff between numbers {0}, {1} too big, bad", comparison, actually_a_number);
                fail = true;
            } else {    // This is all in an else cause i don't want to set increase or decrease if number diff is too large
                if (comparison > actually_a_number) {
                    if (increasing) {
                        Console.WriteLine("Was increasing, is now decreasing, bad");
                        fail = true;
                    }
                    decreasing = true;
                } else if (comparison < actually_a_number) {
                    if (decreasing) {
                        Console.WriteLine("Was decreasing, is not increasing, bad");
                        fail = true;
                    }
                    increasing = true;
                } else {
                    Console.WriteLine("Same number twice in a row, bad");
                    fail = true;
                }
            }
        }
        comparison = actually_a_number;            
    }
    return fail;
}