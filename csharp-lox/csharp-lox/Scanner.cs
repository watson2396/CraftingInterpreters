using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static csharp_lox.TokenType;
using static csharp_lox.Token;
using System.Text.RegularExpressions;
using System.Diagnostics.Tracing;

namespace csharp_lox;
public class Scanner {
    private readonly string source;
    private readonly List<Token> tokens = new List<Token>();
    private int start = 0;
    private int current = 0;
    private int line = 1;
    Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>();

    Scanner(string source) {
        this.source = source;
        keywords.Add("and", AND);
        keywords.Add("class", CLASS);
        keywords.Add("else", ELSE);
        keywords.Add("false", FALSE);
        keywords.Add("for", FOR);
        keywords.Add("fun", FUN);
        keywords.Add("if", IF);
        keywords.Add("nil", NIL);
        keywords.Add("or", OR);
        keywords.Add("print", PRINT);
        keywords.Add("return", RETURN);
        keywords.Add("super", SUPER);
        keywords.Add("this", THIS);
        keywords.Add("true", TRUE);
        keywords.Add("var", VAR);
        keywords.Add("while", WHILE);
    }

    public List<Token> ScanTokens() {
        while (!isAtEnd()) {
            // we are at the beginning of the next lexeme
            start = current;
            ScanToken();
        }
        tokens.Add(new Token(EOF, "", null, line));
        return tokens;
    }

    private bool isAtEnd() {
        return current >= source.Length;
    }

    private void ScanToken() {
        char c = Advance();
        switch (c) {
            case '(': AddToken(LEFT_PAREN); break;
            case ')': AddToken(RIGHT_PAREN); break;
            case '{': AddToken(RIGHT_BRACE); break;
            case '}': AddToken(LEFT_BRACE); break;
            case ',': AddToken(COMMA); break;
            case '.': AddToken(DOT); break;
            case '-': AddToken(MINUS); break;
            case '+': AddToken(PLUS); break;
            case ';': AddToken(SEMICOLON); break;
            case '*': AddToken(STAR); break;
            case '!':
                AddToken(Match('=') ? BANG_EQUAL : BANG); break;
            case '=':
                AddToken(Match('=') ? EQUAL_EQUAL : EQUAL); break;
            case '<':
                AddToken(Match('=') ? LESS_EQUAL : LESS); break;
            case '>':
                AddToken(Match('=') ? GREATER_EQUAL : GREATER); break;
            case '/':
                if (Match('/')) {
                    // A comment goes until the end of the line
                    while (peek() != '\n' && !isAtEnd()) Advance();
                } else {
                    AddToken(SLASH);
                }
                break;
            case ' ':
            case '\r':
            case '\t':
                break;
            case '\n':
                line++;
                break;
            case '"': m_string(); break;
            default:
                if (isDigit(c)) {
                    m_number();
                } else if (isAlpha(c)) {
                    m_identifier();
                } else {
                    csharp_lox.Program.error(line, "unexpected character.");
                }
                break;
        }
    }

    private void m_number() {
        while (isDigit(peek())) Advance();

        // Look for a fractional part
        if (peek() == '.' && isDigit(peekNext())) {
            // Consume the "."
            Advance();

            while (isDigit(peek())) Advance();
        }

        AddToken(NUMBER, Double.Parse(source.Substring(start, current)));
    }

    private void m_string() {
        while (peek() != '"' && !isAtEnd()) {
            if (peek() == '\n') line++;
            Advance();
        }

        if (isAtEnd()) {
            Program.error(line, "Unterminated string.");
            return;
        }

        // The closing "
        Advance();

        // Trim the surrounding quotes
        string value = source.Substring(start + 1, current - 1);
        AddToken(STRING, value);

    }

    private bool Match(char expected) {
        if (isAtEnd()) return false;
        if (source[current] != expected) return false;

        current++;
        return true;
    }

    private char peek() {
        if (isAtEnd()) return '\0';
        return source[current];
    }

    private char peekNext() {
        if (current + 1 >= source.Length) return '\0';
        return source[current + 1];
    }

    private bool isAlpha(char c) {
        return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_';
    }

    private bool isAlphaNumeric(char c) {
        return isAlpha(c) || isDigit(c);
    }

    private char Advance() {
        return source[current++];
    }

    private bool isDigit(char c) {
        return c >= '0' && c <= '9';
    }

    private void m_identifier() {
        while (isAlpha(peek())) Advance();

        string text = source.Substring(start, current);
        TokenType type = keywords[text.ToLower()];
        if (type == null) type = IDENTIFIER;
        AddToken(type);
    }

    private void AddToken(TokenType type) {
        AddToken(type, null);
    }

    private void AddToken(TokenType type, object literal) {
        string text = source.Substring(start, current);
        tokens.Add(new Token(type, text, literal, line));
    }

     

}
