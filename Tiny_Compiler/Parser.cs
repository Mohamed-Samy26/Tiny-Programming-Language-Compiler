using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tiny_Compiler
{
    public class Node
    {
        public List<Node> Children = new List<Node>();
        
        public string Name;
        public Node(string N)
        {
            this.Name = N;
        }
    }
    public class Parser
    {
        int InputPointer = 0;
        List<Token> TokenStream;
        public Node root;
        
        public Node StartParsing(List<Token> TokenStream)
        {
            this.InputPointer = 0;
            this.TokenStream = TokenStream;
            root = new Node("Program");
            root.Children.Add(Program());
            return root;
        }

        public Node match(Token_Class ExpectedToken)
    {
        if (InputPointer < TokenStream.Count)
        {
            if (ExpectedToken == TokenStream[InputPointer].token_type)
            {
                InputPointer++;
                Node newNode = new Node(ExpectedToken.ToString());

                return newNode;

            }

            else
            {
                Errors.Error_List.Add("Parsing Error: Expected "
                    + ExpectedToken.ToString() + " and " +
                    TokenStream[InputPointer].token_type.ToString() +
                    "  found\r\n");
                InputPointer++;
                return null;
            }
        }
        else
        {
            Errors.Error_List.Add("Parsing Error: Expected "
                    + ExpectedToken.ToString()  + "\r\n");
            InputPointer++;
            return null;
        }
    }

        private bool isDatatype(int InputPointer)
        {
            bool isInt = TokenStream[InputPointer].token_type == Token_Class.T_Int;
            bool isFloat = TokenStream[InputPointer].token_type == Token_Class.T_Float;
            bool isString = TokenStream[InputPointer].token_type == Token_Class.T_String;
            return (isInt || isFloat || isString);
        }

        private bool isStatement(int InputPointer)
        {
            bool isDecleration = isDatatype(InputPointer);
            bool isWrite = TokenStream[InputPointer].token_type == Token_Class.T_Write;
            bool isRead = TokenStream[InputPointer].token_type == Token_Class.T_Read;
            bool isConditionOrFunctionCallOrAssignment = TokenStream[InputPointer].token_type == Token_Class.T_Identifier;
            bool isIf = TokenStream[InputPointer].token_type == Token_Class.T_If;
            bool isRepeat = TokenStream[InputPointer].token_type == Token_Class.T_Repeat;
            return (isDecleration || isWrite || isRead || isConditionOrFunctionCallOrAssignment || isIf || isRepeat);
        }
        private bool isConditionOp(int InputPointer)
        {
            bool isLessThan = TokenStream[InputPointer].token_type == Token_Class.T_SmallerThanOP;
            bool isGreaterThan = TokenStream[InputPointer].token_type == Token_Class.T_GreaterThanOP;
            bool isEqual = TokenStream[InputPointer].token_type == Token_Class.T_EqualOP;
            bool isNotEqual = TokenStream[InputPointer].token_type == Token_Class.T_NotEqualOP;
            return (isEqual || isGreaterThan || isLessThan || isNotEqual);
        }

        private bool isBooleanOp(int InputPointer)
        {
            bool isOr = TokenStream[InputPointer].token_type == Token_Class.T_OrOP;
            bool isAnd = TokenStream[InputPointer].token_type == Token_Class.T_AndOP;
            return (isAnd || isOr);
        }
        private bool isTerm(int InputPointer)
        {
            bool isNumber = TokenStream[InputPointer].token_type == Token_Class.T_Number;
            bool isIdentifier = TokenStream[InputPointer].token_type == Token_Class.T_Identifier;

            return (isNumber || isIdentifier);
        }
        private bool isAddOp(int InputPointer)
        {
            bool isPlus = TokenStream[InputPointer].token_type ==  Token_Class.T_PlusOP;
            bool isMinus = TokenStream[InputPointer].token_type ==  Token_Class.T_MinusOP;
            return isPlus || isMinus;
        }
        private bool isMultOp(int InputPointer)
        {
            bool isMult = TokenStream[InputPointer].token_type == Token_Class.T_MulitplyOP;
            bool isDivide = TokenStream[InputPointer].token_type == Token_Class.T_DivideOP;
            return isMult || isDivide;
        }
        private bool isEquation(int InputPointer)
        {
            bool isLParanthesis = TokenStream[InputPointer].token_type == Token_Class.T_LeftParentheses;
            return (isLParanthesis || isTerm(InputPointer));
        }
        private bool isExpression(int InputPointer)
        {
            bool isString = TokenStream[InputPointer].token_type == Token_Class.T_String;
            return (isString || isEquation(InputPointer));
        }
        private void printError(string Expected, int inputPointer = -1)
        {
            if (inputPointer == -1)
                inputPointer = InputPointer;
            if (inputPointer < TokenStream.Count)
            {
                Errors.Error_List.Add("Parsing Error: Expected "
                            + Expected + " and " +
                            TokenStream[inputPointer].token_type.ToString() +
                            "  found\r\n");
            }
            else
            {
                Errors.Error_List.Add("Parsing Error: Expected "
                            + Expected + " and found nothing\r\n");
            }
            InputPointer++;
        }

        Node Program()
        {
            Node program = new Node("Program");
            program.Children.Add(ProgramD());
            program.Children.Add(MainFunction());
            return program;
        }

        private Node ProgramD()
        {
            Node programD = new Node("Program'");
            if (InputPointer + 1 < TokenStream.Count && isDatatype(InputPointer) && TokenStream[InputPointer + 1].token_type != Token_Class.T_Main)
            {
                programD.Children.Add(FunctionStatement());
                programD.Children.Add(ProgramD());
                return programD;
            }
            else
                return null;
        }

        private Node FunctionStatement()
        {
            Node functionStatement = new Node("Function Statement");
            functionStatement.Children.Add(FunctionDeclaration());
            functionStatement.Children.Add(FunctionBody());
            return functionStatement;
        }

        private Node FunctionDeclaration()
        {
            Node functionDec = new Node("Function Declaration");
            functionDec.Children.Add(Datatype());
            functionDec.Children.Add(match(Token_Class.T_Identifier));
            functionDec.Children.Add(match(Token_Class.T_LeftParentheses));
            functionDec.Children.Add(Parameters());
            functionDec.Children.Add(match(Token_Class.T_RightParentheses));
            return functionDec;
        }

        private Node Parameters()
        {
            Node parameteres = new Node("Parameters");
            if (InputPointer < TokenStream.Count && isDatatype(InputPointer))
            {
                parameteres.Children.Add(Parameter());
                parameteres.Children.Add(ParametersD());
                return parameteres;
            }
            else
                return null;
        }

        private Node ParametersD()
        {
            Node parameterD = new Node("Parameter'");
            if (InputPointer < TokenStream.Count && TokenStream[InputPointer].token_type == Token_Class.T_Comma)
            {
                parameterD.Children.Add(match(Token_Class.T_Comma));
                parameterD.Children.Add(Parameter());
                parameterD.Children.Add(ParametersD());
                return parameterD;
            }
            else
                return null;
        }

        private Node Parameter()
        {
            Node parameter = new Node("Parameter");
            parameter.Children.Add(Datatype());
            parameter.Children.Add(match(Token_Class.T_Identifier));
            return parameter;
        }

        private Node Datatype()
        {
            Node datatype = new Node("Datatype");
            if (InputPointer < TokenStream.Count && TokenStream[InputPointer].token_type == Token_Class.T_Int)
            {
                datatype.Children.Add(match(Token_Class.T_Int));
            }
            else if (InputPointer < TokenStream.Count && TokenStream[InputPointer].token_type == Token_Class.T_Float)
            {
                datatype.Children.Add(match(Token_Class.T_Float));
            }
            else if (InputPointer < TokenStream.Count && TokenStream[InputPointer].token_type == Token_Class.T_String)
            {
                datatype.Children.Add(match(Token_Class.T_String));
            }
            else
            {
                printError("Datatype");
            }
            return datatype;
        }

        private Node MainFunction()
        {
            Node main = new Node("Main");
            main.Children.Add(Datatype());
            main.Children.Add(match(Token_Class.T_Main));
            main.Children.Add(match(Token_Class.T_LeftParentheses));
            main.Children.Add(match(Token_Class.T_RightParentheses));
            main.Children.Add(FunctionBody());
            return main;
        }

        private Node FunctionBody()
        {
            Node functionBody = new Node("Function Body");
            functionBody.Children.Add(match(Token_Class.T_LeftBrace));
            functionBody.Children.Add(Statements());
            functionBody.Children.Add(ReturnStatement());
            functionBody.Children.Add(match(Token_Class.T_RightBrace));
            return functionBody;
        }

        private Node ReturnStatement()
        {
            Node returnStatement = new Node("Return Statement");
            returnStatement.Children.Add(match(Token_Class.T_Return));
            returnStatement.Children.Add(Expression());
            returnStatement.Children.Add(match(Token_Class.T_Semicolon));
            return returnStatement;
        }

        private Node Expression()
        {
            Node exp = new Node("Expression");
            if (InputPointer < TokenStream.Count && TokenStream[InputPointer].token_type == Token_Class.T_String)
            {
                exp.Children.Add(match(Token_Class.T_String));
            }
            else if (InputPointer < TokenStream.Count && isEquation(InputPointer))
            {
                exp.Children.Add(Equation());
            }
            else
                printError("Expression");
            return exp;
        }

 

        public static TreeNode PrintParseTree(Node root)
        {
            TreeNode tree = new TreeNode("Parse Tree");
            TreeNode treeRoot = PrintTree(root);
            if (treeRoot != null)
                tree.Nodes.Add(treeRoot);
            return tree;
        }
        static TreeNode PrintTree(Node root)
        {
            if (root == null || root.Name == null)
                return null;
            TreeNode tree = new TreeNode(root.Name);
            if (root.Children.Count == 0)
                return tree;
            foreach (Node child in root.Children)
            {
                if (child == null)
                    continue;
                tree.Nodes.Add(PrintTree(child));
            }
            return tree;
        }
    }
    
}
