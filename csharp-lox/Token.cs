using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static csharp_lox.TokenType;


namespace csharp_lox;

public struct Token {
    public TokenType type;
    public string lexeme;
    public object literal;
    public int line;

    public Token( TokenType type, string lexeme, object literal, int line) {
        this.type = type;
        this.lexeme = lexeme;
        this.literal = literal;
        this.line = line;
    }

    public override string ToString() {
        return type + " " + lexeme + " " + literal;
    }
}

