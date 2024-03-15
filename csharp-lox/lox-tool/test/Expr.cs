namespace lox_tool;
abstract class Expr {

	 static class Binary extends Expr {
	 Binary(Expr left, Token operator, Expr right) {
		this.left = left ;
		this.operator = operator ;
		this.right = right ;

	}
	Expr left{ get; set; }
	Token operator{ get; set; }
	Expr right{ get; set; }

}
	 static class Grouping extends Expr {
	 Grouping(Expr expression) {
		this.expression = expression ;

	}
	Expr expression{ get; set; }

}
	 static class Literal extends Expr {
	 Literal(Object value) {
		this.value = value ;

	}
	Object value{ get; set; }

}
	 static class Unary extends Expr {
	 Unary(Token operator, Expr right) {
		this.operator = operator ;
		this.right = right ;

	}
	Token operator{ get; set; }
	Expr right{ get; set; }

}
}