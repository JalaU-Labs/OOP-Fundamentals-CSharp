using OOPFundamentals.Core.Abstraction;

namespace OOPFundamentals.ConsoleApp.Demos;

/// <summary>
/// Demonstration of Abstraction concepts.
/// Shows how to hide implementation details and focus on what objects do, not how they do it.
/// </summary>
public class AbstractionDemo
{
    public void Run()
    {
        Console.WriteLine("📝 Abstracción es el segundo pilar de OOP.");
        Console.WriteLine("   Consiste en ocultar la complejidad y mostrar solo lo esencial.");
        Console.WriteLine("   Se logra mediante clases abstractas e interfaces.\n");
        
        DemonstrateShapes();
        Console.WriteLine("\n" + new string('-', 80) + "\n");
        DemonstrateInterfaces();
        
        Console.WriteLine("\n✅ Concepto clave:");
        Console.WriteLine("   La abstracción permite trabajar con conceptos de alto nivel sin");
        Console.WriteLine("   preocuparse por los detalles de implementación subyacentes.");
    }
    
    private void DemonstrateShapes()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("📐 EJEMPLO 1: Figuras Geométricas (Shape Hierarchy)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        Console.WriteLine("\n1️⃣ Creando diferentes figuras:");
        
        // Create different shapes
        Shape circle = new Circle(5, "Rojo");
        Shape rectangle = new Rectangle(4, 6, "Azul");
        Shape triangle = Triangle.CreateEquilateral(5, "Verde");
        
        Console.WriteLine($"   ✅ Círculo con radio 5");
        Console.WriteLine($"   ✅ Rectángulo de 4x6");
        Console.WriteLine($"   ✅ Triángulo equilátero de lado 5");
        
        // Demonstrate polymorphic calls to abstract methods
        Console.WriteLine("\n2️⃣ Calculando áreas (llamadas polimórficas a métodos abstractos):");
        Console.WriteLine($"   Círculo:    {circle.CalculateArea():F2} unidades cuadradas");
        Console.WriteLine($"   Rectángulo: {rectangle.CalculateArea():F2} unidades cuadradas");
        Console.WriteLine($"   Triángulo:  {triangle.CalculateArea():F2} unidades cuadradas");
        
        Console.WriteLine("\n3️⃣ Calculando perímetros:");
        Console.WriteLine($"   Círculo:    {circle.CalculatePerimeter():F2} unidades");
        Console.WriteLine($"   Rectángulo: {rectangle.CalculatePerimeter():F2} unidades");
        Console.WriteLine($"   Triángulo:  {triangle.CalculatePerimeter():F2} unidades");
        
        // Using template method (concrete method that uses abstract methods)
        Console.WriteLine("\n4️⃣ Mostrando información completa (Template Method Pattern):");
        circle.DisplayInfo();
        
        // Comparing shapes
        Console.WriteLine("\n5️⃣ Comparando áreas:");
        Console.WriteLine($"   {circle.CompareAreaWith(rectangle)}");
        Console.WriteLine($"   {rectangle.CompareAreaWith(triangle)}");
        
        // Drawing shapes (virtual method override)
        Console.WriteLine("\n6️⃣ Dibujando figuras (métodos virtuales):");
        circle.Draw();
        
        // Resizing (virtual method)
        Console.WriteLine("\n7️⃣ Redimensionando el círculo (escala 2x):");
        circle.Resize(2.0);
        Console.WriteLine($"   Nueva área: {circle.CalculateArea():F2} unidades cuadradas");
        
        Console.WriteLine("\n💡 Abstracción en acción:");
        Console.WriteLine("   ✅ No sabemos CÓMO se calcula cada área (π*r² vs w*h vs Herón)");
        Console.WriteLine("   ✅ Solo sabemos QUÉ hace cada método (calcula el área)");
        Console.WriteLine("   ✅ Cada forma implementa su propia lógica");
        Console.WriteLine("   ✅ Usamos todas las formas de manera uniforme");
    }
    
    private void DemonstrateInterfaces()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n🎨 EJEMPLO 2: Interfaces (IDrawable, IResizable)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        Console.WriteLine("\n1️⃣ Creando un DrawableCircle (implementa múltiples interfaces):");
        var drawableCircle = new DrawableCircle(7, "Morado");
        Console.WriteLine($"   ✅ DrawableCircle creado: radio = 7, color = Morado");
        
        // Using interface methods
        Console.WriteLine("\n2️⃣ Usando métodos de IDrawable:");
        drawableCircle.Draw();
        
        Console.WriteLine("\n3️⃣ Dibujando en posición específica:");
        drawableCircle.DrawAt(10, 5);
        
        // Using interface methods from IResizable
        Console.WriteLine("\n4️⃣ Usando métodos de IResizable:");
        Console.WriteLine($"   Área actual: {drawableCircle.CalculateArea():F2}");
        drawableCircle.Resize(1.5);
        Console.WriteLine($"   Nueva área: {drawableCircle.CalculateArea():F2}");
        
        Console.WriteLine("\n5️⃣ Ajustando para caber en un espacio de 20x20:");
        drawableCircle.ResizeToFit(20, 20);
        
        // Demonstrating interface polymorphism
        Console.WriteLine("\n6️⃣ Polimorfismo de interfaces:");
        Console.WriteLine("   Tratando DrawableCircle como diferentes tipos:");
        
        // As Shape
        Shape shape = drawableCircle;
        Console.WriteLine($"\n   Como Shape: {shape.CalculateArea():F2} unidades cuadradas");
        
        // As IDrawable
        IDrawable drawable = drawableCircle;
        Console.WriteLine($"   Como IDrawable: Color = {drawable.Color}");
        drawable.Draw();
        
        // As IResizable
        IResizable resizable = drawableCircle;
        Console.WriteLine($"\n   Como IResizable: redimensionando...");
        resizable.Resize(0.8);
        
        // Status
        Console.WriteLine("\n7️⃣ Estado completo del objeto:");
        Console.WriteLine(drawableCircle.GetStatus());
        
        Console.WriteLine("\n💡 Interfaces vs Clases Abstractas:");
        Console.WriteLine("   📌 Clases Abstractas:");
        Console.WriteLine("      • Definen QUÉ es algo (IS-A): Circle IS-A Shape");
        Console.WriteLine("      • Pueden tener implementación parcial");
        Console.WriteLine("      • Herencia simple (una clase base)");
        Console.WriteLine("\n   📌 Interfaces:");
        Console.WriteLine("      • Definen QUÉ puede hacer algo (CAN-DO):");
        Console.WriteLine("        Circle CAN-BE-DRAWN (IDrawable)");
        Console.WriteLine("        Circle CAN-BE-RESIZED (IResizable)");
        Console.WriteLine("      • Solo contratos, sin implementación");
        Console.WriteLine("      • Herencia múltiple (muchas interfaces)");
    }
}