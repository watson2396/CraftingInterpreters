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

}

