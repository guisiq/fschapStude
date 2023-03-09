// Define a type for arithmetic expressions
type Expr =
  | Const of int
  | Add of Expr * Expr
  | Sub of Expr * Expr
  | Mul of Expr * Expr

// Define a function to evaluate an expression
let rec eval expr =
  match expr with
  | Const n -> n
  | Add (a, b) -> eval a + eval b
  | Sub (a, b) -> eval a - eval b
  | Mul (a, b) -> eval a * eval b

// Define a function to generate a random expression
let rec randomExpr depth =
  let rand = System.Random()
  if depth = 0 then Const(rand.Next(10))
  else 
    let op = rand.Next(3)
    let left = randomExpr (depth - 1)
    let right = randomExpr (depth - 1)
    match op with
    | 0 -> Add(left, right)
    | 1 -> Sub(left, right)
    | _ -> Mul(left, right)

// Define a function to mutate an expression by replacing a subexpression with another one
let rec mutate expr newExpr =
  let rand = System.Random()
  if rand.NextDouble() < 0.1 then newExpr // replace with probability of 0.1
  else 
    match expr with
    | Const _ -> expr // cannot mutate constants further
    | Add (a, b) -> Add(mutate a newExpr, mutate b newExpr) // recursively mutate left and right operands 
    | Sub (a, b) -> Sub(mutate a newExpr, mutate b newExpr)
    | Mul (a, b) -> Mul(mutate a newExpr, mutate b newExpr)

// Define a function to crossover two expressions by swapping subexpressions at random points 
let rec crossover expr1 expr2 =
   let rand = System.Random()
   if rand.NextDouble() < 0.5 then expr2 // swap with probability of 0.5 
   else 
     match expr1 with 
     | Const _ -> expr1 // cannot crossover constants further 
     | Add (a,b) -> Add(crossover a expr2,crossover b expr2) // recursively crossover left and right operands 
     | Sub (a,b) -> Sub(crossover a expr2,crossover b expr2)
     | Mul (a,b) -> Mul(crossover a expr2,crossover b expr2)

// Define the target value that we want to find an expression for 
let target = -42

// Define the fitness function that measures how close an expression is to the target value 
let fitness expr = abs(target - eval(expr))

// Define the initial population size and maximum depth of expressions  
let popSize = 100  
let maxDepth = 5  

// Generate an initial population of random expressions  
let population = List.init popSize (fun _ -> randomExpr maxDepth)

// Define the number of generations to run the genetic algorithm for  
let generations = 100  

// Run the genetic algorithm for the specified number of generations  
for i in [1..generations] do 

   // Evaluate the fitness of each individual in the population  
   let scores = population |> List.map fitness 

   // Find the best individual and its fitness score  
   let bestScore,bestIndiv = List.zip scores population |> List.minBy fst 

   // Print the best individual and its fitness score  
   printfn "Generation %d: Best score = %d; Best individual = %A" i bestScore bestIndiv 

   // Check if we have found an exact solution  
   if bestScore = target then break 

   // Select two parents from the population using tournament selection  
   let selectParent () =
      let candidates = List.take popSize scores |> List.zip scores population |> List.sortBy fst   
      candidates.[rand.Next(popSize/10)].snd

   let parent1,parent2= selectParent(),selectParent()

   // Crossover the parents to produce two offspring   
   let offspring1= crossover parent1 parent2   
   let offspring2= crossover parent2 parent1   

   // Mutate each offspring by replacing some subexpressions with random ones   
   let mutant1= mutate offspring1 <| randomExpr maxDepth   
   let mutant2= mutate offspring2 <| randomExpr maxDepth   

   // Replace two individuals in the population with lower fitness scores by mutants   
   let replaceMutant mutant =
      let worst