// System.Console.Write "Type a value:"
// let str = System.Console.ReadLine()
// printfn "You typed %s" str;;

// //#%

// // let first = "32"
// // let numberVersion = System.Int32.Parse(first) 
// // printfn "Number %i" numberVersion // Output: Number 32

// //#%

// let first = "32"
// let numberVersion =  int first 
// printfn "Number %i" numberVersion;;

// //#%

// let no = 10
// let isDivisibleByTwo = no % 2 = 0
// printfn "Divisible by two %b" isDivisibleByTwo;;
open System
open System.Numerics
open MathNet.Numerics
open MathNet.Symbolics

open NJection.LambdaConverter.Fluent
open System.Linq.Expressions
open Operators
open AgileObjects.ReadableExpressions 

let parse (value:string):Int32 = 
    Int32.Parse(value)
let add (value:Int32,value2:Int32):Int32 = 
    value+value2
let parse2 value value2= 
    value+value2
let add1 x = x + 1
let add2 x y = 
    let a = x + y
    a + y

let funcAdd1 = Lambda.TransformMethodTo<Func<Int32,Int32>>()
                         .From(fun () -> add1)
                         .ToLambda();
let funcAdd2 = Lambda.TransformMethodTo<Func<Int32,Int32,Int32>>()
                         .From(fun () -> add2)
                         .ToLambda();
let funcAdd2 = Lambda.TransformMethodTo<Func<Int32,Int32,Int32>>()
                         .From(fun () -> add2)
                         .ToLambda();
let funcAdd2 = Lambda.ResolveMethodTo <Func<Int32,Int32,Int32>>()
                         .From(fun () -> add2)
                         .ToLambda();

let funcAdd2_2 :Expression<Func<Int32,Int32,Int32>> = add2 
let funcParse2 = Lambda.TransformMethodTo<Func<string,string,string>>()
                         .From(fun () -> parse2)
                         .ToLambda();
System.Console.WriteLine(funcAdd1.ToReadableString())
System.Console.WriteLine(funcAdd2_2.ToReadableString())
System.Console.WriteLine(funcParse2.ToReadableString())

// let x = symbol "x"
// let y = symbol "y"
// let a = symbol "a"
// let b = symbol "b"
// let c = symbol "c"
// let d = symbol "d"

// System.Console.WriteLine(Infix.format(a + a))                 // returns 2*a
// System.Console.WriteLine(Infix.format((a + b)*(c+d)))       
// //printf "a"
// System.Console.WriteLine( Infix.format(a * a))                  // returns a^2
// System.Console.WriteLine(Infix.format(2 + 1/x - 1))            // returns 1 + 1/x
// System.Console.WriteLine(Infix.format((a/b/(c*a))*(c*d/a)/d))  // returns 1/(a*b)