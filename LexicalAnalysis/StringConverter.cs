using System;
using System.Collections;
using System.Collections.Generic;


namespace LexicalAnalysis
{
    public class StringConverter
    {
		//constructor 
		public StringConverter() { }
        
        static bool IsOperator(char c)
        {
            List<char> operators = new List<char>(new char[] { '*', '/', '-', '+', '(', ')' });
            return operators.Contains(c);
        }
		// Function to find priority 
		// of given operator. 
		static int getPriority(char C)
		{
			if (C == '-' || C == '+')
				return 1;
			else if (C == '*' || C == '/')
				return 2;
			else if (C == '^')
				return 3;
			return 0;
		}

		static bool StrCmp(string s1, string s2)
		{
			return (string.Compare(s1, s2, StringComparison.Ordinal) == 0);
		}

		public static String InfixToPostfix(Sentence infix)
		{
			// stack for operators.
			Stack<char> operators = new Stack<char>();

			// stack for operands. 
			Queue<string> operands = new Queue<string>();

			for (int i = 0; i < infix.Tokens.Count; i++)
			{
				// if the scanned character is an operand,
				// added to the operands queue
				if (!IsOperator(infix.Tokens[i][0]))
				{
					operands.Enqueue(infix.Tokens[i].ToString());
				}
				else if (StrCmp(infix.Tokens[i],"("))
				{
					operators.Push('(');
				}
				else if (StrCmp(infix.Tokens[i], ")"))
				{
					while (operators.Peek() != '(')
					{
						var temp = operators.Pop().ToString();
						operands.Enqueue(temp);
					}
					operators.Pop();
				}
				else
				{
					if (operators.Count == 0)
					{
						operators.Push(infix.Tokens[i][0]);
					}
					else if (getPriority(infix.Tokens[i][0]) > getPriority(operators.Peek()) || operators.Peek() == ')')
					{
						operators.Push(infix.Tokens[i][0]);
					}
					else
					{
						while (operators.Count != 0 && getPriority(operators.Peek()) >= getPriority(infix.Tokens[i][0]))
						{
							var temp = operators.Pop().ToString();
							operands.Enqueue(temp);
						}
						operators.Push(infix.Tokens[i][0]);

					}
				}
			}
			while (operators.Count != 0)
			{
				var temp = operators.Pop().ToString();
				operands.Enqueue(temp);
			}
			string res = "";
			while (operands.Count != 0)
			{
				var temp = operands.Dequeue();
				res += temp;
			}
			return res;
		}

	
		public static String InfixToPostfix(String infix)
		{
            // stack for operators.
            Stack<char> operators = new Stack<char>();

			// stack for operands. 
			Queue <string> operands = new Queue<string>();

			for (int i = 0; i < infix.Length; i++)
			{
				// if the scanned character is an operand,
				// added to the operands queue
				if (!IsOperator(infix[i]))
				{ 
					operands.Enqueue(infix[i].ToString());
				}
				else if (infix[i] == '(') {
					operators.Push('(');
				}
				else if (infix[i] == ')')
				{
                    while (operators.Peek() != '(')
                    {
						var temp = operators.Pop().ToString();
						operands.Enqueue(temp);
					}
					operators.Pop();
				}
				else
				{
					if (operators.Count == 0)
					{
					operators.Push(infix[i]);
					}
					else if (getPriority(infix[i]) > getPriority(operators.Peek()) || operators.Peek() == ')')
					{
						operators.Push(infix[i]);
					}
					else{
						while(getPriority(operators.Peek()) >= getPriority(infix[i]) )
                        {
							var temp = operators.Pop().ToString();
							operands.Enqueue(temp);
                        }
						operators.Push(infix[i]);

					}
				}
			}
			while(operators.Count != 0)
            {
				var temp = operators.Pop().ToString();
				operands.Enqueue(temp);
            }
			string res = "";
			while (operands.Count != 0)
			{
				var temp = operands.Dequeue();
				res += temp;
			}
			return res;
		}


