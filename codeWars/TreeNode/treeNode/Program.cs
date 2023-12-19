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
for (int i = 0; i < 1000; i++)
{
    Console.WriteLine(i);
    rootSum = TreeNode<int>.buildTree(rootCombnaitions,value: (i*10)+1000+(i%9),Depth: 3,root: root);
}
//força a execulcao tardia dos metodos linq para que o plantUML possa ser gerado

var plantUmlOutput = TreeNode<int>.PrintTreeforplantUML(rootSum);
Console.WriteLine(plantUmlOutput);
TreeNode<int>.InvertTree(rootSum).ForEach(x => Console.WriteLine(TreeNode<int>.PrintTreeforplantUML(rootSum)));
Console.WriteLine(plantUmlOutput);

