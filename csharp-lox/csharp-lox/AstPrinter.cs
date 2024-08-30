using System.Text;

namespace csharp_lox;

public class AstPrinter implements Expr.Visitor<string>
{
    string Print(Expr expr)
    {
        return expr.Accept(this);
    }

    public override string visitBinaryExpr(Binary expr)
{
    return parenthesize(expr.operator.lexeme, expr.left, expr.right);
}

public override string visitGroupingExpr(Grouping expr)
{
    return parenthesize(expr.operator.lexeme, expr.left, expr.right);
}

public override string visitLiteralExpr(Literal expr)
{
    return parenthesize(expr.operator.lexeme, expr.left, expr.right);
}

public override string visitUnaryexpr(Unary expr)
{
    return parenthesize(expr.operator.lexeme, expr.left, expr.right);
}

private string parenthesize(string name, Expr[] exprs)
{
    StringBuilder builder = new StringBuilder();

    builder.Append("(").Append(name);
    foreach (Expr e in exprs)
    {
        builder.Append(" ");
        builder.Append(expr.Accept(this));
    }
}
}

