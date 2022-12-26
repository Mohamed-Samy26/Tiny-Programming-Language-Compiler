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
        Node Program()
        {
            Node program = new Node("Program");
            /*program.Children.Add(Header());
            program.Children.Add(DeclSec());
            program.Children.Add(Block());
            program.Children.Add(match(Token_Class.Dot));*/
            MessageBox.Show("Success");
            return program;
        }

/*        Node Header()
        {
            Node header = new Node("Header");
            // write your code here to check the header sructure
            header.Children.Add(match(Token_Class.Program));
            header.Children.Add(match(Token_Class.Idenifier));
            header.Children.Add(match(Token_Class.Semicolon));
            return header;
        }
        Node DeclSec()
        {
            Node declsec = new Node("DeclSec");
            // write your code here to check atleast the declare sturcure 
            // without adding procedures
            return declsec;
        }
        Node Block()
        {
            Node block = new Node("block");
            // write your code here to match statements
            block.Children.Add(match(Token_Class.Begin));
            block.Children.Add(Statements());
            return block;
        }
        // Implement your logic here
        Node Statements() {
            Node Statements = new Node("Statements");
            Statements.Children.Add(Statement());
            Statements.Children.Add(State());
            return Statements;
        }
        Node Statement()
        {
            Node Statement = new Node("Statement");
            if (Token_Class.Read == TokenStream[InputPointer].token_type)
            {
                Statement.Children.Add(match(Token_Class.Read));
                Statement.Children.Add(match(Token_Class.Idenifier));
                return Statement;

            }
            else if (Token_Class.Write == TokenStream[InputPointer].token_type)
            {
                Statement.Children.Add(match(Token_Class.Write));
                Statement.Children.Add(match(Token_Class.Idenifier));
                return Statement;

            }
            else if (Token_Class.Set == TokenStream[InputPointer].token_type)
            {
                Statement.Children.Add(match(Token_Class.Set));
                Statement.Children.Add(match(Token_Class.Idenifier));
                Statement.Children.Add(match(Token_Class.EqualOp));
                Statement.Children.Add(Expression());
                return Statement;
            }
            else if (Token_Class.Call == TokenStream[InputPointer].token_type)
            {
                Statement.Children.Add(match(Token_Class.Call));
                Statement.Children.Add(match(Token_Class.Idenifier));
                Statement.Children.Add(ArgList());
                return Statement;
            }
            return null;
        }
        public Node State() {
            Node state = new Node("State");
            if(Token_Class.T_Semicolon == TokenStream[InputPointer].token_type)
            { 
                state.Children.Add(match(Token_Class.Semicolon));
                state.Children.Add(Statement());
                State();
                return state;
            }
            return null;
        }
        Node Expression() {
            Node Expression = new Node("Expression");
            Expression.Children.Add(Term());
            Expression.Children.Add(Exp());

            return Expression;
        }
        Node Exp()
        {
            Node Expr = new Node("Exp");
            Expr.Children.Add(AddOP());
            Expr.Children.Add(Term());
            Exp();
            return Expr;
        }
        Node AddOP()
        {
            Node AddOPr = new Node("AddOP");
            if (Token_Class.PlusOp == TokenStream[InputPointer].token_type) {
                AddOPr.Children.Add(match(Token_Class.PlusOp));
            }
            else if (Token_Class.MinusOp == TokenStream[InputPointer].token_type)
            {
                AddOPr.Children.Add(match(Token_Class.MinusOp));
            }
            return AddOPr;
        }
        Node MultiOP()
        {
            Node MultiOPr = new Node("MultiOP");
            if (Token_Class.MultiplyOp == TokenStream[InputPointer].token_type)
            {
                MultiOPr.Children.Add(match(Token_Class.MultiplyOp));
            }
            else if (Token_Class.DivideOp == TokenStream[InputPointer].token_type)
            {
                MultiOPr.Children.Add(match(Token_Class.DivideOp));
            }
            return MultiOPr;
        }
        Node Term()
        {
            Node trm = new Node("Term");
            trm.Children.Add(Factor());
            trm.Children.Add(Ter());
            return trm;
        }
        Node Factor()
        {
            Node fact = new Node("Factor");

            if (Token_Class.Idenifier == TokenStream[InputPointer].token_type)
            {
                fact.Children.Add(match(Token_Class.Idenifier));
            }
            else if (Token_Class.Constant == TokenStream[InputPointer].token_type)
            {
                fact.Children.Add(match(Token_Class.Constant));
            }

            return fact;
        }
        Node Ter()
        {
            Node ter = new Node("Ter");
            if (Token_Class.DivideOp == TokenStream[InputPointer].token_type 
                || Token_Class.MultiplyOp == TokenStream[InputPointer].token_type)
            {
                ter.Children.Add(MultiOP());
                ter.Children.Add(Factor());
                Ter();
                return ter;
            }
            return null;
        }
        Node ArgList() {
            Node ArgListr = new Node("ArgList");
            return ArgListr;
        }
        Node Arg()
        {
            Node Argr = new Node("Arg");
            Argr.Children.Add(match(Token_Class.Comma));
            return Argr;
        }
        Node Arguments()
        {
            Node Argumentsr = new Node("Arguments");
            return Argumentsr;
        }*/
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
