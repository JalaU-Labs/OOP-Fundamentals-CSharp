using OOPFundamentals.Core.Polymorphism;

namespace OOPFundamentals.ConsoleApp.Demos;

/// <summary>
/// Demonstration of Polymorphism concepts.
/// Shows how objects of different types can be treated through a common interface.
/// </summary>
public class PolymorphismDemo
{
    public void Run()
    {
        Console.WriteLine("📝 Polimorfismo es el cuarto pilar de OOP.");
        Console.WriteLine("   Significa 'muchas formas' y permite que objetos de diferentes tipos");
        Console.WriteLine("   sean tratados de manera uniforme a través de una interfaz común.\n");
        
        DemonstrateRuntimePolymorphism();
        Console.WriteLine("\n" + new string('-', 80) + "\n");
        DemonstrateOperatorOverloading();
        
        Console.WriteLine("\n✅ Concepto clave:");
        Console.WriteLine("   El polimorfismo permite escribir código genérico que funciona con");
        Console.WriteLine("   múltiples tipos, facilitando la extensibilidad y mantenibilidad.");
    }
    
    private void DemonstrateRuntimePolymorphism()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("💳 EJEMPLO 1: Sistema de Pagos (Runtime Polymorphism)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        Console.WriteLine("\n1️⃣ Creando diferentes métodos de pago:");
        
        // Create different payment types
        List<Payment> payments = new List<Payment>
        {
            new CreditCardPayment(100m, "4111111111111111", "Juan Pérez", 
                                 DateTime.Now.AddYears(2), "123", CardType.Visa),
            new PayPalPayment(75m, "usuario@example.com", PayPalFundingSource.Balance),
            new CashPayment(50m, 50m, "USD"),
            new BitcoinPayment(200m, "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa", 45000m)
        };
        
        Console.WriteLine($"   ✅ {payments.Count} pagos creados:");
        foreach (var payment in payments)
        {
            Console.WriteLine($"      • {payment.PaymentMethod}: ${payment.Amount:N2}");
        }
        
        // Process all payments polymorphically
        Console.WriteLine("\n2️⃣ Procesando TODOS los pagos (mismo código, diferente comportamiento):");
        Console.WriteLine(new string('-', 80));
        
        int successCount = 0;
        decimal totalProcessed = 0;
        
        foreach (var payment in payments)
        {
            Console.WriteLine($"\n💰 Procesando {payment.PaymentMethod} de ${payment.Amount:N2}...");
            
            // The SAME method call, but DIFFERENT behavior for each type!
            // This is runtime polymorphism in action!
            bool success = payment.ProcessPayment();
            
            if (success)
            {
                successCount++;
                totalProcessed += payment.Amount;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ Pago completado exitosamente");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Pago falló");
                Console.ResetColor();
            }
        }
        
