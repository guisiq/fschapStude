open System
open System.Numerics
open MathNet.Numerics
open MathNet.Symbolics

open Operators

let x = symbol "x"
let y = symbol "y"
let a = symbol "a"
let b = symbol "b"
let c = symbol "c"
// let d = symbol "d"

System.Console.WriteLine(Infix.format((a + b)*(c-d)))                  // returns 2*a
// print nfix.format(a * a)                  // returns a^2
// print Infix.format(2 + 1/x - 1)            // returns 1 + 1/x
// print Infix.format((a/b/(c*a))*(c*d/a)/d)  // returns 1/(a*b)