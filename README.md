# Tiny Programming Language Compiler 🤏

 An implementation of Scanner and Parser for Tiny programming language

 ![Screenshot](images/Screenshot.jpg)

## Language syntax and documentation

- [Regex](docs/regex-expressions.md)
- [DFA](docs/dfa.md)
- [CFG](docs/Tiny_Language_CFG_Rules.md)
- [Sample Codes](docs/samples.md)

## Lexer "Scanner"

- Implemented in `Scanner.cs`
- Based on the following [Regular Expressions (Regex)](docs/regex-expressions.md) and [Deterministic Finite Automata (DFA)](docs/dfa.md)
- Scans the code to identify Lexemes
- Produces a list of Tokens
- Checks for unrecognized Tokens and outputs them in errors list

## Parser

- Implemented in `Parser.cs`
- Based on the following [Context Free Grammer (CFG) rules](docs/Tiny_Language_CFG_Rules.md)
- Uses the token stream produced by Scanner to Build an abstract syntax tree
- Checks for syntax errors and outputs them in errors list
