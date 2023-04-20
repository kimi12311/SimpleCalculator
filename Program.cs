var quit = false;
var registry = new Dictionary<string, double>();
var queue = new List<QueueObject>();

while (!quit)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write(">");
    ReadInput(Console.ReadLine()!.ToLower().Split(' '));
}

void ReadInput(string[] input)
{
    if (VerifyInput(input) == -1) return;
    ShouldAddToRegistry(input[0]);
    queue.Add(new QueueObject
    {
        Operation = input[1],
        RegistryValue = input[0],
        Value = input[2],
        Priority = int.TryParse(input[2], out var _) ? 0 : 1
    });
}

void Execute(string input)
{
    if (!registry.ContainsKey(input))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Non existing registry value");
        return;
    }
    var sortedQueue = queue.Where(x => x.RegistryValue != input).ToList().OrderBy(x => x.Priority);
    var deferredQueue = queue.Where(x => x.RegistryValue == input).ToList();
    foreach (var command in sortedQueue) ApplyCommands(command);
    foreach (var command in deferredQueue) ApplyCommands(command);
    Console.WriteLine(registry.First(x => x.Key == input).Value);
    queue.Clear();
}

void ApplyCommands(QueueObject command)
{
    var registryValue = registry[command.RegistryValue];
    double value;
    switch (command.Operation)
    {
        case "add":
            value = registryValue + DetermineValue(command.Value!);
            break;
        case "subtract":
            value = registryValue - DetermineValue(command.Value!);
            break;
        case "multiply":
            value = registryValue * DetermineValue(command.Value!);
            break;
        case "equal":
            value = DetermineValue(command.Value!);
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid operation provided");
            return;
    }
    ReplaceEntry(command.RegistryValue, value);
}

void ReplaceEntry(string name, double value)
{
    registry[name] = value;
}

double DetermineValue(string input)
{
    return registry.ContainsKey(input) ? registry[input] : double.Parse(input);
}

void ShouldAddToRegistry(string registryValue)
{
    if (!registry.ContainsKey(registryValue)) registry.Add(registryValue, 0);
}

int VerifyInput(string[] input)
{
    if (double.TryParse(input[0], out _))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid registry name");
        return -1;
    }
    switch (input[0])
    {
        case "print":
            if(input.Length == 1 || input.Length > 2){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid print command");
                return -1;
            }
            Execute(input[1]);
            return -1;
        case "quit":
            quit = true;
            return -1;
        case "clear":
            Console.Clear();
            return -1;
    }
    if (input.Length > 1 && (input[1] == "add" || input[1] == "subtract" || input[1] == "multiply" || input[1] == "equal"))
    {
        return 0;
    }
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Invalid command");
    return -1;
}