using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace csharp_lox;

class Program
{
    public static void Main(string[] args) {
        if (args.Length > 1) {
            Console.WriteLine("Usage: c-sharp_lox [script]");
        } else if (args.Length == 1) {
            runFile(args[0]);
        } else {
            runPrompt();
        }
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
        }

    }

    private static void run(string source) {
        
    }

}
