# Tiny Language CFG Rules

- Non-Terminals
    - *Function_Call* , *Term* , *Arithmatic_Op*  , *Equation* , *Expression* , *Assignment_Statement* , *Datatype*  , *Declaration_Statement* , *Write_Statement* ,  *Read_Statement* ,  *Return_Statement* ,  *Condition_Op* ,  *Condition* ,  *Boolean_Op* ,  *Condition_Statement* ,  *If_Statement* ,  *Else_If_Statement* ,  *Else_Statement* ,  *Repeat_Statement* ,  *Parameter* ,  *Function_Declaration* ,  *Function_Body* ,  *Function_Statement* ,  *Main_Function* ,  *Program*
- Terminals
    - **main** , **(** , **)** , **{** , **}** , **identifier** , **repeat** , **until** , **if** , **then** , **elseif**, **else** , **end** , **return** , **read** , **write** , **endl** , **int** , **float** , **string** , **&& , || , + , - , * , / , < , > , <> , =  , number**
1. *Program* **→** <u>*Program*'</u> *Main_Function*
2. <u>*Program*</u>' → *Function_Statement* <u>*Program*'</u> | e
3. *Main_Function* → *Datatype* **main** **(** **)** *Function_Body*
4. *Function_Statement* → *Function_Declaration* *Function_Body*
5. *Function_Body* → **{** <u>*Statements*</u> *Return_Statement* **}**
6. <u>*Statements*</u> → <u>*Statement*</u> <u>*Statements*'</u> | e
7. <u>*Statements*'</u> → <u>*Statement*</u> <u>*Statements*'</u> | e
8. <u>*Statement*</u> → *Assignment_Statement* **;**| *Declaration_Statement* | *Write_Statement* | *Read_Statement* | *Condition_Statement* | *If_Statement* | *Repeat_Statement* | *Function_Call* **;**
9. *Function_Declaration* → *Datatype* Function_Name **(** <u>*Parameters*</u> **)**
10. <u>*Parameters*</u> → *Parameter* <u>*Parameters*'</u> | e
11. <u>*Parameters*'</u> → **,** *Parameter* <u>*Parameters*'</u> | e
12. *Parameter* → *Datatype* **identifier**
13. Function_Name → **identifier**
14. *Repeat_Statement* → **repeat** <u>*Statements*</u> **until** *Condition_Statement*
15. *Condition_Statement* → *Condition* <u>*Conditions*</u> 
16. <u>*Conditions*</u> → *Boolean_Op* *Condition* <u>*Conditions*</u> | e
17. *If_Statement*  → **if** *Condition_Statement* **then** <u>*StatementsOrReturn*</u> <u>*Rest_if*</u> 
18. <u>*Rest_if*</u>  → *Else_If_Statement* | *Else_Statement* | **end** 
19. <u>*StatementsOrReturn*</u> → *Statements* <u>*StatementsOrReturn*</u> | *Return_Statement* | e
20. *Else_If_Statement*  → **elseif** *Condition_Statement* **then** <u>*StatementsOrReturn*</u> <u>*Rest_if*</u> 
21. *Else_Statement*  → **else** <u>*StatementsOrReturn*</u> **end** 
22. *Return_Statement* → **return** *Expression* **;**
23. *Read_Statement* → **read** **identifier** **;**
24. *Write_Statement* → **write** <u>Write'</u> **;**
25. <u>Write'</u> → *Expression* | **endl** 
26. *Declaration_Statement* → *Datatype* <u>*Identifiers*</u> **;**
27. <u>*Identifiers*</u> → **identifier** <u>*Identifiers*'</u> | *Assignment_Statement* <u>*Identifiers*'</u>
28. <u>*Identifiers*'</u> → **,** <u>*Identifiers*</u> |e
29. *Datatype* → **int** | **float** | **string**
30. *Assignment_Statement* → **identifier** **:=** *Expression* 
31. *Expression* → **string** | *Equation*
32. *Arithmatic_Op* → *Add_Op* | *Mult_Op*
33. *Boolean_Op* → **&&** | "**||**"
34. *Condition_Op* → **< | > | = | <>**
35. *Function_Call* → *Function_Name* **(** <u>*Arguments*</u> **)**
36. <u>*Arguments*</u> → **identifier** <u>*Argument*'</u> | e
37. <u>*Argument*'</u> → **,** **identifier** <u>*Argument*'</u> | e
38. *Condition*  →  **identifier** *Condition_Op* *Term*
39. *Add_Op* →  **+ | -**
40. *Mult_Op* →  *** | /**
41. *Equation* →  *Factor* <u>*Equation*'</u>
42. <u>*Equation*'</u> →  *Add_Op* *Factor* <u>*Equation*'</u> | e
43. *Factor* →  **(** *Equation* **)** <u>*Factor*'</u>  | *Term* <u>*Factor*'</u>
2. <u>*Factor*'</u> →  *Mult_Op* *Equation* <u>*Factor*'</u> | e
45. *Term* →  **number** | **identifier** | *Function_Call*
