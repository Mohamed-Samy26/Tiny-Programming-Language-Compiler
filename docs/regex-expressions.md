|         Name          |                            Regex                             |
| :-------------------: | :----------------------------------------------------------: |
|        Number         |                     `[0-9]+(\.[0-9]+)?`                      |
|        String         |                            `“.*“`                            |
|   Reserved_Keywords   | `int|float|string|read|write|repeat|until|if|elseif|else|then|return|endl` |
|   Comment_Statement   |                         `/\* .* \*/`                         |
|      Identifiers      |                `[a-z A-Z] [a-z A-z Number]*`                 |
|     Function_Call     |        `Identifier\( ((Identifier,)* Identifier)?\)`         |
|         Term          |              `Number|Identifier|Function_Call`               |
|  Arithamtic_Operator  |                           `[+-*/]`                           |
|       Equation        |              `(TermArithamtic_Operator)+ Term`               |
|      Expression       |                    `String|Term|Equation`                    |
|      Assignment       |                      `:= (Expression)`                       |
| Assignment_Statement  |                   `identifier Assignment`                    |
|       Datatype        |                      `int|float|string`                      |
| Declaration_Statement | `data_type identifier assignment? (, identifier assignment?)*;` |
|    Write_Statement    |                   `write expression|endl;`                   |
|    Read_Statement     |                      `read identifier;`                      |
|   Return_Statement    |                     `return expression;`                     |
|  Condition_Operator   |                          `<|>|=|<>`                          |
|       Condition       |                 `identifier condition term`                  |
|   Boolean_Operator    |                          `&&| \|\|`                          |
|  Condition_Statement  |          `condition (boolean_opearator condition)*`          |
|     If_Statement      | `if (Condition_statement) then (statement)+ (Elseif_statement|Else_statement|end)` |
|   Else_If_Statement   |                    `(else)(if_statement)`                    |
|    Else_Statement     |                 `(else) (statement)+ (end)`                  |
|   Repeat_Statement    |     `(repeat) (statement)+ (until)(Condition_statement)`     |
|     FunctionName      |                         `Identifier`                         |
|       Parameter       |                   `(Datatype)(Identifier)`                   |
| Function_Declaration  |   `(Datatype) FunctionName\((Parameter,)*(Parameter))?\)`    |
|     Function_Body     |            `{ (statement)+ (Return_statement) }`             |
|  Function_Statement   |           `(Function_declarartion)(Function_body)`           |
|     Main_Function     |              `(Datatype) (main)(Function_body)`              |
|        Program        |           `(Function_statement)* (Main_Function)`            |

