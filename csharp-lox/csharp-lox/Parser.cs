using System.Numerics;
using System.Reflection;
using static csharp_lox.TokenType;
using static csharp_lox.Token;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace csharp_lox;

public class Parser {

    private List<Token> _tokens;
    private int current = 0;

    private static class ParseError : Exception;

    Parser (List<Token> tokens) {
        _tokens = tokens;
    }

    private Expr expression() {
        return equality();
    }

    private Expr equality() {
        Expr expr = comparison();

        while(match(BANG_EQUAL, EQUAL_EQUAL)) {
            Token _operator = previous();
            Expr right = comparison();
            expr = new Expr.Binary(expr, _operator, right);
        }

        return expr;
    }

    private Expr comparison() {
        Expr expr = term();

        while(match(GREATER, GREATER_EQUAL, LESS, LESS_EQUAL)) {
            Token _operator = previous();
            Expr right = term();
            expr = new Expr.Binary(expr, _operator, right);
        }
    }

    private Expr term() {
        Expr expr = factor();

        while(match(MINUS, PLUS)) {
            Token _operator = previous();
            Expr right = factor();
            expr = new Expr.Binary(expr, _operator, right);
        }

        return expr;
    }

    private Expr factor() {
        Expr expr = unary();

        while(match(SLASH, STAR)) {
            Token _operator = previous();
            Expr right = unary();
            expr = new Expr.Binary(expr, _operator, right);
        }

        return expr;
    }

    private Expr unary() {
        if(match(BANG, MINUS)) {
            Token _operator = previous();
            Expr right = unary();
            return new Expr.Unary(_operator, right);
        }

        return primary();
    }

    private Expr primary() {
        if (match(FALSE)) return new Expr.Literal(false);
        if (match(TRUE)) return new Expr.Literal(true);
        if (match(NIL)) return new Expr.Literal(null);

        if (match(NUMBER, STRING)) {
            return new Expr.Literal(previous().literal);
        }

        if (match(LEFT_PAREN)) {
            Expr expr = expression();
            consume(RIGHT_PAREN, "Expect ')' after expression.");
            return new Expr.Grouping(expr);
        }
    }

    private Token consume(TokenType type, string message) {
        if (check(type)) advance();

        throw Program.error(peek(), message);
    }

    private ParseError error(Token token, String message) {
        Program.error(token, message);
        return new ParseError();
    }

    static void error(Token token, String message) {
        if (token.type == TokenType.EOF) {
        report(token.line, " at end", message);
        } else {
        report(token.line, " at '" + token.lexeme + "'", message);
        }
    }

    private bool match(params TokenType[] types) {
        foreach (var type in types) {
            if (check(type)) {
                advance();
                return true;
            }
        }
        return false;
    }

    private bool check(TokenType type) {
        if (isAtEnd()) return false;
        return peek().type == type;
    }

    private Token advance() {
        if (!isAtEnd()) current++;
        return previous();
    }

    private bool isAtEnd() {
        return peek().type == EOF;
    }

    private Token peek() {
        return _tokens.ElementAt(current);
    }

    private Token previous() {
        return _tokens.ElementAt(current - 1);
    }

}