		public static String InfixToPrefix(Sentence infix)
		{
			// stack for operators. 
			Stack<char> operators = new Stack<char>();

			// stack for operands. 
			Stack<String> operands = new Stack<String>();

			for (int i = 0; i < infix.Tokens.Count; i++)
			{

				// If current character is an 
				// opening bracket, then 
				// push into the operators stack. 
				if (StrCmp(infix.Tokens[i], "("))
				{
					operators.Push(infix.Tokens[i][0]);
				}

				// If current character is a 
				// closing bracket, then pop from 
				// both stacks and push result 
				// in operands stack until 
				// matching opening bracket is 
				// not found. 
				else if (StrCmp(infix.Tokens[i], ")"))
				{
					while (operators.Count != 0 &&
						operators.Peek() != '(')
					{

						// operand 1 
						String op1 = operands.Peek();
						operands.Pop();

						// operand 2 
						String op2 = operands.Peek();
						operands.Pop();

						// operator 
						char op = operators.Peek();
						operators.Pop();

						// Add operands and operator 
						// in form operator + 
						// operand1 + operand2. 
						String tmp = op + op2 + op1;
						operands.Push(tmp);
					}

					// Pop opening bracket 
					// from stack. 
					operators.Pop();
				}

				// If current character is an 
				// operand then push it into 
				// operands stack. 
				else if (!IsOperator(infix.Tokens[i][0]))
				{
					operands.Push(infix.Tokens[i]);
				}

				// If current character is an 
				// operator, then push it into 
				// operators stack after popping 
				// high priority operators from 
				// operators stack and pushing 
				// result in operands stack. 
				else
				{
					while (operators.Count != 0 &&
						getPriority(infix.Tokens[i][0]) <=
							getPriority(operators.Peek()))
					{

						String op1 = operands.Peek();
						operands.Pop();

						String op2 = operands.Peek();
						operands.Pop();

						char op = operators.Peek();
						operators.Pop();

						String tmp = op + op2 + op1;
						operands.Push(tmp);
					}

					operators.Push(infix.Tokens[i][0]);
				}
			}

			// Pop operators from operators 
			// stack until it is empty and 
			// operation in add result of 
			// each pop operands stack. 
			while (operators.Count != 0)
			{
				String op1 = operands.Peek();
				operands.Pop();

				String op2 = operands.Peek();
				operands.Pop();

				char op = operators.Peek();
				operators.Pop();

				String tmp = op + op2 + op1;
				operands.Push(tmp);
			}

			// Final prefix expression is 
			// present in operands stack. 
			return operands.Peek();
		}

	}
	// Function that converts infix 
	// expression to prefix expression. 
	//public static String InfixToPrefix(String infix)
	//	{
	//		// stack for operators. 
	//		Stack<char> operators = new Stack<char>();

	//		// stack for operands. 
	//		Stack<String> operands = new Stack<String>();

	//		for (int i = 0; i < infix.Length; i++)
	//		{

	//			// If current character is an 
	//			// opening bracket, then 
	//			// push into the operators stack. 
	//			if (infix[i] == '(')
	//			{
	//				operators.Push(infix[i]);
	//			}

	//			// If current character is a 
	//			// closing bracket, then pop from 
	//			// both stacks and push result 
	//			// in operands stack until 
	//			// matching opening bracket is 
	//			// not found. 
	//			else if (infix[i] == ')')
	//			{
	//				while (operators.Count != 0 &&
	//					operators.Peek() != '(')
	//				{

	//					// operand 1 
	//					String op1 = operands.Peek();
	//					operands.Pop();

	//					// operand 2 
	//					String op2 = operands.Peek();
	//					operands.Pop();

	//					// operator 
	//					char op = operators.Peek();
	//					operators.Pop();

	//					// Add operands and operator 
	//					// in form operator + 
	//					// operand1 + operand2. 
	//					String tmp = op + op2 + op1;
	//					operands.Push(tmp);
	//				}

	//				// Pop opening bracket 
	//				// from stack. 
	//				operators.Pop();
	//			}

	//			// If current character is an 
	//			// operand then push it into 
	//			// operands stack. 
	//			else if (!IsOperator(infix[i]))
	//			{
	//				operands.Push(infix[i] + "");
	//			}

	//			// If current character is an 
	//			// operator, then push it into 
	//			// operators stack after popping 
	//			// high priority operators from 
	//			// operators stack and pushing 
	//			// result in operands stack. 
	//			else
	//			{
	//				while (operators.Count != 0 &&
	//					getPriority(infix[i]) <=
	//						getPriority(operators.Peek()))
	//				{

	//					String op1 = operands.Peek();
	//					operands.Pop();

	//					String op2 = operands.Peek();
	//					operands.Pop();

	//					char op = operators.Peek();
	//					operators.Pop();

	//					String tmp = op + op2 + op1;
	//					operands.Push(tmp);
	//				}

	//				operators.Push(infix[i]);
	//			}
	//		}

	//		// Pop operators from operators 
	//		// stack until it is empty and 
	//		// operation in add result of 
	//		// each pop operands stack. 
	//		while (operators.Count != 0)
	//		{
	//			String op1 = operands.Peek();
	//			operands.Pop();

	//			String op2 = operands.Peek();
	//			operands.Pop();

	//			char op = operators.Peek();
	//			operators.Pop();

	//			String tmp = op + op2 + op1;
	//			operands.Push(tmp);
	//		}

	//		// Final prefix expression is 
	//		// present in operands stack. 
	//		return operands.Peek();
	//	}

 //   }

}