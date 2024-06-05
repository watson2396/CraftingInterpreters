using csharp_lox;

namespace csharp_lox.tool;

// Generate AST
class Program {
    public static void Main(string[] args) {

        if (args.Length == 0) {
            Console.WriteLine("Please provide output directory name");
        }

        string outputDir = args[0];
        List<string> strings = new List<string>()
        {
            "Binary : Expr left,Token opr,Expr right",
            "Grouping : Expr expression",
            "Literal : Object value",
            "Unary : Token opr,Expr right"
        };
        DefineAst(outputDir, "Expr", strings);
    }


    private static void DefineAst(string outputDir, string baseName, List<string> types) {
        string path = outputDir + '/' + baseName + ".cs";
        if (!Directory.Exists(outputDir)) {
            Directory.CreateDirectory(outputDir);
            Console.WriteLine($"{outputDir} created");
        }
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        Console.WriteLine($"{path} exists");

        using (StreamWriter sw = new StreamWriter(path)) {
            sw.WriteLine("namespace csharp_lox;");
            sw.WriteLine("");
            sw.WriteLine("abstract class " + baseName + " {");
            DefineVisitor(sw, baseName, types);
            sw.WriteLine("\tpublic abstract void Accept(Visitor<" + baseName + "> visitor);");
            sw.WriteLine("}");
            sw.WriteLine("");


            foreach (string type in types) {
                string className = type.Split(":")[0].Trim();
                string fields = type.Split(":")[1].Trim();
                DefineType(sw, baseName, className, fields);
            }

        }
    }

    private static void DefineType(
     StreamWriter sw, string baseName, string className, string fieldList) {
        sw.WriteLine("class " + className + " : " + baseName + " {");

        // Constructor
        sw.WriteLine("\t" + className + "(" + fieldList + ") {");

        // Store parameters in fields
        string[] fields = fieldList.Split(",");
        foreach (string field in fields) {
            string name = field.Split(" ")[1];
            sw.WriteLine("\t\t" + className + "." + name + " = " + name + ";");
        }

        sw.WriteLine("\t}");

        sw.WriteLine();
        sw.WriteLine("\tpublic override void Accept(Visitor<" + baseName + "> visitor) {");
        sw.WriteLine("\t\tvisitor.visit" + className + baseName + "(this);");
        sw.WriteLine("\t}");

        // Fields
        sw.WriteLine("");
        foreach (string field in fields) {
            sw.WriteLine("\tstatic " + field + ";");
        }

        sw.WriteLine("}");
        sw.WriteLine("");
    }

    private static void DefineVisitor (StreamWriter sw, string baseName, List<string> types)
    {
        sw.WriteLine("public interface Visitor<T> {");
        foreach (string type in types)
        {
            string typeName = type.Split(':')[0].Trim();
            sw.WriteLine("\tT visit" + typeName + baseName + "(" + typeName + " " + baseName.ToLower() + ");");
        }
        sw.WriteLine("}");
        sw.WriteLine("");
    }
}
