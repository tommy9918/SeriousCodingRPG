using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumVariable
{
    public string variable_name;
    public List<int> step_log;
    public List<double> variable_value;

    public NumVariable()
    {
        variable_name = "";
        step_log = new List<int>();
        variable_value = new List<double>();
    }

    public NumVariable(string name, string value, int step)
    {
        variable_name = name;
        step_log = new List<int>();
        variable_value = new List<double>();
        step_log.Add(step);
        variable_value.Add(double.Parse(value));
    }

    public void ModifyValue(string value, int step)
    {
        step_log.Add(step);
        variable_value.Add(double.Parse(value));
    }

    public string GetLatestValue()
    {
        return variable_value[variable_value.Count - 1].ToString();
    }

    public string GetValueAtStep(int step)
    {
        for(int i = 0; i <= step_log.Count - 2; i++)
        {
            if(step >= step_log[i] && step < step_log[i + 1])
            {
                return variable_value[i].ToString();
            }
        }
        if (step >= step_log[step_log.Count - 1]) return GetLatestValue();
        return null;
    }

    string SpaceString(int length)
    {
        string temp = "";
        for (int i = 0; i <= length - 1; i++)
        {
            temp += " ";
        }
        return temp;
    }

    public string ToCharValue()
    {
        return SpaceString(int.Parse(this.GetLatestValue()));
    }
}

public class CharVariable
{
    public string variable_name;
    public List<int> step_log;
    public List<string> variable_value;

    public CharVariable()
    {
        variable_name = "";
        step_log = new List<int>();
        variable_value = new List<string>();
    }

    public CharVariable(string name, string value, int step)
    {
        variable_name = name;
        step_log = new List<int>();
        variable_value = new List<string>();
        step_log.Add(step);
        variable_value.Add(value);
    }

    public void ModifyValue(string value, int step)
    {
        step_log.Add(step);
        variable_value.Add(value);
    }

    public string GetLatestValue()
    {
        return variable_value[variable_value.Count - 1];
    }

    public string GetLatestValueAt(int index)
    {
        string latest = variable_value[variable_value.Count - 1];
        if (index > latest.Length - 1) return "";
        else return latest[index].ToString();
    }

    public string GetValueAtStep(int step)
    {
        for (int i = 0; i <= step_log.Count - 2; i++)
        {
            if (step >= step_log[i] && step < step_log[i + 1])
            {
                return variable_value[i];
            }
        }
        if(step >= step_log[step_log.Count - 1]) return GetLatestValue();
        return null;
    }

    public string ToNumValue()
    {
        return this.GetLatestValue().Length.ToString();
    }
}
public class ExecutionSpace
{
    public List<NumVariable> num_variable;
    public List<CharVariable> char_variable;
    public List<int> line_number_traversed;
    public List<int> jump_index;
    public List<int> jump_amount;
    public int current_line_number;
    public int current_step;
    public bool execution_end;
    public string[] input_array;
    int input_index;

    public ExecutionSpace()
    {
        num_variable = new List<NumVariable>();
        char_variable = new List<CharVariable>();
        line_number_traversed = new List<int>();
        jump_index = new List<int>();
        jump_amount = new List<int>();
        current_line_number = 1;
        current_step = 0;
        execution_end = false;
        input_index = 0;

        //Debug.Log("Construct Execution Space Success");
    }

