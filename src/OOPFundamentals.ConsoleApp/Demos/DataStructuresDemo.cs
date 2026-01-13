namespace OOPFundamentals.ConsoleApp.Demos;

/// <summary>
/// Demonstration of Data Structures comparison between C# and Java.
/// Shows arrays, lists, and dictionaries/maps with side-by-side code examples.
/// </summary>
public class DataStructuresDemo
{
    public void Run()
    {
        Console.WriteLine("📝 Comparación de Estructuras de Datos: C# vs Java");
        Console.WriteLine("   Exploraremos las diferencias en sintaxis y funcionalidad entre");
        Console.WriteLine("   las estructuras de datos más comunes en ambos lenguajes.\n");
        
        DemonstrateArrays();
        Console.WriteLine("\n" + new string('-', 80) + "\n");
        DemonstrateLists();
        Console.WriteLine("\n" + new string('-', 80) + "\n");
        DemonstrateDictionaries();
        
        Console.WriteLine("\n✅ Conclusión:");
        Console.WriteLine("   Aunque la sintaxis difiere, los conceptos fundamentales son similares.");
        Console.WriteLine("   C# tiende a tener una sintaxis más concisa, mientras Java es más verboso.");
    }
    
    private void DemonstrateArrays()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("📋 ESTRUCTURAS 1: Arrays (Arreglos)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        Console.WriteLine("\n1️⃣ Declaración e Inicialización:");
        Console.WriteLine("\n╔═══════════════════════════╦═══════════════════════════╗");
        Console.WriteLine("║          C#               ║          Java             ║");
        Console.WriteLine("╠═══════════════════════════╬═══════════════════════════╣");
        Console.WriteLine("║ int[] numbers;            ║ int[] numbers;            ║");
        Console.WriteLine("║ numbers = new int[5];     ║ numbers = new int[5];     ║");
        Console.WriteLine("║                           ║                           ║");
        Console.WriteLine("║ // Con valores iniciales  ║ // Con valores iniciales  ║");
        Console.WriteLine("║ int[] nums = {1,2,3,4,5}; ║ int[] nums = {1,2,3,4,5}; ║");
        Console.WriteLine("║                           ║                           ║");
        Console.WriteLine("║ // Arrays de objetos      ║ // Arrays de objetos      ║");
        Console.WriteLine("║ string[] names;           ║ String[] names;           ║");
        Console.WriteLine("║ names = new string[3];    ║ names = new String[3];    ║");
        Console.WriteLine("╚═══════════════════════════╩═══════════════════════════╝");
        
        // Actual C# demonstration
        Console.WriteLine("\n2️⃣ Ejemplo en C# (ejecución real):");
        
        int[] numbers = { 10, 20, 30, 40, 50 };
        string[] names = { "Alice", "Bob", "Charlie" };
        
        Console.WriteLine($"   Array de números: [{string.Join(", ", numbers)}]");
        Console.WriteLine($"   Array de nombres: [{string.Join(", ", names)}]");
        
        Console.WriteLine("\n3️⃣ Acceso y Modificación:");
        Console.WriteLine("\n╔════════════════════════════════╦════════════════════════════════╗");
        Console.WriteLine("║             C#                 ║            Java                ║");
        Console.WriteLine("╠════════════════════════════════╬════════════════════════════════╣");
        Console.WriteLine("║ int first = numbers[0];        ║ int first = numbers[0];        ║");
        Console.WriteLine("║ numbers[0] = 100;              ║ numbers[0] = 100;              ║");
        Console.WriteLine("║                                ║                                ║");
        Console.WriteLine("║ // Longitud                    ║ // Longitud                    ║");
        Console.WriteLine("║ int length = numbers.Length;   ║ int length = numbers.length;   ║");
        Console.WriteLine("║                                ║                                ║");
        Console.WriteLine("║ // Iterar                      ║ // Iterar                      ║");
        Console.WriteLine("║ foreach(int n in numbers)      ║ for(int n : numbers)           ║");
        Console.WriteLine("║     Console.WriteLine(n);      ║     System.out.println(n);     ║");
        Console.WriteLine("╚════════════════════════════════╩════════════════════════════════╝");
        
        Console.WriteLine($"\n   Primer elemento: {numbers[0]}");
        Console.WriteLine($"   Longitud del array: {numbers.Length}");
        
        Console.Write("   Iterando: ");
        foreach (int num in numbers)
        {
            Console.Write($"{num} ");
        }
        Console.WriteLine();
        
        Console.WriteLine("\n4️⃣ Métodos Útiles:");
        Console.WriteLine("\n╔══════════════════════════════════╦══════════════════════════════════╗");
        Console.WriteLine("║              C#                  ║             Java                 ║");
        Console.WriteLine("╠══════════════════════════════════╬══════════════════════════════════╣");
        Console.WriteLine("║ Array.Sort(numbers);             ║ Arrays.sort(numbers);            ║");
        Console.WriteLine("║ Array.Reverse(numbers);          ║ // Manual o Collections.reverse  ║");
        Console.WriteLine("║ Array.IndexOf(numbers, 30);      ║ Arrays.binarySearch(nums, 30);   ║");
        Console.WriteLine("║ Array.Copy(src, dest, length);   ║ System.arraycopy(src,0,dest,..); ║");
        Console.WriteLine("╚══════════════════════════════════╩══════════════════════════════════╝");
        
        int[] sortDemo = { 5, 2, 8, 1, 9 };
        Console.WriteLine($"\n   Array antes de ordenar: [{string.Join(", ", sortDemo)}]");
        Array.Sort(sortDemo);
        Console.WriteLine($"   Array después de ordenar: [{string.Join(", ", sortDemo)}]");
        
        Console.WriteLine("\n💡 Diferencias clave:");
        Console.WriteLine("   ✅ Sintaxis muy similar entre C# y Java");
        Console.WriteLine("   ✅ C#: Length (propiedad) vs Java: length (campo)");
        Console.WriteLine("   ✅ C# tiene métodos estáticos en clase Array");
        Console.WriteLine("   ✅ Java tiene métodos en clase Arrays (nota la 's')");
    }
    
