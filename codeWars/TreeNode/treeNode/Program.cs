// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
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
var rootSum = TreeNode<int>.buildTree(rootCombnaitions,value: 9999,Depth: 3,root: root);
//todadas as combinacoes de numeros de 2 digitos com //[5,6,7,8,9] podendo repitir 
List<string> GenerateCombinations(List<int> numbers, int digitCount)
{
    if (digitCount == 1)
    {
        return numbers.Select(n => n.ToString()).ToList();
    }
    else
    {
        var result = new List<string>();
        var smallerCombinations = GenerateCombinations(numbers, digitCount - 1);

        foreach (var number in numbers)
        {
            result.AddRange(smallerCombinations.Select(c => number.ToString() + c));
        }

        return result;
    }
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
    rootSum = TreeNode<int>.buildTree(rootCombnaitions,value: x ,Depth: 3,root: root);
});
//força a execulcao tardia dos metodos linq para que o plantUML possa ser gerado

var plantUmlOutput = TreeNode<int>.PrintTreeforplantUML(rootSum);
Console.WriteLine(plantUmlOutput);
TreeNode<int>.InvertTree(rootSum).ForEach(x => Console.WriteLine(TreeNode<int>.PrintTreeforplantUML(rootSum)));
Console.WriteLine(plantUmlOutput);

