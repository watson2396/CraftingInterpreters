using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace csharp_lox;

class Program
{
    static bool hadError = false;

    public static int Main(string[] args) {
        if (args.Length > 1) {
            Console.WriteLine("Usage: c-sharp_lox [script]");
        } else if (args.Length == 1) {
            runFile(args[0]);
        } else {
            runPrompt();
        }
        if (hadError) { return 65; }
        return 0;
    }

    private static void runFile(string path) {
        char[] chars;
        using (StreamReader reader = File.OpenText(Path.GetFullPath(path)) ) {
            chars = new char[reader.BaseStream.Length];
            string str = new string(chars);
            Task.Run(() => str);
        }
    }

    private static void runPrompt() {
        for (;;) {
            Console.WriteLine("> ");
            string? line = Console.ReadLine();
            if (line == null) break;
            run(line);
            hadError = false;
        }

    }

    private static void run(string source) {
        List<string> tokens = source.Split(' ').ToList();

        foreach (string token in tokens) {
            Console.WriteLine($"{token}");
        }
    }

    public static void error (int line,  string message) {
        report(line, "", message);
    }

    private static void report (int line, string where, string message) {
        Console.WriteLine($"[line {line}] Error {where}: {message}");
        hadError = true;
    }
}