    private void DemonstrateLists()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("\n📝 ESTRUCTURA 2: Lists (Listas Dinámicas)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        Console.WriteLine("\n1️⃣ Declaración e Inicialización:");
        Console.WriteLine("\n╔════════════════════════════════════════╦════════════════════════════════════════╗");
        Console.WriteLine("║                 C#                     ║                Java                    ║");
        Console.WriteLine("╠════════════════════════════════════════╬════════════════════════════════════════╣");
        Console.WriteLine("║ List<int> numbers;                     ║ ArrayList<Integer> numbers;            ║");
        Console.WriteLine("║ numbers = new List<int>();             ║ numbers = new ArrayList<Integer>();    ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Inicialización con valores          ║ // Inicialización con valores          ║");
        Console.WriteLine("║ List<int> nums = new() {1,2,3};        ║ List<Integer> nums =                   ║");
        Console.WriteLine("║                                        ║     Arrays.asList(1,2,3);              ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Strings                             ║ // Strings                             ║");
        Console.WriteLine("║ List<string> names = new();            ║ ArrayList<String> names =              ║");
        Console.WriteLine("║                                        ║     new ArrayList<String>();           ║");
        Console.WriteLine("╚════════════════════════════════════════╩════════════════════════════════════════╝");
        
        // Actual C# demonstration
        Console.WriteLine("\n2️⃣ Ejemplo en C# (ejecución real):");
        
        List<int> numbers = new List<int> { 10, 20, 30 };
        List<string> names = new List<string> { "Alice", "Bob", "Charlie" };
        
        Console.WriteLine($"   Lista de números: [{string.Join(", ", numbers)}]");
        Console.WriteLine($"   Lista de nombres: [{string.Join(", ", names)}]");
        
        Console.WriteLine("\n3️⃣ Operaciones Comunes:");
        Console.WriteLine("\n╔════════════════════════════════════════╦════════════════════════════════════════╗");
        Console.WriteLine("║                 C#                     ║                Java                    ║");
        Console.WriteLine("╠════════════════════════════════════════╬════════════════════════════════════════╣");
        Console.WriteLine("║ // Agregar elementos                   ║ // Agregar elementos                   ║");
        Console.WriteLine("║ numbers.Add(40);                       ║ numbers.add(40);                       ║");
        Console.WriteLine("║ numbers.Insert(0, 5);                  ║ numbers.add(0, 5);                     ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Remover elementos                   ║ // Remover elementos                   ║");
        Console.WriteLine("║ numbers.Remove(20);                    ║ numbers.remove(Integer.valueOf(20));   ║");
        Console.WriteLine("║ numbers.RemoveAt(0);                   ║ numbers.remove(0);                     ║");
        Console.WriteLine("║ numbers.Clear();                       ║ numbers.clear();                       ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Buscar                              ║ // Buscar                              ║");
        Console.WriteLine("║ bool exists = numbers.Contains(30);    ║ boolean exists = numbers.contains(30); ║");
        Console.WriteLine("║ int index = numbers.IndexOf(30);       ║ int index = numbers.indexOf(30);       ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Tamaño                              ║ // Tamaño                              ║");
        Console.WriteLine("║ int count = numbers.Count;             ║ int size = numbers.size();             ║");
        Console.WriteLine("╚════════════════════════════════════════╩════════════════════════════════════════╝");
        
        Console.WriteLine("\n   Demostrando operaciones:");
        Console.WriteLine($"   Lista inicial: [{string.Join(", ", numbers)}]");
        
        numbers.Add(40);
        Console.WriteLine($"   Después de Add(40): [{string.Join(", ", numbers)}]");
        
        numbers.Insert(0, 5);
        Console.WriteLine($"   Después de Insert(0, 5): [{string.Join(", ", numbers)}]");
        
        numbers.Remove(20);
        Console.WriteLine($"   Después de Remove(20): [{string.Join(", ", numbers)}]");
        
        bool contains = numbers.Contains(30);
        Console.WriteLine($"   ¿Contiene 30?: {contains}");
        
        Console.WriteLine($"   Tamaño de la lista: {numbers.Count}");
        
        Console.WriteLine("\n4️⃣ Métodos LINQ (C#) vs Stream API (Java):");
        Console.WriteLine("\n╔════════════════════════════════════════╦════════════════════════════════════════╗");
        Console.WriteLine("║                 C#                     ║                Java                    ║");
        Console.WriteLine("╠════════════════════════════════════════╬════════════════════════════════════════╣");
        Console.WriteLine("║ // Filtrar                             ║ // Filtrar                             ║");
        Console.WriteLine("║ var filtered = numbers                 ║ List<Integer> filtered = numbers       ║");
        Console.WriteLine("║     .Where(n => n > 20)                ║     .stream()                          ║");
        Console.WriteLine("║     .ToList();                         ║     .filter(n -> n > 20)               ║");
        Console.WriteLine("║                                        ║     .collect(Collectors.toList());     ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Mapear                              ║ // Mapear                              ║");
        Console.WriteLine("║ var doubled = numbers                  ║ List<Integer> doubled = numbers        ║");
        Console.WriteLine("║     .Select(n => n * 2)                ║     .stream()                          ║");
        Console.WriteLine("║     .ToList();                         ║     .map(n -> n * 2)                   ║");
        Console.WriteLine("║                                        ║     .collect(Collectors.toList());     ║");
        Console.WriteLine("╚════════════════════════════════════════╩════════════════════════════════════════╝");
        
        var filtered = numbers.Where(n => n > 20).ToList();
        Console.WriteLine($"\n   Números > 20: [{string.Join(", ", filtered)}]");
        
        var doubled = numbers.Select(n => n * 2).ToList();
        Console.WriteLine($"   Números duplicados: [{string.Join(", ", doubled)}]");
        
        Console.WriteLine("\n💡 Diferencias clave:");
        Console.WriteLine("   ✅ C#: List<T> vs Java: ArrayList<T> o List<T> (interface)");
        Console.WriteLine("   ✅ C#: Count (propiedad) vs Java: size() (método)");
        Console.WriteLine("   ✅ C# tiene LINQ integrado vs Java tiene Stream API");
        Console.WriteLine("   ✅ C# permite sintaxis más concisa en inicialización");
    }
    
