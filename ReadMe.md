# Simple Calculator

# Implementation
This application uses a queue system that is executed when the **print** command is called which sorts numerical operations with higher priority than operations that read from the register. The value that is printed has commands assigned to it deferred to a separate queue which is called after the initial queue, this allows values to be properly processed before a value is read from the register.

All values are stored in a registry as **key, value** pairs with double values by default and inputs for operations can be both registry items and values.

A few utility functions include the **clear** command that clears the console and **quit** which exits the application.

Values are implicitly added to the registry with a value of 0 if they do not exist when an operation is called on them.

Error messages are outputted to the console, but execution is not stopped.


## Usage
`x add y` - Adds the value of y to the value of x and stores the result in x

`x subtract y` - Subtracts the value of y from the value of x and stores the result in x

`x multiply y` - Multiplies the value of x by the value of y and stores the result in x

`x equal y` - Sets the value of x to the value of y and stores the result in x

`clear` - Clears the console

`quit` - Exits the application

`print x` - Prints the value of x to the console and executes the queue

## How to extend
In program.cs you will find a ApplyCommands method that takes a QueueObject as an argument, you can extend it by
adding a new case to the switch statement which will be called when the command is read from the queue.

Example (adding a divide operator):
    
        case "divide":
            value = registryValue / DetermineValue(command.Value);
            break;

## Running the application

- Download the source code and CD into the file, open in your IDE of choice.
- Ensure your terminal points to `...\SectraCalculator`
- Run the restore command:
- `dotnet restore`
- Then run the following command:
- `dotnet run`
- The application should now start


## Example Commands
**Example 1:**

    >a add b
    >a add 2
    >b add 2
    >print a
    
    Output: 4
**Example 2:**

    >result add revenue
	>result subtract costs
	>revenue add 200
	>costs add salaries
	>salaries add 20
	>salaries multiply 5
	>costs add 10
	>print result
	
	Output: 90