        // Summary
        Console.WriteLine("\n\n3️⃣ Resumen de procesamiento:");
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"Total de pagos: {payments.Count}");
        Console.WriteLine($"Exitosos: {successCount}");
        Console.WriteLine($"Monto total procesado: ${totalProcessed:N2}");
        
        // Display payment details polymorphically
        Console.WriteLine("\n4️⃣ Detalles de cada pago (usando métodos polimórficos):");
        Console.WriteLine(new string('-', 80));
        
        foreach (var payment in payments)
        {
            Console.WriteLine($"\n{payment.GetPaymentSummary()}");
        }
        
        // Demonstrate different fee structures (polymorphic properties)
        Console.WriteLine("\n5️⃣ Comparación de comisiones (propiedades polimórficas):");
        Console.WriteLine(new string('-', 80));
        
        foreach (var payment in payments)
        {
            Console.WriteLine($"{payment.PaymentMethod,-20} Fee: {payment.TransactionFeePercentage}% = ${payment.TransactionFee:N2}");
        }
        
        Console.WriteLine("\n💡 Polimorfismo en Tiempo de Ejecución:");
        Console.WriteLine("   ✅ MISMO código: payment.ProcessPayment()");
        Console.WriteLine("   ✅ DIFERENTES comportamientos:");
        Console.WriteLine("      • CreditCard: Autorización con gateway");
        Console.WriteLine("      • PayPal: Flujo OAuth");
        Console.WriteLine("      • Cash: Manejo físico de efectivo");
        Console.WriteLine("      • Bitcoin: Confirmaciones blockchain");
        Console.WriteLine("   ✅ El tipo correcto se determina en TIEMPO DE EJECUCIÓN");
    }
    
    private void DemonstrateOperatorOverloading()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n🔢 EJEMPLO 2: Sobrecarga de Operadores (Compile-time Polymorphism)");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));
        
        Console.WriteLine("\n1️⃣ Creando pagos para demostrar operadores:");
        
        var payment1 = new CashPayment(100m, 100m, "USD");
        var payment2 = new CreditCardPayment(50m, "4111111111111111", "María García",
                                             DateTime.Now.AddYears(2), "456", CardType.Mastercard);
        var payment3 = new PayPalPayment(75m, "test@example.com");
        
        Console.WriteLine($"   Pago 1 (Efectivo): ${payment1.Amount:N2}");
        Console.WriteLine($"   Pago 2 (Tarjeta):  ${payment2.Amount:N2}");
        Console.WriteLine($"   Pago 3 (PayPal):   ${payment3.Amount:N2}");
        
        // Operator + (addition)
        Console.WriteLine("\n2️⃣ Usando operador + para combinar montos:");
        decimal total = payment1 + payment2;
        Console.WriteLine($"   Pago1 + Pago2 = ${total:N2}");
        
        total = payment1 + payment2 + payment3;
        Console.WriteLine($"   Pago1 + Pago2 + Pago3 = ${total:N2}");
        
        // Operator > (comparison)
        Console.WriteLine("\n3️⃣ Usando operador > para comparar:");
        if (payment1 > payment2)
        {
            Console.WriteLine($"   Pago1 (${payment1.Amount:N2}) > Pago2 (${payment2.Amount:N2}) ✓");
        }
        
        if (payment3 > payment2)
        {
            Console.WriteLine($"   Pago3 (${payment3.Amount:N2}) > Pago2 (${payment2.Amount:N2}) ✓");
        }
        
        // Operator < (comparison)
        Console.WriteLine("\n4️⃣ Usando operador < para comparar:");
        if (payment2 < payment1)
        {
            Console.WriteLine($"   Pago2 (${payment2.Amount:N2}) < Pago1 (${payment1.Amount:N2}) ✓");
        }
        
        // Demonstrating with different types
        Console.WriteLine("\n5️⃣ Los operadores funcionan con CUALQUIER tipo de Payment:");
        
        Payment genericPayment1 = new BitcoinPayment(150m, "1BvBMSEYstWetqTFn5Au4m4GFg7xJaNVN2", 50000m);
        Payment genericPayment2 = new CashPayment(200m, 200m, "USD");
        
        Console.WriteLine($"   Bitcoin Payment: ${genericPayment1.Amount:N2}");
        Console.WriteLine($"   Cash Payment:    ${genericPayment2.Amount:N2}");
        
        if (genericPayment2 > genericPayment1)
        {
            Console.WriteLine($"   Cash (${genericPayment2.Amount:N2}) > Bitcoin (${genericPayment1.Amount:N2}) ✓");
        }
        
        decimal combinedTotal = genericPayment1 + genericPayment2;
        Console.WriteLine($"   Bitcoin + Cash = ${combinedTotal:N2}");
        
        Console.WriteLine("\n💡 Sobrecarga de Operadores:");
        Console.WriteLine("   ✅ Operadores como +, >, < funcionan con objetos Payment");
        Console.WriteLine("   ✅ Hace el código más intuitivo y legible");
        Console.WriteLine("   ✅ Es polimorfismo en TIEMPO DE COMPILACIÓN");
        Console.WriteLine("   ✅ El compilador decide qué operador usar");
        
        // Demonstrate refunds polymorphically
        Console.WriteLine("\n\n6️⃣ Procesando reembolsos (polimorfismo de método):");
        Console.WriteLine(new string('-', 80));
        
        payment1.ProcessPayment();
        payment2.ProcessPayment();
        payment3.ProcessPayment();
        
        Console.WriteLine("\nAhora reembolsando todos...\n");
        
        payment1.Refund(50m);   // Cash refund - immediate
        payment2.Refund(25m);   // Credit card refund - 3-5 days
        payment3.Refund(30m);   // PayPal refund - instant to balance
        
        Console.WriteLine("\n💡 Mismo método (Refund), diferentes comportamientos:");
        Console.WriteLine("   • Efectivo: Reembolso inmediato en mano");
        Console.WriteLine("   • Tarjeta: Reembolso en 3-5 días hábiles");
        Console.WriteLine("   • PayPal: Reembolso instantáneo a balance");
    }
}