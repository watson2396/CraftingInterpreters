using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static csharp_lox.TokenType;
using static csharp_lox.Token;
using System.Text.RegularExpressions;


namespace csharp_lox;
public class Scanner {
    private readonly string source;
    private readonly List<Token> tokens = new List<Token>();
    private int start = 0;
    private int current = 0;
    private int line = 1;

    Scanner(string source) {
        this.source = source;
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
                csharp_lox.Program.error(line, "unexpected character.");
                break;
        }
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

    private char Advance() {
        return source[current++];
    }

    private void AddToken(TokenType type) {
        AddToken(type, null);
    }

    private void AddToken (TokenType type, object literal) {
        string text = source.Substring(start, current);
        tokens.Add(new Token(type, text, literal, line));
    }
}
