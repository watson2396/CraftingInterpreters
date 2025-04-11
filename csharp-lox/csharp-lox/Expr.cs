namespace csharp_lox;

public abstract class Expr
{
    public abstract T Accept<T>(Visitor<T> visitor);

    public interface Visitor<T>
    {
        T VisitBinaryExpr(Binary expr);
        T VisitGroupingExpr(Grouping expr);
        T VisitLiteralExpr(Literal expr);
        T VisitUnaryExpr(Unary expr);
    }

    public class Binary : Expr
    {
        public Expr left;
        public Token opr;
        public Expr right;

        Binary(Expr left, Token opr, Expr right)
        {
            this.left = left;
            this.opr = opr;
            this.right = right;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitBinaryExpr(this);
        }
    }

    public class Grouping : Expr
    {
        Grouping(Expr expression)
        {
            this.expression = expression;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitGroupingExpr(this);
        }

        public Expr expression;
    }

    public class Literal : Expr
    {
        Literal(Object value)
        {
            this.value = value;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitLiteralExpr(this);
        }

        public Object value;
    }

    public class Unary : Expr
    {
        Unary(Token opr, Expr right)
        {
            this.opr = opr;
            this.right = right;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitUnaryExpr(this);
        }

        public Token opr;
        public Expr right;
    }
}