    public string StartExecution(List<CommandBlock> commands, string input_long_string)
    {
        input_index = 0;
        input_array = input_long_string.Split(',');

        while (!execution_end)
        {
            //Debug.Log(commands[current_line_number - 1].command);
            line_number_traversed.Add(current_line_number);
            //Debug.Log("Running line number:" + current_line_number);
            switch (commands[current_line_number - 1].command)
            {
                case "assign":
                    //Debug.Log("Running assign");
                    current_step++;
                    if (commands[current_line_number - 1].value_blocks[0].value_operation != "at")
                    {
                        string var_name = commands[current_line_number - 1].value_blocks[0].value;
                        //string value = commands[current_line_number - 1].value_blocks[1].GetValue();
                        string value = TranslateToValue(commands[current_line_number - 1].value_blocks[1]);
                        string type = commands[current_line_number - 1].value_blocks[1].value_type;
                        ExecuteAssign(var_name, value, type, current_step);
                        //current_step++;
                        //i++;
                    }
                    else
                    {
                        string var_name = commands[current_line_number - 1].value_blocks[0].value_blocks[0].value;
                        string value = TranslateToValue(commands[current_line_number - 1].value_blocks[1]);
                        string index = TranslateToValue(commands[current_line_number - 1].value_blocks[0].value_blocks[1]);
                        ExecuteAssignAt(var_name, index, value, current_step);
                    }
                    current_line_number++;                 
                    break;
                case "output":
                    //Debug.Log("Running output");
                    current_step++;
                    return TranslateToValue(commands[current_line_number - 1].value_blocks[0]);
                case "input":
                    //Debug.Log("Running input");
                    current_step++;
                    string var_name2 = commands[current_line_number - 1].value_blocks[0].value;
                    string value2 = input_array[input_index];
                    input_index++;
                    string type2 = "num";
                    ExecuteAssign(var_name2, value2, type2, current_step);
                    current_line_number++;                 
                    break;
                case "if":
                    //Debug.Log("Running if");
                    //Debug.Log(TranslateToValue(commands[current_line_number - 1].value_blocks[0]));
                    current_step++;
                    if (TranslateToValue(commands[current_line_number - 1].value_blocks[0]).Equals("True"))
                    {
                        string sub_output = ExecuteSubroutine(commands[current_line_number - 1].command_blocks1);
                        if (sub_output.Length > 0) return sub_output;
                    }
                    else if (TranslateToValue(commands[current_line_number - 1].value_blocks[0]).Equals("False"))
                    {
                        //Debug.Log("Outputing laaaaa");
                        string sub_output = ExecuteSubroutine(commands[current_line_number - 1].command_blocks2);
                        if (sub_output.Length > 0) return sub_output;
                    }
                    //current_step++;
                    break;
                case "jump":
                    //Debug.Log("Running jump");
                    current_step++;
                    current_line_number = int.Parse(TranslateToValue(commands[current_line_number - 1].value_blocks[0]));
                    break;
            }
            //DebugAllVariable();
            //Debug.Log("Run Success");
            //current_step++;
            if (current_step > 100) execution_end = true;
            if(current_line_number - 1 > commands.Count-1) execution_end = true;
        }
        return "";
    }

    public string ExecuteSubroutine(List<CommandBlock> commands)
    {
        for(int i = 0; i <= commands.Count - 1; i++)
        {
            line_number_traversed.Add(current_line_number);
            //Debug.Log("Running sub line number:" + (i+1).ToString());
            switch (commands[i].command)
            {
                case "assign":
                    current_step++;
                    if (commands[i].value_blocks[0].value_operation != "at")
                    {
                        string var_name = commands[i].value_blocks[0].value;
                        //string value = commands[current_line_number - 1].value_blocks[1].GetValue();
                        string value = TranslateToValue(commands[i].value_blocks[1]);
                        string type = commands[i].value_blocks[1].value_type;
                        ExecuteAssign(var_name, value, type, current_step);
                        //current_step++;
                        //i++;
                    }
                    else
                    {
                        string var_name = commands[i].value_blocks[0].value_blocks[0].value;
                        string value = TranslateToValue(commands[i].value_blocks[1]);
                        string index = TranslateToValue(commands[i].value_blocks[0].value_blocks[1]);
                        ExecuteAssignAt(var_name, index, value, current_step);
                    }
                    break;
                case "output":
                    current_step++;
                    return TranslateToValue(commands[i].value_blocks[0]);
                case "input":
                    current_step++;
                    string var_name2 = commands[i].value_blocks[0].value;
                    string value2 = input_array[input_index];
                    input_index++;
                    string type2 = "num";
                    ExecuteAssign(var_name2, value2, type2, current_step);
                    //current_step++;
                    //i++;
                    break;
                case "if":
                    current_step++;
                    if (TranslateToValue(commands[i].value_blocks[0]).Equals("True"))
                    {
                        string sub_output = ExecuteSubroutine(commands[i].command_blocks1);
                        if (sub_output.Length > 0) return sub_output;
                    }
                    else if (TranslateToValue(commands[i].value_blocks[0]).Equals("False"))
                    {
                        string sub_output = ExecuteSubroutine(commands[i].command_blocks2);
                        if (sub_output.Length > 0) return sub_output;
                    }
                    //current_step++;
                    break;
                case "jump":
                    current_step++;
                    current_line_number = int.Parse(TranslateToValue(commands[i].value_blocks[0]));
                    //current_step++;
                    return "";                  
            }
            //DebugAllVariable();
            //current_step++;
        }
        current_line_number++;
        return "";
    }

