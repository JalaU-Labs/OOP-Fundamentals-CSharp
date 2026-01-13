using OOPFundamentals.Core.Encapsulation;

namespace OOPFundamentals.ConsoleApp.Demos;

/// <summary>
/// Demonstration of Encapsulation concepts.
/// Shows how to protect data through private fields and expose it through controlled methods.
/// </summary>
public class EncapsulationDemo
{
    public void Run()
    {
        Console.WriteLine("📝 Encapsulamiento es el primer pilar de OOP.");
        Console.WriteLine("   Consiste en ocultar los detalles internos de implementación");
        Console.WriteLine("   y exponer solo lo necesario a través de una interfaz pública.\n");
        
        DemonstrateBankAccount();
        Console.WriteLine("\n" + new string('-', 80) + "\n");
        DemonstratePerson();
        
        Console.WriteLine("\n✅ Concepto clave:");
        Console.WriteLine("   El encapsulamiento protege los datos y mantiene la integridad del objeto,");
        Console.WriteLine("   permitiendo cambios internos sin afectar el código que usa la clase.");
    }
    
    private void DemonstrateBankAccount()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("🏦 EJEMPLO 1: Cuenta Bancaria (BankAccount)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        try
        {
            // Creating a bank account
            Console.WriteLine("\n1️⃣ Creando una cuenta bancaria con depósito inicial de $1,000...");
            var account = new BankAccount("ACC-2026-001", "Juan Pérez", 1000m);
            
            // Depositing money
            Console.WriteLine("\n2️⃣ Depositando $500...");
            account.Deposit(500m);
            
            // Withdrawing money
            Console.WriteLine("\n3️⃣ Retirando $300...");
            account.Withdraw(300m);
            
            // Trying to withdraw more than balance
            Console.WriteLine("\n4️⃣ Intentando retirar $2,000 (más del balance disponible)...");
            account.Withdraw(2000m);
            
            // Checking balance (encapsulated - read-only access)
            Console.WriteLine($"\n5️⃣ Balance actual: ${account.GetBalance():N2}");
            
            // Displaying account summary
            Console.WriteLine("\n6️⃣ Resumen de la cuenta:");
            Console.WriteLine(account.GetAccountSummary());
            
            // Transfer to another account
            Console.WriteLine("\n7️⃣ Creando segunda cuenta para demostrar transferencia...");
            var destinationAccount = new BankAccount("ACC-2026-002", "María García", 500m);
            
            Console.WriteLine("\n8️⃣ Transfiriendo $200 entre cuentas...");
            account.Transfer(destinationAccount, 200m);
            
            Console.WriteLine($"\n   Balance cuenta origen: ${account.GetBalance():N2}");
            Console.WriteLine($"   Balance cuenta destino: ${destinationAccount.GetBalance():N2}");
            
            // Demonstrating encapsulation - cannot access private fields directly
            Console.WriteLine("\n💡 Nota sobre Encapsulamiento:");
            Console.WriteLine("   ❌ No podemos acceder directamente a _balance (campo privado)");
            Console.WriteLine("   ✅ Solo podemos acceder a través de métodos públicos como GetBalance()");
            Console.WriteLine("   ✅ Esto protege la integridad de los datos");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }
    }
    
    private void DemonstratePerson()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n👤 EJEMPLO 2: Persona (Person)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        try
        {
            // Creating a person with validation
            Console.WriteLine("\n1️⃣ Creando una persona con validación...");
            var person = new Person("Carlos", "Rodríguez", 25, "carlos@example.com")
            {
                Id = "P-001"
            };
            
            Console.WriteLine($"   ✅ Persona creada: {person.FullName}");
            
            // Using computed properties
            Console.WriteLine("\n2️⃣ Accediendo a propiedades calculadas:");
            Console.WriteLine($"   Nombre completo: {person.FullName}");
            Console.WriteLine($"   Edad: {person.Age} años");
            Console.WriteLine($"   Año de nacimiento (calculado): {person.BirthYear}");
            Console.WriteLine($"   ¿Es adulto?: {(person.IsAdult ? "Sí" : "No")}");
            
            // Celebrating birthday
            Console.WriteLine("\n3️⃣ Celebrando cumpleaños...");
            person.CelebrateBirthday();
            Console.WriteLine($"   Nueva edad: {person.Age}");
            
            // Updating name
            Console.WriteLine("\n4️⃣ Actualizando nombre...");
            person.UpdateName("Carlos Alberto", "Rodríguez López");
            
            // Getting introduction
            Console.WriteLine("\n5️⃣ Presentación:");
            Console.WriteLine($"   {person.Introduce()}");
            
            // Displaying full details
            Console.WriteLine("\n6️⃣ Detalles completos:");
            Console.WriteLine(person.GetDetails());
            
            // Demonstrating validation
            Console.WriteLine("\n7️⃣ Demostrando validación en propiedades:");
            try
            {
                Console.WriteLine("   Intentando establecer edad negativa...");
                person.Age = -5;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"   ⚠️ Validación funcionó: {ex.Message}");
                Console.ResetColor();
            }
            
            try
            {
                Console.WriteLine("\n   Intentando establecer email inválido...");
                person.Email = "invalid-email";
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"   ⚠️ Validación funcionó: {ex.Message}");
                Console.ResetColor();
            }
            
            Console.WriteLine("\n💡 Beneficios del Encapsulamiento:");
            Console.WriteLine("   ✅ Validación automática de datos");
            Console.WriteLine("   ✅ Propiedades calculadas (FullName, BirthYear, IsAdult)");
            Console.WriteLine("   ✅ Control sobre cómo se modifican los datos");
            Console.WriteLine("   ✅ Imposible establecer datos inválidos");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }
    }
}