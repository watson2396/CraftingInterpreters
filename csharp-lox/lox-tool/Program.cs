namespace lox_tool;

public class Program {
    static void Main(string[] args) {
        if (args.Length != 1) {
            Console.WriteLine("Usage: generate_ast <output directory>");
        }
        string outputDir = args[0];
        List<string> types = new List<string> { 
            "Binary: Expr left, Token operator, Expr right",
            "Grouping: Expr expression",
            "Literal: Object value",
            "Unary: Token operator, Expr right"
        };

        defineAst(outputDir, "Expr", types);
    }

    private static void defineAst(string ouputDir, string baseName, List<string> types) {
        if (!Directory.Exists(ouputDir)) {
            Directory.CreateDirectory(ouputDir);
        }
        string path = ouputDir + "/" + baseName + ".cs";

        File.AppendAllText(path, "namespace lox_tool;\n");
        File.AppendAllText(path, "abstract class " + baseName + " {\n");

        // The AST classes
        foreach (string type in types) {
            string className = type.Split(':')[0].Trim();
            string fields = type.Split(":")[1].Trim();
            defineType(path, baseName, className, fields);
        }

        File.AppendAllText(path, "\n}");
    }

    private static void defineType(string path, string baseName, string className, string fieldList) {

        File.AppendAllText(path, "\n\t static class " + className + " extends " + baseName + " {\n");

        // Constructor
        File.AppendAllText(path, "\t " + className + "(" + fieldList + ") {\n");

        // Store parameters in fields
        List<string> fields = fieldList.Split(", ").ToList();
        foreach(string field in fields) {
            string name = field.Split(" ")[1];
            File.AppendAllText(path, "\t\tthis." + name + " = " + name + " ;\n");
        }
        
        File.AppendAllText(path, "\n\t}");

        // Fields
        File.AppendAllText(path, "\n");
        foreach(string field in fields) {
            File.AppendAllText(path, "\t" + field + "{ get; set; }\n");
        }

        File.AppendAllText(path, "\n}");
    }
}

