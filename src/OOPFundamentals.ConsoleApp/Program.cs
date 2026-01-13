using OOPFundamentals.ConsoleApp.Demos;

namespace OOPFundamentals.ConsoleApp;

/// <summary>
/// Main entry point for the OOP Fundamentals demonstration application.
/// This console application demonstrates all four pillars of Object-Oriented Programming:
/// Encapsulation, Abstraction, Inheritance, and Polymorphism.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        DisplayWelcomeBanner();
        
        bool exit = false;
        
        while (!exit)
        {
            DisplayMainMenu();
            
            Console.Write("\n👉 Selecciona una opción: ");
            string? choice = Console.ReadLine();
            
            Console.Clear();
            
            switch (choice)
            {
                case "1":
                    RunEncapsulationDemo();
                    break;
                    
                case "2":
                    RunAbstractionDemo();
                    break;
                    
                case "3":
                    RunInheritanceDemo();
                    break;
                    
                case "4":
                    RunPolymorphismDemo();
                    break;
                    
                case "5":
                    RunDataStructuresDemo();
                    break;
                    
                case "6":
                    RunAllDemos();
                    break;
                    
                case "7":
                    DisplayAboutInfo();
                    break;
                    
                case "0":
                    exit = true;
                    DisplayGoodbye();
                    break;
                    
                default:
                    Console.WriteLine("❌ Opción inválida. Por favor, intenta de nuevo.\n");
                    break;
            }
            
            if (!exit && choice != "6")
            {
                Console.WriteLine("\n" + new string('═', 80));
                Console.Write("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
    
    /// <summary>
    /// Displays the welcome banner with ASCII art.
    /// </summary>
    static void DisplayWelcomeBanner()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════════════╗
║                                                                               ║
║   ███████╗██╗   ██╗███╗   ██╗██████╗  █████╗ ███╗   ███╗███████╗███╗   ██╗ ║
║   ██╔════╝██║   ██║████╗  ██║██╔══██╗██╔══██╗████╗ ████║██╔════╝████╗  ██║ ║
║   █████╗  ██║   ██║██╔██╗ ██║██║  ██║███████║██╔████╔██║█████╗  ██╔██╗ ██║ ║
║   ██╔══╝  ██║   ██║██║╚██╗██║██║  ██║██╔══██║██║╚██╔╝██║██╔══╝  ██║╚██╗██║ ║
║   ██║     ╚██████╔╝██║ ╚████║██████╔╝██║  ██║██║ ╚═╝ ██║███████╗██║ ╚████║ ║
║   ╚═╝      ╚═════╝ ╚═╝  ╚═══╝╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝ ║
║                                                                               ║
║                    LOS 4 PILARES DE LA PROGRAMACIÓN                          ║
║                        ORIENTADA A OBJETOS EN C#                              ║
║                                                                               ║
╚═══════════════════════════════════════════════════════════════════════════════╝
");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("                      🎓 Proyecto Académico - Programming 3");
        Console.WriteLine("                      👨‍💻 Por: CodeWithBotinaOficial");
        Console.WriteLine("                      📅 " + DateTime.Now.ToString("yyyy"));
        Console.ResetColor();
        Console.WriteLine("\n" + new string('═', 80) + "\n");
        
        Console.Write("Presiona cualquier tecla para comenzar...");
        Console.ReadKey();
        Console.Clear();
    }
    
    /// <summary>
    /// Displays the main menu with all available options.
    /// </summary>
    static void DisplayMainMenu()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                              MENÚ PRINCIPAL                                   ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        
        Console.WriteLine("\n📚 DEMOSTRACIONES DE LOS 4 PILARES DE OOP:\n");
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("   1. 🔒 Encapsulamiento");
        Console.ResetColor();
        Console.WriteLine("      └─ Demostración de campos privados, propiedades y validación");
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n   2. 🎭 Abstracción");
        Console.ResetColor();
        Console.WriteLine("      └─ Demostración de clases abstractas, interfaces y ocultamiento");
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n   3. 🧬 Herencia");
        Console.ResetColor();
        Console.WriteLine("      └─ Demostración de jerarquías de clases y reutilización de código");
        
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n   4. 🔄 Polimorfismo");
        Console.ResetColor();
        Console.WriteLine("      └─ Demostración de múltiples formas y comportamientos dinámicos");
        
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("\n   5. 📊 Estructuras de Datos (C# vs Java)");
        Console.ResetColor();
        Console.WriteLine("      └─ Comparación de Arrays, Lists y Dictionaries");
        
        Console.WriteLine("\n" + new string('─', 80));
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n   6. 🚀 Ejecutar TODAS las demostraciones");
        Console.WriteLine("   7. ℹ️  Acerca de este proyecto");
        Console.WriteLine("   0. 🚪 Salir");
        Console.ResetColor();
        
        Console.WriteLine("\n" + new string('═', 80));
    }
    
    /// <summary>
    /// Runs the Encapsulation demonstration.
    /// </summary>
    static void RunEncapsulationDemo()
    {
        PrintDemoHeader("ENCAPSULAMIENTO", "🔒", ConsoleColor.Cyan);
        
        var demo = new EncapsulationDemo();
        demo.Run();
    }
    
    /// <summary>
    /// Runs the Abstraction demonstration.
    /// </summary>
    static void RunAbstractionDemo()
    {
        PrintDemoHeader("ABSTRACCIÓN", "🎭", ConsoleColor.Magenta);
        
        var demo = new AbstractionDemo();
        demo.Run();
    }
    
    /// <summary>
    /// Runs the Inheritance demonstration.
    /// </summary>
    static void RunInheritanceDemo()
    {
        PrintDemoHeader("HERENCIA", "🧬", ConsoleColor.Yellow);
        
        var demo = new InheritanceDemo();
        demo.Run();
    }
    
    /// <summary>
    /// Runs the Polymorphism demonstration.
    /// </summary>
    static void RunPolymorphismDemo()
    {
        PrintDemoHeader("POLIMORFISMO", "🔄", ConsoleColor.Blue);
        
        var demo = new PolymorphismDemo();
        demo.Run();
    }
    
    /// <summary>
    /// Runs the Data Structures comparison demonstration.
    /// </summary>
    static void RunDataStructuresDemo()
    {
        PrintDemoHeader("ESTRUCTURAS DE DATOS (C# vs Java)", "📊", ConsoleColor.DarkCyan);
        
        var demo = new DataStructuresDemo();
        demo.Run();
    }
    
    /// <summary>
    /// Runs all demonstrations sequentially.
    /// </summary>
    static void RunAllDemos()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n🚀 Ejecutando TODAS las demostraciones...\n");
        Console.ResetColor();
        
        RunEncapsulationDemo();
        Console.WriteLine("\n" + new string('═', 80));
        Console.Write("Presiona cualquier tecla para continuar con la siguiente demo...");
        Console.ReadKey();
        Console.Clear();
        
        RunAbstractionDemo();
        Console.WriteLine("\n" + new string('═', 80));
        Console.Write("Presiona cualquier tecla para continuar con la siguiente demo...");
        Console.ReadKey();
        Console.Clear();
        
        RunInheritanceDemo();
        Console.WriteLine("\n" + new string('═', 80));
        Console.Write("Presiona cualquier tecla para continuar con la siguiente demo...");
        Console.ReadKey();
        Console.Clear();
        
        RunPolymorphismDemo();
        Console.WriteLine("\n" + new string('═', 80));
        Console.Write("Presiona cualquier tecla para continuar con la siguiente demo...");
        Console.ReadKey();
        Console.Clear();
        
        RunDataStructuresDemo();
        
        Console.WriteLine("\n\n" + new string('═', 80));
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n✅ ¡Todas las demostraciones completadas exitosamente!\n");
        Console.ResetColor();
    }
    
    /// <summary>
    /// Displays information about the project.
    /// </summary>
    static void DisplayAboutInfo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                          ACERCA DE ESTE PROYECTO                              ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        
        Console.WriteLine("\n📚 Proyecto: OOP Fundamentals in C#");
        Console.WriteLine("🎓 Curso: Programming 3");
        Console.WriteLine("🏫 Institución: Universidad");
        Console.WriteLine("👨‍💻 Autor: CodeWithBotinaOficial");
        Console.WriteLine("📅 Año: 2026");
        Console.WriteLine("⚖️  Licencia: MIT License");
        
        Console.WriteLine("\n📖 Descripción:");
        Console.WriteLine("   Proyecto educativo que demuestra los cuatro pilares fundamentales");
        Console.WriteLine("   de la Programación Orientada a Objetos (OOP) en C#.");
        
        Console.WriteLine("\n🎯 Objetivos:");
        Console.WriteLine("   ✓ Comprender los principios de Encapsulamiento, Abstracción,");
        Console.WriteLine("     Herencia y Polimorfismo");
        Console.WriteLine("   ✓ Comparar estructuras de datos entre C# y Java");
        Console.WriteLine("   ✓ Aplicar conceptos de OOP en ejemplos prácticos");
        Console.WriteLine("   ✓ Desarrollar código limpio y profesional");
        
        Console.WriteLine("\n📊 Estadísticas del Proyecto:");
        Console.WriteLine("   • 5,690+ líneas de código C#");
        Console.WriteLine("   • 21 clases profesionales");
        Console.WriteLine("   • 3 interfaces bien diseñadas");
        Console.WriteLine("   • 8 enums para tipos");
        Console.WriteLine("   • 40+ archivos totales");
        Console.WriteLine("   • 100% documentado con XML comments");
        
        Console.WriteLine("\n🔗 GitHub:");
        Console.WriteLine("   https://github.com/JalaU-Labs/OOP-Fundamentals-CSharp");
        
        Console.WriteLine("\n💡 Tecnologías:");
        Console.WriteLine("   • .NET 9.0");
        Console.WriteLine("   • C# 12.0");
        Console.WriteLine("   • JetBrains Rider IDE");
        
        Console.WriteLine("\n" + new string('═', 80));
    }
    
    /// <summary>
    /// Displays goodbye message.
    /// </summary>
    static void DisplayGoodbye()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════════════╗
║                                                                               ║
║                          ¡GRACIAS POR USAR ESTA APP!                          ║
║                                                                               ║
║              🎓 Espero que hayas aprendido sobre OOP en C#                    ║
║                                                                               ║
║                    👨‍💻 CodeWithBotinaOficial - 2026                            ║
║                                                                               ║
╚═══════════════════════════════════════════════════════════════════════════════╝
");
        Console.ResetColor();
        Console.WriteLine("\n¡Hasta luego! 👋\n");
    }
    
    /// <summary>
    /// Prints a formatted header for each demo.
    /// </summary>
    /// <param name="title">Demo title</param>
    /// <param name="icon">Icon emoji</param>
    /// <param name="color">Header color</param>
    static void PrintDemoHeader(string title, string icon, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  {icon} {title.PadRight(74)} ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }
}