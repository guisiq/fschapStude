System.Console.Write "Type a value:"
let str = System.Console.ReadLine()
printfn "You typed %s" str

//#%

// let first = "32"
// let numberVersion = System.Int32.Parse(first) 
// printfn "Number %i" numberVersion // Output: Number 32

//#%

let first = "32"
let numberVersion =  int first 
printfn "Number %i" numberVersion

//#%

let no = 10
let isDivisibleByTwo = no % 2 = 0
printfn "Divisible by two %b" isDivisibleByTwo