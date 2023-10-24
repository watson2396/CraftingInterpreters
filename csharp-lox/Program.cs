using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace csharp_lox;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 1) {
            Console.WriteLine("Usage: c-sharp_lox [script]");
        } else if (args.Length == 1) {
            runFile(args[0]);
        } else {
            runPrompt();
        }
    }

    private static void runFile(string filePath) {
        byte[] bytes = File.ReadAllBytes(Path.GetFullPath(filePath));
        Task.Run(() => System.Text.Encoding.Default.GetString(bytes));
    }

    private static void runPrompt() {
        for (;;) {
            Console.WriteLine("> ");
            string? line = Console.ReadLine();
            if (line == null) break;
            run(line);
        }

    }

    private static void run(string source) {

    }

}
