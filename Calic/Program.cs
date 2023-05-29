public void ProcessStart(string text)
{
    List<char> symbols = SymbolFinder(text);
    string[] nums = text.Split('*', '/', '+', '-');
    List<double> numbers = FindNum(nums);
    Console.WriteLine($"Результат: {Prioritets(symbols, numbers)}");
}

public List<double> FindNum(string[] nums)
{
    List<double> numbers = new List<double>();

    for (int i = 0; i < nums.Length; i++)
    {
        numbers.Add(Convert.ToDouble(nums[i]));
    }

    return numbers;
}

public List<char> SymbolFinder(string text)
{
    List<char> symbols = new List<char>();

    for (int i = 0; i < text.Length; i++)
    {
        switch (text[i])
        {
            case '+':
            case '-':
            case '*':
            case '/':
                symbols.Add(text[i]);
                break;
        }
    }

    return symbols;
}

public double Prioritets(List<char> symbols, List<double> numbers)
{
    char[] supportedOperators = { '*', '/', '+', '-' };
    int[] priorities = { 0, 0, 1, 1 };

    foreach (int priority in priorities.Distinct())
    {
        List<char> operators = new List<char>();

        for (int i = 0; i < priorities.Length; i++)
        {
            if (priorities[i] == priority)
                operators.Add(supportedOperators[i]);
        }

        for (int i = 0; i < symbols.Count; i++)
        {
            if (operators.Contains(symbols[i]))
            {
                numbers[i] = Calculate(numbers[i], numbers[i + 1], symbols[i]);
                numbers.RemoveAt(i + 1);
                symbols.RemoveAt(i);
                i--;
            }
        }

    }

    return numbers[0];
}

public double Calculate(double left, double right, char op)
{
    double num = 0;

    switch (op)
    {
        case '/':
            num = left / right;
            break;
        case '*':
            num = left * right;
            break;
        case '+':
            num = left + right;
            break;
        case '-':
            num = left - right;
            break;
    }

    return num;
}