    private void DemonstrateDictionaries()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("\n🗂️  ESTRUCTURA 3: Dictionaries/Maps (Diccionarios)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        Console.WriteLine("\n1️⃣ Declaración e Inicialización:");
        Console.WriteLine("\n╔═══════════════════════════════════════════╦═══════════════════════════════════════════╗");
        Console.WriteLine("║                  C#                       ║                 Java                      ║");
        Console.WriteLine("╠═══════════════════════════════════════════╬═══════════════════════════════════════════╣");
        Console.WriteLine("║ Dictionary<string, int> ages;             ║ HashMap<String, Integer> ages;            ║");
        Console.WriteLine("║ ages = new Dictionary<string, int>();     ║ ages = new HashMap<String, Integer>();    ║");
        Console.WriteLine("║                                           ║                                           ║");
        Console.WriteLine("║ // Inicialización con valores             ║ // Inicialización con valores             ║");
        Console.WriteLine("║ Dictionary<string, int> dict = new()      ║ Map<String, Integer> map =                ║");
        Console.WriteLine("║ {                                         ║     Map.of(                               ║");
        Console.WriteLine("║     {\"Alice\", 25},                        ║         \"Alice\", 25,                      ║");
        Console.WriteLine("║     {\"Bob\", 30}                           ║         \"Bob\", 30                         ║");
        Console.WriteLine("║ };                                        ║     );                                    ║");
        Console.WriteLine("╚═══════════════════════════════════════════╩═══════════════════════════════════════════╝");
        
        // Actual C# demonstration
        Console.WriteLine("\n2️⃣ Ejemplo en C# (ejecución real):");
        
        Dictionary<string, int> ages = new Dictionary<string, int>
        {
            { "Alice", 25 },
            { "Bob", 30 },
            { "Charlie", 35 }
        };
        
        Dictionary<string, string> countries = new Dictionary<string, string>
        {
            { "CO", "Colombia" },
            { "US", "United States" },
            { "JP", "Japan" }
        };
        
        Console.WriteLine("   Diccionario de edades:");
        foreach (var pair in ages)
        {
            Console.WriteLine($"      {pair.Key}: {pair.Value} años");
        }
        
        Console.WriteLine("\n3️⃣ Operaciones Comunes:");
        Console.WriteLine("\n╔════════════════════════════════════════╦════════════════════════════════════════╗");
        Console.WriteLine("║                 C#                     ║                Java                    ║");
        Console.WriteLine("╠════════════════════════════════════════╬════════════════════════════════════════╣");
        Console.WriteLine("║ // Agregar                             ║ // Agregar                             ║");
        Console.WriteLine("║ ages[\"David\"] = 40;                    ║ ages.put(\"David\", 40);                 ║");
        Console.WriteLine("║ ages.Add(\"Eve\", 28);                   ║ ages.put(\"Eve\", 28);                   ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Obtener                             ║ // Obtener                             ║");
        Console.WriteLine("║ int age = ages[\"Alice\"];               ║ int age = ages.get(\"Alice\");           ║");
        Console.WriteLine("║ ages.TryGetValue(\"X\", out int val);    ║ ages.getOrDefault(\"X\", 0);            ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Verificar existencia                ║ // Verificar existencia                ║");
        Console.WriteLine("║ bool exists = ages.ContainsKey(\"Bob\"); ║ boolean exists = ages.containsKey(..); ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Remover                             ║ // Remover                             ║");
        Console.WriteLine("║ ages.Remove(\"Charlie\");                ║ ages.remove(\"Charlie\");                ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Tamaño                              ║ // Tamaño                              ║");
        Console.WriteLine("║ int count = ages.Count;                ║ int size = ages.size();                ║");
        Console.WriteLine("╚════════════════════════════════════════╩════════════════════════════════════════╝");
        
        Console.WriteLine("\n   Demostrando operaciones:");
        
        ages["David"] = 40;
        Console.WriteLine($"   Agregado David: 40");
        
        int aliceAge = ages["Alice"];
        Console.WriteLine($"   Edad de Alice: {aliceAge}");
        
        bool hasBob = ages.ContainsKey("Bob");
        Console.WriteLine($"   ¿Existe Bob?: {hasBob}");
        
        if (ages.TryGetValue("Unknown", out int unknownAge))
        {
            Console.WriteLine($"   Edad de Unknown: {unknownAge}");
        }
        else
        {
            Console.WriteLine($"   Unknown no existe en el diccionario");
        }
        
        Console.WriteLine($"   Tamaño del diccionario: {ages.Count}");
        
        Console.WriteLine("\n4️⃣ Iterar sobre Diccionarios:");
        Console.WriteLine("\n╔════════════════════════════════════════╦════════════════════════════════════════╗");
        Console.WriteLine("║                 C#                     ║                Java                    ║");
        Console.WriteLine("╠════════════════════════════════════════╬════════════════════════════════════════╣");
        Console.WriteLine("║ // Iterar sobre pares key-value        ║ // Iterar sobre pares key-value        ║");
        Console.WriteLine("║ foreach(var pair in ages)              ║ for(Map.Entry<String,Integer> entry    ║");
        Console.WriteLine("║ {                                      ║      : ages.entrySet())                ║");
        Console.WriteLine("║     Console.WriteLine(                 ║ {                                      ║");
        Console.WriteLine("║         $\"{pair.Key}: {pair.Value}\");   ║     System.out.println(                ║");
        Console.WriteLine("║ }                                      ║         entry.getKey() + \": \" +        ║");
        Console.WriteLine("║                                        ║         entry.getValue());             ║");
        Console.WriteLine("║                                        ║ }                                      ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Solo keys                           ║ // Solo keys                           ║");
        Console.WriteLine("║ foreach(string key in ages.Keys)       ║ for(String key : ages.keySet())        ║");
        Console.WriteLine("║                                        ║                                        ║");
        Console.WriteLine("║ // Solo values                         ║ // Solo values                         ║");
        Console.WriteLine("║ foreach(int value in ages.Values)      ║ for(Integer val : ages.values())       ║");
        Console.WriteLine("╚════════════════════════════════════════╩════════════════════════════════════════╝");
        
        Console.WriteLine("\n   Iterando sobre todas las edades:");
        foreach (var pair in ages)
        {
            Console.WriteLine($"      {pair.Key}: {pair.Value} años");
        }
        
        Console.WriteLine("\n   Solo las keys:");
        Console.Write("      ");
        foreach (string key in ages.Keys)
        {
            Console.Write($"{key}, ");
        }
        Console.WriteLine("\n");
        
        Console.WriteLine("💡 Diferencias clave:");
        Console.WriteLine("   ✅ C#: Dictionary<K,V> vs Java: HashMap<K,V>");
        Console.WriteLine("   ✅ C#: indexador dict[key] vs Java: map.get(key)");
        Console.WriteLine("   ✅ C#: TryGetValue() más seguro vs Java: getOrDefault()");
        Console.WriteLine("   ✅ C#: Count (propiedad) vs Java: size() (método)");
    }
}