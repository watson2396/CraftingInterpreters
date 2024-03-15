using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lox_tool.test;

public abstract class Test_expr {

}
public class Grouping : Test_expr {
    Grouping( Expr expression) {
        this.expression = expression
    }

    Expr expression {  get; set; }
}
public class Binary : Test_expr {

}
public class Literal : Test_expr {

}
public class Unary : Test_expr {

}