    public void DebugAllVariable()
    {
        Debug.Log("In step " + current_step + ", the memory space:");
        foreach(NumVariable nv in num_variable)
        {
            Debug.Log(nv.variable_name + ": " + nv.GetValueAtStep(current_step));
        }
        foreach (CharVariable cv in char_variable)
        {
            Debug.Log(cv.variable_name + ": " + cv.GetValueAtStep(current_step));
        }
    }

    public string DebugTextAtStep(int step)
    {
        //Debug.Log(step);
        if (step == 0) step = 1;
        string debugtext = "Finish executing line " + line_number_traversed[step - 1] + ".\n";
        debugtext = debugtext +  "Currently in memory: \n";
        //Debug.Log("In step " + current_step + ", the memory space:");
        foreach (NumVariable nv in num_variable)
        {
            //Debug.Log(nv.variable_name + ": " + nv.GetValueAtStep(current_step));
            if(nv.GetValueAtStep(step) != null)
                debugtext = debugtext + nv.variable_name + ": " + nv.GetValueAtStep(step) + "\n";
        }
        foreach (CharVariable cv in char_variable)
        {
            //Debug.Log(cv.variable_name + ": " + cv.GetValueAtStep(current_step));
            if (cv.GetValueAtStep(step) != null)
                debugtext = debugtext + cv.variable_name + ": " + cv.GetValueAtStep(step) + "\n";
        }
        return debugtext;
    }

    public string TranslateToValue(ValueBlock value_blk)
    {        
        if (value_blk.value_operation.Equals("variable"))   //value block is a variable
        {           
            if (GetNumVariable(value_blk.value) != null)     //get the value of the variable in the main memory
            {
                return GetNumVariable(value_blk.value).GetLatestValue();
            }
            else if (GetCharVariable(value_blk.value) != null)
            {
                return GetCharVariable(value_blk.value).GetLatestValue();
            }
            else return null;
        }
        else if (value_blk.value_operation.Equals("at"))    //value block is an at block
        {
            if (GetCharVariable(value_blk.value_blocks[0].value) != null)
            {
                int index = int.Parse(TranslateToValue(value_blk.value_blocks[1]));
                //Debug.Log(index);
                return GetCharVariable(value_blk.value_blocks[0].value).GetLatestValueAt(index);
            }
            return null;
        }
        else
        {
            switch (value_blk.value_operation)      //recursively calculate the value of value block
            {
                case "":
                    return value_blk.value;
                case "num":
                    return value_blk.value;
                case "char":
                    return value_blk.value;
                case "plus":
                    return (double.Parse(TranslateToValue(value_blk.value_blocks[0])) + double.Parse(TranslateToValue(value_blk.value_blocks[1]))).ToString();
                case "minus":
                    return (double.Parse(TranslateToValue(value_blk.value_blocks[0])) - double.Parse(TranslateToValue(value_blk.value_blocks[1]))).ToString();
                case "multiply":
                    return (double.Parse(TranslateToValue(value_blk.value_blocks[0])) * double.Parse(TranslateToValue(value_blk.value_blocks[1]))).ToString();
                case "divide":
                    return ((int)(double.Parse(TranslateToValue(value_blk.value_blocks[0])) / double.Parse(TranslateToValue(value_blk.value_blocks[1])))).ToString();
                case "remainder":
                    return (double.Parse(TranslateToValue(value_blk.value_blocks[0])) % double.Parse(TranslateToValue(value_blk.value_blocks[1]))).ToString();
                case "equal":
                    return TranslateToValue(value_blk.value_blocks[0]).Equals(TranslateToValue(value_blk.value_blocks[1])).ToString();
                case "smaller":
                    return (double.Parse(TranslateToValue(value_blk.value_blocks[0])) < double.Parse(TranslateToValue(value_blk.value_blocks[1]))).ToString();
                case "smaller_equal":
                    return (double.Parse(TranslateToValue(value_blk.value_blocks[0])) <= double.Parse(TranslateToValue(value_blk.value_blocks[1]))).ToString();
                case "larger":
                    return (double.Parse(TranslateToValue(value_blk.value_blocks[0])) > double.Parse(TranslateToValue(value_blk.value_blocks[1]))).ToString();
                case "larger_equal":
                    return (double.Parse(TranslateToValue(value_blk.value_blocks[0])) >= double.Parse(TranslateToValue(value_blk.value_blocks[1]))).ToString();
                case "and":
                    return (bool.Parse(TranslateToValue(value_blk.value_blocks[0])) && bool.Parse(TranslateToValue(value_blk.value_blocks[1]))).ToString();
                case "or":
                    return (bool.Parse(TranslateToValue(value_blk.value_blocks[0])) || bool.Parse(TranslateToValue(value_blk.value_blocks[1]))).ToString();
                case "not":
                    return (!bool.Parse(TranslateToValue(value_blk.value_blocks[0]))).ToString();
                case "skill":
                    string input_string = "";
                    for (int i = 0; i <= value_blk.value_blocks.Count - 1; i++)
                    {
                        input_string = input_string + TranslateToValue(value_blk.value_blocks[i]);
                        if (i < value_blk.value_blocks.Count - 1)
                        {
                            input_string = input_string + ',';
                        }
                    }
                    foreach (Skill skill in Player.Instance.data.skills)
                    {
                        
                        if (value_blk.value.Equals(skill.name.ToLower()))
                        {
                            ExecutionSpace skill_exec = new ExecutionSpace();
                            string output = skill_exec.StartExecution(skill.GetOriginalCommandBlockList(), input_string);
                            //current_step += skill_exec.current_step;
                            jump_index.Add(current_step);
                            jump_amount.Add(skill_exec.TotalStep());
                            return output;
                        }
                    }
                    return null;
            }
            //return value_blk.GetValue();
        }
        return null;
    }

