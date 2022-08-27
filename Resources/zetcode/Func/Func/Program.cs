// See https://aka.ms/new-console-template for more information

//Func1

Console.WriteLine("Func1:{0}", new Func1().sayHello());

Utility.HowToContinue("Func2");

//Func2
Console.WriteLine("Func2:{0}", new Func2().res);
int a = 2, b = 5;
Console.WriteLine("Func2:{0}", Func2.add(a, b));

Console.WriteLine("delegate1:{0}", new Func2().Add1(2, 5));
Console.WriteLine("delegate2:{0}", Func2.Add2(a, b));

Utility.HowToContinue("StaticVarAndMethod");

//StaticVarAndMethod
StaticVarAndMethod staticVarAndMethod = new StaticVarAndMethod();
Console.WriteLine("Invoking a public method {0}", staticVarAndMethod.PublicSum(5, 10));
Console.WriteLine(StaticVarAndMethod.B = "No need to initialize the class for accessing to static var B");
StaticVarAndMethod.StaticMethod();

Utility.HowToContinue("Func3 with lambda expression");

//Func3 with lambda expression
Console.WriteLine("Func3 with lambda expression {0} \r\n Generally Func and its method should be declared in static",
    Func3.randInt(1, 100));

Utility.HowToContinue("Func4 LINQ Where");

/*
  This is the way
  you can comment
  multi lines 
*/
//Func4 LINQ Where
Console.WriteLine("Func4 LINQ Where");
foreach (var word in new Func4().threeLetterWords)
{
    Console.WriteLine(word);
}

new Func4().PrintThreeLetterWords(new Func4().threeLetterWords);

Utility.HowToContinue("Func5 list of Func delegates");

//Func5 list of Func delegates
Func5.PrnOperations();
Func5.PrnOperations1(new Func5().fns, new Func5().vals);
Func5.PrnOperation();

Utility.HowToContinue("Func6 Func filter array");

//Func6 Func filter array
new Func6().PrnBratislava();
new Func6().PrnAge();

Utility.HowToContinue("Func7 Predicate");

//Func7 Predicate
new Func7().PrnBratislava();
new Func7().PrnAge();

Utility.HowToContinue("Func8 pass Func as parameter");

//Func8 pass Func as parameter
var f8=new Func8();
new Func8().ShowOutput(new Func8().data,new Func8().Condition); //Doesn't work 
new Func8().ShowOutput(new Func8().data, r => r.Occupation == "teacher");

Utility.HowToContinue("Func9 compose");

//Func9 compose
new Func9().PrnCalculation();