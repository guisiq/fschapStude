
Func<int, IEnumerable<int>> rootCombnaitions = x => {
    if (x == 0||x < 10)
    {
        return new List<int>();
    }
    else
    {
         return new List<int> { x.ToString().Select(y => int.Parse(y.ToString())).Sum() };
    }
};

var root = new Dictionary<int,TreeNode<int>>();

var rootSum = TreeNodehelper.BuildTree(rootCombnaitions,value: 9999,Depth: 3,root: root);
//todadas as combinacoes de numeros de 2 digitos com //[5,6,7,8,9] podendo repitir 
List<string> GenerateCombinations(List<int> numbers, int digitCount)
{   
    IQueryable<String> aux(IQueryable<int> numbers2, int digitCount2){
        if (digitCount2 == 1)
        {
            return numbers2.Select(n => n.ToString());
        }
        else
        {
            return aux(numbers2, digitCount2 - 1).SelectMany(x => numbers2, (x, y) => x + y.ToString());
        }
    }
    // recupera a expressao linq para que possa ser executada posteriormente
    // var ex = aux(numbers.AsQueryable(), digitCount).Expression;
    // var CanReduce = ex.CanReduce;
    // var x2 = ex.ReduceExtensions;
    return aux(numbers.AsQueryable(), digitCount).ToList();
}
var combinations2 = GenerateCombinations(new List<int> { 5, 6, 7, 8, 9 }, 2);
var combinations3e2 = GenerateCombinations(new List<int> { 5, 6, 7, 8, 9 }, 3)
                        .Select(x => int.Parse(x))
                        .Union(combinations2.Select(x => int.Parse(x)))
                        .ToList();
combinations3e2.ForEach(x => 
{
    //[5,6,7,8,9] 
    Console.WriteLine(x);
    rootSum = TreeNodehelper.BuildTree(rootCombnaitions,value: x ,Depth: 3,root: root);
});
//força a execulcao tardia dos metodos linq para que o plantUML possa ser gerado

var plantUmlOutput = rootSum.PrintTreeforplantUML();
Console.WriteLine(plantUmlOutput);
rootSum.InvertTree().GetEnumerator().Current.PrintTreeforplantUML();
Console.WriteLine(plantUmlOutput);