    public int TotalStep()
    {
        int sum = 0;
        foreach(int stp in jump_amount)
        {
            sum += stp;
        }
        return sum + current_step;
    }

    public bool containsVariable(string var_name, out string type)
    {
        type = "";
        foreach(NumVariable nv in num_variable)
        {
            if (nv.variable_name.Equals(var_name))
            {
                type = "num";
                return true;
            }
        }
        foreach (CharVariable cv in char_variable)
        {
            if (cv.variable_name.Equals(var_name))
            {
                type = "char";
                return true;
            }
        }
        return false;
    }

    public NumVariable GetNumVariable(string var_name)
    {
        foreach (NumVariable nv in num_variable)
        {
            if (nv.variable_name.Equals(var_name)) return nv;
        }
        return null;
    }

    public CharVariable GetCharVariable(string var_name)
    {
        foreach (CharVariable cv in char_variable)
        {
            if (cv.variable_name.Equals(var_name)) return cv;
        }
        return null;
    }

    
    

    public void ExecuteAssign(string var_name, string value, string value_type, int step)
    {
        string var_type = "";
        if (containsVariable(var_name, out var_type))
        {
            //Debug.Log(var_type);
            if (value_type.Equals("num") && value_type == var_type)
            {
                GetNumVariable(var_name).ModifyValue(value, step);
            }
            else if (value_type.Equals("char") && value_type == var_type)
            {
                GetCharVariable(var_name).ModifyValue(value, step);
            }
        }
        else
        {          
            if (value_type.Equals("num"))
            {
                num_variable.Add(new NumVariable(var_name, value, step));
            }
            else if (value_type.Equals("char"))
            {
                char_variable.Add(new CharVariable(var_name, value, step));
            }
        }
    }


    public void ExecuteAssignAt(string var_name, string index_value, string string_value, int step)
    {
        string original_string = GetCharVariable(var_name).GetLatestValue();
        int total_length = Mathf.Max(original_string.Length, int.Parse(index_value) + string_value.Length);
        char[] new_string = new char[total_length];
        for(int i = 0; i <= total_length - 1; i++)
        {
            if(i < int.Parse(index_value))
            {
                if(i <= original_string.Length - 1)
                {
                    new_string[i] = original_string[i];
                }
                else
                {
                    new_string[i] = ' ';
                }
            }
            else
            {
                if(i- int.Parse(index_value) <= string_value.Length - 1)
                {
                    new_string[i] = string_value[i - int.Parse(index_value)];
                }
                else new_string[i] = original_string[i];
            }
        }
        GetCharVariable(var_name).ModifyValue(string.Join("",new_string), step);
    }

    string SpaceString(int length)
    {
        string temp = "";
        for(int i = 0; i <= length-1; i++)
        {
            temp += " ";
        }
        return temp;
    }
    
}
