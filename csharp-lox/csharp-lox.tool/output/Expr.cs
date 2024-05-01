namespace csharp_lox.tool;


abstract class Expr {
    public interface Visitor<T> {
        T visitBinaryExpr(Binary expr);
        T visitGroupingExpr(Grouping expr);
        T visitLiteralExpr(Literal expr);
        T visitUnaryExpr(Unary expr);
    }

	public abstract void Accept(Visitor<Expr> visitor);
}

class Binary : Expr {
	Binary(Expr left,Token opr,Expr right) {
		Binary.left = left;
		Binary.opr = opr;
		Binary.right = right;
	}

	public override void Accept(Visitor<Expr> visitor) {
		visitor.visitBinaryExpr(this);
	}

	static Expr left;
	static Token opr;
	static Expr right;
}

class Grouping : Expr {
	Grouping(Expr expression) {
		Grouping.expression = expression;
	}

	public override void Accept(Visitor<Expr> visitor) {
		visitor.visitGroupingExpr(this);
	}

	static Expr expression;
}

class Literal : Expr {
	Literal(Object value) {
		Literal.value = value;
	}

	public override void Accept(Visitor<Expr> visitor) {
		visitor.visitLiteralExpr(this);
	}

	static Object value;
}

class Unary : Expr {
	Unary(Token opr,Expr right) {
		Unary.opr = opr;
		Unary.right = right;
	}

	public override void Accept(Visitor<Expr> visitor) {
		visitor.visitUnaryExpr(this);
	}

	static Token opr;
	static Expr right;
}

