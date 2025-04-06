namespace csharp_lox;

public interface Visitor<T>
{
    T VisitBinaryExpr(Binary expr);
    T VisitGroupingExpr(Grouping expr);
    T VisitLiteralExpr(Literal expr);
    T VisitUnaryExpr(Unary expr);
}

public abstract class Expr
{
    public abstract void Accept(Visitor<Expr> visitor);
}

public class Binary : Expr
{
    Binary(Expr left, Token opr, Expr right)
    {
        Binary.left = left;
        Binary.opr = opr;
        Binary.right = right;
    }

    public override void Accept(Visitor<Expr> visitor)
    {
        visitor.VisitBinaryExpr(this);
    }

    static Expr left;
    static Token opr;
    static Expr right;
}

public class Grouping : Expr
{
    Grouping(Expr expression)
    {
        Grouping.expression = expression;
    }

    public override void Accept(Visitor<Expr> visitor)
    {
        visitor.VisitGroupingExpr(this);
    }

    static Expr expression;
}

public class Literal : Expr
{
    Literal(Object value)
    {
        Literal.value = value;
    }

    public override void Accept(Visitor<Expr> visitor)
    {
        visitor.VisitLiteralExpr(this);
    }

    static Object value;
}

public class Unary : Expr
{
    Unary(Token opr, Expr right)
    {
        Unary.opr = opr;
        Unary.right = right;
    }

    public override void Accept(Visitor<Expr> visitor)
    {
        visitor.VisitUnaryExpr(this);
    }

    static Token opr;
    static Expr right;
}

