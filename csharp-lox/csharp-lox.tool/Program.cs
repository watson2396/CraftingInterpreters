using csharp_lox;

namespace csharp_lox.tool;

// Generate AST
class Program
{
    public static void Main(string[] args)
    {

        if (args.Length == 0)
        {
            Console.WriteLine("Please provide output directory name");
        }
        
        string outputDir = args[0];
        List<string> strings = new List<string>()
        {
            "Binary : Expr left, Token operator, Expr right",
            "Grouping : Expr expression",
            "Literal : Object value",
            "Unary : Token operator, Expr right"
        };
        DefineAst(outputDir, "Expr", strings);
    }


    private static void DefineAst(string outputDir, string baseName, List<string> types)
    {
        string path = outputDir + '/' + baseName + ".cs";
        if(!Directory.Exists(outputDir)){
            Directory.CreateDirectory(outputDir);
            Console.WriteLine($"{path} created");
        }
        Console.WriteLine($"{path} exists");
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.WriteLine("using csharp_lox;");
            sw.WriteLine("\n");
            sw.WriteLine("namespace csharp_lox;");
            sw.WriteLine("\n");
            sw.WriteLine("abstract class " + baseName + " {");
            sw.WriteLine("\n");

            //foreach (string type in types)
            //{
            //    string className = type.Split(":")[0].Trim();
            //    string fields = type.Split(":")[1].Trim();
            //    DefineType(sw, baseName, className, fields);
            //}

            sw.WriteLine("}");
        }
    }
}
