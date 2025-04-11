using System.Text;

namespace csharp_lox;

public class AstPrinter : Expr.Visitor<string>
{
    public string print(Expr expr)
    {
        return expr.Accept(this);
    }

    public string VisitGroupingExpr(Expr.Grouping expr)
    {
        return parenthesize("group", expr.expression);
    }

    public string VisitBinaryExpr(Expr.Binary expr)
    {
        return parenthesize(expr.opr.lexeme, expr.left, expr.right);

    }

    public string VisitLiteralExpr(Expr.Literal expr)
    {
        if (expr.value == null) return "nil";
        return expr.value.ToString()!;
    }

    public string VisitUnaryExpr(Expr.Unary expr)
    {
        return parenthesize(expr.opr.lexeme, expr.right);

    }

    private string parenthesize(string name, params Expr[] exprs)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("(").Append(name);
        foreach (var expr in exprs.ToList())
        {
            builder.Append(" ");
            builder.Append(expr.Accept(this));
        }

        builder.Append(")");

        return builder.ToString();
    }

    public static void main(String[] args)
    {
        Expr expression = new Expr.Binary(
            new Expr.Unary(
                new Token(TokenType.MINUS, "-", null, 1),
                new Expr.Literal(123)
                ),
            new Token(TokenType.STAR, "*", null, 1),
            new Expr.Grouping(
                new Expr.Literal(45.67))
            );

        System.Console.WriteLine(new AstPrinter().print(expression));
    }

}

